using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private PlayerController input = null;
    private Status status;
    private WeaponScript wScript;
    private int mouseClick;
    private bool mouseClickHold;
    private LookScript look;
    private PlayerAnimator animator;
    private SoundEffectController sound;
    //private WeaponScript.Weapon currentWeapon;

    void Awake()
    {
        wScript = GetComponent<WeaponScript>();
        //currentWeapon = wScript.GetCurrentWeapon();
        animator = GetComponent<PlayerAnimator>();
        sound = GetComponent<SoundEffectController>();
        status = GetComponent<Status>();
        look = GetComponent<LookScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(status.GetCurrentWeapon() != WeaponScript.Weapon.MACHINEGUN)
        {
            mouseClick = input.RetrieveAttackInput();
        
            if(mouseClick == 0) 
            {
                // if(animator.GetcurrentState() != PlayerAnimator.pAnim.PLAYER_ATTACK_1 || animator.GetcurrentState() != PlayerAnimator.pAnim.PLAYER_ATTACK_2)
                // {
                    if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                    {
                        animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_ATTACK_1);
                    }
                    else
                    {
                        animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_ATTACK_2);
                    }
                    //wScript.attack();
            // }
                

            }
        }
        if(status.GetCurrentWeapon() == WeaponScript.Weapon.MACHINEGUN)
        {
            mouseClickHold = input.RetrieveAttackInputHold();
            if(mouseClickHold)
            {
                if(look.GetCurrentLook() == LookScript.Look.FORWARD)
                {
                    animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_ATTACK_1);
                }
                else
                {
                    animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_ATTACK_2);
                }
            }
            
        }
        

        if(input.RetrieveWeaponSwitch())
        {
            status.switchWeapon();
        }
    }


    void FixedUpdate()
    {
        
    }
}
