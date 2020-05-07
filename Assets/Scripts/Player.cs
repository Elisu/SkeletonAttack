using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int health = 150;         
    public int maxHealth;
    public int depth = -100;                 //minimální hodnota y souřadnice postavy

    
    public RectTransform[] hearts;        //pole srdíček značících životy

    private int hitNumber = 0;
    
    

    // Update is called once per frame
    void Update()
    {
        if ( transform.position.y <= depth)     //pokud je postava níže, je mimo herní pole tedy umřela
        {
            
            for (int i = hitNumber; i <= 2; i++)
            {
                Destroy(hearts[i].gameObject);          //odebere všechna zbylá srdíčka představující životy
            }
               
            MasterScript.KillPlayer(gameObject);
        }
    }

    public void DamagePlayer ( int damage )
    {
       
        health -= damage;                               //sníží život
        if ( health <= 0)                               //kontrola, zda hráč umřel
        {
            MasterScript.KillPlayer(gameObject);
            
            Destroy(hearts[hitNumber].gameObject);
            
        }
        else
        {
            
            Destroy(hearts[hitNumber].gameObject);

        }

        hitNumber += 1;                 //připočte záasah, který je index srdíček
    }
}
