using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField, Range(0f,100f)] private float maxSpeed = 3;
    [SerializeField, Range(0f,100f)] private float acceleration = 25;
    public GameObject player;
    public Animator animator;
    public enum destination
    {
        player,
        home
    }
    
    private Transform hive;
    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;

    private Vector2 curTargetVector;
    private Transform curTarget;

    private float maxSpeedChange;
    private bool goHome = true;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hive = transform.parent.gameObject.transform;
        setTargetPos(transform.parent.gameObject.transform);

    }

    // Update is called once per frame
    void Update()
    {  
        if(goHome)
        {
            //Set target to hive
            setTargetPos(hive);
            
        }
        if(!goHome)
        {
            setTargetPos(curTarget);
        }
        desiredVelocity = new Vector2(direction.x * maxSpeed, direction.y * maxSpeed);
        
        //Vector2 startPos = new Vector2(transform.position.x*direction.x - 0.3f, transform.position.y*direction.y - 0.3f);
        //RaycastHit2D hit = Physics2D.Raycast(startPos, direction);
        //Debug.DrawRay(startPos, direction, Color.red);
    }

    void FixedUpdate()
    {
        

            velocity = body.velocity;
            if(goHome)
            {
                setAggro(false);
                maxSpeedChange = 1000;
            }
            else if(!goHome)
            {
                setAggro(true);
                maxSpeedChange = acceleration * Time.deltaTime;
                
            }
                float xVel = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
                float yVel = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);

                velocity = new Vector2(xVel,yVel);
                body.velocity = velocity;
            
        
    }

    public void setTargetPos(Transform target)
    {
        
        direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        curTargetVector = new Vector2(direction.x, direction.y);     
        curTarget = target;    
    
        direction.Normalize();
        
        
    }

    public void returnHome(bool dir)
    {
        goHome = dir;
    }

    // private void setTargetPos(destination dest)
    // {
    //     if(dest == destination.player)
    //     {
    //         direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
    //     }
    //     if(dest == destination.home)
    //     {
    //         direction = new Vector2(hive.position.x - transform.position.x, hive.position.y - transform.position.y);
    //     }
    //     direction.Normalize();
    // }

    public void setAggro(bool state)
    {
        animator.SetBool("aggro", state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Status>().takeDamage(1,0);
        }
    }
}
