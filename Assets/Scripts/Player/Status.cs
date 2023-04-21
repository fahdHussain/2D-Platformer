using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int health = 5;
    public int score = 0;
    public bool isAlive = true;

    public void takeDamage(int damage)
    {
        if(damage >= health)
        {
            health = 0;
            isAlive = false;
        } 
        else
        {
            health -= damage;
        }
    }
}
