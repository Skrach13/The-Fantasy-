using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonePanelUI : MonoBehaviour
{
    [SerializeField] private Image _imagePersone;
    [SerializeField] private TextMeshProUGUI _namePersone;
    [Tooltip("Количество должно совподать с количеством возможных для экиперовки предметов у персонажей игрока")]
    [SerializeField] private SlotItemPersonePanelUI[] _itemSlots;
    [SerializeField] private ItemUI _itemUIPrefab;

    private PlayerPersone _PersoneSelect;

    public PlayerPersone PersoneSelect { get => _PersoneSelect; set => _PersoneSelect = value; }

    public void UpddatePanel(PlayerPersone playerPersone)
    {
        _imagePersone.sprite = playerPersone.Sprite;
        _namePersone.text = playerPersone.Name;
        PersoneSelect = playerPersone;
        int i = 0;
        UpdateSlot(playerPersone.RightHandItem.Item, i); i++;
        UpdateSlot(playerPersone.LeftHandItem.Item, i); i++;
        UpdateSlot(playerPersone.BodyItem.Item, i); i++;
        UpdateSlot(playerPersone.NecklaceItem.Item, i); i++;
        UpdateSlot(playerPersone.RingItem.Item, i); i++;
        UpdateSlot(playerPersone.PotionItem.Item, i);

    }

    private void UpdateSlot(ItemBase itemBase,int indexSlot)
    {
        if (_itemSlots[indexSlot].ItemUI == null)
        {
            if (itemBase != null)
            {
                ItemUI itemUI = Instantiate(_itemUIPrefab, _itemSlots[indexSlot].transform);
                //TODO
                _itemSlots[indexSlot].ItemUI = itemUI;
                itemUI.Item = ItemsData.Instance.GetItem(itemBase.ItemID);
                itemUI.name = itemUI.Item.name;
                itemUI.SetSprite();
            }
        }
        else if (_itemSlots[indexSlot].ItemUI != null && itemBase != null)
        {
            _itemSlots[indexSlot].ItemUI.Item = itemBase;
            _itemSlots[indexSlot].ItemUI.SetSprite();
        }
        else if (_itemSlots[indexSlot].ItemUI != null && itemBase == null)
        {
            Destroy(_itemSlots[indexSlot].ItemUI.gameObject);
        }
    }
}
