using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject particleEffect;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyStats>() != null)
        {
            //Debug.Log("HIT");
            collision.gameObject.GetComponent<EnemyStats>().takeDamage(1);
            Destroy(gameObject);
        }
        else if(collision.gameObject.GetComponent<BreakCrateScript>() != null)
            {
                collision.gameObject.GetComponent<BreakCrateScript>().destroyCrate();
        }
        else if(collision.gameObject.GetComponent<PotDestroyScript>() != null)
        {
            collision.gameObject.GetComponent<PotDestroyScript>().destroyPot();
        }
        else
        {
            Instantiate(particleEffect, transform.position, Quaternion.AngleAxis(180, Vector3.back));
            Destroy(gameObject);
        }
    }
}
