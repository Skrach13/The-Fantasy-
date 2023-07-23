using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    /// <summary>
    /// Стоимость ребер для узла(вход и выход из ячейки)
    /// </summary>
    [SerializeField] protected float _ribsWeight = 1;
    [SerializeField] protected Vector2 _positionInGraff;
    public float RibsWeight { get => _ribsWeight; set => _ribsWeight = value; }
    public Vector2 PositionInGraff { get => _positionInGraff; set => _positionInGraff = value; }
    public Vector2 PositiongCell { get; set; }
    /// <summary>
    ///ссылка на основной скрипт системы боя
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
