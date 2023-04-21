using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public int health;
    public Text healthText;
    public int score = 0;
    public Text scoreText;
    public Text gameOver;
    GameObject player;
    
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //health = player.GetComponent<Status>().health;
        gameOver.enabled = false;
    }
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {  
            if(player.GetComponent<Status>().isAlive == true)
            {
                health = player.GetComponent<Status>().health;

                healthText.text = "HEALTH: " + health.ToString();
                scoreText.text = "Score: "+ score.ToString();
            }
            else
            {
                GameOver();
            }
        }
        
        
    }

    void GameOver()
    {
        Destroy(healthText);
        Destroy(scoreText);

        gameOver.enabled = true;
        gameOver.text = "GAME OVER";
        
    }
}
