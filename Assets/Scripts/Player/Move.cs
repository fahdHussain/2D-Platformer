using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f,100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f,100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f,100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;
    private bool facingRight = true;

    public Animator animator;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.getFriction(), 0f);
    }

    void FixedUpdate()
    {
        onGround = ground.getOnGround();
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

        if(Mathf.Abs(velocity.x) > 0){
            if(velocity.x > 0 && !facingRight)
            {
                transform.localScale *=  new Vector2(-1,1);
                facingRight = true;
            }
            else if(velocity.x < 0 && facingRight)
            {
                transform.localScale *= new Vector2(-1,1);
                facingRight = false;
            }
            animator.SetBool("running", true);

        }
        else if(Mathf.Abs(velocity.x) == 0)
        {
            animator.SetBool("running", false);
        }

        

    }
}
