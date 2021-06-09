using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseContentPresenter : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Image _icon;
    [SerializeField] protected TMP_Text _name;
    protected Action _onClickCallback;
    protected Action _onRightClickCallback;
    protected float _prevClickTime;

    public void Init(ElementData elementData, Action onClickCallback, Action onRightClickCallback = null)
    {
        _icon.sprite = elementData.ElementSprite;
        _icon.SetNativeSize();
        var rect = _icon.GetComponent<RectTransform>();
        if (rect.sizeDelta.x > 230)
        {
            var newSize = rect.sizeDelta;
            newSize.x = 230;
            newSize.y = (230f / rect.sizeDelta.x) * newSize.y;
            rect.sizeDelta = newSize;
        }
        if (rect.sizeDelta.y > 90)
        {
            var newSize = rect.sizeDelta;
            newSize.y = 90;
            newSize.x = (90f / rect.sizeDelta.y) * newSize.x;
            rect.sizeDelta = newSize;
        }
        _name.text = elementData.ElementDisplayName;
        _onClickCallback = onClickCallback;
        _onRightClickCallback = onRightClickCallback;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(Time.time);
        if (Time.time - _prevClickTime < 1)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
               _onClickCallback?.Invoke(); 
            }
            else
            {
                _onRightClickCallback?.Invoke();
            }
        }

        _prevClickTime = Time.time;
    }
}
