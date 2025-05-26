using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SkillsTreeUI",menuName = "Persone/SkillsTreeUI")]
public class SkillsTreeProperties : ScriptableObject
{
    [SerializeField] private DegreeSkills[] _levelSkill;
    public DegreeSkills[] LevelSkill { get => _levelSkill; set => _levelSkill = value; }

    [Serializable]
    public class DegreeSkills
    {
        [SerializeField] private int _nededLevel;
        [SerializeField] private CellSkill[] _cellSkills;

        public int NededLevel { get => _nededLevel; set => _nededLevel = value; }
        public CellSkill[] CellSkills { get => _cellSkills; set => _cellSkills = value; }
    }

    [Serializable]
    public class CellSkill
    {
        [SerializeField] private KeySkills _keySkills;

        public KeySkills KeySkills { get => _keySkills; set => _keySkills = value; }
    }

}
