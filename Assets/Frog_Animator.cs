using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Animator : MonoBehaviour
{
    public Animator animator;
    public enum fAnim
    {
        FROG_IDLE,
        FROG_WALK,
        FROG_RIBBIT,
        FROG_JUMP_UP,
        FROG_JUMP_DOWN,
        FROG_ATTACK
    }

    private fAnim currentState;
     void Start()
    {
        animator = GetComponent<Animator>();
    }

    private string getAnimation(fAnim state)
    {
        if(state == fAnim.FROG_IDLE)
        {
            return "frog_idle";
        }
        if(state == fAnim.FROG_WALK)
        {
            return "frog_walk";
        }
        if(state == fAnim.FROG_RIBBIT)
        {
            return "frog_ribbit";
        }
        if(state == fAnim.FROG_JUMP_UP)
        {
            return "frog_jump_up";
        }
        if(state == fAnim.FROG_JUMP_DOWN)
        {
            return "frog_jump_down";
        }
        if(state == fAnim.FROG_ATTACK)
        {
            return "frog_attack";
        }
        else
        {
            Debug.Log("Invalid State");
            return null;
        }
    }

    public void changeAnimationState(fAnim newState)
    {
        if(currentState == newState) return;

        //play animation
        animator.Play(getAnimation(newState));

        //reassign current state
        currentState = newState;
    }
    public fAnim GetcurrentState()
    {
        return currentState;
    }
    public Animator getAnimator()
    {
        return animator;
    }


   

    // Update is called once per frame
    void Update()
    {
        
    }
}
