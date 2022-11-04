using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : APersoneScripts
{
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

    private void OnMouseEnter()
    {
        Debug.Log("Enemy");
    }
}
