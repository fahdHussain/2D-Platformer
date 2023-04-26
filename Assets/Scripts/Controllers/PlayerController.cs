using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public int RetrieveAttackInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Left Click");
            return 0;
        }
        if(Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Right Click");
            return 1;
        }
        if(Input.GetMouseButtonDown(2))
        {
            //Debug.Log("Middle Click");
            return 2;
        }
        else
        {
            return -1;
        }
    }
}
