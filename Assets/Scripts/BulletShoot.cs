using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public float speed = 5;                 //bullet speed
    public int damage = 50;                 

    public float distance;                  //distance for collision check
    public LayerMask whatToHit;             
    public GameObject destroyEffect;   


    // Update is called once per frame
    void Update()
    {
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
        }        
    }

    /// <summary>
    /// Destroys the bullet and plays particle effect
    /// </summary>
    void DestroyBullet()
    {        
        Instantiate(destroyEffect, transform.position, Quaternion.identity);     
        Destroy(gameObject);
    }
}
