using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;

public enum ActionType
{
    Move = 0,
    Attack = 1,
    PerformsAction = 2//персонаж выполняет какое то действие
}

/// <summary>
/// класс содержащий в себе основную систему отвечающую за бой ( по окончанию написания кода изменить описание)
/// </summary>
public class MainBattleSystems : SingletonBase<MainBattleSystems>
{
    //[SerializeField] private CellInBattle[,] _cells; //массив содержащий ячейки поля
    [SerializeField] private PanelSkillsUI _panelSkillsUI;
    private PersoneGroupsManager _groupsManager;
    private InitiativeManager _initiativeManager;

    private SkillActive _skill;

    public PersoneInBattle Target;
    public PersoneInBattle ActivePersone { get => InitiativeManager.ActivePersone; set => InitiativeManager.ActivePersone = value; }
    public ActionType ActionTypePersone { get => InitiativeManager.ActionTypePersone; set => InitiativeManager.ActionTypePersone = value; }

    public int _step;//шаг в передвижении персонажа
    public bool _personeMove;// перемещаеться ли персонаж
    [SerializeField] private GrafMapInBatle _map;

    public List<Vector2> _path;// путь перемещения содержащее положение ячеек в графе 
    public CellInBattle[,] Cells { get => _map.Cells; }
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
        _panelSkillsUI.OnClickButtonActive += changeAttack;
        Map.OnEnterClell += EnterMouseInCell;
        Map.OnExitCell += ExitMouseInCell;
        Map.OnCellClicked += OnAction;
        _groupsManager.OnPersoneEnterA += OnPersoneEnter;
        _groupsManager.OnPersoneExitA += OnPersoneExit;
        _groupsManager.OnPersoneClickedA += OnPersoneCkliked;

    }
    private void OnDestroy()
    {
        _panelSkillsUI.OnClickButtonActive -= changeAttack;

        Map.OnEnterClell -= EnterMouseInCell;
        Map.OnExitCell -= ExitMouseInCell;
        Map.OnCellClicked -= OnAction;

        _groupsManager.OnPersoneEnterA -= OnPersoneEnter;
        _groupsManager.OnPersoneExitA -= OnPersoneExit;
        _groupsManager.OnPersoneClickedA -= OnPersoneCkliked;
    }

    private void EnterMouseInCell(CellInBattle cell)
    {
        //проверка неходит ли сейчас персонаж и закрыта ли ячейка  
        if (!_personeMove && !cell.CloseCell && (ActionTypePersone == 0))
        {
            _path = Map.GetPathVector(ActivePersone.BattlePosition, cell.PositionInGraff);
            PathFinderInBattle.PaintPath(_path, Cells, ColorsCell.PaintPath);
        }
        if (!_personeMove && !cell.CloseCell && ((int)ActionTypePersone == 1))
        {
            _skill.DefiningAreaEffectSkill(cell);
        }

    }
    private void ExitMouseInCell(CellInBattle cell)
    {
        if (!_personeMove && (ActionTypePersone == 0))
        {
            PathFinderInBattle.PaintPath(_path, Cells, ColorsCell.None);
        }
    }
    public void OnAction(CellInBattle cell)
    {

        if (ActionTypePersone == ActionType.Attack)
        {
            if (cell.CloseCell)
            {
                _skill.UseSkill(cell);
            }
        }
        else
        {
            StartMove(Map.GetCellsInPath(ActivePersone.BattlePosition, cell.PositionInGraff));
        }
    }
    public void StartMove(List<CellInBattle> path)
    {
        StartCoroutine(ActivePersone.Move.PersoneMove(path, Cells));
    }

    public void changeAttack(SkillActive skill)
    {
        if (!(ActionTypePersone == ActionType.Attack))
        {
            _skill = skill;
            ActionTypePersone = ActionType.Attack;
            _skill.DefiningScope(ActivePersone, Cells);
        }
        else
        {
            ActionTypePersone = ActionType.Move;
            Map.ResetStatsCellFields();
        }
    }

    public void OnPersoneEnter(PersoneInBattle persone)
    {
        if (!_personeMove && (ActionTypePersone == 0))
        {
            var neighbortPosition = DefiningArea.NeighborCellToAttack(Cells, ActivePersone, persone, (SkillActive)ActivePersone.GetSkill(KeySkills.AttackMelle));
            _path = Map.GetPathVector(ActivePersone.BattlePosition, neighbortPosition.PositionInGraff);
            PathFinderInBattle.PaintPath(_path, Cells, ColorsCell.PaintPath);
        }
        if (!_personeMove && ((int)ActionTypePersone == 1))
        {
            _skill.DefiningAreaEffectSkill(Cells[(int)persone.BattlePosition.x, (int)persone.BattlePosition.y]);
        }
    }
    public void OnPersoneExit(PersoneInBattle persone)
    {
        if (!_personeMove && (ActionTypePersone == 0))
        {
            Map.ResetStatsCellFields();
        }
    }
    public void OnPersoneCkliked(PersoneInBattle persone)
    {           
        if (!_personeMove && ActionTypePersone == ActionType.Attack)
        {
            _skill.UseSkill(persone);
        }
        else if(!_personeMove && ActionTypePersone == ActionType.Move)
        {
            Map.ResetStatsCellFields();
            var neighbortPosition = DefiningArea.NeighborCellToAttack(Cells, ActivePersone, persone, (SkillActive)ActivePersone.GetSkill(KeySkills.AttackMelle));
            StartMove(Map.GetCellsInPath(ActivePersone.BattlePosition, neighbortPosition.PositionInGraff));
        }
    }

}
