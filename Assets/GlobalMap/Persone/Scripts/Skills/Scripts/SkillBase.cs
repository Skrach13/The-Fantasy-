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

    [SerializeField] protected MethodAction _methodAction;
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected string description;
    public string Name { get => _name; protected set => _name = value; }
    public string Description { get => description; protected set => description = value; }
    public Sprite Icon { get => icon; set => icon = value; }
}
