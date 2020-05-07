using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum State { spawning, waiting, counting };

    [System.Serializable]   // aby bylo promene videt v Unity
    public class Wave
    {

        public Transform enemy;                //objekt nepřítele
        public int enemyCount;                  //počet nepřátel které chceme vypustit
        public float spawnRate;                 

    }

    public Wave[] waves;                    //pole obsahující objekty vln
    private int nextWave;                   //index vlny

    public float timeBtwWaves;              //čas mezivlnami
    public float waveCountDown;             //odpočet mezi vlnami
    private State state = State.counting;    //aktuální stav - začne odpočtem

    private float searchCountDown;
    private int Multiplier = 1;             //násobí počet nepřátel k zvyšování obtížnosti

    public Transform[] spawnPoints;         //pole bodů k spawnování nepřátel


    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBtwWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if ( state == State.waiting)                //cekame až zabije vsechno
        {
            if ( !EnemyAlive())                    //kontrola, zda nějaký nepřítel naživu
            {

                NextRound();                    //když ne, tak další kolo
            }
            else
            {
                return;
            }

        }

        if (waveCountDown <= 0 && state != State.spawning)     //odpočet vlny a nepřátelé nejsou už vypouštěni
        {
            StartCoroutine(WaveSpawn(waves[nextWave]));    //vypuštění nepřátel, jako couritne protože WaveSpawn je IEnumerator 
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
            if (GameObject.FindGameObjectWithTag("Enemy") == null)       //pokud nenajde nepřítele, nikdo nežije
                return false;
        }
        

        return true;
    }

    void NextRound()
    {
        state = State.counting;
        waveCountDown = timeBtwWaves;

        if ( nextWave + 1 > waves.Length - 1)   //źvýšení indexu vlny
        {
            nextWave = 0;
            Multiplier += 1;
        }
        else        
         nextWave++;

    }

    IEnumerator WaveSpawn (Wave wave)                       //potřebujeme mít možnost pozastavit vykonávání, proto IEnumerator
    {
        
        state = State.spawning;

        for ( int i = 0; i <= wave.enemyCount * Multiplier ; i++)       //počet cyklu je počet neprátel, multiplier zvyšuje počet v dalších kolech
        {
            Spawn(wave.enemy);
            yield return new WaitForSeconds(1f/ wave.spawnRate);   //pozastavíme vykonávání, protože jsme si určily spawnRate a nechceme všechny najednou
        }


        state = State.waiting;
        yield break;

    }

    void Spawn ( Transform enemy)
    {
        Transform sPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];     //náhodný výběr jednoho ze spawnPointů
        Instantiate(enemy, sPoint.position, sPoint.rotation);                   //nepřitel se objeví
    }
}
