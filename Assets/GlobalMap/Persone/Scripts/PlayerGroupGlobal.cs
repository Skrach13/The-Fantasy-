using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedPlayerPersoneGroup 
{
    public string namegroup = "group";
    public List<PlayerPersone> Group;
}

public class PlayerGroupGlobal : SingletonBase<PlayerGroupGlobal>
{
    [SerializeField] private SkillsProperties _skillsTree;
    [SerializeField] private PersoneAssets _personeAssets;
    [SerializeField] private StatsUpExpiriensProperties _statsUpExpiriensProperties;

    [SerializeField] private bool _IsTestCreateGroup;


    [SerializeField] private List<PlayerPersone> _group = new();

    public List<PlayerPersone> Group { get => _group; private set => _group = value; }
    public StatsUpExpiriensProperties StatsUpExpiriensProperties { get => _statsUpExpiriensProperties; private set => _statsUpExpiriensProperties = value; }

    private void Start()
    {
        SaveManager.Instance.GroupGlobalMap = this;

        if (SaveManager.IsLoad == false)
        {
            if (_IsTestCreateGroup == true)
            {
                if (_personeAssets.Persones.Length == 0)
                {
                    Debug.LogError("PersoneAssets not filled in");
                    return;
                }
                for (int i = 0; i < _personeAssets.Persones.Length; i++)
                {
                    AddPersoneInGroup(i);
                }
            }
        }
        else
        {
            _group = SaveManager.Save.PlayerPersoneGroup.Group;
        }
    }

    public void AddPersoneInGroup(int indexPersone)
    {
        Group.Add(PlayerPersone.CreatePersone(_personeAssets, indexPersone));

        //TEST
        _group[indexPersone].Skills.Add(KeySkills.AttackMelle, _skillsTree.GetSkill(0));
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

    public SavedPlayerPersoneGroup GetSaveGroup()
    {
        SavedPlayerPersoneGroup saveGroup = new ()
        {
            Group = _group
        };
        return saveGroup;
    }


}
