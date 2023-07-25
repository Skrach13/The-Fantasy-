using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroupOnTheMap : MonoBehaviour
{
    [SerializeField] private GlobalMapGraf _globalMapGraf;
    [SerializeField] private MoveInMap _moveInMap;

    public MoveInMap MoveInMap { get => _moveInMap;private set => _moveInMap = value; }

    private void Start()
    {
        MoveInMap = GetComponent<MoveInMap>();
        _globalMapGraf.OnCellClicked += StartMove;
    }

    private void StartMove(List<CellBase> cells)
    {
        MoveInMap.StartMove(cells);
    }
}


