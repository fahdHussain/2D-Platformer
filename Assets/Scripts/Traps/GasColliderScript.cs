using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Status>().takeDamage(2,0);
            GetComponent<SelfDestruct>().enabled = true;
        }
        
    }

}
