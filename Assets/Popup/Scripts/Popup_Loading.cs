using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
public class Popup_Loading : Popup
{
    [SerializeField]TextMeshProUGUI txt_header;
    [SerializeField]TextMeshProUGUI txt_detail;
    public static Popup_Loading Launch(Dictionary<string,object> _parameter = null)
    {
        parameter = _parameter;
        var prefab = Resources.Load<Popup_Loading>(PopupKeys.POPUP_LOADING);
        return Instantiate<Popup_Loading>(prefab);
    }

    public override void OnCreated()
    {
        //throw new System.NotImplementedException();
        UIDisplay.OnUIOpened.Subscribe(id =>{
            Dispose();
        }).AddTo(this);
    }

    public override void OnDestroy()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnShow()
    {
        //throw new System.NotImplementedException();
    }
}
