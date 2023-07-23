using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInBattle : CellInBattleBase
{

    public MainBattleSystems MainSystemBattleScript { get; set; }
    public int AttackRange { get; set; }
    public PersoneInBattle PersoneStayInCell { get; set; }
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // void Start()
    // {
    // }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        //проверка неходит ли сейчас персонаж и закрыта ли €чейка  
        if (!MainSystemBattleScript.personeMove && !CloseCell && (MainSystemBattleScript.actionTypePersone == 0))
        {
            //пока не нужно но авось пригодитс€
            //GetComponent<SpriteRenderer>().color = Color.red;

            //получение пути от персонажа до этой €чейки
            MainSystemBattleScript.path = PathFinderInBattle.Path(MainSystemBattleScript.MassiveFields, MainSystemBattleScript.activePersone.battlePosition, PositiongCell);
            //закрашивание €чеек по сгенерированому пути
            PathFinderInBattle.paintPath(MainSystemBattleScript.path, MainSystemBattleScript.MassiveFields, Color.green);
        }
    }
    private void OnMouseExit()
    {
        if (!MainSystemBattleScript.personeMove && (MainSystemBattleScript.actionTypePersone == 0))
        {
            //перекрашивание в стандартный цвет €чеек после выхода курсора мыши из €чейки
            PathFinderInBattle.paintPath(MainSystemBattleScript.path, MainSystemBattleScript.MassiveFields, Color.white);
        }
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseUp()
    {
        // если €чейка не закрыта\зан€та то после отжати€ мыши над €чейкой начанает движение персонажа
        if (!CloseCell && (MainSystemBattleScript.actionTypePersone == 0))
        {
            MainSystemBattleScript.personeMove = true;
            StartCoroutine(MainSystemBattleScript.PersoneMove(MainSystemBattleScript.activePersone));
        }

    }

    public void paintCellBattle(Color color)
    {
        spriteRenderer.color = color;
    }
}
