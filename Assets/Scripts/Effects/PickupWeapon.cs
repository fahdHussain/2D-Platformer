using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public WeaponScript.Weapon weapon;
    public GameObject pickupLaser;
    private Animator animator;
 
    void Start()
    {
        animator = GetComponent<Animator>();
        switch(weapon)
        {
            case WeaponScript.Weapon.PISTOL:
                animator.Play("PistolPickUp");
                break;
            case WeaponScript.Weapon.SHOTGUN:
                animator.Play("ShotgunPickUp");
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Status>().pickUpWeapon(weapon);
            Vector2 laserPos = new Vector2(transform.position.x, transform.position.y + 0.3f);
            Instantiate(pickupLaser, laserPos, transform.rotation);
            Destroy(gameObject);
        }
    }
}
