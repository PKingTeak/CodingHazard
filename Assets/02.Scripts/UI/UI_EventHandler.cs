using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Action OnClickHandler = null;
    public Action OnPressedHandler = null;
    public Action OnPointerDownHandler = null;
    public Action OnPointerUpHandler = null;
    public Action OnPointerEnterHandler = null;
    public Action OnPointerExitHandler = null;
    bool _pressed = false;

    private void Update()
    {
        if (_pressed)
            OnPressedHandler?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke();
            AudioManager.Instance.PlayAudio(AudioID.ClickUI, 0.3f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        OnPointerDownHandler?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = true;
        OnPointerUpHandler?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterHandler?.Invoke();
        AudioManager.Instance.PlayAudio(AudioID.EnterUI, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitHandler?.Invoke();
    }
}
