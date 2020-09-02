using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public Missile missile;
    public float Speed;
    public int shootCountdown = 10;    
    public ForceMode2D forceMode;

    private int countdown;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        countdown = shootCountdown;

        if (player == null)
        {
            //if no target - start search
            if (!lookingORnot)
            {
                lookingORnot = true;
                StartCoroutine(Searching());
            }
            return;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 direction = new Vector3 { x = player.position.x - transform.position.x, y = 0, z = 0 };
            direction = direction.normalized;
            direction *= Speed * Time.fixedDeltaTime;
            rb.AddForce(direction, forceMode);

            if (countdown <= 0)
                SpawnMissile();

            countdown--;
        }
    }

    void SpawnMissile()
    {
        countdown = shootCountdown;
        Instantiate(missile, transform.position, transform.rotation);
    }
}
