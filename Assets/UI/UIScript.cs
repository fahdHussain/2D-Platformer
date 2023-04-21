using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public int health;
    public int score = 0;
    public Text healthText;
    public Text scoreText;
    public Text gameOver;
    public Text keyText;
    public Text levelComplete;
    public Text needKey;
    float timerVal = 5;

    GameObject player;
    

    
    
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //health = player.GetComponent<Status>().health;
        gameOver.enabled = false;
        needKey.enabled = false;
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
                score = player.GetComponent<Status>().score;

                healthText.text = "HEALTH: " + health.ToString();
                scoreText.text = "Score: "+ score.ToString();

                if(player.GetComponent<Status>().hasKey == true)
                {
                    keyText.text = "Got Key";
                    needKey.enabled = false;
                }
                if(player.GetComponent<Status>().reachedExit && !player.GetComponent<Status>().hasKey)
                {
                    StartCoroutine(NeedKeytimer());
                    
                }
                if(player.GetComponent<Status>().valExit && player.GetComponent<Status>().hasKey)
                {
                    LevelComplete();
                }
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
        Destroy(keyText);

        gameOver.enabled = true;
        gameOver.text = "GAME OVER";
        
    }


    void LevelComplete()
    {
        Destroy(healthText);
        Destroy(scoreText);
        Destroy(keyText);

        levelComplete.text = "Level Complete!\nScore: "+ score.ToString();
    }

    private IEnumerator NeedKeytimer(float time = 5)
    {
        timerVal = time;
        while(timerVal > 0)
        {
            needKey.enabled = true;
            needKey.text = "Need Key!";

            yield return new WaitForSeconds(3.0f);
            timerVal--;
        }
        needKey.enabled = false;
    }
}
