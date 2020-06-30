using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int offset;

    public GameObject bullet;     
    //point at which bullets are instantiated
    public Transform firePoint;                 

    private float timeBetweenShots;                
    public float startTimeBetweenShots;
    // Update is called once per frame
    void Update()
    {
        // mouse position - weapon position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;   
        difference.Normalize();   

        //computes angle at which weapon should be pointing
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + offset); 

        if (timeBetweenShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //spawns bullets
                Instantiate(bullet, firePoint.position, transform.rotation); 
                timeBetweenShots = startTimeBetweenShots;
            }
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

       
    }
}
