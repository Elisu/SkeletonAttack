using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Seeker seeker;
    private Rigidbody2D rb;

    public Transform player;                    //cíl
    public float updatePathRate = 2f;                //rate jak často hledáme novou cestu


    // cesta
    public Path path;

    public float nextDestinationDistance = 3;
    private int currentDestination = 0;                 //kam aktuálně chceme

    private bool lookingORnot = false;                  //zda hledáme cíl či ne

    public float speed = 300f; //rychlost nepritele
    public ForceMode2D forceMode;

    public bool pathIsEnded = false;

    

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            if ( !lookingORnot)             //když nemáme cíl spustíme hledání
            {
                lookingORnot = true;
                StartCoroutine(Searching());
            }
            return;
        }

        seeker.StartPath(transform.position, player.position, FoundPath);  //spuštění cesty k hráči a vrátí výsledek do procedury FoundPath
        StartCoroutine(UpdatePath());
    }

    IEnumerator Searching()
    {
        GameObject pom = GameObject.FindGameObjectWithTag("Player");  //chceme najít hráče
        
        if ( pom == null)                                    //neobjevily jsme objekt hráče
        {
            yield return new WaitForSeconds(0.5f);           //chvíli počkáme
            StartCoroutine(Searching());                     //chceme zkusit znovu - zavoláme znovu
        }
        else                                                 //našli jsme
        {
            lookingORnot = false;                             //už nehledáme
            player = pom.transform;                           //dosadíme jako cíl to co jsme našli
            StartCoroutine(UpdatePath());                      //zavoláme hledání cesty
            yield break;
        }

    }

    IEnumerator UpdatePath()                               //hledání cesty, potřebujeme pozastavit provádění
    {                                                      //abychom neobnovovali cestu moc často, bylo by to zbytečné
        if (player == null)                                 //když nemáme cíl
        {
            if (!lookingORnot)
            {
                lookingORnot = true;
                StartCoroutine(Searching());            //spustíme hledání cíle
            }
            yield break;
        }

        seeker.StartPath(transform.position, player.position, FoundPath); //spustí trasu
        yield return new WaitForSeconds(1f / updatePathRate);                    //počkáme, abychom hned zbytečně neaktualizovali cestu
        StartCoroutine(UpdatePath());                               //znovu zavoláme k aktualizaci cesty, cíl se totiž mohl pohnout jinam
    }


    public void FoundPath(Path p)   //dostane nalezenou cestu
    {
        if (!p.error)    //když je cesta v pořádku, dosadíme jí do path
        {
            path = p;
            currentDestination = 0;   
        }
    }

    void FixedUpdate()                      //stará se o pohyb
    {
        if (player == null)
        {
            return;
        }

        if (path == null)  
        {
            return;
        }

        if (currentDestination >= path.vectorPath.Count)     
        {
            if (pathIsEnded)
                return;
        }

        pathIsEnded = false;

        // smer k dalsimu bodu
        Vector3 direction = (path.vectorPath[currentDestination] - transform.position).normalized;   //odecteme od sebe 2 pozice - dostaneme směr a normalizujeme
        direction *= speed * Time.fixedDeltaTime;             

        rb.AddForce(direction, forceMode);   // pohyb ve směr

        float vzdal = Vector3.Distance(transform.position, path.vectorPath[currentDestination]);
        if ( vzdal < nextDestinationDistance)
        {
            currentDestination++;
            return;
        }

    }
}
