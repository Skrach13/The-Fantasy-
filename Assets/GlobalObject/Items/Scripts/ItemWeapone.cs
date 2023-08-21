using UnityEngine;

[CreateAssetMenu(fileName = "ItemWeapone", menuName = "ScriptableObjects/Item/ItemWeapone")]
public class ItemWeapone : ItemBase
{
    [SerializeField] private SkillAttacking _skillAttacking;

    public SkillAttacking SkillAttacking { get => _skillAttacking; private set => _skillAttacking = value; }
}
