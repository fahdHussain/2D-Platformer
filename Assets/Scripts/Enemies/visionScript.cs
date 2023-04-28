using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionScript : MonoBehaviour
{
   private Vector2 rayDirection;
   public GameObject target;
   public EnemyController controller;
   public bool inLineOfSight = false;

   void OnTriggerStay2D(Collider2D collider)
   {
    if(collider.gameObject.CompareTag("Player"))
    {
        rayDirection = new Vector2(collider.transform.position.x - transform.position.x,collider.transform.position.y -transform.position.y);
        Vector2 startPos = new Vector2(transform.position.x + 0.6f * transform.parent.localScale.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(startPos, rayDirection);
        //Debug.DrawRay(startPos, rayDirection, Color.red);
        
        if(hit.collider != null)
        {
            
            if(hit.collider.gameObject.tag == "Player")
            {
                inLineOfSight = true;
                target = hit.collider.gameObject;
                //Debug.Log("TEST2");
                controller.SetAggro(true);
            }
            else
            {
                inLineOfSight = false;
            }
        }
    }
   }

   public GameObject getTarget()
   {
    return target;
   }

   public bool getSight()
   {
    return inLineOfSight;
   }
}
