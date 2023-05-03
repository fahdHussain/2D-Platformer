using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapScript : MonoBehaviour
{
    private bool triggered = false;
    public Animator animator;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!triggered)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<Status>().takeDamage(3,1);
                triggered = true;
                animator.Play("Bear_Trap_Triggered");
            }
        }
    }
}
