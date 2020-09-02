using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public int damage = 50;
    public Transform player;
    public GameObject destroyEffect;

    protected bool lookingORnot = false;

    public void DamageEnemy(int damage)
    {
        Debug.Log("HIT");
        health -= damage;

        if (health <= 0)                        
        {
            // To differentiate between enemy killed by bullet and killed by collision with player
            if ( damage != 99999)                      
            {
                // Killed by bullet - player gets kill points
                MasterScript.KillEnemy(gameObject, true);   
            }
            else
                MasterScript.KillEnemy(gameObject, false);
        }
    }

    protected IEnumerator Searching()
    {
        //looking for player
        GameObject pl = GameObject.FindGameObjectWithTag("Player");

        if (pl == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Searching());
        }
        else
        {
            lookingORnot = false;
            player = pl.transform;
            yield break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)                  
    {
        Player player = collision.collider.GetComponent<Player>();  
        
        if ( player != null)    
        {
            player.DamagePlayer(damage);
            // Enemy collides with player = instant death of enemy and no kill points for player
            DamageEnemy(99999);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }
}
