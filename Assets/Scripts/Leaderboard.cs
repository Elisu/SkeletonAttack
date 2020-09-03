using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text[] scores;
    public Text[] names;

    public bool mode2P = false;
    public ChangeButtonText tx;

    private const string prefs = "HighScore";
    private const string prefs2P = "2PHighScore";

    private void Start()    
    {
        Reload();
    }

    /// <summary>
    /// Reloads the leaderboard
    /// </summary>
    public void Reload()
    {
        string prefsName;

        if (mode2P)
            prefsName = prefs2P;
        else
            prefsName = prefs;

        // Prints leaderboard data from PlayerPrefs
        for (int i = 0; i <= 4; i++)
        {
            scores[i].text = PlayerPrefs.GetInt(prefsName, 0).ToString();
            names[i].text = PlayerPrefs.GetString("name" + prefsName, "Empty");
            Debug.Log(prefsName);
            prefsName += "1";
        }

        tx.ChangeText(mode2P);
    }
}
