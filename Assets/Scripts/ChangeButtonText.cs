using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonText : MonoBehaviour
{
    public string texts = "2 Players";
    public string text2P = "Single";
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();   
    }

    /// <summary>
    /// Changes text based on mode
    /// </summary>
    /// <param name="mode2P">mode single or multiplayer</param>
    public void ChangeText(bool mode2P)
    {
        if (mode2P)
            text.text = text2P;
        else
            text.text = texts;

    }
}
