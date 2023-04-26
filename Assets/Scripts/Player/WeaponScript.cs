using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public enum Weapon 
    {
        BASIC,
        PISTOL
    }

    private Weapon currentWeapon;

    public void SetCurrentWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    


}
