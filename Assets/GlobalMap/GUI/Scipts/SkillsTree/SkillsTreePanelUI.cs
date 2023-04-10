using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillsTreePanelUI : MonoBehaviour
{
    [Header("Build Tree")]
    [SerializeField] private SkillsTreeProperties _properties;
    [SerializeField] private GameObject _levelSkillsUIPref;
    [SerializeField] private SkillButtonInTreeUI _buttonSkillsUIPref;

    [Header("Skills Tree")]
    [SerializeField] private List<SkillButtonInTreeUI> _buttonSkillsUI;
    [SerializeField] private List<GameObject> _levelSkillsUI;


    private void Start()
    {

    }
    public void ShowSkillsTreePersone(string name,int levelStat)
    {
        var persone = GroupGlobalMap.Instance.GetPerosne(name);
        for (int i = 0; i < _buttonSkillsUI.Count; i++)
        {
            if(_buttonSkillsUI[i].NeededLevelStatAvailable <= levelStat)
            {
                _buttonSkillsUI[i].UnlockedSkill();
            }
            else
            {
                _buttonSkillsUI[i].LockSkill();
            }

            if (persone.TryGetSkills(_buttonSkillsUI[i].Skill,out var skill))
            {
                _buttonSkillsUI[i].LockButton();
            }
            else
            {
                _buttonSkillsUI[i].UnlockedButton();
            }
        }
    }

    [ContextMenu(nameof(BuildTree))]
    private void BuildTree()
    {       
        if (_levelSkillsUI.Count != 0)
        {
            foreach (var level in _levelSkillsUI)
            {
                DestroyImmediate(level);
            }
        }
        _buttonSkillsUI.Clear();
        _levelSkillsUI.Clear();

        for (int i = 0; i < _properties.LevelSkill.Length; i++)
        {
            var levelskill = Instantiate(_levelSkillsUIPref, transform);
            _levelSkillsUI.Add(levelskill);
            for (int y = 0; y < _properties.LevelSkill[i].CellSkills.Length; y++)
            {
                var buttonSkills = Instantiate(_buttonSkillsUIPref, levelskill.transform);
                buttonSkills.BuildButton(_properties.LevelSkill[i].CellSkills[y].KeySkills, null , _properties.LevelSkill[i].NededLevel);
                _buttonSkillsUI.Add(buttonSkills);
            }
        }

    }
}
