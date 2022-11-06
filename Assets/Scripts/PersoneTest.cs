using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoneTest : APersoneScripts
{
    public bool playerActive;
  


    // Start is called before the first frame update
    void Start()
    {
        personeType = Persone.Player;
        maxHealthPoints = 10;
        movementPointsMax = 10;
        damage = 3;
        healthPoint = maxHealthPoints;
        UpdatingPointStartTurn();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    
}
