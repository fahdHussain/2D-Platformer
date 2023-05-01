using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeaponUIScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void basicSelect()
    {
        animator.Play("basic_select");
    }
    public void basicDeselect()
    {
        animator.Play("basic_deselect");
    }
}
