using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public int health;
    public int score = 0;
    public Text scoreText;
    public Text gameOver;
    public Text keyText;
    public Text levelComplete;
    public Text needKey;
    float timerVal = 5;

    string gameOverText = "We'll\nbe\nright\nback ";

    public GameObject[] healthChunks;
    private Color gameOverColor = Color.white;
    private FontStyle gameOverFontStyle = FontStyle.Bold;
    private TextAnchor gameOverAlignment = TextAnchor.UpperLeft;

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
            healthFiller();
            health = player.GetComponent<Status>().health;
            if(player.GetComponent<Status>().isAlive == true)
            { 
                score = player.GetComponent<Status>().score;
                
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
        Destroy(scoreText);
        Destroy(keyText);

        gameOver.enabled = true;
        gameOver.color = gameOverColor;
        gameOver.fontStyle = gameOverFontStyle;
        gameOver.alignment = gameOverAlignment;
        gameOver.text = gameOverText;
        
    }
    public void resetGameOverText()
    {
        
        gameOverAlignment = TextAnchor.MiddleCenter;
        gameOverFontStyle = FontStyle.BoldAndItalic;
        
        gameOverColor = Color.red;
        gameOverText = "Game Over";
        //Debug.Log(gameOver.text);

    }

    void healthFiller()
    {
        for(int i = 0; i < healthChunks.Length; i++)
        {
            if(!displayHealth(i))
            {
                healthChunks[i].GetComponent<HealthBar_AnimateScript>().triggerAnim();
            }
        }
    }

    private bool displayHealth(int dipslayPoint)
    {
        if(health <= dipslayPoint|| health == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    void LevelComplete()
    {
        
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
