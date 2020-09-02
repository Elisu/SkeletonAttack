using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Enemy
{
    

    public static UnityEvent BossEntered = new UnityEvent();
    
    private void Start()
    {
        BossEntered.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
