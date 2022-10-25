using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainBattleSystemScripts : MonoBehaviour
{
    public int widhtField;
    public int heightField;
    public float marginsCell;
    public GameObject PrefloorUnit;
    public GameObject[,] massiveFields;
   // public int[,] massiveStateGraff;

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
    public Vector2 positionPersone;

    public int step;
    public bool personeMove;
    public List <Vector2> path;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

        massiveFields = BattlefieldGeneration.generate(widhtField, heightField, marginsCell, PrefloorUnit, gameObject);

        massiveBattlePlayerPersone = new GameObject[1];
        massiveBattleEnemyPersone = new GameObject[1];
        testPlayer = Instantiate(testPreFabPlayer);
        testEnemy = Instantiate(testPreFabEnemy);
        massiveBattlePlayerPersone[0] = testPlayer;
        massiveBattleEnemyPersone[0] = testEnemy;

        testPlayerScript = testPlayer.GetComponent<PersoneTest>();
        testPlayerScript.mainBattleSystemScripts = gameObject.GetComponent<MainBattleSystemScripts>();
        testEnemyScript = testEnemy.GetComponent<EnemyTest>();

        setPositionPersone(newPosition, massiveFields, testPlayer);
        setPositionPersone(new Vector2(4, 2), massiveFields, testEnemy);

       


    }

    // Update is called once per frame
    void Update()
    {
        if (personeMove)
        {
            PersoneMove(massiveBattlePlayerPersone[0], path);
        }

    }

    public void setPositionPersone(Vector2 positionSet, GameObject[,] fieldmap, GameObject persone)
    {

        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];
        var cellFieldScript = cellField.GetComponent<CellFloorScripts>();
        if (!cellFieldScript.closeCell)
        {
            var position = cellField.transform.position;
            position.z = positionSet.y;
            persone.GetComponent<APersoneScripts>().battlePosition = cellFieldScript.positiongGrafCellField;
            //persone.transform.position=cellField.transform.position;
            persone.transform.position = position;
            cellFieldScript.closeCell = true;
        }
        else
        {
            Debug.Log("ячейка зан€та или закрыта");
        }

    }
    public void PersoneMove(GameObject pesrone, List<Vector2> path)
    {
        if (step < path.Count)
        {
           
            Debug.Log($"Step = :{step}");
            if (pesrone.gameObject.transform.position.x != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.x ||
                pesrone.gameObject.transform.position.y != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)
            {

                pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, massiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);
                Debug.Log($"persone move");
                pesrone.GetComponent<PersoneTest>().battlePosition = massiveFields[(int)path[step].x, (int)path[step].y].GetComponent<CellFloorScripts>().positiongGrafCellField;
            }
            else
            {
                step++;
            }
        }
        else
        {

            personeMove = false;
           
           // for (int i = 0; i < path.Count; i++)
            //{
              //  Vector2 floor = path[i];
               // massiveFields[(int)floor.x, (int)floor.y].GetComponent<FloorScripts>().setPicture(sprite);

            //}
        }

    }
}
