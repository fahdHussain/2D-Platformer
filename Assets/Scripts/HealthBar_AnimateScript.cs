using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_AnimateScript : MonoBehaviour
{
    public Animator animator;
    
    public void triggerAnim()
    {
        animator.SetTrigger("tookDamage");
    }
}
