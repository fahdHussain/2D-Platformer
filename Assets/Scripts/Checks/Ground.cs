using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private float friction;
    public bool onSpike = false;
    private new BoxCollider2D collider2D;
    
    [SerializeField] LayerMask layer;

    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        friction = 0;
    }

    private void EvaluateCollision(Collision2D collision)
    {
       
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }

    private void RetrieveFriction(Collision2D colliision)
    {
        PhysicsMaterial2D material = colliision.rigidbody.sharedMaterial;

        friction = 0;
        if(material != null)
        {
            friction = material.friction;
        }
    }

    public bool rayGroundCheck()
    {

        return Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0f, Vector2.down, 0.1f, layer);



        // Vector2 rayStart = new Vector2(transform.position.x, transform.position.y - 0.6f);
        // LayerMask tiles = LayerMask.GetMask("tiles");
        // RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.25f);
        // Vector2 rayEnd = new Vector2(transform.position.x, transform.position.y - 0.7f);
        
       
        // if(hit.collider != null)
        // {
        //     Debug.DrawRay(rayStart, Vector2.down, Color.green);
        //     return true;
        // }else
        // {
        //     Debug.DrawRay(rayStart, Vector2.down, Color.red);
        //     return false;
        // }
    }

    public bool getOnGround()
    {
        return onGround;
    }

    public float getFriction()
    {
        return friction;
    }
}
