using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public enum pAnim 
    {
        PLAYER_IDLE,
        PLAYER_RUN,
        PLAYER_JUMP_UP,
        PLAYER_JUMP_DOWN,
        PLAYER_LAND,
        PLAYER_BASIC_ATTACK_1,
        PLAYER_BASIC_ATTACK_2

    }

    private pAnim currentState;
    //private bool playing;

    string getAnimation(pAnim state)
    {
        if(state == pAnim.PLAYER_IDLE)
        {
            return "idle";
        }
        if(state == pAnim.PLAYER_RUN)
        {
            return "running";
        }
        if(state == pAnim.PLAYER_JUMP_UP)
        {
            return "jump_up";
        }
        if(state == pAnim.PLAYER_JUMP_DOWN)
        {
            return "jump_down";
        }
        if(state == pAnim.PLAYER_LAND)
        {
            return "player_land";
        }
        if(state == pAnim.PLAYER_BASIC_ATTACK_1)
        {
            return "basic_attack";
        }
        if(state == pAnim.PLAYER_BASIC_ATTACK_2)
        {
            return "basic_attack_up";
        }

        else
        {
            Debug.Log("Missing Animation");
            return null;
        }
    }


    public void changeAnimationState(pAnim newState)
    {
        //stop anim from repeating
        if(currentState == newState) return;

        //play animation
        animator.Play(getAnimation(newState));

        //reassign current state
        currentState = newState;

    }

    public pAnim GetcurrentState()
    {
        return currentState;
    }
    public Animator getAnimator()
    {
        return animator;
    }

    public bool isAttacking()
    {
        if(currentState == pAnim.PLAYER_BASIC_ATTACK_1)
        {
            return true;
        }
        else if(currentState == pAnim.PLAYER_BASIC_ATTACK_2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // public void isPlaying()
    // {
    //     playing = true;
    // }
    // public void notPlaying()
    // {
    //     playing = false;
    // }
    // public bool getIsPlaying()
    // {
    //     return playing;
    // }

}
