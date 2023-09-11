using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    /// <summary>
    /// ��������� ����� ��� ����(���� � ����� �� ������)
    /// </summary>
    [SerializeField] protected float _ribsWeight = 1;
    [SerializeField] protected Vector2 _positionInGraff;
    public float RibsWeight { get => _ribsWeight; set => _ribsWeight = value; }
    public Vector2 PositionInGraff { get => _positionInGraff; set => _positionInGraff = value; }
    public Vector2 PositiongCell { get; set; }
    /// <summary>
    ///������ �� �������� ������ ������� ���
    /// </summary>
    public bool CloseCell { get; set; }
    public void PaintCell(Color color)
    {      
        var sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = color;
        }
    }
}
