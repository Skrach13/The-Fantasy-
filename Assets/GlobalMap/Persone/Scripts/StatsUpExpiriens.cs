using UnityEngine;

public class StatsUpExpiriens : SingletonBase<StatsUpExpiriens>
{
    [SerializeField] private int[] _upExpiriensStat;

    public int[] UpExpiriensStat { get => _upExpiriensStat; private set => _upExpiriensStat = value; }
}
