using UnityEngine;

/// <summary>
/// —крипт €чейки пол€(возможно станет абстрактным)
/// </summary>
public class CellInBattleBase : MonoBehaviour 
{
    /// <summary>
    /// —тоимость ребер дл€ узла(вход и выход из €чейки)
    /// </summary>
    [SerializeField] private float _ribsWeight = 1;
    public float RibsWeight { get => _ribsWeight; set => _ribsWeight = value; }
    public Vector2 PositionInGraff { get; set; }
    public Vector2 PositiongCell { get; set; }
    /// <summary>
    ///ссылка на основной скрипт системы бо€
    /// </summary>
    public bool CloseCell { get; set; }
   
}
