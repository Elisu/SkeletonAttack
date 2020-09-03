using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{    
    Leaderboard lead;
    public bool modeForTwo;

    private void Start()
    {
        lead = GetComponent<Leaderboard>();
        modeForTwo = lead.mode2P;

    }
    /// <summary>
    /// Loads gameplay based on mode
    /// </summary>
    public void PLayButton()
    {
        if (!modeForTwo)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    /// <summary>
    /// Loads main menu scene
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    /// <summary>
    /// Changes mode in leaderboard to show highscores for selected mode
    /// </summary>
    public void ChangeScores()
    {
        if (lead.mode2P)
            lead.mode2P = false;
        else
            lead.mode2P = true;

        //Reloads leaderboard
        lead.Reload();
    }
}
