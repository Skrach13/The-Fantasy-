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
    public MainBattleSystemScripts mainSystemBattleScript;
    /// <summary>
    /// ����������� ������� ��� ������ ������
    /// </summary>
     public bool closeCell;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
        
    private void OnMouseEnter()
    {
        //�������� ������� �� ������ �������� � ������� �� ������  
        if (!mainSystemBattleScript.personeMove && !closeCell && (mainSystemBattleScript.actionTypePersone == 0))
        {
            Debug.Log("����� ����: " + positiongGrafCellField + "������ ����:" + mainSystemBattleScript.testPlayerScript.battlePosition);
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
        if (!mainSystemBattleScript.personeMove)
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
        if (!closeCell) 
        {
            mainSystemBattleScript.personeMove = true;
            StartCoroutine(mainSystemBattleScript.PersoneMove(mainSystemBattleScript.activePersone));
        }
           
    }
}
