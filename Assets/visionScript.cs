using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class visionScript : MonoBehaviour
{
    private Vector2 rayDirection;
    public GameObject target;
    
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            
            rayDirection = new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y);
            
            LayerMask tiles = LayerMask.GetMask("tiles");

            Vector2 startPos = new Vector2(transform.position.x, transform.position.y - 0.6f);
            RaycastHit2D hit = Physics2D.Raycast(startPos, rayDirection, tiles);
            //Debug.DrawRay(startPos, rayDirection, Color.red);

            if(hit.collider.gameObject.tag == "Player")
            {
                setTarget(hit.collider.gameObject);
                transform.parent.gameObject.GetComponentInChildren<ChasePlayer>().setAggro(true);
            }
        }
    }

    public void setTarget(GameObject player)
    {
        target = player;
    }

}
