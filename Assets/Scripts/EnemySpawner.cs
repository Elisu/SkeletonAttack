using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Score currentScore;
    private int chosenScore;

    public Transform[] fireballSPawns;
    public Fireball fireball;
    public enum State { spawning, waiting, counting, boss, fireRain };    

    [System.Serializable]  
    public class Wave
    {

        public Transform[] enemy;            
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
    public Transform boss1Spawn;
    public Transform[] bomberSpawns;
    public Boss boss1;
    public Transform bossGuard;

    private GameObject boss;
    private int scoreRange = 301;
    private int fSpawnCounter = 0;

    // Start is called before the first frame update
    void Start()
    { 
        waveCountDown = timeBtwWaves;
        chosenScore = Random.Range(0, scoreRange);

        //DEBUG
        //state = State.boss;
        //boss = Instantiate(boss1.gameObject, boss1Spawn.position, Quaternion.identity);
        //boss.GetComponent<Boss>().bossLowHealth.AddListener((() => { SpawnGuards(); }));
    }

    // Update is called once per frame
    void Update()
    {        
        if (state != State.boss && state != State.fireRain)
        {            
            //waiting for player to kill the whole wave
            if (state == State.waiting)
            {
                if (!EnemyAlive())
                {
                    if (currentScore.ReturnScore() >= chosenScore)
                    {
                        state = State.fireRain;
                        for (int j = 0; j < fireballSPawns.Length; j++)
                           StartCoroutine(FireballRain(j));
                        chosenScore = Random.Range(scoreRange, scoreRange + 500);
                        return;
                    }
                    else
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
        else
        {
            //If state boss but boss not alive then next round
            if (state == State.boss && !BossAlive())
                NextRound();
            
        }
    }

    bool BossAlive()
    {
        if (GameObject.FindGameObjectWithTag("Boss") == null)
            return false;
        else
            return true;
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
            boss = Instantiate(boss1.gameObject, boss1Spawn.position, Quaternion.identity);
            boss.GetComponent<Boss>().bossLowHealth.AddListener((() => { SpawnGuards(); }));
            state = State.boss;
            //To raise difficulty
            Multiplier += 1;
        }
        else        
         nextWave++;

    }   

    IEnumerator WaveSpawn (Wave wave)                       
    {
        
        state = State.spawning;

        for (int i = 0; i <= wave.enemyCount * Multiplier; i++)
        {
            Spawn(wave.enemy[Random.Range(0, wave.enemy.Length)]);
            //suspends spawning so enemies spawn at given rate and not all at once
            yield return new WaitForSeconds(1f/ wave.spawnRate); 
        }


        state = State.waiting;
        yield break;

    }

    void Spawn ( Transform enemy)
    {
        Transform sPoint;
        //selects random spawnpoint
        if (enemy.GetComponent<EnemyAI>() != null)
            sPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        else
            sPoint = bomberSpawns[Random.Range(0, bomberSpawns.Length)];

        Instantiate(enemy, sPoint.position, sPoint.rotation);                 
    }

    IEnumerator FireballRain(int index)
    {
        int i = 0;
        int ballCount = Random.Range(3, 6);
        
        while (i < ballCount)
        {
            yield return new WaitForSeconds(Random.Range(0, 8));
            Instantiate(fireball, fireballSPawns[index].position, fireballSPawns[index].rotation);
            i++;
        }

        fSpawnCounter++;

        if (fSpawnCounter >= fireballSPawns.Length)
        {
            fSpawnCounter = 0;
            NextRound();
        }         

        yield break;
    }

    /// <summary>
    /// Called when boss in trouble
    /// </summary>
    public void SpawnGuards()
    {
        for (int i = 0; i < 2; i++)
        {
            Spawn(bossGuard);
        }
    }
}

