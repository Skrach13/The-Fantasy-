using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// класс содержащий в себе основную систему отвечающую за бой ( по окончанию написания кода изменить описание)
/// </summary>
public class MainBattleSystemScripts : MonoBehaviour
{
    
    public int widhtField; // ширина поля в количестве ячеек
    public int heightField; // высота поля в коолистве ячеек
    public GameObject PrefloorUnit;// префаб ячейки поля (изменить название переменной?)
    public GameObject[,] massiveFields; //массив содержащий ячейки поля
   // public int[,] massiveStateGraff;// массив для алгоритма поиска пути где (пока) 1 закрыто 0 откртая ячейка (использоваеться для меньшего использования GetComponent)
 
    public GameObject[] massiveBattleSystemPersone;// массив для системы инициативы ( скорее всего будет изменен на List<>)
    public GameObject[] massiveBattlePlayerPersone;//массив для персонажей Игрока ( скорее всего будет изменен на List<>)
    public GameObject[] massiveBattleEnemyPersone;//массив для персонажей Противников ( скорее всего будет изменен на List<>)
    public GameObject testPreFabPlayer;//префаб персонажа(будет изменено)
    public GameObject testPlayer;//ссылка на персонажа игрока
    public PersoneTest testPlayerScript;//ссылка на скрипт персонажа
    public GameObject testPreFabEnemy;//префаб противника(будет изменено)
    public GameObject testEnemy;// ссылка на противника
    public EnemyTest testEnemyScript;//ссылка на скрипт противника

    public Vector2 newPosition;//позиция ячейки для установки персонажа (будет изменено)
  //  public Vector2 positionPersone;//

    public int step;//шаг в передвижении персонажа
    public bool personeMove;// перемещаеться ли персонаж
    public List <Vector2> path;// путь перемещения содержащее положение ячеек в графе 

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

        massiveFields = BattlefieldGeneration.generate(widhtField, heightField, PrefloorUnit, gameObject);//создание поле боя

        massiveBattlePlayerPersone = new GameObject[1];// пока затычка для тестов
        massiveBattleEnemyPersone = new GameObject[1];// пока затычка для тестов
        testPlayer = Instantiate(testPreFabPlayer);// создание персонажа "игрока"
        testEnemy = Instantiate(testPreFabEnemy);// создание персонажа противника
        massiveBattlePlayerPersone[0] = testPlayer;// пока затычка для тестов
        massiveBattleEnemyPersone[0] = testEnemy;// пока затычка для тестов

        testPlayerScript = testPlayer.GetComponent<PersoneTest>();// пока затычка для тестов
        testPlayerScript.mainBattleSystemScripts = gameObject.GetComponent<MainBattleSystemScripts>();//пока затычка для тестов для присваивания положения на поле
        testEnemyScript = testEnemy.GetComponent<EnemyTest>(); //пока затычка для тестов

        setPositionPersone(newPosition, massiveFields, testPlayer); //пока затычка для тестов
        setPositionPersone(new Vector2(3, 2), massiveFields, testEnemy);//пока затычка для тестов




    }

    // Update is called once per frame
    void Update()
    {
        if (personeMove) //перемещение персонажа
        {
            PersoneMove(massiveBattlePlayerPersone[0], path);
        }

    }

    /// <summary>
    /// метод для установки персонажа на поле боя (применение этим не ограничено) 
    /// </summary>
    /// <param name="positionSet"> позиция на поле ссылающее на ячейку в массиве поля </param>
    /// <param name="fieldmap">массив поля боя</param>
    /// <param name="persone">персонаж которого надо поставить на поле</param>
    public void setPositionPersone(Vector2 positionSet, GameObject[,] fieldmap, GameObject persone)
    {

        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];//локалная ссылочная переменая на ячейку поля
        var cellFieldScript = cellField.GetComponent<CellFloorScripts>();//локальная ссылка на скрипт ячейки поля
        if (!cellFieldScript.closeCell) // проверка не занята ли 
        {
            var position = cellField.transform.position;//локальная переменая, координаты ячейки на экране( что то вроде того НЕ В ГРАФЕ)
            position.z = positionSet.y;// установка Z для коеретной отрисовки ( что бы обьекты друг друга перекрывали привильно) НЕ ПРОВЕРЕНО!!!
            persone.GetComponent<APersoneScripts>().battlePosition = cellFieldScript.positiongGrafCellField;//присваивание персонажу позиции ячейки в графе на которой он стоит
            persone.transform.position = position;//установка персонажу координат на экране
            cellFieldScript.closeCell = true;//закрытие ячейки на которой стоит персонаж
        }
        else
        {
            Debug.Log("Ячейка занята или закрыта");
        }

    }
    /// <summary>
    /// метод перемещения персонажа по пути из своей позиции на ячейку на поле 
    /// </summary>
    /// <param name="pesrone">Какого персонажа надо переместить</param>
    /// <param name="path">путь перемещения</param>
    public void PersoneMove(GameObject pesrone, List<Vector2> path)
    {

        if (step < path.Count && pesrone.GetComponent<PersoneTest>().movementPoints>0)// не превышает ли количество шагов длину пути (вообще надо?) и есть ли очки передвижения у персонажа
        {
            if (step == 0)
            {
                massiveFields[(int)path[step].x, (int)path[step].y].GetComponent<CellFloorScripts>().closeCell = false; //открытие ячейки начала пути ( где в начале находится персонаж)
            }
                    Debug.Log($"Step = :{step}");
            if (pesrone.gameObject.transform.position.x != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.x || 
                pesrone.gameObject.transform.position.y != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)// проверка не вышли за пределы поля ( перестраховка?)
            {
                pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, massiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);//движение с одной ячейки на другую
                Debug.Log($"persone move");
                pesrone.GetComponent<PersoneTest>().battlePosition = massiveFields[(int)path[step].x, (int)path[step].y].GetComponent<CellFloorScripts>().positiongGrafCellField;//присваевание персанажу позицию графа ячейки на которой он стоит
           
            }
            else
            {
                pesrone.GetComponent<PersoneTest>().movementPoints--;// уменьшение очков движения персонажа
                   step++;// увелечения номера шага
            }
        }
        else
        {
            massiveFields[(int)pesrone.GetComponent<PersoneTest>().battlePosition.x, (int)pesrone.GetComponent<PersoneTest>().battlePosition.y].GetComponent<CellFloorScripts>().closeCell = true;//закрытие ячейки на которую пришел персонаж
            personeMove = false;// персонаж не движеться
            step = 0;// сброс счетчика ходов
            Debug.Log($"Количесто очков передвижения :{pesrone.GetComponent<PersoneTest>().movementPoints}");
        
        }

    }
}
