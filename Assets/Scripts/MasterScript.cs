using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterScript : MonoBehaviour
{
    public static MasterScript ms;                  

    public int killPoints;                                      //body za setřelení
    public Score scoreC;                                        //skóre
    private int highScore;                      
    public Text highScoreText;                                  //text kolonky nejlepšího skóre
    public Text yourScore;                                      //text kolonky skóre

    public GameObject GameOverScreen;                           //objekt obrazovky konce hry
    public GameObject ScoreBar;                                 //ukazatel skóre

    private void Awake()                
    {
        if (ms == null)
        {
            ms = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterScript>();       //dosazení objektu s tagem "Master"

        }
    }

    private void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();  //dosazení textu nej skóre
    }


    public static void KillPlayer (GameObject player)    // zničení hráče a spustění konce hry
    {
        Destroy(player);
        ms.GameOver();
    }

    public static void KillEnemy (GameObject enemy, bool AddScore)   //zničení nepřítele a zvýšení skóre
    {
       
        Destroy(enemy);

        if (AddScore == true)                                        //kontrola, zda opravdu sestřelil hráč
            ms.ScoreAdder();
        
    }

    public void ScoreAdder()                        //přičte skóre
    {
        scoreC.ScoreCount(killPoints);
    }

    public void GameOver()                      // Game Over obrazovka
    {
        GameOverScreen.SetActive(true);                     //zapnutí GameOver obrazovky
        ScoreBar.SetActive(false);                          // vypnutí zobrazní skóre v pravém horním rohu


        int score = scoreC.ReturnScore();                   //získání skóre
        yourScore.text = "Your score: " + score;             //zobrazení skóre
        
        if (score > PlayerPrefs.GetInt("HighScore1111", 0))         //kontrola zda se zlepšilo nějaké uložené nej skóre
        {
            OrganizeScores(score, "HighScore1111");            
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
            


        Debug.Log("GAME OVER");
    }

    public static void OrganizeScores(int score, string name)       //zatřídí nové skóre
    {
        int[] values = new int [6];        
        values[0] = score;

        for ( int i = 0; i <= 4; i++)        {

            
            values[i + 1] = PlayerPrefs.GetInt(name.Substring(0, 13 - i), 0);     // názvy jsou HighScore, HighScore1, HighScore11... - bereme substringy 
            Debug.Log(name.Substring(0, 13 - i));


        }

        for (int i = 1; i <= 5; i++)                //porovnávání dvojic 
        {
            if (values[i - 1] > values[i])
            {
                int help = values[i - 1];
                values[i - 1] = values[i];
                values[i] = help;
            }
        }

        for (int i = 0; i <= 4; i++)
        {
            PlayerPrefs.SetInt(name.Substring(0, 13 - i), values[i+1]);         //uložení 5 nej zpět do PlayerPrefs, aby si program pamatoval i v budoucnu
        }
    }

    
}
