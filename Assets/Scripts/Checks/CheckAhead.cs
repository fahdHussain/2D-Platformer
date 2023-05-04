using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAhead : MonoBehaviour
{
    private bool groundAhead = false;
    [SerializeField, Range(0,10)] private float xRange = 0.5f;
    [SerializeField, Range(-10,10)] private float yRange = -0.5f;

    private float originalXRange;
    private float originalYRange;

    void Start()
    {
        originalXRange = xRange;
        originalYRange = yRange;
    }

    void Update()
    {
        checkAhead();
    }
    private void checkAhead()
    {
        Vector2 rayStart = new Vector2(transform.position.x + xRange *transform.localScale.x, transform.position.y - yRange);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.25f);
        Debug.DrawRay(rayStart, Vector2.down, Color.red, 0.25f);

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
    public void setRange(float x, float y)
    {
        xRange = x;
        yRange = y;
    }
    public void reset()
    {
        xRange = originalXRange;
        yRange = originalYRange;
    }

}
