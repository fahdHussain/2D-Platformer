using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    private bool inAir = false;
    public AudioSource moveSoundSource;
    //public AudioClip moveSoundClip;

    
    public ParticleSystem dust;
    private SoundEffectController sound;
    public PlayerAnimator animator;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        sound = GetComponent<SoundEffectController>();
        moveSoundSource.volume = 0.4f;

        animator = GetComponent<PlayerAnimator>();
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

        if(onGround)
        {
            if(inAir)
            {
                playDust();
            }
            inAir = false;
        }
        else if(!onGround)
        {
            inAir = true;
            if(moveSoundSource.isPlaying)
            {
                moveSoundSource.Stop();
            }
        }

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

        if(Mathf.Abs(velocity.x) > 0){
            if(velocity.x > 0 && !facingRight)
            {
                transform.localScale *=  new Vector2(-1,1);
                facingRight = true;
                if(onGround)
                {
                    playDust();
                }
                
            }
            else if(velocity.x < 0 && facingRight)
            {
                transform.localScale *= new Vector2(-1,1);
                facingRight = false;
                if(onGround)
                {
                    playDust();
                }
            }
            
            if(onGround)
            {
                OnRunningAnimation();
                if(!moveSoundSource.isPlaying)
                {
                    moveSoundSource.Play();
                }
            }

        }
        else if(Mathf.Abs(velocity.x) == 0)
        {
            if(onGround)
            {
                //landing animation
                OnGroundAnimation();

            }

            if(moveSoundSource.isPlaying)
            {
                moveSoundSource.Stop();
            }
        }

        

    }

    void playDust()
    {
        dust.Play();
    }


    private void OnRunningAnimation()
    {
        if(animator.isAttacking())
        {
            if(animator.getAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.getAnimator().IsInTransition(0))
            {
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_RUN);
            }
        }
        else
        {
            animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_RUN);
        }

    }
    private void OnGroundAnimation()
    {
        //Check if player was falling
        if(animator.GetcurrentState() == PlayerAnimator.pAnim.PLAYER_JUMP_DOWN)
        {
            animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_LAND);
        }
        if(animator.GetcurrentState() == PlayerAnimator.pAnim.PLAYER_LAND)
        {
            //Finishes landing animation befor setting to idle
            if(animator.getAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.getAnimator().IsInTransition(0))
            {
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_IDLE);
            }
        }
        // Check if attacking
        if(animator.isAttacking())
        {
            if(animator.getAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.getAnimator().IsInTransition(0))
            {
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_IDLE);
            }
        }
        //Check if player was running
        if(body.velocity.x == 0 && animator.GetcurrentState() == PlayerAnimator.pAnim.PLAYER_RUN)
        {
            animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_IDLE);
        }
    }
}
