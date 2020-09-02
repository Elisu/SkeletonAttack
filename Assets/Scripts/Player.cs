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
    public Weapon normalWep;
    public RocketLauncher rocketLauncher;

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
            
            hearts[hitNumber].gameObject.SetActive(false);
            
        }
        else
        {

            hearts[hitNumber].gameObject.SetActive(false);

        }

        hitNumber += 1;                 
    }   

    public void TakeHeart(Collider2D collider)
    {
        if (collider.tag == "Life" && health < maxHealth)
        {
            Destroy(collider.gameObject);
            hitNumber--;
            hearts[hitNumber].gameObject.SetActive(true);
            health += maxHealth / hearts.Length;
        }
            
    }

    public void TakeWeapon(Collider2D collider)
    {
        Destroy(collider.gameObject);
        StartCoroutine(activateWeapon());
    }

    IEnumerator activateWeapon()
    {
        normalWep.gameObject.SetActive(false);
        rocketLauncher.gameObject.SetActive(true);
        yield return new WaitForSeconds(rocketLauncher.duration);
        rocketLauncher.gameObject.SetActive(false);
        normalWep.gameObject.SetActive(true);
        yield break;
    }
}
