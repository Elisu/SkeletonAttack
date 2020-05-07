using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 velocity;

    public float smoothTimeY;              //aby byl pohyb kamery plynulý
    public float smoothTimeX;
    public float posYrestriction = -1;

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
            return;

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);   //pozice kamery x, cíl - pozice hráče x - 
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        posY = Mathf.Clamp(posY, posYrestriction, Mathf.Infinity);   //omezí y souradnici kamery mezi posYrestriction a infinity
        transform.position = new Vector3(posX, posY, transform.position.z);  //nová pozice kamrery
    }
}
