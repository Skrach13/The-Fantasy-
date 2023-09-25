using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum KeySkills
{
    AttackMelle,
    AttackRange
}

[Serializable]
class SkillInDictonary
{
    public KeySkills key;
    public SkillBase skill;
}

[CreateAssetMenu(fileName = "Skillstree", menuName = "Persone/Skillstree")]
public class SkillsProperties : ScriptableObject
{
    [SerializeField] private List<SkillInDictonary> _skillInDictonary = new List<SkillInDictonary>();
    public SkillBase GetSkill(KeySkills key)
    {
        var skillinDictonary = _skillInDictonary.Find(skillinDictonary => skillinDictonary.key == key);
        SkillBase skill = skillinDictonary.skill;
        return skill;
    }

}
