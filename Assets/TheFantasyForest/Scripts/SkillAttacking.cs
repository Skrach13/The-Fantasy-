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

    [SerializeField] private int _damage;
    [SerializeField] private int _areaAtack;   
    private CellInBattle _targetCell;

    public int Damage { get => _damage; private set => _damage = value; }

    public override void UseSkill(CellInBattle cell)
    {
        if (cell.AttackRange != 0)
        {
            if (_typeAttackTargets == TypeAttackTargets.OneTarget)
            {
                DealDamage(cell.PersoneStayInCell);
            }
        }
    }
    public override void UseSkill(PersoneInBattle persone)
    {
        if (_typeAttackTargets == TypeAttackTargets.OneTarget)
        {
            DealDamage(persone);
        }
    }
    public override void DefiningScope(PersoneInBattle persone, CellInBattle[,] cells)
    {
        base.DefiningScope(persone, cells);
        // DefiningArea.DefiningScope
    }
    public override void DefiningAreaEffectSkill(CellInBattle cell)
    {
        if (cell.AttackRange != 0)
        {
            if (_targetCell != null)
            {
                _targetCell.PaintCellBattle(ColorsCell.RangeArea);
            }

            if (_typeAttackTargets == TypeAttackTargets.OneTarget)
            {
                _targetCell = cell;
                cell.PaintCellBattle(ColorsCell.ScopeOfAction);
            }
        }
    }

    public void DealDamage(PersoneInBattle target)
    {
        target.HealthPoint -= _damage;
    }
}
