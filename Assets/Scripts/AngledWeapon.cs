using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for a weapon with rotation controlled by chosen keys
/// </summary>
public class AngledWeapon : Weapon
{
    public KeyCode rotateLeft;
    public KeyCode rotateRight;
    public KeyCode shoot;

    private float rotation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(rotateLeft))
            rotation += 0.2f;
        else if (Input.GetKey(rotateRight))
            rotation -= 0.2f;       
        
        //Sets weapon rotation
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + offset);

        if (timeBetweenShots <= 0)
        {
            if (Input.GetKeyDown(shoot))
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
