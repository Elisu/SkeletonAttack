using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int offset;

    public GameObject bullet;                   //projektil
    public Transform firePoint;                 //pozice, odkud vylétají projektily

    private float timeMeziShots;                
    public float startTimeMeziShots;
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;   // pozice mysi - pozice ruky
        difference.Normalize();   // normalizujeme, aby mensi cisla

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // vypocet uhlu, jaky mame dat ruce a prevedeme na stupne
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + offset); // upresnime ze ve stupnich

        if (timeMeziShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Instantiate(bullet, firePoint.position, transform.rotation); // spawnuje kulky
                timeMeziShots = startTimeMeziShots;
            }
        }
        else
        {
            timeMeziShots -= Time.deltaTime;
        }

       
    }
}
