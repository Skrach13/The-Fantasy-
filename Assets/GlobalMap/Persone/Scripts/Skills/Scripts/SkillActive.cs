using UnityEngine;

[CreateAssetMenu(fileName = "SkillActive", menuName = "Persone/Skill/SkillActive")]
public class SkillActive : SkillBase
{
    [SerializeField] protected int cooldown;
    public int Cooldown { get => cooldown; set => cooldown = value; }
}
