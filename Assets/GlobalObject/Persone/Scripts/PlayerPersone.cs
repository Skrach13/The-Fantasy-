using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerPersone : BasePersone
{

    private Sprite _sprite;
    public Sprite Icon;
    #region Items Persone
    [SerializeField] private SlotItem _rightHandItem = new();
    [SerializeField] private SlotItem _LeftHandItem = new();
    [SerializeField] private SlotItem _bodyItem = new();
    [SerializeField] private SlotItem _necklaceItem = new();
    [SerializeField] private SlotItem _ringItem = new();
    [SerializeField] private SlotItem _potionItem = new();
    public SlotItem RightHandItem { get => _rightHandItem; set => _rightHandItem = value; }
    public SlotItem LeftHandItem { get => _LeftHandItem; set => _LeftHandItem = value; }
    public SlotItem BodyItem { get => _bodyItem; set => _bodyItem = value; }
    public SlotItem NecklaceItem { get => _necklaceItem; set => _necklaceItem = value; }
    public SlotItem RingItem { get => _ringItem; set => _ringItem = value; }
    public SlotItem PotionItem { get => _potionItem; set => _potionItem = value; }
    #endregion

    [SerializeField] private List<SkillBase> _skills = new();
    internal RuntimeAnimatorController AnimationsInBattle;

    public List<SkillBase> Skills { get => _skills; private set => _skills = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }

    public bool TryGetSkills(KeySkills keySkills, out SkillBase skill)
    {
        bool beSkill = false;
        skill = _skills.First(skills => skills.KeySkill == keySkills);
        if (skill != null)
        {
            beSkill = true;
        }
        Debug.Log($"{skill.Name}");
        return beSkill;
    }
    public SkillBase GetSkills(KeySkills keySkills)
    {
        var skill = _skills.First(skills => skills.KeySkill == keySkills);
        return skill;
    }
    public bool TrySkill(KeySkills keySkills)
    {
        bool beSkill = false; 
            if(_skills.First(skills => skills.KeySkill == keySkills) != null)
        {
            beSkill = true;
        }
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
                p.Stats[i].NeededExperience = PlayerGroupGlobal.Instance.StatsUpExpiriensProperties.UpExpiriensStat[p.Stats[i].Value];
            }
        }
        p.Icon = assets.Persones[index].IconInBattle;
        p.Sprite = assets.Persones[index].Sprite;
        p.AnimationsInBattle = assets.Persones[index].AnimatorController;
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
