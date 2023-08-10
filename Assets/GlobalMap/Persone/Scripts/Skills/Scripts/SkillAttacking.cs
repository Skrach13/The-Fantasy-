using UnityEngine;

enum TypeAttackTargets
{
    Area,
    OneTarget,    
    Combinate
}
[CreateAssetMenu(fileName = "SkillAttacking", menuName = "Persone/Skill/SkillAttacking")]
public class SkillAttacking : SkillActive
{
    [SerializeField] private TypeAttackTargets _typeAttackTargets;
    [SerializeField] private int _rangeAttack;
    [SerializeField] private int _damage;
    [SerializeField] private int _areaAtack;

    public int Damage { get => _damage;private set => _damage = value; }
}
