using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesProperties", menuName = "Enemies/EnemyProperties")]
public class EnemyProperties : ScriptableObject
{    
    enum ClassEnemy
    {
        Melle,
        Range,
        Mage
    }
    [SerializeField] private ClassEnemy _enemyClass;
    [SerializeField] private Race _race;
    [SerializeField] private Sprite _sprite;
    public Stat[] Stats;    
    public Sprite Icon;
    //TODO
    [SerializeField] private SkillBase _skillAttack;
    public SkillBase SkillAttack { get => _skillAttack; set => _skillAttack = value; }
    //
}
