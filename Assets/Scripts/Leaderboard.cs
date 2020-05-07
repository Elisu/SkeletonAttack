using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text[] scores;
    public Text[] names;

    private string prefsName = "HighScore";

    private void Start()    
    {
        for ( int i = 0; i <= 4; i++)
        {
            scores[i].text = PlayerPrefs.GetInt(prefsName, 0).ToString();
            names[i].text = PlayerPrefs.GetString("name"+prefsName, "Empty");
            Debug.Log(prefsName);
            prefsName += "1";
        }
    }
}
