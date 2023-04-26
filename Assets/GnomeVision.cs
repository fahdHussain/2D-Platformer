using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeVision : MonoBehaviour
{
   private Vector2 rayDirection;
   public GameObject target;

   void OnTriggerStay2D(Collider2D collider)
   {
    if(collider.gameObject.CompareTag("Player"))
    {
        rayDirection = new Vector2(collider.transform.position.x - transform.position.x,collider.transform.position.y -transform.position.y);
        LayerMask player = LayerMask.GetMask("Player");
        Vector2 startPos = new Vector2(transform.position.x + 0.6f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(startPos, rayDirection, player);
        Debug.DrawRay(startPos, rayDirection, Color.red);
        
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
            Debug.Log(hit.collider.transform.position);
            //Attack
            if(hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Player Hit");
            }
        }
    }
   }
}
