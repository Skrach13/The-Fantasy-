using System.Collections.Generic;
using UnityEngine;

public class GroupGlobalMap : SingletonBase<GroupGlobalMap>
{
    [SerializeField] private SkillsProperties _skillsTree;
    [SerializeField] private PersoneAssets _personeAssets;
    [SerializeField] private StatsUpExpiriensProperties _statsUpExpiriensProperties;
    [SerializeField] private string _testName;

    [SerializeField] private List<PlayerPersone> _group = new List<PlayerPersone>();

    public List<PlayerPersone> Group { get => _group; private set => _group = value; }
    public StatsUpExpiriensProperties StatsUpExpiriensProperties { get => _statsUpExpiriensProperties; private set => _statsUpExpiriensProperties = value; }

    private void Start()
    {
        if (_personeAssets.Persones.Length == 0)
        {
            Debug.LogError("PersoneAssets not filled in");
            return;
        }
        for (int i = 0; i < _personeAssets.Persones.Length; i++)
        {
            Group.Add(PlayerPersone.CreatePersone(_personeAssets, i));
            //TEST
            _group[i].Skills.Add(KeySkills.AttackMelle,_skillsTree.GetSkill(0));
        }               
    }
    public void AddSkillPersone(string name, KeySkills keySkills)
    {
        if (GetPerosne(name).TrySkill(keySkills) == false)
        {
             GetPerosne(name).Skills.Add(keySkills,_skillsTree.GetSkill(keySkills));
        }
        else
        {
            Debug.Log("этот скилл есть у персонажа");
        }
    }

    public PlayerPersone GetPerosne(string name)
    {
        PlayerPersone perosne = _group.Find(x => x.Name == name);
        return perosne;
    }


}
