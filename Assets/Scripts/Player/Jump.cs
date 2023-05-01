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

    public ParticleSystem dust;

    private PlayerAnimator animator;
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
        animator = GetComponent<PlayerAnimator>();
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

        if(onGround && falling == true)
        {
            sound.playSound(1);
            falling = false;
            if(!animator.isAttacking())
            {
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_LAND);
            }
            
        }
        
        if(desiredJump)
        {
            desiredJump = false;
            JumpAction();
            //animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_JUMP_UP);
        }
        if(body.velocity.y > 0)
        {
            body.gravityScale = upMovementMultiplier;
            
        }
        else if(body.velocity.y < 0)
        {
            body.gravityScale = downMovementMultiplier;
            //animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_JUMP_DOWN);
        }
        else if(body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }

        if(body.velocity.y < -0.5f)
        {
            falling = true;

            if(animator.isAttacking())
            {
                if(animator.getAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.getAnimator().IsInTransition(0))
                {
                    animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_JUMP_DOWN);
                }
            }
            



            if(!animator.isAttacking())
            {
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_JUMP_DOWN);
            }
            
        }
        body.velocity = velocity;
    }

    private void JumpAction()
    {   
        //Debug.Log(onGround);
        
        if(onGround || jumpPhase <= maxAirJumps)
        {
            if(!animator.isAttacking())
            {
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_JUMP_UP);
            }
            
            jumpPhase += 1;
            //Debug.Log(jumpPhase);
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

    void UpdateJumpModifiers()
    {
        //TO-DO
        //update modifiers for different weapon
    }
}
