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
                    case WeaponScript.Weapon.SHOTGUN:
                        if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "idle_shotgun";
                            }
                            else
                            {
                                return "idle_shotgun_up";
                            }
                    case WeaponScript.Weapon.MACHINEGUN:
                        if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "idle_machinegun";
                            }
                            else
                            {
                                return "idle_machinegun_up";
                            }
                    default:
                        return "idle";
                }
                
            
            case pAnim.PLAYER_RUN:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.PISTOL:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "running_pistol_f";
                            }
                            else
                            {
                                return "running_pistol_u";
                            }
                        case WeaponScript.Weapon.SHOTGUN:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "running_shotgun_f";
                            }
                            else
                            {
                                return "running_shotgun_u";
                            }
                        case WeaponScript.Weapon.MACHINEGUN:
                        if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "running_machinegun_f";
                            }
                            else
                            {
                                return "running_machinegun_u";
                            }
                            
                        default:
                            return "running";;
                    }
                
            case pAnim.PLAYER_LOOKUP:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.PISTOL:
                            return "look_up_pistol";
                        case WeaponScript.Weapon.SHOTGUN:
                            return "look_up_shotgun";
                        case WeaponScript.Weapon.MACHINEGUN:
                            return "look_up_machinegun";
                        default:
                            return null;
                    }

            case pAnim.PLAYER_LOOKDOWN:
                switch(wScript.GetCurrentWeapon())
                {
                    case WeaponScript.Weapon.PISTOL:
                        return "look_down_pistol";
                    case WeaponScript.Weapon.SHOTGUN:
                        return "look_down_shotgun";
                    case WeaponScript.Weapon.MACHINEGUN:
                        return "look_down_machinegun";
                    default:
                        return null;
                }
             

            case pAnim.PLAYER_JUMP_UP:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.PISTOL:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_up_pistol";
                            }
                            else
                            {
                                return "jump_up_pistol_up";
                            }
                        case WeaponScript.Weapon.SHOTGUN:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_up_shotgun";
                            }
                            else
                            {
                                return "jump_up_shotgun_up";
                            }
                        case WeaponScript.Weapon.MACHINEGUN:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_up_machinegun";
                            }
                            else
                            {
                                return "jump_up_machinegun_up";
                            }
                        default:
                            return "jump_up";
                    }            
            case pAnim.PLAYER_JUMP_DOWN:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.PISTOL:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                                {
                                    return "jump_down_pistol";
                                }
                                else
                                {
                                    return "jump_down_pistol_up";
                                }
                        case WeaponScript.Weapon.SHOTGUN:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_down_shotgun";
                            }
                            else
                            {
                                return "jump_down_shotgun_up";
                            }
                         case WeaponScript.Weapon.MACHINEGUN:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_down_machinegun";
                            }
                            else
                            {
                                return "jump_down_machinegun_up";
                            }
                        default:
                            return "jump_down";
                    }

            case pAnim.PLAYER_LAND:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.PISTOL:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_down_pistol";
                            }
                            else
                            {
                                return "jump_down_pistol_up";
                            }
                        case WeaponScript.Weapon.SHOTGUN:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_down_shotgun";
                            }
                            else
                            {
                                return "jump_down_shotgun_up";
                            }
                        case WeaponScript.Weapon.MACHINEGUN:
                            if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                            {
                                return "jump_down_machinegun";
                            }
                            else
                            {
                                return "jump_down_machinegun_up";
                            }
                        default:
                            return "player_land";;
                    }


            case pAnim.PLAYER_ATTACK_1:
                switch(wScript.GetCurrentWeapon())
                    {
                            
                        case WeaponScript.Weapon.PISTOL:
                            return "pistol_attack";
                        case WeaponScript.Weapon.GRENADE:
                            return "grenade_attack";
                        case WeaponScript.Weapon.SHOTGUN:
                            return "shotgun_attack";
                        case WeaponScript.Weapon.MACHINEGUN:
                            return "machinegun_attack";
                        default:
                            return "basic_attack";;
                    }
            
            case pAnim.PLAYER_ATTACK_2:
                switch(wScript.GetCurrentWeapon())
                    {
                        case WeaponScript.Weapon.PISTOL:
                            return "pistol_attack_up";
                        case WeaponScript.Weapon.GRENADE:
                            return "grenade_attack_up";
                        case WeaponScript.Weapon.SHOTGUN:
                            return "shotgun_attack_up";
                        case WeaponScript.Weapon.MACHINEGUN:
                            return "machinegun_attack_up";
                        default:
                            return "basic_attack_up";
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
