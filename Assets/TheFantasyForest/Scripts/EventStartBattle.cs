using UnityEngine;

[CreateAssetMenu (fileName = "EventStartBattle",menuName = "EventScene/Event/StartBattle")]
public class EventStartBattle : GameEvent
{
    [SerializeField] private EnemyProperties[] _enemys;
    [SerializeField] private GameEvent _gameEvent;
    public override void StartEvent()
    {
        BattleData battledata = FindFirstObjectByType<BattleData>();
        EventSceneManager eventSceneManager = FindFirstObjectByType<EventSceneManager>();
        if (_gameEvent != null)
        {
            eventSceneManager.GameEvent = _gameEvent;
        }
        battledata.StartBattle(_enemys);

    }
}
