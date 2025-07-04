using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ect 
{
    public enum EUIEvent
    {
        Click,
        Preseed,
        PointerDown,
        PointerUp,
        PointerEnter,
        PointerExit,    

    }
    public static void BindEvent(this GameObject go, Action action = null, Action<BaseEventData> dragAction = null, EUIEvent type = EUIEvent.Click)
    {
        UI_Base.BindEvent(go, action, dragAction, type);
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static void OnHoverSetting(GameObject HoverObj, bool IsHover)
    {
        HoverObj.transform.GetChild(0).gameObject.SetActive(IsHover);
    }
}
