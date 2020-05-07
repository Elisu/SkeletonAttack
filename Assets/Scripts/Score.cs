using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    private int currentScore = 0;

    public void ScoreCount(int scoreAdd)
    {
        currentScore += scoreAdd;
        score.text = "Score: " + currentScore;
    }

    public int ReturnScore()
    {
        return currentScore;
    }
}
