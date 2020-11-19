using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class UIDisplay : MonoBehaviour
{
    public GameObject root;
    public string id;
    public virtual void Open(){
        root.gameObject.SetActive(true);
    }
    public virtual void Close(){
        root.gameObject.SetActive(false);
    }
}
