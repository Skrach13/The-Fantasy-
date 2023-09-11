using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveDataPlayerGroupOnTheMap
{    
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector2 PositionInGraff;
}


public class PlayerGroupOnTheMap : GroupInMap
{  

    private new void Start()
    {       
        base.Start();
        MoveInMap = GetComponent<MoveInMap>();
        GlobalMapGraf.Instance.OnCellClicked += StartMove;
        _raycast.OnRaycastHit += CheckedAnotherGroup;
        transform.position = GlobalMapGraf.Instance.Cells[(int)MoveInMap.PositionInMap.x, (int)MoveInMap.PositionInMap.y].transform.position;
        //TODO TEST
        if(SaveManager.Save.PlayerGroupOnTheMap != null)
        {
            transform.SetPositionAndRotation(SaveManager.Save.PlayerGroupOnTheMap.Position, SaveManager.Save.PlayerGroupOnTheMap.Rotation);
            MoveInMap.PositionInMap = SaveManager.Save.PlayerGroupOnTheMap.PositionInGraff;
        }
    }
    private void OnDestroy()
    {
        GlobalMapGraf.Instance.OnCellClicked -= StartMove;
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
    public SaveDataPlayerGroupOnTheMap GetSaveDataPlayerGroup()
    {
        SaveDataPlayerGroupOnTheMap saveData = new()
        {
            Position = this.transform.position,
            Rotation = this.transform.rotation,
            PositionInGraff = MoveInMap.PositionInMap
        };
        return saveData;
    }

}


