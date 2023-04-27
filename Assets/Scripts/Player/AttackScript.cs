using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private PlayerController input = null;
    private WeaponScript wScript;
    private int mouseClick;
    private float up;
    private WeaponScript.Look direction;
    private PlayerAnimator animator;
    private SoundEffectController sound;
    //private WeaponScript.Weapon currentWeapon;

    void Start()
    {
        wScript = GetComponent<WeaponScript>();
        //currentWeapon = wScript.GetCurrentWeapon();
        animator = GetComponent<PlayerAnimator>();
        sound = GetComponent<SoundEffectController>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseClick = input.RetrieveAttackInput();
        up = input.RetrieveLookUp();
        if(up > 0)
        {
            wScript.SetCurrentLook(WeaponScript.Look.UP);
        }
        else
        {
            wScript.SetCurrentLook(WeaponScript.Look.FORWARD);
        }
        
        if(mouseClick == 0)
        {
            //BASIC ATTACKS
            if(wScript.GetCurrentWeapon() == WeaponScript.Weapon.BASIC && wScript.GetCurrentLook() == WeaponScript.Look.FORWARD)
            {
                
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_BASIC_ATTACK_1);
                wScript.attack();
                sound.playSound(6);
                
   
            }
            if(wScript.GetCurrentWeapon() == WeaponScript.Weapon.BASIC && wScript.GetCurrentLook() == WeaponScript.Look.UP)
            {
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_BASIC_ATTACK_2);
                wScript.attack();
                sound.playSound(6);
            }
        }
    }


    void FixedUpdate()
    {
        
    }
}
