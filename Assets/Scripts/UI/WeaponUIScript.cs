using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIScript : MonoBehaviour
{
    private Animator animator;
    private bool hasWeapon = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void weaponPickUp(WeaponScript.Weapon weapon)
    {
        switch(weapon)
        {
            case WeaponScript.Weapon.PISTOL:
                animator.Play("pistol_deselct");
                break;
            case WeaponScript.Weapon.SHOTGUN:
                animator.Play("shotgun_deselect");
                break;
            default:
                break;
        }
    }
    public void weaponDrop()
    {
        animator.Play("weapon_empty");
    }

    public void weaponSelect(WeaponScript.Weapon weapon)
    {
        switch(weapon)
        {
            case WeaponScript.Weapon.PISTOL:
                animator.Play("pistol_select");
                break;
            case WeaponScript.Weapon.SHOTGUN:
                animator.Play("shotgun_select");
                break;
            default:
                break;
        }
    }
    public void weaponDeselect(WeaponScript.Weapon weapon)
    {
        switch(weapon)
        {
            case WeaponScript.Weapon.PISTOL:
                animator.Play("pistol_deselect");
                break;
            case WeaponScript.Weapon.SHOTGUN:
                animator.Play("shotgun_deselect");
                break;
            default:
                break;
        }
    }

    public bool getHasWeapon()
    {
        return hasWeapon;
    }
    public void setHasWeapon(bool state)
    {
        hasWeapon = state;
    }
    
}
