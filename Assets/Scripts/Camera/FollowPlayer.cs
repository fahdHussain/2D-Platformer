using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private bool foundPlayer = false;
    private GameObject player;
    public Transform target;
    public float smoothing = 5f;
    private Vector3 offSet = new Vector3(0,0,10);
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(foundPlayer == false)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                target = player.transform;
                
                foundPlayer = true;
            }
        }
        else
        {
            Vector3 targetCamPosition = target.position - offSet;
            transform.position = Vector3.Lerp(transform.position, targetCamPosition, smoothing * Time.deltaTime);
            
        }
    }
}
