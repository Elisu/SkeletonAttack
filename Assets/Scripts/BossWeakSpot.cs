using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakSpot : MonoBehaviour
{
    public int damage = 2000;
    public int rechargeTime = 60;

    private bool active = true;
    Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            boss.DamageEnemy(damage);
            StartCoroutine(Recharge());
        }
           
    }

    //Disables weak spot function for a bit so player can only hit once per chance
    IEnumerator Recharge()
    {
        active = false;
        yield return new WaitForSeconds(rechargeTime);
        active = true;
        yield break;
    }
}
