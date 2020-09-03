using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BulletShoot
{    
    public int damageRadius = 10;
   
    Collider2D[] overlaping;    

    private void Start()   
    {        
        overlaping = new Collider2D[30];        
    }

    // Update is called once per frame
    protected override void Update()
    {        
        if (lifeSpan <= 0)
            Destroy(gameObject);

        //computes bullet position in time
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        //collision check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatToHit);
        if (hit.collider != null)
        {            
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

            //After collison checks radius for other objects
            overlaping = Physics2D.OverlapCircleAll(transform.position, damageRadius, whatToHit);
            DestroyBullet();

            //For enemies and fireballs in radius - deals damage
            for (int i = 0; i < overlaping.Length; i++)
            {
                    Enemy enemyOther = overlaping[i].GetComponent<Enemy>();
                    if (enemyOther != null)
                        enemyOther.DamageEnemy(damage);
                    else
                    {

                        Fireball fbOther = hit.collider.GetComponent<Fireball>();
                        if (fbOther != null)
                        {
                            if (fbOther.shootable)
                                fbOther.Damage(damage);
                        }
                    }
            }
                    
        }

        lifeSpan--;
    }
}
