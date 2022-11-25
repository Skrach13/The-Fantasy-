using UnityEngine;

/// <summary>
/// Class ��� ��������� ����� ���
/// </summary>

public class BattleFieldGeneration : MonoBehaviour
{
    /// <summary>
    /// ����� ���������� �� u�������� ���� ��� ���������� ��������� ������ (x,y) 
    /// c��������� ������(�������) ���� 
    /// </summary>
    /// <param name="widht">���������� ����� �� �����������</param>
    /// <param name="height">���������� ����� �� ��������� (� ���� � ����)</param>
    /// <param name="prefabCell">������ ������ </param>
    /// <param name="mainSystem">������ �� �������� ���� ������� ��� ��� �������� ������ �� ���� ������ � �������</param>
    /// <returns></returns>
    public static CellFieldInBattle[,] GenerateFields(int widht, int height, GameObject prefabCell, GameObject mainSystem)
    {
        //������ ������� ����� ��������� ������ (������ ����)
        var battleFields = new CellFieldInBattle[widht, height];
        //��������� ���������� �� �������� ������ ��� 
        var maintBattleSystemScripts = mainSystem.GetComponent<MainBattleSystems>();

        for (int x = 0; x < widht; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //�������� ������ ��� �� �������
                var cell = Instantiate(prefabCell);
                //��������� ����������  ��� ���� ��� �� ������ ��� �� �������� GetComponet
                var cellScripts = cell.GetComponent<CellFieldInBattle>();
                //������������ ������ � ������ �� �������� ������ ���
                cellScripts.mainSystemBattleScript = maintBattleSystemScripts;
                //������ �� ��������� ������ ���������� �� ��������� ����������� ( � ������ ��� �� ��� ������������� ������� ������ ����� �� �� �����, ��� ��� size ���������� x = 1;y = 0,5 ������)
                var spriteRender = cell.GetComponent<SpriteRenderer>();
                //�������� � ������ ��������� � ����� 
                cellScripts.positiongGrafCellField = new Vector2(x, y);
                //�������� ������ � ����� ������ ����� ���� ��� 
                battleFields[x, y] = cellScripts;

                //������ �������� � ��������� ������, ��� ���� �� ����������, ���� ����������� �������� ���� �� ������ ( ��������� ������� ����) 
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
