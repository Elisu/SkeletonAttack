using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Enemy
{
    public int speed;
    public Missile missile;
    public int shootingCountdown;
    public static UnityEvent BossEntered = new UnityEvent();
    public UnityEvent bossLowHealth = new UnityEvent();

    //low health triggers behaviour
    public int lowHealthTrigger = 150;
    //how close to player it goes
    public int proximity = 40;
    //how often it flies up to give player chance to hit weak spot
    public int chanceToHit = 1000;

    private int toHit;
    private Vector3 direction;
    private int count = 0;
    private float initialY;
    private int initialHealth;
    private int shotCoutdown;
    private BossHealthBar healthBar;
    private int maxCount = 5000;

    private void Start()
    {
        BossEntered.Invoke();
        healthBar = FindObjectOfType<BossHealthBar>();
        healthBar.SetMaxHealth(health);
        initialHealth = health;
        initialY = transform.position.y;
        toHit = chanceToHit;
        direction = transform.position;
        Player[] players = FindObjectsOfType<Player>();
        player = players[Random.Range(0, players.Length)].transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when health drops below bound
        if (health <= lowHealthTrigger)
        {
            //needs to be killed by weak spot
            if (health > 0)
            {
                bossLowHealth.Invoke();
                //Boss heals
                health = initialHealth/9;
                healthBar.SetHealth(health);
            }            
        }


        if (player != null)
        {
            if (chanceToHit < 0)
            {
                //Boss rises to show weak spot
                if (transform.position.y < 25)
                    direction.y += 10;
                else
                    count = maxCount;
                count++;
            }
            else
            {
                if (shotCoutdown <= 0)
                    SpawnMissile();
                //returning to normal altitute
                direction.y = (initialY - transform.position.y);
            }


            //move toward or from player based on proximity
            if (System.Math.Abs(transform.position.x - player.position.x) > proximity)
            {
                direction.x = (player.transform.position.x - transform.position.x);
                direction.Normalize();
                transform.Translate(direction * speed * Time.fixedDeltaTime);
            }
            else
            {
                direction.x = (player.transform.position.x - transform.position.x) * -1;
                direction.Normalize();
                transform.Translate(direction * speed * Time.fixedDeltaTime);

            }

            //Time for boss rise elapsed?
            if (count > maxCount)
            {
                count = 0;
                chanceToHit = toHit;
            }

            shotCoutdown--;
            chanceToHit--;

        }
        else
            Searching();
        
    }

    void SpawnMissile()
    {
        shotCoutdown = shootingCountdown;
        Instantiate(missile, transform.position, transform.rotation);
    }

    public override void DamageEnemy(int damage)
    {        
        base.DamageEnemy(damage);
        healthBar.SetHealth(health);
    }

    //Called when weak spot damaged
    public void bossHurt()
    {
        count = 0;
        chanceToHit = toHit;
        direction.y = (initialY - transform.position.y);
    }


}
