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

    private PersoneGroupsManager _groupsManager;
    private InitiativeManager _initiativeManager;

    public PersoneInBattle Target;
    public PersoneInBattle ActivePersone { get => InitiativeManager.ActivePersone; set => InitiativeManager.ActivePersone = value; }
    public ActionType ActionTypePersone { get => InitiativeManager.ActionTypePersone; set => InitiativeManager.ActionTypePersone = value; }

    public int _step;//шаг в передвижении персонажа
    public bool _personeMove;// перемещаеться ли персонаж
    [SerializeField] private GrafMapInBatle _map;

    public List<Vector2> _path;// путь перемещения содержащее положение ячеек в графе 
    public CellInBattle[,] Cells { get => _map.Cells;}
    public List<PersoneInBattle> MassivePersoneInBattle { get => _groupsManager.MassivePersoneInBattle; }
    public GrafMapInBatle Map { get => _map; set => _map = value; }
    public InitiativeManager InitiativeManager { get => _initiativeManager; set => _initiativeManager = value; }

    private new void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    void Start()
    {
        _groupsManager = GetComponent<PersoneGroupsManager>();
        InitiativeManager = GetComponent<InitiativeManager>();

        // Map.OnCellClicked += StartMove;
        //_groupsManager.SortIniciative();
       // _initiativeManager.NextPersoneIniciative();
        //ActivePersone = MassivePersoneInBattle[0];
    }
    private void OnDestroy()
    {
        Map.OnCellClicked += StartMove;
    }

    public void StartMove(List<CellInBattle> path)
    {
        StartCoroutine(ActivePersone.Move.PersoneMove(path, Cells));
    }

    public void changeAttack()
    {
        if (!(ActionTypePersone == ActionType.Attack))
        {
            ActionTypePersone = ActionType.Attack;
            AreaAttack.AttackArea(Cells, ActivePersone);
        }
        else
        {
            ActionTypePersone = ActionType.Move;
            Map.ResetStatsCellFields();
        }
    }

    public enum ActionType
    {
        Move,
        Attack,
        PerformsAction//персонаж выполняет какое то действие
    }
}
