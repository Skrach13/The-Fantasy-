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
        movementPointsMax = 10;
        movementPoints = movementPointsMax;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void move(List<Vector2> path)
    {
        if (movementPoints>0)
        {
            for (int i = 0; i < movementPoints; i++)
            {
                Vector2 target = mainBattleSystemScripts.massiveFields[(int)path[i].x, (int)path[i].y].GetComponent<CellFloorScripts>().positiongGrafCellField;
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target, 0.9f * Time.deltaTime);
              //  path[i];
            }

        }
    }
    */
    
}
