using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] ItemBase _item;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Canvas _mainCanvas;
    private Image _image;

    private Transform _parantSlotTransform;

    public Transform ParantSlotTransform { get => _parantSlotTransform; set => _parantSlotTransform = value; }
    public ItemBase Item { get => _item; set => _item = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _mainCanvas = GetComponentInParent<Canvas>();
        ParantSlotTransform = transform.parent.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = _rectTransform.parent;
        transform.SetParent(_mainCanvas.transform);
        transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        SlotItemUI parant = transform.parent?.GetComponent<SlotItemUI>();
        if (parant == null)
        {
            transform.SetParent(ParantSlotTransform);
        }
        else
        {
            if (parant.ItemUI == null)
            {
                parant.ItemUI = this;
                _parantSlotTransform.GetComponent<SlotItemUI>().ItemUI = null;
                _parantSlotTransform = parant.transform;
            }
            else
            {    //если в слоте есть предмет          
                ItemUI itemUI = parant.ItemUI;//временно сохраняем это предмет            
                parant.ItemUI = this;//записываем в новый слот перетаскиваемый предмет
                itemUI.transform.SetParent(_parantSlotTransform);//назначаем родительский трансформу сохраненому предмету 
                itemUI.ParantSlotTransform = _parantSlotTransform;//записываем сохраненому предмету его родительский трансформ
                _parantSlotTransform.GetComponent<SlotItemUI>().ItemUI = itemUI;//записываем в слот из которого перетаскиваем сохраненый предмет 
                itemUI.transform.localPosition = Vector3.zero;//устанавливаем позицию сохраненого предмета в слот перетаскиваемого предмета
                _parantSlotTransform = parant.transform;//сохраняем родительску трансформу перетаскиваемому предмету

            }
        }
        transform.localPosition = Vector3.zero;

        _canvasGroup.blocksRaycasts = true;
    }

    public void SetSprite()
    {
        _image.sprite = _item.SpriteImage;
    }

}
