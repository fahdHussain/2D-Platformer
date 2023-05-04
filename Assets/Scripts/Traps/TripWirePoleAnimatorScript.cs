using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripWirePoleAnimatorScript : MonoBehaviour
{
    private Animator animiator;
    private bool triggered;
    void Start()
    {
        animiator = GetComponent<Animator>();
    }
    void Update()
    {
        if(!triggered)
        {
            if(checkIfTriggered())
            {
                 animiator.Play("Tripwire_Pole_Triggered");
            }
        }
    }
    private bool checkIfTriggered()
    {
        if(transform.parent.GetComponentInChildren<TripWireTriggerScript>().triggered == true)
        {
            triggered = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
