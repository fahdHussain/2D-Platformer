using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeScript : MonoBehaviour
{
    public GameObject effect;
    public Animator animator;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            if(collider.attachedRigidbody.velocity.y < -0.25)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                //Vector2 bPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
                //Instantiate(effect, bPos, transform.rotation);
                player.GetComponent<Status>().takeDamage(5,1);
                animator.SetTrigger("bloody");
            }
        }
    }
    
}
