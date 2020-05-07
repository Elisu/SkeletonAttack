using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    public InputField playerName;

    public void Quit ()                                          // ukonci hru
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void Restart ()                           // spusti novou hru
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
    }

    public void LeaderBoard()                        // nacte tabulku nejlepsich vysledku
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()                        //vrati do menu
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    
}
