using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafMapInBatle : MonoBehaviour
{
    [SerializeField] private CellInBattle[,] _cells;   
    [SerializeField] private MainBattleSystems _mainSystem;

    public CellInBattle[,] Cells { get => _cells; set => _cells = value; }
    public event Action<List<CellInBattle>> OnCellClicked;
    private List<Vector2> _path;

    private void Awake()
    {
           
    }
    private void Start()
    {
        OnCellClicked += MainBattleSystems.Instance.StartMove;
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

    private void EnterCellMouse(Vector2 posGraf)
    {
        _path = PathFinderInBattle.Path(_cells, _mainSystem.ActivePersone.BattlePosition, posGraf);
        PathFinderInBattle.PaintPath(_path, _cells, Color.green);
    }
    private void ExitCellMouse()
    {
        PathFinderInBattle.PaintPath(_path, _cells, Color.white);
    }
    private void ClickedCell()
    {
        var _cellInPath = new List<CellInBattle>();
        for (int i = 0; i < _path.Count; i++)
        {
            _cellInPath.Add(_cells[(int)_path[i].x, (int)_path[i].y]);
        }

        OnCellClicked?.Invoke(_cellInPath);
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
                    Cells[i, y].paintCellBattle(Color.white);
                    Cells[i, y].AttackRange = 0;
                }
            }
        }
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
}
