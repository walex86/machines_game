using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool MouseUnderDeleteZone { get; private set; } = false;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseUnderDeleteZone = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseUnderDeleteZone = false;
    }
}
