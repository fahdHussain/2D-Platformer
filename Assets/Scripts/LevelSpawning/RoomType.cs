using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    public GameObject playerSpawn;
    public GameObject exitSpawn;
    public GameObject player;
    public GameObject exit;
    
    public void destroyRoom()
    {
        Destroy(gameObject);
    }
    public void spawnPlayer()
    {
        Instantiate(player, playerSpawn.GetComponent<Transform>().position, playerSpawn.GetComponent<Transform>().rotation);
    }
    public void spawnExit()
    {
        Instantiate(exit, exitSpawn.GetComponent<Transform>().position, exitSpawn.GetComponent<Transform>().rotation);
    }
}
