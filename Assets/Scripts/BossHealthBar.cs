using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient gradient;
    public Image fill;

    void Awake()
    {        
        Boss.BossEntered.AddListener(() => { gameObject.SetActive(true); });
        MasterScript.onGameOVer.AddListener(() => { gameObject.SetActive(false); });
        gameObject.SetActive(false);
    }

    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;      
        fill.color = gradient.Evaluate(1f);
    }    

    public void SetHealth(int health)
    {
        healthBar.value = health;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);

        //disable bar when health below zero
        if (health <= 0)
            gameObject.SetActive(false);
       
    }

}
