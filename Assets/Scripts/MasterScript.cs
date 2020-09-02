using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterScript : MonoBehaviour
{
    public static MasterScript ms;                  

    public int killPoints;                                      
    public Score scoreC;                                       
    private int highScore;                      
    public Text highScoreText;                                  
    public Text yourScore;                                     

    public GameObject GameOverScreen;                       
    public GameObject ScoreBar;
    public GameObject NameInput;

    public InputField playerNameInput;
    public Button saveButton;

    private void Awake()                
    {
        if (ms == null)
        {
            ms = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterScript>();     

        }
    }

    private void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();  
    }

    private void Update()
    {
        if (playerNameInput.text == "")
            saveButton.interactable = false;
        else
            saveButton.interactable = true;
    }


    public static void KillPlayer (GameObject player)   
    {
        Destroy(player);
        ms.GameOver();
    }

    public static void KillEnemy (GameObject enemy, bool AddScore)  
    {
        int bonus = 0;
        if (enemy.tag == "Boss")
            bonus += 200;

        Destroy(enemy);

        if (AddScore == true)                                      
            ms.ScoreAdder(bonus);        
    }

    public void ScoreAdder(int bonus = 0)                      
    {
        scoreC.ScoreCount(killPoints + bonus);
    }

    public void GameOver()                     
    {
         int score = scoreC.ReturnScore();                  
        yourScore.text = "Your score: " + score;
        
        if (score > PlayerPrefs.GetInt("HighScore1111", 0))         
        {
            NameInput.SetActive(true);
        }
        else
            Skip();


        Debug.Log("GAME OVER");
    }

    public void SaveName()
    {
        int score = scoreC.ReturnScore();
        OrganizeScores(score, "HighScore1111", playerNameInput.text);       

        Skip();

    }

    public void Skip()
    {
        NameInput.SetActive(false);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        GameOverScreen.SetActive(true);                   
        ScoreBar.SetActive(false);                        
    }

    public static void OrganizeScores(int score, string name, string playerName)  
    {
        int[] values = new int [6];
        string[] pNames = new string[6];
        pNames[0] = playerName;
        values[0] = score;

        for ( int i = 0; i <= 4; i++)
        {
            //names are HighScore, HighScore1, HighScore11... - takes substrings
            values[i + 1] = PlayerPrefs.GetInt(name.Substring(0, 13 - i), 0);     
            pNames[i + 1] = PlayerPrefs.GetString("name"+name.Substring(0, 13 - i), "Empty");
            Debug.Log(name.Substring(0, 13 - i));
        }

        //sort
        for (int i = 1; i <= 5; i++)                
        {
            if (values[i - 1] > values[i])
            {
                int tmp = values[i - 1];
                string tmp2 = pNames[i - 1];
                values[i - 1] = values[i];
                pNames[i - 1] = pNames[i];
                pNames[i] = tmp2;
                values[i] = tmp;
            }
        }

        for (int i = 0; i <= 4; i++)
        {            
            //saves 5 best scores
            PlayerPrefs.SetInt(name.Substring(0, 13 - i), values[i+1]);         
            PlayerPrefs.SetString("name"+name.Substring(0, 13 - i), pNames[i + 1]);
        }
    }

    
}
