using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class для генерации карты боя
/// </summary>
public class BattlefieldGeneration : MonoBehaviour
{
 


   
   
          
    /// <summary>
    /// Генерация поля боя
    /// </summary>
    /// <param name="widht"></param>
    /// <param name="height"></param>
    public static GameObject[,] generate(int widht, int height,float margins ,GameObject floorUnit , GameObject mainSystem)
    {
        var battleFields = new GameObject[widht,height];
        var maintBattleSystemScripts = mainSystem.GetComponent<MainBattleSystemScripts>();

        for(int x = 0; x < widht; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var floor = Instantiate(floorUnit);
                var floorScripts = floor.GetComponent<CellFloorScripts>();
                floorScripts.mainSystemBattleScript = maintBattleSystemScripts;
                var sizefloor = floor.GetComponent<SpriteRenderer>();
                floorScripts.positiongGrafCellField = new Vector2(x, y);
                battleFields[x, y] = floor;
                if (y % 2 == 0)
                {
                    floor.transform.localPosition = new Vector3((float)(x * sizefloor.size.x - (sizefloor.size.x * 0.5)), (float)(y  - (sizefloor.size.y * (y * 0.5))), 10);
                    Debug.Log(sizefloor.size.x + " " + sizefloor.size.y);
                   // floor.transform.localPosition = new Vector3((float)(x * (sizefloor.size.x * 0.5)+ -(sizefloor.size.x * margins)),(float)((y * sizefloor.size.y) - (sizefloor.size.y * (y * (margins/2)))) , 10);
                }
                else
                {
                    floor.transform.localPosition = new Vector3((float)(x * sizefloor.size.x), (float)(y  - (sizefloor.size.y * (y * 0.5))), 10);
                    Debug.Log(sizefloor.size.x + " " + sizefloor.size.y);
                   // floor.transform.localPosition = new Vector3((float)(x * (sizefloor.size.x * 0.5)),(float)((y * sizefloor.size.y) - (floor.GetComponent<SpriteRenderer>().size.y * (y * (margins/2)))) , 10);
                }
            }
        }
        return battleFields;
    }
}
