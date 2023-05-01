using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeController : EnemyController
{
    public GameObject projectile;
    public float waitTime = 2;
    public Animator animator;
    public bool aggro = false;
    public CheckAhead checkAhead;
    public float maxSpeed = 5;

    public VisionScript vision;
    public float attack_x = 1;
    public float attack_y = 0.5f;
    public float attackForce = 5;

    


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
                animator.SetBool("running", true);
                
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
            animator.SetBool("running", false);
            target = vision.getTarget();
            if(target.transform.position.x > transform.position.x && !facingRight)
            {
                changeDirection();
            }
            if(target.transform.position.x < transform.position.x && facingRight)
            {
                changeDirection();
            }
            animator.SetBool("attacking", true);
            if(throwCount == 3)
            {
                throwCount = 0;
                SetAggro(false);
                animator.SetBool("attacking", false);
            }

        }
    }


    public void SpawnProjectile()
    {

        Vector2 spawnPosition = new Vector2(this.transform.position.x,this.transform.position.y + 0.5f);
        
        GameObject instance = Instantiate(projectile, spawnPosition, transform.rotation);
        int direction = 1;
        if(target.transform.position.x > transform.position.x)
        {
            direction = 1;
        }
        if(target.transform.position.x < transform.position.x)
        {
            direction = -1;
        }
        Vector2 attackAngle = new Vector2(attack_x*direction, attack_y);
        instance.GetComponent<Rigidbody2D>().AddForce(attackAngle*attackForce, ForceMode2D.Impulse);
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
        animator.SetBool("running", false);
        
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
