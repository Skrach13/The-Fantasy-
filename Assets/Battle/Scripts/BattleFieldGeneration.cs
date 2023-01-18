using UnityEngine;

/// <summary>
/// Class дл€ генерации карты бо€
/// </summary>

public class BattleFieldGeneration : MonoBehaviour
{
    /// <summary>
    /// ћетод отвечающий за uенераци€ пол€ бо€ возвращает двумерный массив (x,y) 
    /// cодержащий €чейки(объекты) пол€ 
    /// </summary>
    /// <param name="widht">количество €чеек по горизонтали</param>
    /// <param name="height">количество €чеек по вертикали (с низу в верх)</param>
    /// <param name="prefabCell">префаб €чейки </param>
    /// <param name="mainSystem">обьект со скриптом €дра системы бо€ дл€ создание ссылки на этот скрипт в €чейках</param>
    /// <returns></returns>
    public static CellFieldInBattle[,] GenerateFields(int widht, int height, GameObject prefabCell, GameObject mainSystem)
    {
        //массив который будет содержать €чейки (массив пол€)
        var battleFields = new CellFieldInBattle[widht, height];
        //ссылочна€ переменна€ на основной скрипт бо€ 
        var maintBattleSystemScripts = mainSystem.GetComponent<MainBattleSystems>();

        for (int x = 0; x < widht; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //создание €чейки бо€ из префаба
                var cell = Instantiate(prefabCell);
                //ссылочна€ переменна€  дл€ того что бы лишний раз не вызывать GetComponet
                var cellScripts = cell.GetComponent<CellFieldInBattle>();
                //присваевание ссылке в €чейке на основной скрипт бо€
                cellScripts.mainSystemBattleScript = maintBattleSystemScripts;
                //ссылка на компонент €чейки отвечающий за отрисовку изображени€ ( с учетом как он тут используетьс€ большой вопрос нужен ли он здесь, так как size возвращает x = 1;y = 0,5 всегда)
                var spriteRender = cell.GetComponent<SpriteRenderer>();
                //внесение в скрипт положени€ в графе 
                cellScripts.positiongGrafCellField = new Vector2(x, y);
                //внесение €чейки в общий массив €чеек пол€ бо€ 
                battleFields[x, y] = cellScripts;

                //расчет смещени€ и положени€ €чейки, дл€ пол€ из гексагонов, путЄм определени€ четности р€да по высоте ( подоброно методом тыка) 
                if (y % 2 == 0)
                {
                    cell.transform.localPosition = new Vector3((float)(x * spriteRender.size.x - (spriteRender.size.x * 0.5)), (float)(y - (spriteRender.size.y * (y * 0.5))), 10);
                }
                else
                {
                    cell.transform.localPosition = new Vector3((float)(x * spriteRender.size.x), (float)(y - (spriteRender.size.y * (y * 0.5))), 10);
                }
            }
        }

        return battleFields;
    }
}
