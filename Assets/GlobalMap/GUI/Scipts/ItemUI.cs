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

    private Transform _slotTransform;

    public Transform SlotTransform { get => _slotTransform; set => _slotTransform = value; }
    public ItemBase Item { get => _item; set => _item = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _mainCanvas = GetComponentInParent<Canvas>();
        SlotTransform = transform.parent.transform;
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
        if (transform.parent.GetComponent<SlotItemUI>() == null)
        {
            transform.SetParent(SlotTransform);
        }
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
    private void OnEnable()
    {
        _image.sprite = _item.SpriteImage;
    }
  
}
