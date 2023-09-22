using UnityEngine;

[CreateAssetMenu(fileName = "SkillActive", menuName = "Persone/Skill/SkillActive")]
public class SkillActive : SkillBase
{
    [SerializeField] protected int _cooldown;
    [SerializeField] protected int _rangeSkill;
    public int CostUse;
    public int Cooldown { get => _cooldown; set => _cooldown = value; }
    public int RangeSkill { get => _rangeSkill; set => _rangeSkill = value; }

    public virtual void UseSkill(CellInBattle cell) { }
    public virtual void UseSkill(PersoneInBattle cell) { }
    public virtual void DefiningScope(PersoneInBattle persone , CellInBattle[,] cells)
    {
        DefiningArea.DefiningScope(cells, persone, RangeSkill);
//#if UNITY_EDITOR
//        Gizmos.DrawLine(cell.transform.position, cell.transform.position);
//#endif
    }
    public virtual void DefiningAreaEffectSkill(CellInBattle cell)
    {

    }
    public virtual void DefiningAreaEffectSkill(PersoneInBattle persone)
    {

    }

}
