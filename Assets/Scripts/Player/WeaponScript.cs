using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponScript : MonoBehaviour
{
    
    public enum Weapon 
    {
        BASIC,
        PISTOL,
        SHOTGUN
    }

    // public enum Look
    // {
    //     UP,
    //     FORWARD
    // }

    public GameObject[] projectiles;
    private Weapon currentWeapon;
    private LookScript look;
    private LookScript.Look currentLook;
    private SoundEffectController sound;
    private PlayerAnimator animator;
    private bool canShoot = true;
    
    public LayerMask enemyLayer;

    void Awake()
    {
        sound = GetComponent<SoundEffectController>();
        look = GetComponent<LookScript>();
        currentLook = look.GetCurrentLook();
        animator = GetComponent<PlayerAnimator>();
    }


    public void SetCurrentWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void attack()
    {
        //TO-DO
        //Add pistol logic
        currentLook = look.GetCurrentLook();
        switch(currentWeapon)
        {
            case Weapon.BASIC:
                if(currentLook == LookScript.Look.FORWARD)
                {
                    Vector2 frontAttackPoint = new Vector2(transform.position.x + 0.75f*transform.localScale.x,transform.position.y);
                    Collider2D[] frontColliderArray = Physics2D.OverlapCircleAll(frontAttackPoint, 0.3f, enemyLayer);
                    //Debug.Log("Attak");
                    foreach(Collider2D enemy in frontColliderArray)
                    {
                        //Debug.Log("HIT");
                        enemy.GetComponent<EnemyStats>().takeDamage(1);
                    }
                    break;
                }
                else
                //LOOK.UP
                {
                    Vector2 upAttackPoint = new Vector2(transform.position.x*transform.localScale.x, transform.position.y + 1); 
                    Collider2D[] upColliderArray = Physics2D.OverlapCircleAll(upAttackPoint, 0.3f, enemyLayer);         
                    foreach(Collider2D enemy in upColliderArray)
                    {
                        //Debug.Log("HIT");
                        enemy.GetComponent<EnemyStats>().takeDamage(1);
                    }
                    break;  
                }
            
            case Weapon.PISTOL:
            //Debug.Log("CANSHOOT: "+canShoot);
                if(currentLook == LookScript.Look.FORWARD)
                {
                    //Instantiate projectile -> forward
                    GameObject bullet = projectiles[0];
                    Vector2 spawnPosition = new Vector2(transform.position.x + 0.5f*transform.localScale.x, transform.position.y);
                    GameObject instance = Instantiate(bullet, spawnPosition , transform.rotation);
                    //StartCoroutine(canShootTimer(0.25f));
                    instance.GetComponent<Rigidbody2D>().velocity = new Vector2(15f*transform.localScale.x, 0);
                    //Recoil
                    GetComponent<Rigidbody2D>().AddForce(10*Vector2.left*transform.localScale.x, ForceMode2D.Impulse);
                    break;
                }
                else if(currentLook == LookScript.Look.UP)
                {
                    //LOOK.UP
                    //Instantiate projectile up
                    GameObject bullet = projectiles[0];
                    Vector2 spawnPosition = new Vector2(transform.position.x + 0.2f*transform.localScale.x, transform.position.y+0.75f);
                    //StartCoroutine(canShootTimer(0.25f));
                    GameObject instance = Instantiate(bullet, spawnPosition , Quaternion.AngleAxis(-90, Vector3.back));
                    Vector2 direction = new Vector2(Mathf.Sin(20*Mathf.PI/180), Mathf.Cos(20*Mathf.PI/180));
                    instance.GetComponent<Rigidbody2D>().velocity = direction*15f;
                    //Recoil
                    GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
                    break;
                }
                else
                {
                    break;
                }

        }


        // if(currentWeapon == Weapon.BASIC && currentLook == LookScript.Look.FORWARD)
        // {
            // Vector2 frontAttackPoint = new Vector2(transform.position.x + 0.75f*transform.localScale.x,transform.position.y);
            // Collider2D[] frontColliderArray = Physics2D.OverlapCircleAll(frontAttackPoint, 0.3f, enemyLayer);
            // //Debug.Log("Attak");
            // foreach(Collider2D enemy in frontColliderArray)
            // {
            //     //Debug.Log("HIT");
            //     enemy.GetComponent<EnemyStats>().takeDamage(1);
            // }
            
        // }
        // if(currentWeapon == Weapon.BASIC && currentLook == LookScript.Look.UP)
        // {
            // Vector2 upAttackPoint = new Vector2(transform.position.x*transform.localScale.x, transform.position.y + 1); 
            // Collider2D[] upColliderArray = Physics2D.OverlapCircleAll(upAttackPoint, 0.3f, enemyLayer);         
            // foreach(Collider2D enemy in upColliderArray)
            // {
            //     //Debug.Log("HIT");
            //     enemy.GetComponent<EnemyStats>().takeDamage(1);
            // }  
        // }
        // if(currentWeapon == Weapon.PISTOL)
        // {
        //     if(currentLook == LookScript.Look.FORWARD)
        //     {
        //         //Forward attack
        //     }
        //     if(currentLook == LookScript.Look.UP)
        //     {
        //         //UP attack
        //     }
        // }
    }

    
    // void OnDrawGizmos()
    // {
        
    //     Vector2 pos = new Vector2(transform.position.x + 0.75f, transform.position.y);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(pos, 0.3f);
    // }

    //Called from animations
    public void setCanShootFalse()
    {
        canShoot = false;
    }
    public void setCanShootTrue()
    {
        Debug.Log("CANSHOOT");
        canShoot = true;
    }

    IEnumerator canShootTimer(float time)
    //Determines interval between shots
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    public void playAttackSound()
    {
        //Played on animation
        if(currentWeapon == Weapon.BASIC)
        {
            sound.playOneShotSound(6);
        }
    }

}
