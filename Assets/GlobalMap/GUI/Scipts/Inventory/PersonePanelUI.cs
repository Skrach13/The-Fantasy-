using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonePanelUI : MonoBehaviour
{
    [SerializeField] private Image _imagePersone;
    [SerializeField] private TextMeshProUGUI _namePersone;
    [Tooltip("���������� ������ ��������� � ����������� ��������� ��� ���������� ��������� � ���������� ������")]
    [SerializeField] private SlotItemUI[] _itemSlots;

    public void UpddatePanel(PlayerPersone playerPersone)
    {
        _imagePersone = playerPersone.Image;
        _namePersone.text = playerPersone.Name;

    }
}
