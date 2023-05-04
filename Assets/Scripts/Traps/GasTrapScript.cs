using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTrapScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tripwirePole;
    public GameObject tripwire;
    public GameObject gas;
    public Animator animator;
    private bool tripwireSpawned = false;
    private float maxTripwireDistance = 3;
    private BoxCollider2D wireCollider;
    
    
    void Update()
    {
        if(!tripwireSpawned)
        {
            SpawnTripwire();
        }
    }
    private void SpawnTripwire()
    {
        bool leftPoleSpawn = false;
        float leftPoleDistance = -maxTripwireDistance;

        bool rightPoleSpawn = false;
        float rightPoleDistance = maxTripwireDistance;
        while(!leftPoleSpawn || !rightPoleSpawn)
        {
            if(!leftPoleSpawn)
            {
                if(checkPoleSpawn(leftPoleDistance, -0.5f))
                {
                    GameObject leftPole = Instantiate(tripwirePole, new Vector2(transform.position.x + leftPoleDistance, transform.position.y -0.25f), transform.rotation);
                    leftPole.transform.parent = transform;
                    leftPoleSpawn = true;

                }
                else
                {
                    leftPoleDistance += 0.5f;
                }
            }
            if(!rightPoleSpawn)
            {
                if(checkPoleSpawn(rightPoleDistance, -0.5f))
                {
                    GameObject rightPole = Instantiate(tripwirePole, new Vector2(transform.position.x + rightPoleDistance, transform.position.y -0.25f), transform.rotation);
                    rightPole.transform.parent = transform;
                    rightPoleSpawn = true;
                }
                else
                {
                    rightPoleDistance -= 0.5f;
                }
            }
        }
        float wireLength = rightPoleDistance - leftPoleDistance;
        float wireCenter = (wireLength/2) + leftPoleDistance;

        GameObject wire = Instantiate(tripwire, new Vector2(transform.position.x + wireCenter,transform.position.y -0.13f), transform.rotation); 
        wire.transform.localScale  = new Vector3(wireLength, 1,1);
        wire.transform.parent = transform;
        tripwireSpawned = true;
    }

    private bool checkPoleSpawn(float xRange, float yRange)
    {
        Vector2 rayStart = new Vector2(transform.position.x + xRange, transform.position.y + yRange);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.25f);
        //Debug.DrawRay(rayStart, Vector2.down, Color.red, 0.25f);

        if(hit.collider == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void triggerTrap()
    {
        animator.Play("Gas_Trap_Triggered");
        Instantiate(gas, transform.position, transform.rotation);
    }
    
}
