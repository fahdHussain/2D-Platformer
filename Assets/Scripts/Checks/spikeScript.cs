using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            if(collider.attachedRigidbody.velocity.y < 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Status>().takeDamage(5);
            }
        }
    }
    
}
