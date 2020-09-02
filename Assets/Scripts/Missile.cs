using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : EnemyWeapon
{
    GameObject pl;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        float x1 = pl.transform.position.x - transform.position.x;
        float y1 = pl.transform.position.y - transform.position.y;
        direction = new Vector3() { x = x1, y = y1, z = transform.position.z };
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.collider.tag == "Ground")
            Destroy(gameObject);
    }
}
