using System.Collections.Generic;
using UnityEngine;

public class GroupGlobalMap : SingletonBase<GroupGlobalMap>
{
    [SerializeField] private SkillsProperties _skillsTree;
    [SerializeField] private List<PlayerPersone> _group = new List<PlayerPersone>();
    [SerializeField] private PersoneAssets _personeAssets;
    [SerializeField] private string _testName;

    public List<PlayerPersone> Group { get => _group; set => _group = value; }

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

    public PlayerPersone GetPerosne(string name)
    {
        PlayerPersone perosne = _group.Find(x => x.Name == name);
        return perosne;
    }


}
