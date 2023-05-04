using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Status>().takeDamage(5,1);
        }
        if(collider.gameObject.CompareTag("tile"))
        {
            collider.gameObject.GetComponent<TileStats>().DestroyTile();
        }
        
        
    }
}
