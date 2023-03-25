using UnityEngine;

/// <summary>
/// Class ��� ��������� ����� ���
/// </summary>

public class BattleFieldGeneration : MonoBehaviour
{
    /// <summary>
    /// ����� ���������� �� ��������� ���� ��� ���������� ��������� ������ (x,y) 
    /// c��������� ������(�������) ���� 
    /// </summary>
    /// <param name="widht">���������� ����� �� �����������</param>
    /// <param name="height">���������� ����� �� ��������� (� ���� � ����)</param>
    /// <param name="prefabCell">������ ������ </param>
    /// <param name="mainSystem">������ �� �������� ���� ������� ��� ��� �������� ������ �� ���� ������ � �������</param>
    /// <returns></returns>
    public static CellInBattle[,] GenerateFields(int widht, int height, CellInBattle prefabCell, GameObject mainSystem)
    {
        //������ ������� ����� ��������� ������ (������ ����)
        var battleFields = new CellInBattle[widht, height];
        //��������� ���������� �� �������� ������ ��� 
        var maintBattleSystemScripts = mainSystem.GetComponent<MainBattleSystems>();

        for (int x = 0; x < widht; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //�������� ������ ��� �� �������
                var cell = Instantiate(prefabCell);
                //��������� ����������  ��� ���� ��� �� ������ ��� �� �������� GetComponet
                var cellScripts = cell.GetComponent<CellInBattle>();
                //������������ ������ � ������ �� �������� ������ ���
                cellScripts.MainSystemBattleScript = maintBattleSystemScripts;
                //������ �� ��������� ������ ���������� �� ��������� ����������� ( � ������ ��� �� ��� ������������� ������� ������ ����� �� �� �����, ��� ��� size ���������� x = 1;y = 0,5 ������)
                var spriteRender = cell.GetComponent<SpriteRenderer>();
                //�������� � ������ ��������� � ����� 
                cellScripts.PositiongCell = new Vector2(x, y);
                //�������� ������ � ����� ������ ����� ���� ��� 
                battleFields[x, y] = cellScripts;

                //������ �������� � ��������� ������, ��� ���� �� ����������, ���� ����������� �������� ���� �� ������ ( ��������� ������� ����) 
                if (y % 2 == 0)
                {
                    cell.transform.localPosition = new Vector3((float)(x * spriteRender.size.x - (spriteRender.size.x * 0.5)), (float)(y - (spriteRender.size.y * (y * 0.5))), 10);
                    cell.PositionInGraff = new Vector2(x, y);
                }
                else
                {
                    cell.transform.localPosition = new Vector3((float)(x * spriteRender.size.x), (float)(y - (spriteRender.size.y * (y * 0.5))), 10);
                    cell.PositionInGraff = new Vector2(x + 0.5f, y);
                }
            }
        }

        return battleFields;
    }
}
