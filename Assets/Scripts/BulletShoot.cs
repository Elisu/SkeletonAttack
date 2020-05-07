using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public float speed = 5;                 //rychlost kulky
    public int damage = 50;                 //hodnota poškození, které uděluje

    public float distance;                  //vzdálenost kontroly
    public LayerMask whatToHit;             //určuje, co může být zasaženo za vrstvu
    public GameObject destroyEffect;         //efekt exploze kulky

    
 


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);      //výpočet pozice kulky v čase
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatToHit);   //kontrola srážky
        if (hit.collider != null)   //poud kontrola najde srážku
        {
            DestroyBullet();     //zničí objekt kulky
            Enemy enemy = hit.collider.GetComponent<Enemy>();    //pokusí se najít objekt nepřítele, jež byl zasažen
            if (enemy != null)
            {
                Debug.Log("Zasah");
                enemy.DamageEnemy(damage);  //poškodí nepřítele
            }
           
        }


        
    }

    void DestroyBullet()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);     //spustí efekt exploze na souřadnicích 
        Destroy(gameObject);
    }
}
