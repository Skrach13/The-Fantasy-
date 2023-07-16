using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonePanelUI : MonoBehaviour
{
    [SerializeField] private Image _imagePersone;
    [SerializeField] private TextMeshProUGUI _namePersone;
    [Tooltip("Количество должно совподать с количеством возможных для экиперовки предметов у персонажей игрока")]
    [SerializeField] private SlotItemPersonePanelUI[] _itemSlots;

    private PlayerPersone _PersoneSelect;

    public void UpddatePanel(PlayerPersone playerPersone)
    {
        _imagePersone.sprite = playerPersone.Sprite;
        _namePersone.text = playerPersone.Name;
        _PersoneSelect = playerPersone;


    }
}
