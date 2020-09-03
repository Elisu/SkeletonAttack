using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : EnemyWeapon
{  
  
    /// <summary>
    /// Fireballs falling from sky
    /// </summary>
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);

        if (transform.position.y <= yBound)
            Destroy(gameObject);
    }

    
}
