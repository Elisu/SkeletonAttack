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

    public virtual void DamageEnemy(int damage)
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

    /// <summary>
    /// Looks for player
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Searching()
    {
        GameObject pl = null;
        //In case of more players
        Player[] players = FindObjectsOfType<Player>();
        if (players.Length > 0)
            pl = players[Random.Range(0, players.Length)].gameObject;

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
            if (gameObject.GetComponent<EnemyAI>() != null)
            {
                DamageEnemy(99999);
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }            
        }
    }
}
