using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int health = 5;
    public int score = 0;
    public bool isAlive = true;
    public bool hasKey = false;
    public bool reachedExit = false;
    public bool valExit = false;

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

    public void gotKey()
    {
        hasKey = true;
    }

    public void plusScore(int itemScore)
    {
        score += itemScore;
    }

    public void atExit()
    {
        reachedExit = true;
    }

    public void leftExit()
    {
        reachedExit = false;
    }

    public void validExit()
    {
        valExit = true;
    }
}
