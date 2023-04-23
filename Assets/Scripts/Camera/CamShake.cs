using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public Animator animator;
    
    public void shakeCam()
    {
        int rand = Random.Range(0,5);
        if(rand == 0)
        {
            animator.SetTrigger("shake_trigger");
        }
        if(rand == 1)
        {
            animator.SetTrigger("shake_trigger1");
        }
        if(rand == 2)
        {
            animator.SetTrigger("shake_trigger2");
        }
        if(rand == 3)
        {
            animator.SetTrigger("shake_trigger3");
        }
        if(rand == 4)
        {
            animator.SetTrigger("shake_trigger4");
        }
    }
}
