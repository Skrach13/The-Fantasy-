using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum KeySkills
{
    AttackMelle,
    AttackRange
}

[CreateAssetMenu(fileName = "Skillstree", menuName = "Persone/SkillsProperties")]
public class SkillsProperties : ScriptableObject
{
    [SerializeField] private Skills.Atack _skillas = new();
    [SerializeField] private Skills.AtackRange _skillasz = new();

    private Dictionary<KeySkills, SkillBase> _skillMap;

    public SkillsProperties()
        {
            _skillMap = new Dictionary<KeySkills, SkillBase>() 
            {
                { KeySkills.AttackMelle, _skillas }, 
                { KeySkills.AttackRange, _skillasz } 
            };

       // _skillMap[KeySkills.AttackMelle] = _skillas;
        }
    
    public SkillBase GetSkill(KeySkills key)
    {        
        return _skillMap[key];
    }
    
}
