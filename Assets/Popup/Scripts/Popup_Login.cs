using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;
public class Popup_Login : Popup
{
    [SerializeField]TextMeshProUGUI txt_header;
    [SerializeField]TextMeshProUGUI txt_detail;
    public static Popup_Login Launch(Dictionary<string,object> _parameter)
    {
        parameter = _parameter;
        var prefab = Resources.Load<Popup_Login>(PopupKeys.POPUP_LOGIN);
        return Instantiate<Popup_Login>(prefab);
    }
    public override void OnCreated()
    {
        if(parameter.ContainsKey(PopupKeys.PARAMETER_POPUP_HEADER))
            txt_header.text = parameter[PopupKeys.PARAMETER_POPUP_HEADER].ToString();
        if(parameter.ContainsKey(PopupKeys.PARAMETER_MESSAGE))
            txt_detail.text = parameter[PopupKeys.PARAMETER_MESSAGE].ToString();
        
        PlayFabController.OnPlayFabLoginComplete.Subscribe(_=>{
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
