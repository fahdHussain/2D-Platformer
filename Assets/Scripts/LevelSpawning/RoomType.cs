using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    public GameObject playerSpawn;
    public GameObject exitSpawn;
    public GameObject keySpawn;
    public GameObject carrotSpawn;
    public GameObject gnomeSpawn;
    public GameObject beeSpawn;
    public GameObject player;
    public GameObject exit;
    public GameObject key;
    public GameObject carrot;
    public GameObject gnome;
    public GameObject bee;

    void Start()
    {
        spawnCarrot();
        spawnKey();
        spawnBees();
        spawnGnome();
    }
    
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
        GameObject instance = Instantiate(exit, exitSpawn.GetComponent<Transform>().position, exitSpawn.GetComponent<Transform>().rotation);
        instance.transform.parent = transform;
    }
    public void spawnKey()
    {
        if(keySpawn != null)
        {
            GameObject instance = Instantiate(key, keySpawn.GetComponent<Transform>().position, keySpawn.GetComponent<Transform>().rotation);
            instance.transform.parent = transform;
        }
        
    }
    public void spawnCarrot()
    {
        if(carrotSpawn != null)
        {
            GameObject instance = Instantiate(carrot, carrotSpawn.GetComponent<Transform>().position, carrotSpawn.GetComponent<Transform>().rotation);
            instance.transform.parent = transform;
        }
    }
    public void spawnGnome()
    {
        if(gnomeSpawn != null)
        {
            GameObject instance = Instantiate(gnome, gnomeSpawn.GetComponent<Transform>().position, gnomeSpawn.GetComponent<Transform>().rotation);
            instance.transform.parent = transform;
        }
    }
    public void spawnBees()
    {
        if(beeSpawn != null)
        {
            GameObject instance = Instantiate(bee, beeSpawn.GetComponent<Transform>().position, beeSpawn.GetComponent<Transform>().rotation);
            instance.transform.parent = transform;
        }
    }
}
