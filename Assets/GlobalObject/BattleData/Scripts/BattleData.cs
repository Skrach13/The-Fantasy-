using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : SingletonBase<BattleData>
{
    [SerializeField] private EnemyProperties[] _enemyProperties;
    public EnemyProperties[] EnemyProperties { get => _enemyProperties; set => _enemyProperties = value; }

    public void StartBattle(EnemyProperties[] enemies)
    {
        _enemyProperties = enemies;
        SceneHelper.Instance.LoadLevel(2);
    }
}
