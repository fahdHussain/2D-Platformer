using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAhead : MonoBehaviour
{
    private bool groundAhead = false;

    void Update()
    {
        checkAhead();
    }
    private void checkAhead()
    {
        Vector2 rayStart = new Vector2(transform.position.x + 0.5f *transform.localScale.x, transform.position.y - 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.25f);
        //Debug.DrawRay(rayStart, Vector2.down, Color.red, 0.25f);

        if(hit.collider == null)
        {
            setGroundAhead(false);
        }
        else
        {
            setGroundAhead(true);
        }
    }
    private void setGroundAhead(bool state)
    {
        groundAhead = state;
    }
    public bool getGroundAhead()
    {
        return groundAhead;
    }

}
