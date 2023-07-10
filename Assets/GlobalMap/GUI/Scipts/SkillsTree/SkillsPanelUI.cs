using System;
using UnityEngine;

public class SkillsPanelUI : MonoBehaviour
{
    [SerializeField] private SelectedPersonePanel _panelSelectedPersone;
    [SerializeField] private StatUI[] _statUI;
    [SerializeField] private SkillsTreePanelUI[] _panelSkillsUI;
    private string _nameVisablePersone;

    public string NameVisablePersone { get => _nameVisablePersone; private set => _nameVisablePersone = value; }

    private void Start()
    {
        _panelSelectedPersone.OnSelectedPerson += ShowStatsAndSkillsTreePersone;
    }

    private void OnEnable()
    {
        ShowStatsAndSkillsTreePersone(GroupGlobalMap.Instance.Group[0].Name);
    }
    private void OnDestroy()
    {
        _panelSelectedPersone.OnSelectedPerson -= ShowStatsAndSkillsTreePersone;
    }

    public void AddSkill(KeySkills keySkills)
    {

    }
    private void ShowStatsAndSkillsTreePersone(string name)
    {
        _nameVisablePersone = name;
        if (_statUI.Length > GroupGlobalMap.Instance.GetPerosne(name).Stats.Length) { Debug.LogError("_statUI.Length > _groupGlobalMap.GetPerosne(name).Stats.Length"); }
        for (int i = 0; i < _statUI.Length; i++)
        {
            int levelStat = GroupGlobalMap.Instance.GetPerosne(name).Stats[i + 1].Value;
            _statUI[i].Value.text = levelStat.ToString();
            _statUI[i].ProgressBar.UpdateProgressBar(GroupGlobalMap.Instance.GetPerosne(name).Stats[i + 1].NeededExperience, GroupGlobalMap.Instance.GetPerosne(name).Stats[i + 1].UpExperience);
            
            _panelSkillsUI[i].ShowSkillsTreePersone(name, levelStat);
           
        }
    }

}
