using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int health = 5;
    public int score = 0;
    public bool isAlive = true;
    public bool hasKey = false;
    public bool reachedExit = false;
    public bool valExit = false;
    private CamShake camShake;
    private FreezeFrame freezeFrame;
    private GreyScaleEffect_Script greyScaleEffect;
    private WeaponScript wScript;
    private WeaponScript.Weapon currentWeapon;
    public GameObject bloodSpawn;
    public GameObject bloodEffectSmall;
    public GameObject bloodEffectBig;
    public GameObject corpse;
    public SoundEffectController soundEffectController;
    public UIScript ui;


    void Start()
    {
        camShake = GameObject.FindGameObjectWithTag("CamFollow").GetComponent<CamShake>();
        freezeFrame = GameObject.FindGameObjectWithTag("CamFollow").GetComponent<FreezeFrame>();
        greyScaleEffect = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GreyScaleEffect_Script>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();

        wScript = GetComponent<WeaponScript>();
        wScript.SetCurrentWeapon(WeaponScript.Weapon.BASIC);
        currentWeapon = wScript.GetCurrentWeapon();
    }
    public void takeDamage(int damage, int type)
    {
        //0 -- > Regular damage
        //1 --> SPike
        //Type used for blood spawn effect
        camShake.shakeCam();
        
        bloodSpawn.GetComponent<BloodStains>().SpawnBlood(transform, type);
        Instantiate(bloodEffectSmall, transform.position,transform.rotation);

        if(damage >= health)
        {
            health = 0;
            if(isAlive)
            {
                die();
            }
            
        } 
        else
        {
            soundEffectController.playSound(2);
            health -= damage;
        }
    }
    public void takeDamage(int damage, int type, Vector2 direction)
    //Add directional movement
    {
        camShake.shakeCam();
        
        bloodSpawn.GetComponent<BloodStains>().SpawnBlood(transform, type);;
        Instantiate(bloodEffectSmall, transform.position,transform.rotation);
        //GetComponent<Rigidbody2D>().velocity += direction;

        if(damage >= health)
        {
            health = 0;
            if(isAlive)
            {
                die();
            }
        } 
        else
        {
            soundEffectController.playSound(2);
            health -= damage;
            //multiplier = maxPush/(delta(damage)) + b
            float movementMultiplier = damage * (3.33f) + 6.66f;
            this.GetComponent<Rigidbody2D>().AddForce(direction * movementMultiplier, ForceMode2D.Impulse);
        }
    }

    private void die()
    {
        GameObject instance = Instantiate(corpse, transform.position, transform.rotation);
        instance.GetComponent<CorpseExplode>().explode();

        //Disable movement and sprite;
        this.GetComponent<Move>().enabled = false;
        this.GetComponent<Jump>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        soundEffectController.playOneShotSound(5);
        freezeFrame.setDuration(3);
        isAlive = false;
        greyScaleEffect.SpawnGreyScale();
        Instantiate(bloodEffectBig, transform.position,transform.rotation);
        freezeFrame.Freeze();
        StartCoroutine(resetTextTimer());
        
    }

    public void gotKey()
    {
        soundEffectController.playOneShotSound(3);
        hasKey = true;
    }

    public void plusScore(int itemScore)
    {
        soundEffectController.playOneShotSound(4);
        score += itemScore;
    }

    public void atExit()
    {
        reachedExit = true;
    }

    public void leftExit()
    {
        reachedExit = false;
    }

    public void validExit()
    {
        valExit = true;
    }

    IEnumerator resetTextTimer()
    {
        yield return new WaitForSecondsRealtime(3);
        
        ui.resetGameOverText();
    }
}