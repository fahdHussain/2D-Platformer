using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeController : MonoBehaviour
{
    public GameObject projectile;
    public float waitTime = 2;
    public Animator animator;
    public bool aggro = false;
    public CheckAhead checkAhead;
    public float maxSpeed = 5;
    private bool waiting = false;
    private bool setStartPosition = false;
    private Vector2 velocity;
    private int distance;
    private Vector2 startPosition;
    private bool facingRight = true;
    private Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        wait();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
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

                
                Debug.Log(checkAhead.getGroundAhead());
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
                    StartCoroutine(wait());
                }
                
            }
        }
    }

    // private void move(int distance)
    // {
    //     Transform startingPos = this.transform;
    //     animator.SetBool("running", true);

    // }

    public void SpawnProjectile()
    {

    }
    public void SetAggro(bool state)
    {
        aggro = state;
    }

    private void changeDirection()
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

    IEnumerator wait()
    {
        waiting = true;
        animator.SetBool("running", false);
        
        yield return new WaitForSeconds(waitTime);

        changeDirection();
        setStartPosition = false;
        waiting = false;
    }


}
