using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour                 
{
    public GameObject button1P;
    public GameObject button2P;

    void Awake()
    {
        //Add new event for yeach mode button bassed on mode
        button1P.GetComponent<Button>().onClick.AddListener(() => { Play(1); });
        button2P.GetComponent<Button>().onClick.AddListener(() => { Play(3); });
    }

    private void Play(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    }

    /// <summary>
    /// Enables or disables choose mode buttons
    /// </summary>
    public void PLayButton()                              
    {
        if (!button1P.activeSelf)
        {
            button1P.SetActive(true);
            button2P.SetActive(true);
        }
        else
        {
            button1P.SetActive(false);
            button2P.SetActive(false);
        }       

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
