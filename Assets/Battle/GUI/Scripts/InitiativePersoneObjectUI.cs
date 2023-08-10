using UnityEngine;
using UnityEngine.UI;

public class InitiativePersoneObjectUI : MonoBehaviour
{
    [SerializeField] private Image _iconPersone;
    [SerializeField] private Image _imageSelect;

    public Image IconPersone { get => _iconPersone; set => _iconPersone = value; }
    public Image ImageSelect { get => _imageSelect; set => _imageSelect = value; }
}
