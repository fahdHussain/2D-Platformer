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
    private SoundEffectController sound;
    
    public LayerMask enemyLayer;

    void Start()
    {
        sound = GetComponent<SoundEffectController>();
    }

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
            Vector2 frontAttackPoint = new Vector2(transform.position.x + 0.75f*transform.localScale.x,transform.position.y);
            Collider2D[] frontColliderArray = Physics2D.OverlapCircleAll(frontAttackPoint, 0.3f, enemyLayer);
            //Debug.Log("Attak");
            foreach(Collider2D enemy in frontColliderArray)
            {
                Debug.Log("HIT");
                enemy.GetComponent<EnemyStats>().takeDamage(1);
            }
            
        }
        if(currentWeapon == Weapon.BASIC && currentLook == Look.UP)
        {
            Vector2 upAttackPoint = new Vector2(transform.position.x*transform.localScale.x, transform.position.y + 1); 
            Collider2D[] upColliderArray = Physics2D.OverlapCircleAll(upAttackPoint, 0.3f, enemyLayer);         
            foreach(Collider2D enemy in upColliderArray)
            {
                Debug.Log("HIT");
                enemy.GetComponent<EnemyStats>().takeDamage(1);
            }  
        }
    }

    
    // void OnDrawGizmos()
    // {
        
    //     Vector2 pos = new Vector2(transform.position.x + 0.75f, transform.position.y);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(pos, 0.3f);
    // }

    public void playAttackSound()
    {
        if(currentWeapon == Weapon.BASIC)
        {
            sound.playSound(6);
        }
    }

}
