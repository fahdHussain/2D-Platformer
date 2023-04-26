using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f,10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upMovementMultiplier = 1.7f;

    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private bool desiredJump;
    private bool onGround;
    private bool falling = false;

    public Animator animator;
    public ParticleSystem dust;

    private SoundEffectController sound;
    //0: Jump
    //1: Land


    
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        defaultGravityScale = 1f;

        sound = GetComponent<SoundEffectController>();
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump |= input.RetrieveJumpInput();
        onGround = ground.rayGroundCheck();
        if(onGround)
        {
            jumpPhase = 0;
        }
    }

    void FixedUpdate()
    {
        //onGround = ground.rayGroundCheck();
        velocity = body.velocity;
        animator.SetBool("onGround", onGround);
        animator.SetBool("jump_down", false);
        
        if(desiredJump)
        {
            desiredJump = false;
            JumpAction();
            animator.SetBool("jump_up", true);
        }
        if(body.velocity.y > 0)
        {
            body.gravityScale = upMovementMultiplier;
            
        }
        else if(body.velocity.y < 0)
        {
            body.gravityScale = downMovementMultiplier;
            animator.SetBool("jump_up", false);
            animator.SetBool("jump_down", true);
        }
        else if(body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }

        if(body.velocity.y < -0.5f)
        {
            falling = true;
        }
        body.velocity = velocity;
    }

    private void JumpAction()
    {   
        Debug.Log(onGround);
        
        if(onGround || jumpPhase <= maxAirJumps)
        {
            
            jumpPhase += 1;
            Debug.Log(jumpPhase);
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if(velocity.y < 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
            sound.playSound(0);
            PlayDust();
        }
    }

    void PlayDust()
    {
        dust.Play();
    }
}
