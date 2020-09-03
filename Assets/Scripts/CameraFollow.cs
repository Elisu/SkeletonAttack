using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 velocity;

    // For smooth camera movement
    public float smoothTimeY;              
    public float smoothTimeX;

    // Lowest y position camera can reach
    public float posYrestriction = -1;

    //Left end and right end
    public float posXrestrictionLeft;
    public float posXrestrictionRight;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>()?.gameObject;
            if (player == null)
                return;
        }        

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);   
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        // Restricts camera movement to interval  (posYrestriction, infinity)
        posY = Mathf.Clamp(posY, posYrestriction, Mathf.Infinity);
        posX = Mathf.Clamp(posX, posXrestrictionLeft, posXrestrictionRight);
        transform.position = new Vector3(posX, posY, transform.position.z);  
    }
}
