using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Bolt;
using UniRx;
public class Popup_BoltConnect : Popup
{

    [SerializeField]TextMeshProUGUI popup_header_txt,popup_message_txt;
    public static Popup_BoltConnect Launch(Dictionary<string,object> _parameter)
    {
        parameter = _parameter;
        var prefab = Resources.Load<Popup_BoltConnect>(PopupKeys.POPUP_BOLT_CONNECT);
        return Instantiate<Popup_BoltConnect>(prefab);
    }
    public override void OnCreated()
    {
        //throw new System.NotImplementedException();
        if(parameter.ContainsKey(PopupKeys.PARAMETER_POPUP_HEADER)){
            popup_header_txt.text = parameter[PopupKeys.PARAMETER_POPUP_HEADER].ToString();
        }
        if(parameter.ContainsKey(PopupKeys.PARAMETER_MESSAGE)){
            popup_message_txt.text = parameter[PopupKeys.PARAMETER_MESSAGE].ToString();
        }


        LobbyServerCallback.OnBoltStart.Subscribe(_=>{
             Dispose();
        }).AddTo(this);
        LobbyClientCallback.OnBoltStart.Subscribe(_ =>{
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
