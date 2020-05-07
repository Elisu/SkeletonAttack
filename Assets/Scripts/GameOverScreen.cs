using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{    

    /// <summary>
    /// Ends the game
    /// </summary>    
    public void Quit ()                                        
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    /// <summary>
    /// Starts new round
    /// </summary>    
    public void Restart ()                         
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads the leaderboard screen
    /// </summary>
    public void LeaderBoard()                        
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Back to main menu
    /// </summary>
    public void BackToMenu()                        
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    
}
