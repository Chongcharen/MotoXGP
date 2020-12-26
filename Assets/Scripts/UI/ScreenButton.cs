using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;

public class ScreenButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public static Subject<KeyCode> OnGetKeyCode = new Subject<KeyCode>();
    public static Subject<KeyCode> OnCancelKeyCode = new Subject<KeyCode>();
    public KeyCode key;
    public void OnPointerDown(PointerEventData eventData)
    {
       OnGetKeyCode.OnNext(key);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnCancelKeyCode.OnNext(key);
    }
}
