using System;
using UnityEngine;
public enum ColorsCell 
{
    None = 0,
    Enemies = 1,
    ScopeOfAction = 2,
    RangeArea = 3,
    PaintPath = 4
}

public class CellInBattle : CellInBattleBase
{
    [SerializeField] private Color[] _colors;
    public MainBattleSystems MainSystemBattleScript { get; set; }
    public int AttackRange { get; set; }
    public PersoneInBattle PersoneStayInCell { get; set; }
    private SpriteRenderer spriteRenderer;

    public event Action<CellInBattle> OnCellEnter;
    public event Action<CellInBattle> OnCellExit;
    public event Action<CellInBattle> OnClickedCell;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnMouseEnter()
    {
        OnCellEnter?.Invoke(this);
    }

    private void OnMouseExit()
    {
        OnCellExit?.Invoke(this);
    }
    private void OnMouseDown()
    {
        // если €чейка не закрыта\зан€та то после нажати€ мыши над €чейкой начанает движение персонажа
        if (!CloseCell && (MainSystemBattleScript.ActionTypePersone == 0))
        {
            OnClickedCell?.Invoke(this);
            MainSystemBattleScript._personeMove = true;
        }
    }
    public void PaintCellBattle(ColorsCell colorType)
    {
        spriteRenderer.color = _colors[((int)colorType)];
    }
}
