using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform[] startingPositions;
    public LevelGeneration lvlG;
    public LayerMask roomMask;
    void Start()
    {
        transform.position = startingPositions[lvlG.startingPos].position;

        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position,1, roomMask);
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
