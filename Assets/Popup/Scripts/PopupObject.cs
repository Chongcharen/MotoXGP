using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class PopupObject : MonoBehaviour
{
    public abstract void OnShow();
    public abstract void OnCreate();
    public abstract void OnDestroy();
    public abstract void Dispose();
    private void OnEnable()
    {
        OnCreate();
        OnShow();
    }
    private void OnDisable()
    {
        OnDestroy();
    }
}
