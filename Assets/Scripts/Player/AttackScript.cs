using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private PlayerController input = null;
    private WeaponScript wScript;
    private int mouseClick;
    private PlayerAnimator animator;
    //private WeaponScript.Weapon currentWeapon;

    void Start()
    {
        wScript = GetComponent<WeaponScript>();
        //currentWeapon = wScript.GetCurrentWeapon();
        animator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseClick = input.RetrieveAttackInput();
        if(mouseClick == 0)
        {
            //Debug.Log("Attack");
            if(wScript.GetCurrentWeapon() == WeaponScript.Weapon.BASIC)
            {
                
                animator.changeAnimationState(PlayerAnimator.pAnim.PLAYER_BASIC_ATTACK_1);
            }
        }
    }


    void FixedUpdate()
    {
        
    }
}
