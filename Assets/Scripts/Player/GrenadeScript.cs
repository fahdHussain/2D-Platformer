using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public GameObject explosion;
    public float timer = 5;
    public Animator animator;
    
    public void startTimer()
    {
        StartCoroutine(explode());
    }
    IEnumerator explode()
    {
        animator.Play("Grenade_Pulled");
        yield return new WaitForSeconds(timer);
        Instantiate(explosion, transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
