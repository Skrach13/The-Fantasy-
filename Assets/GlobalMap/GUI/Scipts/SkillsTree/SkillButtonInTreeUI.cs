using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonInTreeUI : MonoBehaviour
{
    [SerializeField] private KeySkills _skill;
    [SerializeField] private bool _isAvailable;
    [SerializeField] private int _neededLevelStatAvailable;
    [SerializeField] private SkillButtonInTreeUI _previousSkill;
    [SerializeField] private Image _lock;
    [SerializeField] private Button _button;

    public event Action<KeySkills> OnClickButton;

    public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
    public int NeededLevelStatAvailable { get => _neededLevelStatAvailable; set => _neededLevelStatAvailable = value; }
    public KeySkills Skill { get => _skill; set => _skill = value; }
    public SkillButtonInTreeUI PreviousSkill { get => _previousSkill; set => _previousSkill = value; }

    private void Start()
    {        
        _button.onClick.AddListener(OnDownButton);

        _lock.enabled = true;
        
    }
    private void OnDestroy()
    {

        _button.onClick.RemoveListener(OnDownButton);
    }    

    public void UnlockedButton() => _button.enabled = false;   
    public void LockButton() => _button.enabled = true;
    public void UnlockedSkill() => _lock.enabled = false;
    public void LockSkill() => _lock.enabled = true;


    private void OnDownButton()
    {
        OnClickButton?.Invoke(Skill);
    }
    public void BuildButton(KeySkills skill, SkillButtonInTreeUI previousSkill, int neededLevel)
    {
        Skill = skill;
        PreviousSkill = previousSkill;
        NeededLevelStatAvailable = neededLevel;
    }
}
