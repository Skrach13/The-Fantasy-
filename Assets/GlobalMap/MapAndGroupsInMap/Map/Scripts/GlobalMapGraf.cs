using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapGraf : MonoBehaviour
{
    [SerializeField] private GlobalMapCell[,] _cells;
    [SerializeField] private PlayerGroupOnTheMap _groupGlobal;

    public GlobalMapCell[,] Cells { get => _cells; set => _cells = value; }
    public event Action<List<GlobalMapCell>> OnCellClicked;
    private List<Vector2> _path;

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
    }

    private void EnterCellMouse(Vector2 posGraf)
    {
        _path = PathFinder.Path(_cells, _groupGlobal.PositionInMap, posGraf);
        PathFinder.PaintPath(_path, _cells, Color.green);
    }
    private void ExitCellMouse()
    {
        PathFinder.PaintPath(_path, _cells, Color.yellow);
    }
    private void ClickedCell()
    {
        var _cellInPath = new List<GlobalMapCell>(); 
        for (int i = 0; i < _path.Count; i++)
        {
            _cellInPath.Add(_cells[(int)_path[i].x, (int)_path[i].y]);
        }

        OnCellClicked?.Invoke(_cellInPath);
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
