using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour                 
{

    public void PLayButton()                              
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitButton()                          
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void LeaderBoardButton()                         
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }
}
