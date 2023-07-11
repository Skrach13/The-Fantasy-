using System;

[Serializable]
public class Stat
{

    public string Name;
    public EStats Stats;
    public int Value;
    public int _upExperience;
    private int _neededExperience;
        
    public Stat(int value,  int eStats)
    {
        Stats = (EStats)eStats;
        Name = Stats.ToString();
        Value = value;
    }
        
    public int UpExperience
    {
        get => _upExperience;
        set
        {
            _upExperience = value;
            if (_upExperience >= _neededExperience)
            {
                Value++;
            }
            _upExperience = _upExperience - _neededExperience;
            NeededExperience = GroupGlobalMap.Instance.StatsUpExpiriensProperties.UpExpiriensStat[Value];
        }
    }
    public int NeededExperience { get => _neededExperience; set => _neededExperience = value; }
}
