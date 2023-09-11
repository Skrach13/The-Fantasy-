using UnityEngine;

/// <summary>
/// ������ ������ ����(�������� ������ �����������)
/// </summary>
public class CellInBattleBase : MonoBehaviour 
{
    /// <summary>
    /// ��������� ����� ��� ����(���� � ����� �� ������)
    /// </summary>
    [SerializeField] private float _ribsWeight = 1;
    public float RibsWeight { get => _ribsWeight; set => _ribsWeight = value; }
    public Vector2 PositionInGraff { get; set; }
    public Vector2 PositiongCell { get; set; }
    /// <summary>
    ///������ �� �������� ������ ������� ���
    /// </summary>
    public bool CloseCell { get; set; }
   
}
