using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Controller : EnemyController
{
    public CheckAhead checkAhead;
    public Frog_Animator animator;
    public float waitTime = 2;
    public float maxSpeed = 3;
    public Vector2 jumpPower = new Vector2(5,7);
    public Ground ground;
    public VisionScript vision;


    private bool canJump;
    private bool inAir;
    private bool waiting = false;
    private bool aggro = false;
    private bool facingRight = true;
    private bool setStartPosition = false;
    private Vector2 startPosition;
    private int distance;
    private Rigidbody2D body;
    private Vector2 velocity;
    private GameObject target;
    protected override void Start()
    {   
        animator = GetComponent<Frog_Animator>();
        body = GetComponent<Rigidbody2D>();
        //vision = GetComponent<VisionScript>();
        StartCoroutine(waitIdle());
    }

    // Update is called once per frame
    protected override void Update()
    {
        testJump();
        stickTheLanding();
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
            if(Mathf.Abs(target.transform.position.x - transform.position.x) > 5)
            {
                if(checkJumpGround())
                {
                    jump();
                }
            }
        }
    }
    public void testJump()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            jump();
            //Debug.Log("JUMP");
        }
        
    }
    void OnDrawGizmos()
    {        
        Vector2 pos = new Vector2(transform.position.x + 5f*transform.localScale.x, transform.position.y - 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos, 0.3f);
    }
    private bool checkJumpGround()
    {
        checkAhead.setRange(5, -0.5f);
        bool isGround = checkAhead.getGroundAhead();
        checkAhead.reset();
        return isGround;
    }
    private void stickTheLanding()
    {
        //Debug.Log(ground.rayGroundCheck());
        if(inAir)
        {
            if(body.velocity.y < 0)
            {
                animator.changeAnimationState(Frog_Animator.fAnim.FROG_JUMP_DOWN);
            }
            if(ground.rayGroundCheck())
            {
                //Debug.Log("CHECK GROUND");
                body.velocity = new Vector2(0,0);
                inAir = false;
            }
        }
    }
    public void landAnimation()
    {
        animator.changeAnimationState(Frog_Animator.fAnim.FROG_IDLE);
    }
    private void jump()
    {
        StartCoroutine(waitCheckAir());
        animator.changeAnimationState(Frog_Animator.fAnim.FROG_JUMP_UP);
        body.velocity = new Vector2(jumpPower.x*transform.localScale.x,jumpPower.y);  
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
    IEnumerator waitCheckAir()
    {
        yield return new WaitForSeconds(0.1f);
        inAir = true;
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

    public override void takeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
