using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageScript : MonoBehaviour
{
    public int damage = 1;
    public float multiplier = 0.3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Status>().takeDamage(damage, 0, GetComponent<Rigidbody2D>().velocity*multiplier);
        }
    }

}
