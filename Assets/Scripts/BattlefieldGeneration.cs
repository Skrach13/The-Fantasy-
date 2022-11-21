using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class ��� ��������� ����� ���
/// </summary>
     
public class BattlefieldGeneration : MonoBehaviour
{

    /// <summary>
    /// ����� ���������� �� u�������� ���� ��� ���������� ��������� ������ (x,y) 
    /// c��������� ������(�������) ���� 
    /// </summary>
    /// <param name="widht">���������� ����� �� �����������</param>
    /// <param name="height">���������� ����� �� ��������� (� ���� � ����)</param>
    /// <param name="floorUnit">������ ������ </param>
    /// <param name="mainSystem">������ �� �������� ���� ������� ��� ��� �������� ������ �� ���� ������ � �������</param>
    /// <returns></returns>
    public static CellFloorScripts[,] generateFields(int widht, int height ,GameObject floorUnit , GameObject mainSystem)
    {
        //������ ������� ����� ��������� ������ (������ ����)
        var battleFields = new CellFloorScripts[widht,height];
        //��������� ���������� �� �������� ������ ��� 
        var maintBattleSystemScripts = mainSystem.GetComponent<MainBattleSystems>();

        for(int x = 0; x < widht; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //�������� ������ ��� �� �������
                var floor = Instantiate(floorUnit);
                //��������� ����������  ��� ���� ��� �� ������ ��� �� �������� GetComponet
                var floorScripts = floor.GetComponent<CellFloorScripts>();
                //������������ ������ � ������ �� �������� ������ ���
                floorScripts.mainSystemBattleScript = maintBattleSystemScripts;
                //������ �� ��������� ������ ���������� �� ��������� ����������� ( � ������ ��� �� ��� ������������� ������� ������ ����� �� �� �����, ��� ��� size ���������� x= 1;y=0,5 ������)
                var sizefloor = floor.GetComponent<SpriteRenderer>();
                //�������� � ������ ��������� � ����� 
                floorScripts.positiongGrafCellField = new Vector2(x, y);
                //�������� ������ � ����� ������ ����� ���� 
                battleFields[x, y] = floorScripts;

                //������ �������� � ��������� ������, ��� ���� �� ����������, ���� ����������� �������� ���� �� ������ ( ��������� ������� ����) 
                if (y % 2 == 0)
                {
                    floor.transform.localPosition = new Vector3((float)(x * sizefloor.size.x - (sizefloor.size.x * 0.5)), (float)(y  - (sizefloor.size.y * (y * 0.5))), 10);
                }
                else
                {
                    floor.transform.localPosition = new Vector3((float)(x * sizefloor.size.x), (float)(y  - (sizefloor.size.y * (y * 0.5))), 10);
                }
            }
        }

        return battleFields;
    }
}
