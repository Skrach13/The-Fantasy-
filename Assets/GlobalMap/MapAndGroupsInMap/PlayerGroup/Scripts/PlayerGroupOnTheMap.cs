using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroupOnTheMap : GroupInMap
{
    public MoveInMap MoveInMap { get => _moveInMap; private set => _moveInMap = value; }

    private new void Start()
    {
        base.Start();
        MoveInMap = GetComponent<MoveInMap>();
        _mapGraf.OnCellClicked += StartMove;
        _raycast.OnRaycastHit += CheckedAnotherGroup;
    }
    private void OnDestroy()
    {
        _mapGraf.OnCellClicked -= StartMove;
        _raycast.OnRaycastHit -= CheckedAnotherGroup;
    }
    private void CheckedAnotherGroup(GroupInMap group)
    {
        Debug.Log($"{group.name}");
        BattleData.Instance.StartBattle((group as EnemiesGroupInMap).Enemies);
    }
    private void StartMove(List<CellBase> cells)
    {
        MoveInMap.StartMove(cells);
    }
}


