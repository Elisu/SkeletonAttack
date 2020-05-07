using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public int damage = 50;
    public GameObject destroyEffect;

    public void DamageEnemy(int damage)
    {
        Debug.Log("zasah");

        health -= damage;
        if (health <= 0)                        
        {
                        //odstraneni nepritele

            if ( damage != 99999)                       // aby davalo body jen kdyzz je opravdu trefi hrac
            {
                MasterScript.KillEnemy(gameObject, true);   // pricte skore
            }
            else
                MasterScript.KillEnemy(gameObject, false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)                  //detekce kolizi
    {
        Player player = collision.collider.GetComponent<Player>();   

        
        if ( player != null)    // pokud jsme zasahli hrace
        {
            player.DamagePlayer(damage);
            DamageEnemy(99999);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }
}
