using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Controller : EnemyController
{
    public CheckAhead checkAhead;
    public Frog_Animator animator;
    public float waitTime = 2;
    public float maxSpeed = 3;


    private bool canJump;
    private bool waiting = false;
    private bool aggro = false;
    private bool facingRight = true;
    private bool setStartPosition = false;
    private Vector2 startPosition;
    private int distance;
    private Rigidbody2D body;
    private Vector2 velocity;
    protected override void Start()
    {   
        animator = GetComponent<Frog_Animator>();
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(waitIdle());
    }

    // Update is called once per frame
    protected override void Update()
    {
        
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

    public override void takeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
