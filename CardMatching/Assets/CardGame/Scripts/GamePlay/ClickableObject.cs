using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour , IPointerClickHandler
{

    public Action OnClick = delegate { };

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
}
