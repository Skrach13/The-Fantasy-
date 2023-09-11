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
            {    //���� � ����� ���� �������          
                ItemUI itemUI = parant.ItemUI;//�������� ��������� ��� �������            
                parant.ItemUI = this;//���������� � ����� ���� ��������������� �������
                itemUI.transform.SetParent(_parantSlotTransform);//��������� ������������ ���������� ����������� �������� 
                itemUI.ParantSlotTransform = _parantSlotTransform;//���������� ����������� �������� ��� ������������ ���������
                _parantSlotTransform.GetComponent<SlotItemUI>().ItemUI = itemUI;//���������� � ���� �� �������� ������������� ���������� ������� 
                itemUI.transform.localPosition = Vector3.zero;//������������� ������� ����������� �������� � ���� ���������������� ��������
                _parantSlotTransform = parant.transform;//��������� ����������� ���������� ���������������� ��������

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
