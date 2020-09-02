using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int damage = 50;
    public int health = 50;
    public int speed = 50;
    public int yBound = -100;
    public bool shootable = true;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Debug.Log("Fireball destroyed");

        if (collision.collider.tag == "Player")
        {
            Player player = collision.collider.GetComponent<Player>();
            player.DamagePlayer(damage);
        }

    }

    public void Damage(int dmg)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
