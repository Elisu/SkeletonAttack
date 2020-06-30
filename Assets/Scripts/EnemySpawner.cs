using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum State { spawning, waiting, counting };

    [System.Serializable]  
    public class Wave
    {

        public Transform enemy;            
        //number of enemies to spawn
        public int enemyCount;                  
        public float spawnRate;                 

    }

    public Wave[] waves;                    
    private int nextWave;                

    public float timeBtwWaves;              
    public float waveCountDown;     
    //starts with countdown
    private State state = State.counting;   

    private float searchCountDown;
    //multiplies number of enemies to increase difficulty
    private int Multiplier = 1;             

    public Transform[] spawnPoints;       


    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBtwWaves;
    }

    // Update is called once per frame
    void Update()
    {
        //waiting for player to kill the whole wave
        if ( state == State.waiting)              
        {
            if ( !EnemyAlive())                    
            {

                NextRound();                  
            }
            else
            {
                return;
            }

        }

        if (waveCountDown <= 0 && state != State.spawning)     
        {
            StartCoroutine(WaveSpawn(waves[nextWave]));    
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    bool EnemyAlive()
    {
        searchCountDown -= Time.deltaTime;                              

        if ( searchCountDown <= 0 )
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)       
                return false;
        }
        

        return true;
    }

    void NextRound()
    {
        state = State.counting;
        waveCountDown = timeBtwWaves;

        if ( nextWave + 1 > waves.Length - 1) 
        {
            nextWave = 0;
            Multiplier += 1;
        }
        else        
         nextWave++;

    }

    IEnumerator WaveSpawn (Wave wave)                       
    {
        
        state = State.spawning;

        for ( int i = 0; i <= wave.enemyCount * Multiplier ; i++)     
        {
            Spawn(wave.enemy);
            //suspends spawning so enemies spawn at given rate and not all at once
            yield return new WaitForSeconds(1f/ wave.spawnRate); 
        }


        state = State.waiting;
        yield break;

    }

    void Spawn ( Transform enemy)
    {
        //selects random spawnpoint
        Transform sPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];     
        Instantiate(enemy, sPoint.position, sPoint.rotation);                 
    }
}
