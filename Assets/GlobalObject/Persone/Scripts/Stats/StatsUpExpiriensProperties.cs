using UnityEngine;

[CreateAssetMenu (fileName = "StatsUpExpiriensProperties",menuName = "Persone/StatsUpExpiriensProperties")]
public class StatsUpExpiriensProperties : ScriptableObject
{
    [SerializeField] private int[] _upExpiriensStat;

    public int[] UpExpiriensStat { get => _upExpiriensStat; private set => _upExpiriensStat = value; }
}
