using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Seeker seeker;
    private Rigidbody2D rb;

    // Target
    public Transform player;                    
    public float updatePathRate = 2f;      
    public Path path;

    public float nextDestinationDistance = 3;
    private int currentDestination = 0;                 

    private bool lookingORnot = false;                 

    // Enemy speed
    public float speed = 300f; 
    public ForceMode2D forceMode;

    public bool pathIsEnded = false;    

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            //if no target - start search
            if ( !lookingORnot)             
            {
                lookingORnot = true;
                StartCoroutine(Searching());
            }
            return;
        }

        //start going on path to player and returns rusult of FoundPath
        seeker.StartPath(transform.position, player.position, FoundPath);  
        StartCoroutine(UpdatePath());
    }

    IEnumerator Searching()
    {
        //looking for player
        GameObject pl = null;
        Player[] players = FindObjectsOfType<Player>();
        if (players.Length > 0)
            pl = players[Random.Range(0, players.Length)].gameObject;

        if ( pl == null)                                    
        {
            yield return new WaitForSeconds(0.5f);           
            StartCoroutine(Searching());                     
        }
        else                                                 
        {
            lookingORnot = false;
            player = pl.transform;           
            StartCoroutine(UpdatePath());                    
            yield break;
        }

    }

    //looking for path - need to suspend search, so we dont upade path to often
    IEnumerator UpdatePath()                              
    {                                                     
        if (player == null)                               
        {
            if (!lookingORnot)
            {
                lookingORnot = true;
                //searching for target
                StartCoroutine(Searching());           
            }
            yield break;
        }

        seeker.StartPath(transform.position, player.position, FoundPath); 
        yield return new WaitForSeconds(1f / updatePathRate);             
        //alled to update path in case target moved elsewhere
        StartCoroutine(UpdatePath());                               
    }


    public void FoundPath(Path p) 
    {
        if (!p.error)    
        {
            path = p;
            currentDestination = 0;   
        }
    }

    void FixedUpdate()                      
    {
        if (player == null)
        {
            StartCoroutine(Searching());
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

        // direction to next point
        Vector3 direction = (path.vectorPath[currentDestination] - transform.position).normalized;   
        direction *= speed * Time.fixedDeltaTime;             

        rb.AddForce(direction, forceMode);   

        float vzdal = Vector3.Distance(transform.position, path.vectorPath[currentDestination]);
        if ( vzdal < nextDestinationDistance)
        {
            currentDestination++;
            return;
        }

    }
}
