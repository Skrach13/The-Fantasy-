using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;

/// <summary>
/// класс содержащий в себе основную систему отвечающую за бой ( по окончанию написания кода изменить описание)
/// </summary>
public class MainBattleSystems : SingletonBase<MainBattleSystems>
{
    [SerializeField] private CellInBattle[,] _cells; //массив содержащий ячейки поля

    [SerializeField] private List<PersoneInBattle> _massivePersoneInBattle;// массив для системы инициативы ( изменен на List<>)   
    public GameObject _testPreFabPlayer;//префаб персонажа(будет изменено)
    public GameObject _testPreFabEnemy;//префаб противника(будет изменено)
    
    public PersoneInBattle _activePersone;
    public PersoneInBattle _target;
    
    public bool _playerTurn;
    private int _countPersoneIsRound;
    public ActionType _actionTypePersone;

    public Vector2 _newPosition;//позиция ячейки для установки персонажа (будет изменено)
                                //  public Vector2 positionPersone;//
    public int _step;//шаг в передвижении персонажа
    public bool _personeMove;// перемещаеться ли персонаж
    [SerializeField] private GrafMapInBatle _map;

    public List<Vector2> _path;// путь перемещения содержащее положение ячеек в графе 
    public CellInBattle[,] Cells { get => _cells; set => _cells = value; }
    public List<PersoneInBattle> MassivePersoneInBattle { get => _massivePersoneInBattle; set => _massivePersoneInBattle = value; }
    public GrafMapInBatle Map { get => _map; set => _map = value; }

    private new void Awake()
    {
        base.Awake();
        MassivePersoneInBattle = new List<PersoneInBattle>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        Map.OnCellClicked += StartMove;

        CreatePersoneInBattle(PersoneType.Player, 12, new Vector2(2, 8));
        CreatePersoneInBattle(PersoneType.Player, 15, new Vector2(2, 5));
        CreatePersoneInBattle(PersoneType.Player, 7, new Vector2(2, 2));
        CreatePersoneInBattle(PersoneType.Enemy, 11, new Vector2(8, 8));
        CreatePersoneInBattle(PersoneType.Enemy, 13, new Vector2(8, 5));
        CreatePersoneInBattle(PersoneType.Enemy, 8, new Vector2(8, 2));

        SortIniciative();

        _activePersone = MassivePersoneInBattle[0];
    }
    private void OnDestroy()
    {        
        Map.OnCellClicked += StartMove;
    }

    private void StartMove(List<CellInBattle> path)
    {
       StartCoroutine(_activePersone.Move.PersoneMove(path , Cells));
    }
    /// <summary>
    /// метод для установки персонажа на поле боя (применение этим не ограничено) 
    /// </summary>
    /// <param name="positionSet"> позиция на поле ссылающее на ячейку в массиве поля </param>
    /// <param name="fieldmap">массив поля боя</param>
    /// <param name="persone">персонаж которого надо поставить на поле</param>
    public void SetPositionPersone(Vector2 positionSet, CellInBattle[,] fieldmap, PersoneInBattle persone)
    {
        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];//локалная ссылочная переменая на ячейку поля
                                                                         // var cellFieldScript = cellField.GetComponent<CellFloorScripts>();//локальная ссылка на скрипт ячейки поля
        if (!cellField.CloseCell) // проверка не занята ли 
        {
            var position = cellField.transform.position;//локальная переменая, координаты ячейки на экране( что то вроде того НЕ В ГРАФЕ)
            position.z = positionSet.y;// установка Z для коеретной отрисовки ( что бы обьекты друг друга перекрывали привильно) НЕ ПРОВЕРЕНО!!!
            persone.BattlePosition = cellField.PositionInGraff;//присваивание персонажу позиции ячейки в графе на которой он стоит
            persone.transform.position = position;//установка персонажу координат на экране
            cellField.CloseCell = true;//закрытие ячейки на которой стоит персонаж
            cellField.PersoneStayInCell = persone;//закрытие ячейки на которой стоит персонаж
        }
        else
        {
            Debug.Log("Ячейка занята или закрыта");
        }
    } 

    public void NextPersoneIniciative()
    {
        _countPersoneIsRound++;

        if (_countPersoneIsRound >= MassivePersoneInBattle.Count)
        {
            _countPersoneIsRound = 0;
        }

        _activePersone = MassivePersoneInBattle[_countPersoneIsRound];
        _activePersone.ResetPointActioneStartTurn();
        _actionTypePersone = ActionType.Move;
        Map.ResetStatsCellFields();
        Debug.Log(_activePersone);
        if (_activePersone.PersoneType == PersoneType.Enemy)
        {
            StartCoroutine(BotInBattle.BotAction(_activePersone));
        }

    }

    /// <summary>
    /// Переключение на атаку с визуальным подсвечеванием радиуса атаки и определением доступных для атки врагов(через кнопку)
    /// </summary>
    public void changeAttack()
    {
        if (!(_actionTypePersone == ActionType.Attack))
        {
            _actionTypePersone = ActionType.Attack;
            AreaAttack.AttackArea(Cells, _activePersone);
        }
        else
        {
            _actionTypePersone = ActionType.Move;
            Map.ResetStatsCellFields();
        }
    }

    /// <summary>
    /// сортировка массива инициативы где первый с самой большой инициативой
    /// </summary>
    public void SortIniciative()
    {
        //хер его знает как это работает
        MassivePersoneInBattle.Sort(delegate (PersoneInBattle x, PersoneInBattle y)
        {
            return y.Iniciative.CompareTo(x.Iniciative);
        });
    }

    public void CreatePersoneInBattle(PersoneType personeType, int iniciative, Vector2 startPosition)
    {
        GameObject prefab = null;
        if (personeType == PersoneType.Player)
        {
            prefab = Instantiate(_testPreFabPlayer);// создание персонажа "игрока"
        }
        if (personeType == PersoneType.Enemy)
        {
            prefab = Instantiate(_testPreFabEnemy);// создание персонажа противника
        }
        PersoneInBattle personeScript;
        personeScript = prefab.GetComponent<PersoneInBattle>();// пока затычка для тестов        
        personeScript.Iniciative = iniciative;
        MassivePersoneInBattle.Add(personeScript);// пока затычка для тестов
        SetPositionPersone(startPosition, Cells, personeScript); //пока затычка для тестов

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
