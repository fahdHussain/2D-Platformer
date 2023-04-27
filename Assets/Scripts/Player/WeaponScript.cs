using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponScript : MonoBehaviour
{
    
    public enum Weapon 
    {
        BASIC,
        PISTOL
    }

    public enum Look
    {
        UP,
        FORWARD
    }

    private Weapon currentWeapon;
    private Look currentLook;
    public LayerMask enemyLayer;
    public float up = 1;
    public float radius = 0.3f;

    public void SetCurrentWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
    public void SetCurrentLook(Look direction)
    {
        currentLook = direction;
    }
    public Look GetCurrentLook()
    {
        return currentLook;
    }

    public void attack()
    {
        if(currentWeapon == Weapon.BASIC && currentLook == Look.FORWARD)
        {
            Vector2 frontAttackPoint = new Vector2(transform.position.x + 0.75f,transform.position.y);
            Collider2D[] frontColliderArray = Physics2D.OverlapCircleAll(frontAttackPoint, 0.25f, enemyLayer);
            //Debug.Log("Attak");
            foreach(Collider2D enemy in frontColliderArray)
            {
                Debug.Log("Hit Enemy");
            }
            Vector2 upAttackPoint = new Vector2(transform.position.x + 0.5f,transform.position.y +0.4f);
            Collider2D[] upColliderArray = Physics2D.OverlapCircleAll(upAttackPoint, 0.25f, enemyLayer);
            //Debug.Log("Attak");
            foreach(Collider2D enemy in upColliderArray)
            {
                Debug.Log("Hit Enemy");
            }   
        }
        if(currentWeapon == Weapon.BASIC && currentLook == Look.UP)
        {
            Vector2 upAttackPoint = new Vector2(transform.position.x, transform.position.y + 1); 
            Collider2D[] upColliderArray = Physics2D.OverlapCircleAll(upAttackPoint, 0.3f, enemyLayer);         
            foreach(Collider2D enemy in upColliderArray)
            {
                Debug.Log("Hit Enemy");
            }  
        }
    }

    
    // void OnDrawGizmos()
    // {
        
    //     Vector2 pos = new Vector2(transform.position.x, transform.position.y + up);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(pos, radius);
    // }



}
