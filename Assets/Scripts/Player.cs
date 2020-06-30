using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int health = 150;         
    public int maxHealth;

    //y lower bound for player
    public int depth = -100;                 

    //lives
    public RectTransform[] hearts;       

    private int hitNumber = 0;
    
    

    // Update is called once per frame
    void Update()
    {
        //player below lower bound = death
        if ( transform.position.y <= depth)     
        {
            
            for (int i = hitNumber; i <= 2; i++)
            {
                Destroy(hearts[i].gameObject);         
            }
               
            MasterScript.KillPlayer(gameObject);
        }
    }

    public void DamagePlayer ( int damage )
    {
       
        health -= damage;                               
        if ( health <= 0)                               
        {
            MasterScript.KillPlayer(gameObject);
            
            Destroy(hearts[hitNumber].gameObject);
            
        }
        else
        {
            
            Destroy(hearts[hitNumber].gameObject);

        }

        hitNumber += 1;                 
    }
}
