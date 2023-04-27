using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfExplode : MonoBehaviour
{
    public GameObject particleEffect;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
