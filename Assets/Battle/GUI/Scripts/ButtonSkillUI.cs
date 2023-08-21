using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkillUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _textInButton;
    [SerializeField] private SkillActive _skill;
    public SkillActive Skill { get => _skill; set => _skill = value; }
    public Image Icon { get => _icon; set => _icon = value; }
    public TextMeshProUGUI TextInButton { get => _textInButton; set => _textInButton = value; }

    public event Action<SkillActive> OnClickButton;

    private void Start()
    {        
        _button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log($"{_skill.Name} + {_skill.KeySkill} + {_skill}");
        OnClickButton?.Invoke(_skill);
    }

}
