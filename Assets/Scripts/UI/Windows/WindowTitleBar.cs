using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowTitleBar : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler
{

    [SerializeField]
    Window window;

    Vector2 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        window.transform.parent.SetSiblingIndex(2);
        offset = eventData.position - (Vector2)window.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        window.transform.position = eventData.position - offset;
    }
}