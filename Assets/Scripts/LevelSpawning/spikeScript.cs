using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeScript : MonoBehaviour
{
    public int aHit = 0;
    Vector2 pointPos;
    RaycastHit2D hit;
    void Start()
    {
        //pointPos = new Vector2(transform.position.x, transform.position.y);
        hit = Physics2D.Linecast(transform.position, pointPos);
    }
    void FixedUpdate()
    {
        pointPos = new Vector2(transform.position.x - 0.55f, transform.position.y + 0.25f);
        Debug.DrawRay(pointPos, transform.TransformDirection(Vector2.right));
        hit = Physics2D.Raycast(pointPos, transform.TransformDirection(Vector2.right));
        
        if(hit.transform.CompareTag("Player")){
            aHit += 1;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hit = collision.gameObject;

        if(hit.CompareTag("Player"))
        {
            foreach(ContactPoint2D hitPos in collision.contacts)
            {
                Debug.Log(hitPos.normal);
                if(hitPos.normal.y == -1)
                {
                    aHit += 1;
                }
            }
            
        }
    }
}
