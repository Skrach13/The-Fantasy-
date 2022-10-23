using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Скрипт ячейки поля
/// </summary>
public class CellFloorScripts : MonoBehaviour
{
    public Vector2 positiongGrafCellFloor;
    public MainBattleSystemScripts mainSystemBattleScript;
    public bool closeCell;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {

        GetComponent<SpriteRenderer>().color = Color.red;

        PathFinder.Path(mainSystemBattleScript.massiveFields, mainSystemBattleScript.testPlayerScript.battlePosition, positiongGrafCellFloor);
        //mainSystemBattleScript.newPosition = positiongGrafCellFloor;
}
}
