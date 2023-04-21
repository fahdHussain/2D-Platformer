using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    void Update()
    {
        scoreText.text = "Score: "+ score.ToString();
    }
}
