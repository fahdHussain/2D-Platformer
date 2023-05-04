using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripWireTriggerScript : MonoBehaviour
{
    public bool triggered = false;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void TriggerInParent()
    {
        GetComponentInParent<GasTrapScript>().triggerTrap();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            animator.Play("Tripwire_Triggered");
            TriggerInParent();
            triggered = true;
        }
    }
}
