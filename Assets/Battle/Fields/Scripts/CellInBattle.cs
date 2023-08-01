using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInBattle : CellInBattleBase
{
    public MainBattleSystems MainSystemBattleScript { get; set; }
    public int AttackRange { get; set; }
    public PersoneInBattle PersoneStayInCell { get; set; }
    private SpriteRenderer spriteRenderer;

    public event Action<Vector2> OnCellEnter;
    public event Action OnCellExit;
    public event Action OnClickedCell;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseEnter()
    {
        //проверка неходит ли сейчас персонаж и закрыта ли €чейка  
        if (!MainSystemBattleScript._personeMove && !CloseCell && (MainSystemBattleScript._actionTypePersone == 0))
        {
            OnCellEnter?.Invoke(PositionInGraff);
        }
    }

    private void OnMouseExit()
    {
        if (!MainSystemBattleScript._personeMove && (MainSystemBattleScript._actionTypePersone == 0))
        {
            OnCellExit?.Invoke();
        }
    }
    private void OnMouseDown()
    {
        // если €чейка не закрыта\зан€та то после нажати€ мыши над €чейкой начанает движение персонажа
        if (!CloseCell && (MainSystemBattleScript._actionTypePersone == 0))
        {
            OnClickedCell?.Invoke();
            MainSystemBattleScript._personeMove = true;
        }       
    }
    public void paintCellBattle(Color color)
    {
        spriteRenderer.color = color;
    }
}
