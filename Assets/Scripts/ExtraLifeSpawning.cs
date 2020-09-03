using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeSpawning : MonoBehaviour
{
    public Transform[] spawns;
    public Transform life;
    public Transform rocketLauncher;
    private Transform spawned;

    public int minDuration = 5;
    public int maxDuration = 21;

    private int countDown;

    private void Start()
    {
        //Spawn extra life when boss enters
        Boss.BossEntered.AddListener(delegate { SpawnAndDestroy(life); });
        countDown = Random.Range(3000, 20000);
        //countDown = 5;
    }

    public void SpawnAndDestroy(Transform item)
    {
        Transform sPoint = spawns[Random.Range(0, spawns.Length - 1)];
        spawned = Instantiate(item, sPoint.position, Quaternion.identity);
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        if (countDown <= 0 && spawned == null)
        {
            //Randomly spawn extra life or weapon
            int whichItem = Random.Range(0, 20);
            if (whichItem <= 15)
                SpawnAndDestroy(life);
            else
                SpawnAndDestroy(rocketLauncher);
            countDown = Random.Range(3000, 40000); 
        }          

        countDown--;
    }    
    
    /// <summary>
    /// When time elapses destroy item
    /// </summary>
    /// <returns></returns>
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(Random.Range(minDuration, maxDuration));

        if (spawned != null)
            Destroy(spawned.gameObject);
        spawned = null;
        yield break;
    }
}
