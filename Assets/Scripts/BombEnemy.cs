﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    public Transform[] bomb;
    public int bombFrequency = 5;    
    public float Speed;
    public float updatePathRate = 3f;
    public ForceMode2D forceMode;

    private int bombCountdown;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        bombCountdown = bombFrequency;

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

            if (bombCountdown <= 0)
               SpawnBomb();

            bombCountdown--;
        }
        
    }

    void SpawnBomb()
    {
        bombCountdown = bombFrequency;
        Vector3 bottom = transform.position;
        bottom.y -= 2;
        int whatBomb = Random.Range(0, 10);

        Transform chosenBomb;
        if (whatBomb < 8)
            chosenBomb = bomb[0];
        else
            chosenBomb = bomb[1];

        Instantiate(chosenBomb, bottom, transform.rotation);
    }
}