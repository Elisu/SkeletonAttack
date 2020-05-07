using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour                    //tlačítka v menu
{

    public void PLayButton()                                //spustí hru
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitButton()                            //ukončí aplikaci
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void LeaderBoardButton()                                //spustí hru
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }
}
