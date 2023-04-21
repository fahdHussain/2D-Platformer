using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotKey : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Status>().gotKey();
            Destroy(gameObject);
        }
    }
}
