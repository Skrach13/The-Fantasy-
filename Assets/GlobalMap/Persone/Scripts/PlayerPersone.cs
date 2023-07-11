using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerPersone : BasePersone
{
    [SerializeField] private Dictionary<KeySkills, SkillBase> _skills = new();

    public Dictionary<KeySkills,SkillBase> Skills { get => _skills; private set => _skills = value; }

    public bool TryGetSkills(KeySkills keySkills,out SkillBase skill )
    {
        bool beSkill = _skills.TryGetValue(keySkills, out skill);
        return beSkill;
    }
    public bool TrySkill(KeySkills keySkills)
    {
        bool beSkill = _skills.TryGetValue(keySkills, out var skill);
        return beSkill;
    }

    public static PlayerPersone CreatePersone(PersoneAssets assets, int index)
    {
        var p = new PlayerPersone();
        p.Name = assets.Persones[index].Name;
        for (int i = 0; i < p.Stats.Length; i++)
        {
            p.Stats[i] = new Stat(i, i);
            p.Stats[i].Value = assets.Persones[index].Stats[i].Value;
            if (p.Stats[i].Stats != EStats.MaxHealth)
            {               
                p.Stats[i].NeededExperience = GroupGlobalMap.Instance.StatsUpExpiriensProperties.UpExpiriensStat[p.Stats[i].Value];
            }
        }
        p.Description = assets.Persones[index].Description;
        return p;
    }

    public static PlayerPersone CreatePersone(PersoneAssets assets, string name)
    {
        var p = new PlayerPersone();
        var asset = assets.GetPerosne(name);
        p.Name = asset.Name;
        for (int i = 0; i < p.Stats.Length; i++)
        {
            p.Stats[i].Value = asset.Stats[i].Value;
        }
        p.Description = asset.Description;
        return p;
    }
       
}
