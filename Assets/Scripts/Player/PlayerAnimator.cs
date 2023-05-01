using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public WeaponScript wScript;
    private WeaponScript.Weapon currentWeapon;
    private LookScript look;
    private bool switchedWeapon = false;
    private Status status;

    void Awake()
    {
        animator = GetComponent<Animator>();
        wScript = GetComponent<WeaponScript>();
        look = GetComponent<LookScript>();
        status = GetComponent<Status>();
    }

    public enum pAnim 
    {
        PLAYER_IDLE,
        PLAYER_RUN,
        PLAYER_LOOKUP,
        PLAYER_LOOKDOWN,
        PLAYER_JUMP_UP,
        PLAYER_JUMP_DOWN,
        PLAYER_LAND,
        PLAYER_ATTACK_1,
        PLAYER_ATTACK_2,

    }

    private pAnim currentState;
    //private bool playing;

    string getAnimation(pAnim state)
    //Manages transitions, for each state, checks weapon and look
    {
        switch(state)
        {
            case pAnim.PLAYER_IDLE:
                switch(wScript.GetCurrentWeapon())
                {
                    case WeaponScript.Weapon.BASIC:
                        return "idle";
                    case WeaponScript.Weapon.PISTOL:
                        if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                               // Debug.Log("pistol idle");
                                return "idle_pistol";
                            }
                            else
                            {
                                return "idle_pistol_up";
                            }
                    default:
                        return null;
                }
                
            
            case pAnim.PLAYER_RUN:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.BASIC:
                            return "running";
                        case WeaponScript.Weapon.PISTOL:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "running_pistol_f";
                            }
                            else
                            {
                                return "running_pistol_u";
                            }
                            
                        default:
                            return null;
                    }
                
            case pAnim.PLAYER_LOOKUP:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.PISTOL:
                            return "look_up_pistol";
                        default:
                            return null;
                    }

            case pAnim.PLAYER_LOOKDOWN:
                switch(wScript.GetCurrentWeapon())
                {
                    case WeaponScript.Weapon.PISTOL:
                        return "look_down_pistol";
                    default:
                        return null;
                }
             

            case pAnim.PLAYER_JUMP_UP:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.BASIC:
                            return "jump_up";
                        case WeaponScript.Weapon.PISTOL:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_up_pistol";
                            }
                            else
                            {
                                return "jump_up_pistol_up";
                            }
                        default:
                            return null;
                    }            
            case pAnim.PLAYER_JUMP_DOWN:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.BASIC:
                            return "jump_down";
                        case WeaponScript.Weapon.PISTOL:
                        if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_down_pistol";
                            }
                            else
                            {
                                return "jump_down_pistol_up";
                            }
                        default:
                            return null;
                    }

            case pAnim.PLAYER_LAND:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.BASIC:
                            return "player_land";
                        case WeaponScript.Weapon.PISTOL:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_down_pistol";
                            }
                            else
                            {
                                return "jump_down_pistol_up";
                            }
                        default:
                            return null;
                    }


            case pAnim.PLAYER_ATTACK_1:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.BASIC:
                            return "basic_attack";
                        case WeaponScript.Weapon.PISTOL:
                            return "pistol_attack";
                        default:
                            return null;
                    }
            
            case pAnim.PLAYER_ATTACK_2:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.BASIC:
                            return "basic_attack_up";
                        case WeaponScript.Weapon.PISTOL:
                            return "pistol_attack_up";
                        default:
                            return null;
                    }
            
            default:
                Debug.Log("MISSING ANIMATION");
                return null;
        }
        
    }


    public void changeAnimationState(pAnim newState)
    {
        
        if(currentState == newState && switchedWeapon == true)
        //Checks if weapon was switched
        {
            switchedWeapon = false;
            animator.Play(getAnimation(newState));

        }
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
        //Stops attack spamming
        if(currentState == pAnim.PLAYER_ATTACK_1)
        {
            return true;
        }
        else if(currentState == pAnim.PLAYER_ATTACK_2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isChangingLook()
    {
        if(currentState == pAnim.PLAYER_LOOKUP || currentState == pAnim.PLAYER_LOOKDOWN)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void returnToIdle()
    {
        //trigger from end of lookup, lookdown animation
        changeAnimationState(pAnim.PLAYER_IDLE);
    }

    public void setSwitchWeapon(bool state)
    {
        switchedWeapon = state;
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
