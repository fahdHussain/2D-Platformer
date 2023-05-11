using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeController : EnemyController
{
    public GameObject projectile;
    public float waitTime = 2;
    //public Animator animator;
    public bool aggro = false;
    public CheckAhead checkAhead;
    public float maxSpeed = 5;

    public VisionScript vision;
    public float attack_x = 1;
    public float attack_y = 0.5f;
    public float attackForce = 5;
    public float arrowSpeed = 10;
    public GnomeAnimator animator;

    


    private bool waiting = false;
    private bool setStartPosition = false;
    private Vector2 velocity;
    private int distance;
    private Vector2 startPosition;
    private bool facingRight = true;
    private Rigidbody2D body;
    private GameObject target;
    private EnemyStats stats;
    private Vector2 attackStartPosition;
    private bool setAttackPosCheck = false;
    private bool meleeWaiting = false;
    private bool meleeAttacking = false;
    private bool chargingMelee = false;
    //private SpriteRenderer renderer;
    

    private int throwCount = 0;

    public enum GnomeType
    {
        MUSHROOM,
        ARCHER,
        KNIGHT
    }

    public GnomeType gnomeType;
    protected override void Start()
    {
        body = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
        stats.setType(EnemyStats.EnemyType.GNOME);
        //renderer = GetComponent<SpriteRenderer>();
        waitIdle();
    }

    protected override void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        //Non aggro state
        if(aggro == false)
        {
            if(waiting == false)
            {
                animator.changeAnimationState(GnomeAnimator.gAnim.RUN);
                
                if(!setStartPosition)
                {
                    int rand = Random.Range(1, 6);
                    distance = rand;
                    startPosition = new Vector2(this.transform.position.x, this.transform.position.y);
                    setStartPosition = true;
                }

                // Debug.Log("Facing right: "+facingRight);
                // Debug.Log("curPos: "+this.transform.position.x);
                // Debug.Log("newPos: "+startPosition.x+distance);
                // Debug.Log("ground ahead: "+checkAhead.getGroundAhead());

                if(!checkAhead.getGroundAhead() || checkAhead.getBlockAhead())
                {
                    body.velocity = new Vector2(0,0); 
                    StartCoroutine(waitIdle());
                }
                else if((facingRight && this.transform.position.x > startPosition.x + distance) || (!facingRight && this.transform.position.x < startPosition.x - distance))
                {
                    body.velocity = new Vector2(0,0); 
                    StartCoroutine(waitIdle());
                }
                else if(facingRight && this.transform.position.x < startPosition.x + distance && checkAhead.getGroundAhead() && !checkAhead.getBlockAhead())
                {
                   // Debug.Log("right");
                    velocity = new Vector2(maxSpeed, 0);
                    body.velocity = velocity;
                }
                else if(!facingRight && this.transform.position.x > startPosition.x - distance &&checkAhead.getGroundAhead() && !checkAhead.getBlockAhead())
                {
                    //Debug.Log("left");
                    velocity = new Vector2(-maxSpeed, 0);
                    body.velocity = velocity;
                }
                
            }
        }
        if(aggro)
        {
            //Attack State
            if(gnomeType == GnomeType.KNIGHT)
            {
                if(animator.getCurrentState() == GnomeAnimator.gAnim.ATTACK)
                {
                    if(chargingMelee)
                    {
                        body.velocity = new Vector2(0,0);
                    }
                    //For knight attack movement
                    if(!setAttackPosCheck)
                    {
                        setAttackStartPosition();
                        setAttackPosCheck = true;
                    }
                    if(setAttackPosCheck)
                    {
                        //Attack move distance
                        if(Mathf.Abs(attackStartPosition.x - this.transform.position.x) > 4)
                        {
                            body.velocity = new Vector2(0,body.velocity.y);
                            setAttackPosCheck = false;
                        }
                    }
                }
            }
            waitTime = 3;
            if(gnomeType == GnomeType.KNIGHT && !meleeWaiting)
            {
                animator.changeAnimationState(GnomeAnimator.gAnim.ATTACK);
            }
            if(gnomeType != GnomeType.KNIGHT)
            {
                body.velocity = new Vector2(0,0);
                animator.changeAnimationState(GnomeAnimator.gAnim.ATTACK);
            }    
            //Knight logic
            if(gnomeType == GnomeType.KNIGHT && meleeAttacking)
            {
                Vector2 startPos = new Vector2(transform.position.x + 1*transform.localScale.x, transform.position.y); 
                Collider2D[] colliderArray = Physics2D.OverlapBoxAll(startPos, new Vector2(2,0.5f), 0);
                foreach(Collider2D hit in colliderArray)
                {
                    if(hit.gameObject.CompareTag("Player"))
                    {
                        hit.gameObject.GetComponent<Status>().takeDamage(1,0);
                    }
                }
            }
            target = vision.getTarget();
            if(target.transform.position.x > transform.position.x && !facingRight)
            {
                changeDirection();
            }
            if(target.transform.position.x < transform.position.x && facingRight)
            {
                changeDirection();
            }
            //animator.SetBool("attacking", true);
            if(throwCount == 3)
            {
                throwCount = 0;
                SetAggro(false);
                StartCoroutine(waitIdle());
            }

        }
    }


    public void SpawnProjectile()
    {
        int direction = 1;
        if(target.transform.position.x > transform.position.x)
        {
            direction = 1;
        }
        if(target.transform.position.x < transform.position.x)
        {
            direction = -1;
        }
        switch(gnomeType)
        {
            
            case GnomeType.MUSHROOM:
                Vector2 mushSpawnPosition = new Vector2(this.transform.position.x,this.transform.position.y + 0.5f);
            
                GameObject mushInstance = Instantiate(projectile, mushSpawnPosition, transform.rotation);
                
                Vector2 attackAngle = new Vector2(attack_x*direction, attack_y);
                mushInstance.GetComponent<Rigidbody2D>().AddForce(attackAngle*attackForce, ForceMode2D.Impulse);
                break;
            
            case GnomeType.ARCHER:
                Vector2 arrowSpawnPosition = new Vector2(this.transform.position.x + 1*transform.localScale.x, this.transform.position.y);
                GameObject instance;
                if(transform.localScale.x > 0)
                {
                     instance = Instantiate(projectile, arrowSpawnPosition, transform.rotation);
                }
                else
                {
                    instance = Instantiate(projectile, arrowSpawnPosition, Quaternion.AngleAxis(180,Vector3.back));
                }
                Vector2 arrowVector = new Vector2(arrowSpeed*direction, 0);       
                instance.GetComponent<Rigidbody2D>().velocity = arrowVector;

                //projectile.GetComponent<Rigidbody2D>().velocity = arrowSpeed*transform.localScale.x;
                break;
        }

    }

    public void meleeAttack()
    {
        body.velocity = new Vector2(25*transform.localScale.x, 0);
        chargingMelee = false;
        meleeAttacking = true;
        
    }
    // void OnDrawGizmos()
    // {
        
    //     Vector2 pos = new Vector2(transform.position.x + 1f*transform.localScale.x, transform.position.y);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(pos, new Vector3(2,0.5f,1));
    // }
    public void meleeWait()
    {
        StartCoroutine(waitIdle(1));
    }

    public void setAttackStartPosition()
    {
        attackStartPosition = this.transform.position;
    }
    public override void SetAggro(bool state)
    {
        aggro = state;
    }
    public void ChargingMelee()
    {
        chargingMelee = true;
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

    protected  IEnumerator waitIdle(float aTime)
    {
        meleeWaiting = true;
        meleeAttacking = false;
        animator.changeAnimationState(GnomeAnimator.gAnim.IDLE);
        
        yield return new WaitForSeconds(aTime);

        //Debug.Log("Changing direction");
        meleeWaiting = false;
    }

    public override void takeDamage(int damage)
    {
        stats.takeDamage(damage);
    }
    public GnomeType GetGnomeType()
    {
        return gnomeType;
    }



    public void countThrow()
    {
    
        if(!vision.getSight())
        {
            throwCount ++;
        }
        else
        {
            throwCount = 0;
        }
    }

    protected override IEnumerator waitIdle()
    {
        waiting = true;
        animator.changeAnimationState(GnomeAnimator.gAnim.IDLE);
        
        yield return new WaitForSeconds(waitTime);

        //Debug.Log("Changing direction");
        changeDirection();
        setStartPosition = false;
        waiting = false;
    }
}
