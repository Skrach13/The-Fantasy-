using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelSkillsUI : MonoBehaviour
{
    [SerializeField] private InitiativeManager _initiativeManager;
    [SerializeField] private GameObject _skillsPanel;
    [SerializeField] private ButtonSkillUI _buttonSkillUI;
    [SerializeField] private List<ButtonSkillUI> _buttonsSkill = new();

    public event Action<SkillActive> OnClickButtonActive;

    private void Start()
    {
        _initiativeManager.OnNextPersoneActive += UpdateSkillsPanel;
    }
    public void UpdateSkillsPanel(PersoneInBattle persone)
    {
        if(!(persone is PlayerPersoneInBattle)) return;

        if (_buttonsSkill.Count != 0)
        {
            foreach (ButtonSkillUI button in _buttonsSkill)
            {
                button.OnClickButton -= OnClickButtonInPanel;
                Destroy(button.gameObject);
            }
            _buttonsSkill.Clear();
        }

        foreach (SkillBase skills in persone.Skills)
        {
            if (skills.ActionType == MethodAction.Active)
            {
                ButtonSkillUI button = Instantiate(_buttonSkillUI, _skillsPanel.transform);
                _buttonsSkill.Add(button);
                button.Skill = skills as SkillActive;
                button.TextInButton.text = skills.Name; 
                button.OnClickButton += OnClickButtonInPanel;

                //Debug.Log($"{skills.Key} and {skills.Value}");
            }
        }
    }

    private void OnClickButtonInPanel(SkillActive skillActive)
    {
        OnClickButtonActive?.Invoke(skillActive);
    }


}
