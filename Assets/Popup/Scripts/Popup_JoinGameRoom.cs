using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
public class Popup_JoinGameRoom : Popup
{
    [SerializeField]TextMeshProUGUI txt_header;
    [SerializeField]TextMeshProUGUI txt_detail;
    public static Popup_JoinGameRoom Launch(Dictionary<string,object> _parameter)
    {
        parameter = _parameter;
        var prefab = Resources.Load<Popup_JoinGameRoom>(PopupKeys.POPUP_JOINGAMEROOM);
        return Instantiate<Popup_JoinGameRoom>(prefab);
    }
    public override void OnCreated()
    {
        if(parameter.ContainsKey(PopupKeys.PARAMETER_POPUP_HEADER))
            txt_header.text = parameter[PopupKeys.PARAMETER_POPUP_HEADER].ToString();
        if(parameter.ContainsKey(PopupKeys.PARAMETER_MESSAGE))
            txt_detail.text = parameter[PopupKeys.PARAMETER_MESSAGE].ToString();
        LobbyClientCallback.OnJoinSession.Subscribe(_=>{
            Dispose();
        }).AddTo(this);
        LobbyServerCallback.OnJoinSession.Subscribe(_=>{
            Dispose();
        }).AddTo(this);
        BoltLobbyNetwork.OnSessionEndProgress.Subscribe(_=>{
            Dispose();
        }).AddTo(this);
        //throw new System.NotImplementedException();
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
