using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{
    public PlayerController input;
    private float up;
    private Look currentLook;
    private PlayerAnimator animator;
    private Status status;

    public enum Look
    {
        FORWARD,
        UP
    }
   
    void Awake()
    {
        animator = GetComponent<PlayerAnimator>();
        status = GetComponent<Status>();
    }
    void Update()
    {
         up = input.RetrieveLookUp();
        if(up > 0)
        {
            //if(currentLook == FORWARD)
            //{
            //      triggerLookUpAnimation --> Checks if weapon not BASIC --> triggers lookUp 
            //}
            if(status.GetCurrentWeapon() != WeaponScript.Weapon.BASIC)
            {
                triggerLookUpAnimation();
            }
            SetCurrentLook(Look.UP);
        }
        else
        {
            //if(currentLook == UP)
            //{
            //      triggerLookDownAnimation --> Checks if weapon not BASIX --> triggers lookDown
            //}
            if(status.GetCurrentWeapon() != WeaponScript.Weapon.BASIC)
            {
                triggerLookDownAnimation();
            }
            SetCurrentLook(Look.FORWARD);
        }
    }

    public void SetCurrentLook(Look direction)
    {
        currentLook = direction;
    }
    public Look GetCurrentLook()
    {
        return currentLook;
    }

    public void triggerLookUpAnimation()
    {
        if(GetCurrentLook() == LookScript.Look.FORWARD)
        {
            SetCurrentLook(Look.UP);
            animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_LOOKUP);
        }
    }

    public void triggerLookDownAnimation()
    {
        if(GetCurrentLook() == LookScript.Look.UP)
        {
            SetCurrentLook(Look.FORWARD);
            animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_LOOKDOWN);
        }
    }
}
