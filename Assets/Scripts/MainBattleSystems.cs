using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// класс содержащий в себе основную систему отвечающую за бой ( по окончанию написания кода изменить описание)
/// </summary>
public class MainBattleSystems : MonoBehaviour
{

    public int widhtField; // ширина поля в количестве ячеек
    public int heightField; // высота поля в коолистве ячеек
    public GameObject PrefloorUnit;// префаб ячейки поля (изменить название переменной?)
    public CellFloorScripts[,] massiveFields; //массив содержащий ячейки поля

    public List<APersoneScripts> massivePersoneInBattle;// массив для системы инициативы ( изменен на List<>)
    public GameObject[] massiveBattlePlayerPersone;//массив для персонажей Игрока ( скорее всего будет изменен на List<>)
    public GameObject[] massiveBattleEnemyPersone;//массив для персонажей Противников ( скорее всего будет изменен на List<>)
    public GameObject testPreFabPlayer;//префаб персонажа(будет изменено)
    public GameObject testPlayer;//ссылка на персонажа игрока
    public PersoneTest testPlayerScript;//ссылка на скрипт персонажа
    public GameObject testPreFabEnemy;//префаб противника(будет изменено)
    public GameObject testEnemy;// ссылка на противника
    public EnemyTest testEnemyScript;//ссылка на скрипт противника
    public APersoneScripts activePersone;
    public APersoneScripts target;
    public bool playerTurn;
    private int countPersoneIsRound;
    public ActionType actionTypePersone;

    public Vector2 newPosition;//позиция ячейки для установки персонажа (будет изменено)
                               //  public Vector2 positionPersone;//

    public int step;//шаг в передвижении персонажа
    public bool personeMove;// перемещаеться ли персонаж
    public List<Vector2> path;// путь перемещения содержащее положение ячеек в графе 

    private void Awake()
    {
        massiveFields = BattlefieldGeneration.generateFields(widhtField, heightField, PrefloorUnit, gameObject);//создание поле боя
        massivePersoneInBattle = new List<APersoneScripts>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // massiveBattlePlayerPersone = new GameObject[1];// пока затычка для тестов
        // massiveBattleEnemyPersone = new GameObject[1];// пока затычка для тестов
     
        CreatePersoneInBattle(APersoneScripts.PersoneType.Player,12,new Vector2(2,8));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Player,15,new Vector2(2,5));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Player,7,new Vector2(2,2));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Enemy,11,new Vector2(8,8));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Enemy,13,new Vector2(8,5));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Enemy,8,new Vector2(8,2));

        SortIniciative();
       
        activePersone = massivePersoneInBattle[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// метод для установки персонажа на поле боя (применение этим не ограничено) 
    /// </summary>
    /// <param name="positionSet"> позиция на поле ссылающее на ячейку в массиве поля </param>
    /// <param name="fieldmap">массив поля боя</param>
    /// <param name="persone">персонаж которого надо поставить на поле</param>
    public void SetPositionPersone(Vector2 positionSet, CellFloorScripts[,] fieldmap, APersoneScripts persone)
    {
        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];//локалная ссылочная переменая на ячейку поля
                                                                         // var cellFieldScript = cellField.GetComponent<CellFloorScripts>();//локальная ссылка на скрипт ячейки поля
        if (!cellField.closeCell) // проверка не занята ли 
        {
            var position = cellField.transform.position;//локальная переменая, координаты ячейки на экране( что то вроде того НЕ В ГРАФЕ)
            position.z = positionSet.y;// установка Z для коеретной отрисовки ( что бы обьекты друг друга перекрывали привильно) НЕ ПРОВЕРЕНО!!!
            persone.battlePosition = cellField.positiongGrafCellField;//присваивание персонажу позиции ячейки в графе на которой он стоит
            persone.transform.position = position;//установка персонажу координат на экране
            cellField.closeCell = true;//закрытие ячейки на которой стоит персонаж
            cellField.personeStayInCell = persone;//закрытие ячейки на которой стоит персонаж
        }
        else
        {
            Debug.Log("Ячейка занята или закрыта");
        }
    }
    /// <summary>
    /// метод перемещения персонажа по пути из своей позиции на ячейку на поле 
    /// путь всегда храниться в MainBattleSystemScripts
    /// </summary>
    /// <param name="pesrone">Какого персонажа надо переместить</param>
    public IEnumerator PersoneMove(APersoneScripts pesrone)
    {
        personeMove = true;
        int step = 0;
            massiveFields[(int)path[step].x, (int)path[step].y].closeCell = false; //открытие ячейки начала пути ( где в начале находится персонаж)
            massiveFields[(int)path[step].x, (int)path[step].y].personeStayInCell = null;
        step++;
        while (true)
        {

            if (step < path.Count && pesrone.actionPoints > 0)// не превышает ли количество шагов длину пути (вообще надо?да надо зачем хз) и есть ли очки передвижения у персонажа
            {
                if (step == 0)
                {
                    massiveFields[(int)path[step].x, (int)path[step].y].closeCell = false; //открытие ячейки начала пути ( где в начале находится персонаж)
                    massiveFields[(int)path[step].x, (int)path[step].y].personeStayInCell = null;
                }
                if (pesrone.gameObject.transform.position.x != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.x ||
                    pesrone.gameObject.transform.position.y != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)// проверка не вышли за пределы поля ( перестраховка?)
                {
                    pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, massiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);//движение с одной ячейки на другую
                    pesrone.battlePosition = massiveFields[(int)path[step].x, (int)path[step].y].positiongGrafCellField;//присваевание персанажу позицию графа ячейки на которой он стоит
                    yield return null;
                }
                else
                {
                    pesrone.actionPoints--;// уменьшение очков движения персонажа
                    step++;// увелечения номера шага
                }
            }
            else
            {
                massiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].closeCell = true;//закрытие ячейки на которую пришел персонаж
                massiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].personeStayInCell = pesrone;
                personeMove = false;// персонаж не движеться
                step = 0;// сброс счетчика ходов
                ResetStatsCellFields();
                yield break;

            }

        }
    } 
    public IEnumerator PersoneMove(APersoneScripts pesrone, List<Vector2> path)//затычка для ИИ
    {
        
        int step = 0;
            massiveFields[(int)path[step].x, (int)path[step].y].closeCell = false; //открытие ячейки начала пути ( где в начале находится персонаж)
            massiveFields[(int)path[step].x, (int)path[step].y].personeStayInCell = null;
        step++;
        while (true)
        {

            if (step < path.Count && pesrone.actionPoints > 0)// не превышает ли количество шагов длину пути (вообще надо?да надо зачем хз) и есть ли очки передвижения у персонажа
            {
                if (step == 0)
                {
                    massiveFields[(int)path[step].x, (int)path[step].y].closeCell = false; //открытие ячейки начала пути ( где в начале находится персонаж)
                    massiveFields[(int)path[step].x, (int)path[step].y].personeStayInCell = null;
                }
                if (pesrone.gameObject.transform.position.x != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.x ||
                    pesrone.gameObject.transform.position.y != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)// проверка не вышли за пределы поля ( перестраховка?)
                {
                    pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, massiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);//движение с одной ячейки на другую
                    pesrone.battlePosition = massiveFields[(int)path[step].x, (int)path[step].y].positiongGrafCellField;//присваевание персанажу позицию графа ячейки на которой он стоит
                    yield return null;
                }
                else
                {
                    pesrone.actionPoints--;// уменьшение очков движения персонажа
                    step++;// увелечения номера шага
                }
            }
            else
            {
                massiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].closeCell = true;//закрытие ячейки на которую пришел персонаж
                massiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].personeStayInCell = pesrone;
                personeMove = false;// персонаж не движеться
                step = 0;// сброс счетчика ходов
                ResetStatsCellFields();
                yield break;

            }

        }
    }

    public void NextPersoneIniciative()
    {
        countPersoneIsRound++;

        if (countPersoneIsRound >= massivePersoneInBattle.Count)
        {
            countPersoneIsRound = 0;
        }

        activePersone = massivePersoneInBattle[countPersoneIsRound];
        activePersone.ResetPointActioneStartTurn();
        actionTypePersone = ActionType.Move;
        ResetStatsCellFields();
        Debug.Log(activePersone);
        if(activePersone.personeType == APersoneScripts.PersoneType.Enemy)
        {
            StartCoroutine(AIBattle.AIaction(activePersone));
        }

    }
    /// <summary>
    /// Сброс цвета ячейки и попадания в радиус атаки
    /// </summary>
    public void ResetStatsCellFields()
    {
        for (int i = 0; i < massiveFields.GetLength(0); i++)
        {
            for (int y = 0; y < massiveFields.GetLength(1); y++)
            {
                massiveFields[i, y].paintCellBattle(Color.white);
                massiveFields[i, y].attackRange = 0;
            }
        }
    }

    /// <summary>
    /// Переключение на атаку с визуальным подсвечеванием радиуса атаки и определением доступных для атки врагов(через кнопку)
    /// </summary>
    public void changeAttack()
    {
        if (!(actionTypePersone == ActionType.Attack))
        {
            actionTypePersone = ActionType.Attack;
            AreaAttack.AttackArea(massiveFields, activePersone);
        }
        else
        {
            actionTypePersone = ActionType.Move;
            ResetStatsCellFields();
        }
    }

    /// <summary>
    /// сортировка массива инициативы где первый с самой большой инициативой
    /// </summary>
    public void SortIniciative()
    {
       //хер его знает как это работает
        massivePersoneInBattle.Sort(delegate (APersoneScripts x, APersoneScripts y) {
            return y.iniciative.CompareTo(x.iniciative);
        });
    }

    public void CreatePersoneInBattle(APersoneScripts.PersoneType personeType,int iniciative,Vector2 startPosition)
    {
        var prefab = new GameObject();
        if (personeType == APersoneScripts.PersoneType.Player) 
        {
            prefab = Instantiate(testPreFabPlayer);// создание персонажа "игрока"
        }
        if(personeType == APersoneScripts.PersoneType.Enemy) 
        {
            prefab = Instantiate(testPreFabEnemy);// создание персонажа противника
        }
            APersoneScripts personeScript;
            personeScript = prefab.GetComponent<APersoneScripts>();// пока затычка для тестов
            personeScript.mainSystemBattleScript = this;//пока затычка для тестов для присваивания положения на поле
            personeScript.iniciative = iniciative;
            massivePersoneInBattle.Add(personeScript);// пока затычка для тестов
            SetPositionPersone(startPosition, massiveFields, personeScript); //пока затычка для тестов
       
    }
    /// <summary>
    /// перечесление действий в бою
    /// </summary>
    public enum ActionType
    {
        Move,
        Attack,
        PerformsAction//персонаж выполняет какое то действие
    }
}
