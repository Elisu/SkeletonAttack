using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ScareEnemy(int duration);
public class BulletShoot : MonoBehaviour
{
    public float speed = 5;                 //bullet speed
    public int damage = 50;                 

    public float distance;                  //distance for collision check
    public LayerMask whatToHit;             
    public GameObject destroyEffect;
    public int scareDuration = 100;
    protected int lifeSpan = 5000;

    private ScareEnemy scare;

    public void AddScare(ScareEnemy ch)
    {
        scare = ch;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Prevetnts them staying acitve in scene forever
        if (lifeSpan <= 0)
            Destroy(gameObject);

        //computes bullet position in time
        transform.Translate(Vector2.up * speed * Time.deltaTime);      

        //collision check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatToHit);   
        if (hit.collider != null)   
        {
            DestroyBullet();     
            Enemy enemy = hit.collider.GetComponent<Enemy>();   
            if (enemy != null)
            {
                Debug.Log("Hit");                 
                enemy.DamageEnemy(damage);  
            }
            else
            {
                Fireball fb = hit.collider.GetComponent<Fireball>();
                if (fb != null)
                {
                    if (fb.shootable)
                        fb.Damage(damage);
                }
            }
        }

        if (scare != null)
        {
            scare(scareDuration);
            scare = null;
        }

        lifeSpan--;
    }

    /// <summary>
    /// Destroys the bullet and plays particle effect
    /// </summary>
    protected void DestroyBullet()
    {        
        Instantiate(destroyEffect, transform.position, Quaternion.identity);     
        Destroy(gameObject);
    }

}
