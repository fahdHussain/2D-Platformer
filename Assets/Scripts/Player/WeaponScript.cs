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
        GRENADE,
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

    // public void attack()
    // {
    //     //TO-DO
    //     //Add pistol logic
    //     currentLook = look.GetCurrentLook();
    //     switch(currentWeapon)
    //     {
    //         case Weapon.BASIC:
    //             if(currentLook == LookScript.Look.FORWARD)
    //             {
    //                 Vector2 frontAttackPoint = new Vector2(transform.position.x + 0.75f*transform.localScale.x,transform.position.y);
    //                 Collider2D[] frontColliderArray = Physics2D.OverlapCircleAll(frontAttackPoint, 0.3f, enemyLayer);
    //                 //Debug.Log("Attak");
    //                 foreach(Collider2D enemy in frontColliderArray)
    //                 {
    //                     //Debug.Log("HIT");
    //                     enemy.GetComponent<EnemyStats>().takeDamage(1);
    //                 }
    //                 break;
    //             }
    //             else
    //             //LOOK.UP
    //             {
    //                 Vector2 upAttackPoint = new Vector2(transform.position.x*transform.localScale.x, transform.position.y + 1); 
    //                 Collider2D[] upColliderArray = Physics2D.OverlapCircleAll(upAttackPoint, 0.3f, enemyLayer);         
    //                 foreach(Collider2D enemy in upColliderArray)
    //                 {
    //                     //Debug.Log("HIT");
    //                     enemy.GetComponent<EnemyStats>().takeDamage(1);
    //                 }
    //                 break;  
    //             }
            

    //     }
    // }

    
    // void OnDrawGizmos()
    // {
        
    //     Vector2 pos = new Vector2(transform.position.x + 0.75f, transform.position.y);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(pos, 0.3f);
    // }

    //Called from animations
    public void basicAttackForward()
    {
        Vector2 frontAttackPoint = new Vector2(transform.position.x + 0.75f*transform.localScale.x,transform.position.y);
        Collider2D[] frontColliderArray = Physics2D.OverlapCircleAll(frontAttackPoint, 0.3f, enemyLayer);
        //Debug.Log("Attak");
        foreach(Collider2D enemy in frontColliderArray)
        {
            //Debug.Log("HIT");
            if(enemy.GetComponent<EnemyStats>() != null)
            {
                enemy.GetComponent<EnemyStats>().takeDamage(1);
            }
            if(enemy.GetComponent<BreakCrateScript>() != null)
            {
                enemy.GetComponent<BreakCrateScript>().destroyCrate();
            }
            if(enemy.GetComponent<PotDestroyScript>() != null)
            {
                enemy.GetComponent<PotDestroyScript>().destroyPot();
            }
            
        }
    }
    public void basicAttackUp()
    {
        Vector2 upAttackPoint = new Vector2(transform.position.x*transform.localScale.x, transform.position.y + 1); 
        Collider2D[] upColliderArray = Physics2D.OverlapCircleAll(upAttackPoint, 0.3f, enemyLayer);         
        foreach(Collider2D enemy in upColliderArray)
        {
            //Debug.Log("HIT");
            if(enemy.GetComponent<EnemyStats>() != null)
            {
                enemy.GetComponent<EnemyStats>().takeDamage(1);
            }
            if(enemy.GetComponent<BreakCrateScript>() != null)
            {
                enemy.GetComponent<BreakCrateScript>().destroyCrate();
            }
            if(enemy.GetComponent<PotDestroyScript>() != null)
            {
                enemy.GetComponent<PotDestroyScript>().destroyPot();
            }
        }
    }
    public void pistolBulletForward()
    {
        GameObject bullet = projectiles[0];
        float direction = transform.localScale.x;
        Vector2 spawnPosition = new Vector2(transform.position.x + 0.5f*transform.localScale.x, transform.position.y);
        GameObject instance;
        if(transform.localScale.x > 0)
        {
            instance = Instantiate(bullet, spawnPosition , transform.rotation);
        }
        else
        {
            instance = Instantiate(bullet, spawnPosition , Quaternion.AngleAxis(180, Vector3.back));
        }
        
        //StartCoroutine(canShootTimer(0.25f));
        instance.GetComponent<Rigidbody2D>().velocity = new Vector2(15f*transform.localScale.x, 0);
        //Recoil
        GetComponent<Rigidbody2D>().AddForce(10*Vector2.left*transform.localScale.x, ForceMode2D.Impulse);
        playAttackSound();
    }

    public void pistolBulletUp()
    {
        GameObject bullet = projectiles[0];
        Vector2 spawnPosition = new Vector2(transform.position.x + 0.2f*transform.localScale.x, transform.position.y+0.75f);
        //StartCoroutine(canShootTimer(0.25f));
        GameObject instance = Instantiate(bullet, spawnPosition , Quaternion.AngleAxis(-90, Vector3.back));
        Vector2 direction = new Vector2(Mathf.Sin(20*Mathf.PI/180)*transform.localScale.x, Mathf.Cos(20*Mathf.PI/180));
        instance.GetComponent<Rigidbody2D>().velocity = direction*15f;
        //Recoil
        GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
        playAttackSound();
    }

    public void grenadeAttackForward()
    {
        GameObject grenade = projectiles[1];
        Vector2 spawnPosition = new Vector2(transform.position.x + 0.75f*transform.localScale.x, transform.position.y + 0.75f);

        GameObject instance;
        if(transform.localScale.x > 0)
        {
            instance = Instantiate(grenade, spawnPosition, transform.rotation);
        }
        else
        {
            instance = Instantiate(grenade, spawnPosition, Quaternion.AngleAxis(180, Vector3.back));
        }
        Vector2 force = new Vector2(8*transform.localScale.x, 6);
        instance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        instance.GetComponent<GrenadeScript>().startTimer();
    }
    public void grenadeAttackUp()
    {
        GameObject grenade = projectiles[1];
        Vector2 spawnPosition = new Vector2(transform.position.x + 0.75f*transform.localScale.x, transform.position.y + 0.75f);

        GameObject instance;
        if(transform.localScale.x > 0)
        {
            instance = Instantiate(grenade, spawnPosition, transform.rotation);
        }
        else
        {
            instance = Instantiate(grenade, spawnPosition, Quaternion.AngleAxis(180, Vector3.back));
        }
        Vector2 force = new Vector2(4*transform.localScale.x, 15);
        instance.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        instance.GetComponent<GrenadeScript>().startTimer();
    }
    public void shotgunAttackForward()
    {
        GameObject shotgunPellet = projectiles[2];
        float[] segments = new float[19];
        Vector2[] vectors = new Vector2[19];
        float segmentSize = 4.5f;
        float speed = 30f;
        
        calcAngles();
        calcVectors();

        for(int i = 0; i < 19; i++)
        {
            
            if(transform.localScale.x > 0)
            {
                Vector2 spawnPos = new Vector2(transform.position.x + 0.5f, transform.position.y);
                GameObject instance = Instantiate(projectiles[2], spawnPos, Quaternion.AngleAxis(Mathf.Acos(vectors[i].x)*Mathf.Rad2Deg, Vector3.forward));
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(vectors[i].x*speed,vectors[i].y*speed);
            }
            else
            {
                Vector2 spawnPos = new Vector2(transform.position.x - 0.5f, transform.position.y);
                GameObject instance = Instantiate(projectiles[2], spawnPos, Quaternion.AngleAxis(Mathf.Acos(vectors[i].x)*Mathf.Rad2Deg, Vector3.back));
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(vectors[i].x*speed*-1,vectors[i].y*speed);
            }
            
            
        }
        GetComponent<Rigidbody2D>().AddForce(15*Vector2.left*transform.localScale.x, ForceMode2D.Impulse);
        playAttackSound();



        void calcAngles()
        {
            float curStartAngle = -45;
            for(int i =0; i < 19; i++)
            {
                float rand = Random.Range(curStartAngle, curStartAngle + segmentSize);
                segments[i] = rand;
                curStartAngle += segmentSize;
            }
        }
        void calcVectors()
        {
            for(int i = 0; i < 19; i++)
            {
                float x = Mathf.Cos(segments[i] * Mathf.Deg2Rad);
                float y = Mathf.Sin(segments[i] * Mathf.Deg2Rad);

                vectors[i] = new Vector2(x,y);
            }
        }
        
    }
    public void shotgunAttackUp()
    {
        GameObject shotgunPellet = projectiles[2];
        float[] segments = new float[19];
        Vector2[] vectors = new Vector2[19];
        float segmentSize = 4.5f;
        float speed = 30f;
        
        calcAngles();
        calcVectors();

        for(int i = 0; i < 19; i++)
        {
            
            if(transform.localScale.x > 0)
            {
                Vector2 spawnPos = new Vector2(transform.position.x+0.1f, transform.position.y+0.5f);
                GameObject instance = Instantiate(projectiles[2], spawnPos, Quaternion.AngleAxis(Mathf.Acos(vectors[i].x)*Mathf.Rad2Deg, Vector3.forward));
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(vectors[i].x*speed,vectors[i].y*speed);
            }
            else
            {
                Vector2 spawnPos = new Vector2(transform.position.x-0.1f, transform.position.y+0.5f);
                GameObject instance = Instantiate(projectiles[2], spawnPos, Quaternion.AngleAxis(Mathf.Acos(vectors[i].x)*Mathf.Rad2Deg, Vector3.back));
                instance.GetComponent<Rigidbody2D>().velocity = new Vector2(vectors[i].x*speed*-1,vectors[i].y*speed);
            }
            
            
        }
        GetComponent<Rigidbody2D>().AddForce(15*Vector2.down, ForceMode2D.Impulse);
        playAttackSound();



        void calcAngles()
        {
            float curStartAngle = 45;
            for(int i =0; i < 19; i++)
            {
                float rand = Random.Range(curStartAngle, curStartAngle + segmentSize);
                segments[i] = rand;
                curStartAngle += segmentSize;
            }
        }
        void calcVectors()
        {
            for(int i = 0; i < 19; i++)
            {
                float x = Mathf.Cos(segments[i] * Mathf.Deg2Rad);
                float y = Mathf.Sin(segments[i] * Mathf.Deg2Rad);

                vectors[i] = new Vector2(x,y);
            }
        }
    }

    public void playAttackSound()
    {
        //Played on animation
        switch(currentWeapon)
        {
            case Weapon.BASIC:
                sound.playSound(6);
                break;
            case Weapon.PISTOL:
                sound.playOneShotSound(8);
                break;
            case Weapon.SHOTGUN:
                sound.playOneShotSound(9);
                break;
        }
    }

}
