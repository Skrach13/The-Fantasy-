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
    /// ��������� ���� ���
    /// </summary>
    /// <param name="widht"></param>
    /// <param name="height"></param>
    public static GameObject[,] generate(int widht, int height,float margins ,GameObject floorUnit)
    {
        var battleFields = new GameObject[widht,height];

        for(int x = 0; x < widht; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var floor = Instantiate(floorUnit);
                var floorScripts = floor.GetComponent<CellFloorScripts>();
                var sizefloor = floor.GetComponent<SpriteRenderer>();
                floorScripts.positiongGrafCellFloor = new Vector2(x, y);
                
                battleFields[x, y] = floor;
                floor.transform.localPosition = new Vector3((float)(x * (sizefloor.size.x * 0.5)+(margins*x)),(float)((y * sizefloor.size.y)+(margins*y)) , 10);
            }
        }
        return battleFields;
    }
}