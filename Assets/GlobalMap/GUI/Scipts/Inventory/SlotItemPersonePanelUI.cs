using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItemPersonePanelUI : MonoBehaviour, IDropHandler
{
    public enum SlotPersoneType
    {
        RightHanded,
        LeftHanded,
        Body,
        Necklece,
        Ring,
        Potion
    }

    [SerializeField] private ItemUI _itemUI;
    [SerializeField] private SlotPersoneType _slotPersoneType;

    public ItemUI ItemUI { get => _itemUI; set => _itemUI = value; }

    public void OnDrop(PointerEventData eventData)
    {
        var otherItemTransform = eventData.pointerDrag.transform;
        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;
    }


}
