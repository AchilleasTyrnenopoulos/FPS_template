using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnHoverEvent;

    private void OnEnable()
    {
        OnHoverEvent.AddListener(SetIsSelected);
    }

    private void OnDisable()
    {
        OnHoverEvent.RemoveListener(SetIsSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {        
        OnHoverEvent?.Invoke();
        Debug.Log("Hovered " + eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }    

    public void SetIsSelected()
    {
        var button = GetComponent<Button>();
        button.Select();
    }
}
