using System;
using UnityEngine;
using UnityEngine.UI;

public enum MethodAction
{
    Active,
    Passive
}


public abstract class SkillBase : ScriptableObject
{
    [SerializeField] private KeySkills _keySkill;
    [SerializeField] private MethodAction _actionType;
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected string description;
    public string Name { get => _name; protected set => _name = value; }
    public string Description { get => description; protected set => description = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public KeySkills KeySkill { get => _keySkill; set => _keySkill = value; }
    public MethodAction ActionType { get => _actionType; set => _actionType = value; }
}
