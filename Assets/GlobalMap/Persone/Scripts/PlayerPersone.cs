using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerPersone : BasePersone
{
    private Image _image;
    #region Items Persone
    [SerializeField] private ItemBase _rightHandItem;
    [SerializeField] private ItemBase _LeftHandItem;
    [SerializeField] private ItemBase _bodyItem;
    [SerializeField] private ItemBase _necklaceItem;
    [SerializeField] private ItemBase _ringItem;
    [SerializeField] private ItemBase _potionItem;
    public ItemBase RightHandItem { get => _rightHandItem; set => _rightHandItem = value; }
    public ItemBase LeftHandItem { get => _LeftHandItem; set => _LeftHandItem = value; }
    public ItemBase BodyItem { get => _bodyItem; set => _bodyItem = value; }
    public ItemBase NecklaceItem { get => _necklaceItem; set => _necklaceItem = value; }
    public ItemBase RingItem { get => _ringItem; set => _ringItem = value; }
    public ItemBase PotionItem { get => _potionItem; set => _potionItem = value; }
    [SerializeField] private ItemBase[] _items = new ItemBase[6];
    #endregion
    public ItemBase[] Items { get => _items; }


    [SerializeField] private Dictionary<KeySkills, SkillBase> _skills = new();

    public Dictionary<KeySkills, SkillBase> Skills { get => _skills; private set => _skills = value; }
    public Image Image { get => _image; set => _image = value; }

    public bool TryGetSkills(KeySkills keySkills, out SkillBase skill)
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
        p.Image = assets.Persones[index].Image;
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
