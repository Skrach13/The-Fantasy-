using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public class SaveDataEnemyGroups
{
    public List<DataEnemyGroup> groups = new();
}

[Serializable]
public class DataEnemyGroup
{
    public bool IsBattle;

    public Vector3 Position;
    public Quaternion Rotation;

    public Vector2 PositionInMap;
    public EnemyProperties[] EnemysProperties;

    public List<CellBase> RandomGrafWalk;
    public int CountRangeWalk;
}

public class EnemyGroupsManager : MonoBehaviour
{
    [SerializeField] private bool _test;
    [SerializeField] private EnemiesGroupInMap _prefabEnemiesGroup;
    [SerializeField] private EnemyProperties[] _enemies;
    [SerializeField] private int _countRangeWalk;
    [SerializeField] private Vector2 _positionGroup;

    private void Start()
    {
        SaveManager.Instance.EnemyGroupsManager = this;

        if (SaveManager.Save != null)
        {
            foreach (var loadGroup in SaveManager.Save.EnemyGroups.groups)
            {
                var newGroup = Instantiate(_prefabEnemiesGroup, this.transform);
                newGroup.transform.SetPositionAndRotation(loadGroup.Position, loadGroup.Rotation);
                newGroup.PositionInMap = loadGroup.PositionInMap;
                newGroup.Enemies = loadGroup.EnemysProperties;
                newGroup.RandomGrafWalk = loadGroup.RandomGrafWalk;
                newGroup.CountRangeWalk = loadGroup.CountRangeWalk;
            }
        }
        if(_test)
        {
            var newGroup = Instantiate(_prefabEnemiesGroup, this.transform);
            newGroup.PositionInMap = _positionGroup;
            newGroup.Enemies = _enemies;            
            newGroup.CountRangeWalk = _countRangeWalk;
            newGroup.transform.position = GlobalMapGraf.Instance.Cells[(int)_positionGroup.x, (int)_positionGroup.y].transform.position;
        }
    }

    public SaveDataEnemyGroups GetSaveDataEnemyGroups()
    {
        SaveDataEnemyGroups enemyGroups = new();
        var groups = GetComponentsInChildren<EnemiesGroupInMap>();
        foreach (var group in groups)
        {
            DataEnemyGroup enemyGroup = new()
            {
                Position = group.transform.position,
                Rotation = group.transform.rotation,
                EnemysProperties = group.Enemies,
                PositionInMap = group.MoveInMap.PositionInMap,
                RandomGrafWalk = group.RandomGrafWalk,
                CountRangeWalk = group.CountRangeWalk,
                IsBattle = group.IsBattle
            };
            enemyGroups.groups.Add(enemyGroup);
        }
        return enemyGroups;
    }
}
