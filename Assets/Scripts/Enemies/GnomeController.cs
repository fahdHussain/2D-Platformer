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
    //private SpriteRenderer renderer;
    

    private int throwCount = 0;

    public enum GnomeType
    {
        MUSHROOM,
        ARCHER
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

                
                //Debug.Log(checkAhead.getGroundAhead());
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
            //Attack State
            waitTime = 3;
            body.velocity = new Vector2(0,0);
            animator.changeAnimationState(GnomeAnimator.gAnim.ATTACK);
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
                animator.changeAnimationState(GnomeAnimator.gAnim.IDLE);
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
    public override void SetAggro(bool state)
    {
        aggro = state;
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
        animator.changeAnimationState(GnomeAnimator.gAnim.IDLE);
        
        yield return new WaitForSeconds(waitTime);

        //Debug.Log("Changing direction");
        changeDirection();
        setStartPosition = false;
        waiting = false;
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


}
