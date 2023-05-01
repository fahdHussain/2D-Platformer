using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLaserScript : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("PickUpLaser");
    }
    public void selfDestruct()
    {
        Destroy(gameObject);
    }
}
