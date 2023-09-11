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
        //�������� ������� �� ������ �������� � ������� �� ������  
        if (!MainSystemBattleScript.personeMove && !CloseCell && (MainSystemBattleScript.actionTypePersone == 0))
        {
            //���� �� ����� �� ����� ����������
            //GetComponent<SpriteRenderer>().color = Color.red;

            //��������� ���� �� ��������� �� ���� ������
            MainSystemBattleScript.path = PathFinderInBattle.Path(MainSystemBattleScript.MassiveFields, MainSystemBattleScript.activePersone.battlePosition, PositiongCell);
            //������������ ����� �� ��������������� ����
            PathFinderInBattle.paintPath(MainSystemBattleScript.path, MainSystemBattleScript.MassiveFields, Color.green);
        }
    }
    private void OnMouseExit()
    {
        if (!MainSystemBattleScript.personeMove && (MainSystemBattleScript.actionTypePersone == 0))
        {
            //�������������� � ����������� ���� ����� ����� ������ ������� ���� �� ������
            PathFinderInBattle.paintPath(MainSystemBattleScript.path, MainSystemBattleScript.MassiveFields, Color.white);
        }
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseUp()
    {
        // ���� ������ �� �������\������ �� ����� ������� ���� ��� ������� �������� �������� ���������
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
