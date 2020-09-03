using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemEffector : MonoBehaviour
{
    public bool isWeapon = false;
    
    PolygonCollider2D colliderEffector;
    List<Collider2D> overlaping;
    ContactFilter2D filter = new ContactFilter2D();

    // Start is called before the first frame update
    void Start()
    {       
        colliderEffector = GetComponent<PolygonCollider2D>();
        overlaping = new List<Collider2D>();
    }

    private void Update()
    {
        filter.SetLayerMask(LayerMask.GetMask("Player"));
        colliderEffector.OverlapCollider(filter, overlaping);

        //For spawned items - if player is there to pick it up
        if (overlaping.Count > 0)
        {
            if (!isWeapon)
                overlaping[0].GetComponent<Player>().TakeHeart(colliderEffector);
            else
                overlaping[0].GetComponent<Player>().TakeWeapon(colliderEffector);
        }        
    }
}
