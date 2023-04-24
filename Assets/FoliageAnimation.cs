using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageAnimation : MonoBehaviour
{

    public Animator animator;
    void Start()
    {
        animator.SetTrigger("move");
    }

    
}
