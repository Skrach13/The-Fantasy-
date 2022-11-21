using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ������ ������ ����(�������� ������ �����������)
/// </summary>
public class CellFloorScripts : MonoBehaviour
{
    /// <summary>
    ///������� � �����
    /// </summary>
    public Vector2 positiongGrafCellField;
    /// <summary>
    ///������ �� �������� ������ ������� ���
    /// </summary>
    public MainBattleSystems mainSystemBattleScript;
    /// <summary>
    /// ����������� ������� ��� ������ ������
    /// </summary>
     public bool closeCell;
    public int attackRange;
    public APersoneScripts personeStayInCell;
   // public int distanceFromAttacker;

    public SpriteRenderer spriteRenderer;

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
        if (!mainSystemBattleScript.personeMove && !closeCell && (mainSystemBattleScript.actionTypePersone == 0))
        {
        //���� �� ����� �� ����� ����������
        //GetComponent<SpriteRenderer>().color = Color.red;
        
         //��������� ���� �� ��������� �� ���� ������
            mainSystemBattleScript.path = PathFinder.Path(mainSystemBattleScript.massiveFields, mainSystemBattleScript.activePersone.battlePosition, positiongGrafCellField);
        //������������ ����� �� ��������������� ����
            PathFinder.paintPath(mainSystemBattleScript.path, mainSystemBattleScript.massiveFields, Color.green);
        }
    }
    private void OnMouseExit()
    {
        if (!mainSystemBattleScript.personeMove && (mainSystemBattleScript.actionTypePersone == 0))
        {
            //�������������� � ����������� ���� ����� ����� ������ ������� ���� �� ������
            PathFinder.paintPath(mainSystemBattleScript.path, mainSystemBattleScript.massiveFields, Color.white);
        }
    }
    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        // ���� ������ �� �������\������ �� ����� ������� ���� ��� ������� �������� �������� ���������
        if (!closeCell && (mainSystemBattleScript.actionTypePersone == 0)) 
        {
            mainSystemBattleScript.personeMove = true;
            StartCoroutine(mainSystemBattleScript.PersoneMove(mainSystemBattleScript.activePersone));
        }
                
    }

    public void paintCellBattle(Color color)
    {
        spriteRenderer.color = color;
    }
}
