using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BeeVisionScript : MonoBehaviour
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
            if(hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag == "Player")
                {
                    setTarget(ChasePlayer.destination.player, hit.collider.transform);
                }
            }
            
            
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Bees"))
        {
            setTarget(ChasePlayer.destination.home, transform);
        }
    }

    public void setTarget(ChasePlayer.destination destination, Transform target)
    {
        if(destination == ChasePlayer.destination.player)
        {
            transform.parent.gameObject.GetComponentInChildren<ChasePlayer>().returnHome(false);
            transform.parent.gameObject.GetComponentInChildren<ChasePlayer>().setTargetPos(target);
        }
        if(destination == ChasePlayer.destination.home)
        {
            transform.parent.gameObject.GetComponentInChildren<ChasePlayer>().returnHome(true);
            transform.parent.gameObject.GetComponentInChildren<ChasePlayer>().setTargetPos(target);
        }

        
    }

}
