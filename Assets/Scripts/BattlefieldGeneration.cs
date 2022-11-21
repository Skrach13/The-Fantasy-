using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class дл€ генерации карты бо€
/// </summary>
     
public class BattlefieldGeneration : MonoBehaviour
{

    /// <summary>
    /// ћетод отвечающий за uенераци€ пол€ бо€ возвращает двумерный массив (x,y) 
    /// cодержащий €чейки(объекты) пол€ 
    /// </summary>
    /// <param name="widht">количество €чеек по горизонтали</param>
    /// <param name="height">количество €чеек по вертикали (с низу в верх)</param>
    /// <param name="floorUnit">префаб €чейки </param>
    /// <param name="mainSystem">обьект со скриптом €дра системы бо€ дл€ создание ссылки на этот скрипт в €чейках</param>
    /// <returns></returns>
    public static CellFloorScripts[,] generateFields(int widht, int height ,GameObject floorUnit , GameObject mainSystem)
    {
        //массив который будет содержать €чейки (массив пол€)
        var battleFields = new CellFloorScripts[widht,height];
        //ссылочна€ переменна€ на основной скрипт бо€ 
        var maintBattleSystemScripts = mainSystem.GetComponent<MainBattleSystems>();

        for(int x = 0; x < widht; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //создание €чейки бо€ из префаба
                var floor = Instantiate(floorUnit);
                //ссылочна€ переменна€  дл€ того что бы лишний раз не вызывать GetComponet
                var floorScripts = floor.GetComponent<CellFloorScripts>();
                //присваевание ссылке в €чейке на основной скрипт бо€
                floorScripts.mainSystemBattleScript = maintBattleSystemScripts;
                //ссылка на компонент €чейки отвечающий за отрисовку изображени€ ( с учетом как он тут используетьс€ большой вопрос нужен ли он здесь, так как size возвращает x= 1;y=0,5 всегда)
                var sizefloor = floor.GetComponent<SpriteRenderer>();
                //внесение в скрипт положени€ в графе 
                floorScripts.positiongGrafCellField = new Vector2(x, y);
                //внесение €чейки в общий массив €чеек бол€ 
                battleFields[x, y] = floorScripts;

                //расчет смещени€ и положени€ €чейки, дл€ пол€ из гексагонов, путЄм определени€ четности р€да по высоте ( подоброно методом тыка) 
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
