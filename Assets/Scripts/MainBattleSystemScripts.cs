using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBattleSystemScripts : MonoBehaviour
{
    public int widhtField;
    public int heightField;
    public float marginsCell;
    public GameObject PrefloorUnit;
    public GameObject[,] massiveFields;

    public GameObject[] massiveBattleSystemPersone;
    public GameObject[] massiveBattlePlayerPersone;
    public GameObject[] massiveBattleEnemyPersone;
    public GameObject testPreFabPlayer;
    public GameObject testPlayer;
    public PersoneTest testPlayerScript;
    public GameObject testPreFabEnemy;
    public GameObject testEnemy;
    public EnemyTest testEnemyScript;

    
    public Vector2 newPosition;
   

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        massiveFields = BattlefieldGeneration.generate(widhtField, heightField, marginsCell, PrefloorUnit);

        massiveBattlePlayerPersone = new GameObject[1];
        massiveBattleEnemyPersone = new GameObject[1];
        testPlayer = Instantiate(testPreFabPlayer);
        testEnemy = Instantiate(testPreFabEnemy);
        massiveBattlePlayerPersone[0] = testPlayer;
        massiveBattleEnemyPersone[0] = testEnemy;

        testPlayerScript = testPlayer.GetComponent<PersoneTest>();
        testEnemyScript= testEnemy.GetComponent<EnemyTest>();

        setPositionPersone(newPosition,massiveFields, testPlayer);
        setPositionPersone(new Vector2(4, 2), massiveFields, testEnemy);
    


    }

    // Update is called once per frame
    void Update()
    {
     //   PathFinder.Path();
    }

    public void setPositionPersone(Vector2 positionSet, GameObject[,] fieldmap, GameObject persone){

        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];
        var cellFieldScript = cellField.GetComponent<CellFloorScripts>();
        if (!cellFieldScript.closeCell)
        {
            persone.GetComponent<APersoneScripts>().battlePosition=cellFieldScript.positiongGrafCellFloor;
        persone.transform.position=cellField.transform.position;
            cellFieldScript.closeCell=false;
        }
        else
        {
            Debug.Log("ячейка зан€та или закрыта");
        }
        
        }
}
