using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Controller : EnemyController
{
    public CheckAhead checkAhead;
    public Frog_Animator animator;
    public float waitTime = 2;
    public float maxSpeed = 3;
    private Vector2 jumpPower = new Vector2(5,5);
    public Ground ground;
    public VisionScript vision;


    private bool canJump = true;
    private bool inAir = false;
    private bool hitPlayer = false;
    private bool onGround;
    private bool waiting = false;
    private bool aggro = false;
    private bool facingRight = true;
    private bool setStartPosition = false;
    private bool attacking;
    private bool toungeOut = false;
    private Vector2 startPosition;
    private int distance;
    private RaycastHit2D hit;
    private Rigidbody2D body;
    private Vector2 velocity;
    private GameObject target;
    protected override void Start()
    {   
        animator = GetComponent<Frog_Animator>();
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 1f;
        //vision = GetComponent<VisionScript>();
        StartCoroutine(waitIdle());
    }

    // Update is called once per frame
    protected override void Update()
    {
        //testJump();
        //stickTheLanding();
        //attackCheck();
        onGround = getOnGround();
        getState();
    }

    protected override void FixedUpdate()
    {
        
        
        if(!aggro)
        {
            //Calm state
        
            if(!waiting)
            {
                animator.changeAnimationState(Frog_Animator.fAnim.FROG_WALK);

                if(!setStartPosition)
                {
                    distance = Random.Range(1,7);
                    startPosition = new Vector2(this.transform.position.x, this.transform.position.y);
                    setStartPosition = true;
                }
                if(facingRight && this.transform.position.x < startPosition.x + distance && checkAhead.getGroundAhead())
                {
                    velocity = new Vector2(maxSpeed, 0);
                    body.velocity = velocity;
                }
                else if(!facingRight && this.transform.position.x > startPosition.x - distance &&checkAhead.getGroundAhead())
                {
                    velocity = new Vector2(-maxSpeed, 0);
                    body.velocity = velocity;
                }
                else
                {
                    body.velocity = new Vector2(0,0);
                    
                    StartCoroutine(waitIdle());
                    
                    
                }
            }

        }
        if(aggro)
        {
            target = vision.getTarget();
            if(target.transform.position.x > transform.position.x && !facingRight)
            {
                changeDirection();
            }
            if(target.transform.position.x < transform.position.x && facingRight)
            {
                changeDirection();
            }
            if(Mathf.Abs(target.transform.position.x - transform.position.x) > 4.5)
            {
                if(checkJumpGround() && onGround)
                {
                    jump();
                }
            }
            if(Mathf.Abs(target.transform.position.x - transform.position.x) < 4.5)
            {
                //Checking canJump to make sure landing animation is finished, and canJump gets reset
                if(!inAir && canJump)
                {
                    //Make complete stop after jump before attack
                    body.velocity = new Vector2(0,0);
                    attack();
                }
                
            }
            
        }
    }
    private void attack()
    {
        
        StartCoroutine(waitAttack(1.4f));
        animator.changeAnimationState(Frog_Animator.fAnim.FROG_ATTACK);
        Vector2 startPos = new Vector2(transform.position.x + 0.5f*transform.localScale.x, transform.position.y); 
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.right*transform.localScale.x,2.5f);
        Vector2 rayPos = new Vector2(transform.position.x+0.5f, transform.position.y);
        Debug.DrawRay(rayPos, Vector2.right*transform.localScale.x*2.5f, Color.green, 0.1f);
        if(hit.collider != null && toungeOut && !hitPlayer)
        {
            if(hit.transform.gameObject.CompareTag("Player"))
            {
                hitPlayer = true;
                hit.transform.gameObject.GetComponent<Status>().takeDamage(3, 0);
            } 
        }
            
            
        
        
    
    }
    private void jump()
    {
        if(canJump && !attacking)
        {
            
            animator.changeAnimationState(Frog_Animator.fAnim.FROG_JUMP_UP);
            body.AddForce( new Vector2(jumpPower.x*transform.localScale.x, jumpPower.y), ForceMode2D.Impulse);
            StartCoroutine(waitJump(0.1f));
        }
        
    }
    private bool getOnGround()
    {
        bool state = ground.getOnGround();
        if(state  && !attacking)
        {
            body.velocity = new Vector2(0,0);
            if(inAir && !attacking)
            {
                animator.changeAnimationState(Frog_Animator.fAnim.FROG_JUMP_DOWN);
                inAir = false;
            }
        }
        
        return ground.getOnGround();
    }
    public void startedLanding()
    {
        canJump = false;
    }
    
    public void finishedLanding()
    {
        canJump = true;
    }


    //v make sure ray is cast at correct part of animation
    public void toungeIsOut()
    {
        toungeOut = true;
    }
    public void toungeIsIn()
    {
        //Resets hitplayer so only one attack is registerd instead of attack*update frames
        toungeOut = false;
        hitPlayer = false;
    }
    
    private void getState()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            //Debug.Log("Attacking: "+attacking.ToString());
            Debug.Log("inAir: "+inAir.ToString());
            Debug.Log("CanJump: "+canJump.ToString());
            //Debug.Log("ToungeOUt: "+toungeOut.ToString());
            
        }
    }

    // void OnDrawGizmos()
    // {        
    //     Vector2 pos = new Vector2(transform.position.x + 3f*transform.localScale.x, transform.position.y);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(transform.position, Vector2.right);
    // }
    private bool checkJumpGround()
    {
        checkAhead.setRange(5, -0.5f);
        bool isGround = checkAhead.getGroundAhead();
        checkAhead.reset();
        return isGround;
    }


    public override void SetAggro(bool state)
    {
        aggro = state;
    }
    public bool getAggro()
    {
        return aggro;
    }
    
    
    
    protected override void changeDirection()
    {
        this.GetComponentInParent<Transform>().GetComponentInParent<Transform>().localScale *= new Vector2(-1,1);
        if(facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }
    
    

    protected override IEnumerator waitIdle()
    {
        waiting = true;
        int rand = Random.Range(0,4);
        if(rand == 0)
        {
            animator.changeAnimationState(Frog_Animator.fAnim.FROG_RIBBIT);
        }
        else
        {
            animator.changeAnimationState(Frog_Animator.fAnim.FROG_IDLE);
        }
        

        yield return new WaitForSeconds(waitTime);

        changeDirection();
        waiting = false;

    }

    IEnumerator waitJump(float wait)
    {
        //Sets in air to true after a delay, prevents onGround from loading jump_down before jump
        yield return new WaitForSeconds(wait);
        inAir = true;
    }

    IEnumerator waitAttack(float wait)
    {
        //Sets attacking to prevent onGround from loading down jump, and prevents jumping
        attacking = true;
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }
    
    
    public override void takeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
