using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrafMapInBatle : MonoBehaviour
{
    [SerializeField] private CellInBattle[,] _cells;
    [SerializeField] private MainBattleSystems _mainSystem;

    public CellInBattle[,] Cells { get => _cells; set => _cells = value; }
    public event Action<CellInBattle> OnEnterClell;
    public event Action<CellInBattle> OnExitCell;
    public event Action<CellInBattle> OnCellClicked;
    private List<Vector2> _path;

    private void Awake()
    {

    }
    private void Start()
    {
       // OnCellClicked += MainBattleSystems.Instance.OnAction;
    }
    private void OnDestroy()
    {
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                if (_cells[x, y] != null)
                {
                    _cells[x, y].OnCellEnter -= EnterCellMouse;
                    _cells[x, y].OnCellExit -= ExitCellMouse;
                    _cells[x, y].OnClickedCell -= ClickedCell;
                }
            }

        }
    }

    public void AddListner()
    {
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                if (_cells[x, y] != null)
                {
                    _cells[x, y].OnCellEnter += EnterCellMouse;
                    _cells[x, y].OnCellExit += ExitCellMouse;
                    _cells[x, y].OnClickedCell += ClickedCell;
                }
            }
        }

        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                if (_cells[x, y] != null)
                {
                    _cells[x, y].MainSystemBattleScript = _mainSystem;

                }
            }
        }
    }

    private void EnterCellMouse(CellInBattle cell)
    {        
        OnEnterClell?.Invoke(cell);
    }
    private void ExitCellMouse(CellInBattle cell)
    {        
        OnExitCell?.Invoke(cell);
    }
    private void ClickedCell(CellInBattle cell)
    {       
        OnCellClicked?.Invoke(cell);
    }


    public List<CellInBattle> GetCellsInPath(Vector2 startPosition, Vector2 targetPosition)
    {
        List<Vector2> path = GetPathVector(startPosition, targetPosition);
        var _cellInPath = new List<CellInBattle>();
        for (int i = 0; i < path.Count; i++)
        {
            _cellInPath.Add(_cells[(int)path[i].x, (int)path[i].y]);
        }
        return _cellInPath;
    }

    public List<Vector2> GetPathVector(Vector2 startPosition , Vector2 targetPosition)
    {
        var path = PathFinderInBattle.Path(_cells, startPosition, targetPosition);
        return path;
    }

    /// <summary>
    /// —брос цвета €чейки и попадани€ в радиус атаки
    /// </summary>
    public void ResetStatsCellFields()
    {
        for (int i = 0; i < Cells.GetLength(0); i++)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                if (Cells[i, y] != null)
                {
                    Cells[i, y].PaintCellBattle(ColorsCell.None);
                    Cells[i, y].AttackRange = 0;
                }
            }
        }
    }

}
