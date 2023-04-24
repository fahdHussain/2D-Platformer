using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public GameObject player;
    public float velocity = 3;
    private bool aggro = false;
    private Vector2 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aggro)
        {
            Transform target = transform.parent.GetComponentInChildren<visionScript>().target.transform;
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, velocity*Time.deltaTime);
        }
    }

    public void setAggro(bool aggroBool)
    {
        aggro = aggroBool;
    }
}
