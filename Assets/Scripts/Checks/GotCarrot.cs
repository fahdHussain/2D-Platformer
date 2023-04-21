using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotCarrot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Status>().plusScore(10);
            Destroy(gameObject);
        }
    }
}
