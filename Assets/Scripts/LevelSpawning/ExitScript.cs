using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public bool reachedExit = false;

    void OnTriggerEnter2D(Collider2D collider)
    //Checks if player has key
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            collider.GetComponent<Status>().atExit();
            reachedExit = true;

            if(collider.gameObject.GetComponent<Status>().hasKey)
            {
                collider.gameObject.GetComponent<Status>().validExit();
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    //Check if player leaves exit
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            reachedExit = false;
            collider.GetComponent<Status>().leftExit();
        }
    }

}
