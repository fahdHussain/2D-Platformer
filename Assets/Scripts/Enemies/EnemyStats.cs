using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField, Range(1,10)] private int health;
    [SerializeField, Range(0,2)] private float flashTime;
    private bool isAlive;
    private CamShake camShake;
    private FreezeFrame freezeFrame;
    private SpriteRenderer spriteRenderer;
    public GameObject bloodSpawn;
    public GameObject bloodEffectBig;
    public GameObject bloodEffectSmall;
    public SoundEffectController sound;
    //0 = tookDamage

    public EnemyType type;
    public enum EnemyType
    {
        BEE,
        BEEHIVE,
        GNOME,
        FROG
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        camShake = GameObject.FindGameObjectWithTag("CamFollow").GetComponent<CamShake>();
        freezeFrame = GameObject.FindGameObjectWithTag("CamFollow").GetComponent<FreezeFrame>();
        isAlive = true;
    }

    public void setType(EnemyType getType)
    {
        type = getType;
    }

    public void takeDamage(int damage)
    {
        if(type == EnemyType.GNOME)
        {
            //Debug.Log("TOOK DAMAGE");
            flash();
            freezeFrame.Freeze(0.05f);
            sound.playSound(0);
            genericDamage(damage);
            bloodSpawn.GetComponent<BloodStains>().SpawnBlood(transform, 0);
            Instantiate(bloodEffectSmall, transform.position,transform.rotation);

            if(!isAlive)
            {
                bloodSpawn.GetComponent<BloodStains>().SpawnBlood(transform, 0);
                Instantiate(bloodEffectBig, transform.position,transform.rotation);
                die();
            }
        }
        
    }
    public void die()
    {
        camShake.shakeCam();
        Destroy(gameObject);
    }

    private void genericDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            isAlive = false;
        }
    }

    public int getHealth()
    {
        return health;
    }

    public bool getAlive()
    {
        return isAlive;
    }

    private void flash()
    {
        spriteRenderer.material.color = Color.red;
        Invoke("resetColor", flashTime);

    }

    private void resetColor()
    {
        spriteRenderer.material.color = Color.white;
    }
}
