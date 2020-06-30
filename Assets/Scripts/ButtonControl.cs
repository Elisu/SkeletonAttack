using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    /// <summary>
    /// Loads main scene
    /// </summary>
    public void PLayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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


}
