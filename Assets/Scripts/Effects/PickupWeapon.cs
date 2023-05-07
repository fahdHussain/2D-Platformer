using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private PlayerController input = null;
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
            case WeaponScript.Weapon.MACHINEGUN:
                animator.Play("MachinegunPickUp");
                break;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Vector2 laserPos = new Vector2(transform.position.x, transform.position.y + 0.3f);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(!player.GetComponent<Status>().pickUpWeapon(weapon))
            {
                if(input.RetrieveInteract())
                {
                    //drop current weapon
                    //get new current weapon
                    // if not basic -- > save current weapon -- > drop -- .pickup new --> pick up old
                    //if basic -- > pickup
                    player.GetComponent<Status>().dropWeapon();
                    player.GetComponent<Status>().pickUpWeapon(weapon);
                    player.GetComponent<Status>().switchWeapon();
                    Instantiate(pickupLaser, laserPos, transform.rotation);
                    Destroy(gameObject);
                }
            }
            else
            {   
                Instantiate(pickupLaser, laserPos, transform.rotation);
                Destroy(gameObject);
            }
            
        }
    }
}
