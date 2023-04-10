using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class SkillBase
{

    [SerializeField] protected MethodAction _methodAction;
    [SerializeField] private string _name;
    [SerializeField] private Image _image;

    [Serializable]
    public delegate void EffectUse();
    [SerializeField] private EffectUse Effect;

    public string Name { get => _name; set => _name = value; }

    public void Use()
    {
        Debug.Log($"skills use - {Name}");
    }
}
