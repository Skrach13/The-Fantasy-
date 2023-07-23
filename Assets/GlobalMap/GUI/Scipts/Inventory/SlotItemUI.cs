using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItemUI : MonoBehaviour, IDropHandler
{
    [SerializeField] protected ItemUI _itemUI;
    public ItemUI ItemUI { get => _itemUI; set => _itemUI = value; }

    public void OnDrop(PointerEventData eventData)
    {
        var otherItemTransform = eventData.pointerDrag.transform;
        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;
    }
}
