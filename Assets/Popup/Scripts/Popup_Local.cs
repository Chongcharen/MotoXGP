using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UniRx;
public class Popup_Local : Popup {
    public TextMeshProUGUI header_txt, detail_txt;
    public Button b_close, b_ok, b_yes, b_no;
    public GameObject object_ok;
    public GameObject object_yesno;
    Action yes_callback, no_callback, ok_callback;
    static Dictionary<string, object> parameter = new Dictionary<string, object>();
    public static Popup_Local Launch(Dictionary<string,object> _parameter)
    {
        parameter = _parameter;
        var prefab = Resources.Load<Popup_Local>(PopupKeys.POPUP_BOLT_CONNECT);
        return Instantiate<Popup_Local>(prefab);
    }
    void SocketCluster_OnConnected(string obj)
    {
        if(this.gameObject != null)
            Destroy(this.gameObject);
    }

    public override void OnShow()
    {

    }

    // public override void OnCreate()
    // {
    //     ok_callback = null;
    //     yes_callback = null;
    //     no_callback = null;
    //     b_close.OnClickAsObservable().Subscribe(_=>{
    //         Dispose();
    //     });
    //     b_ok.OnClickAsObservable().Subscribe(_=>{
    //         Dispose();
    //     });
    //     // if(parameter.ContainsKey(ParameterKeys.Message)){
    //     //     detail_txt.text = parameter[ParameterKeys.Message].ToString();
    //     // }
    //     // if(parameter.ContainsKey(ParameterKeys.Header)){
    //     //     header_txt.text = parameter[ParameterKeys.Header].ToString();
    //     // }else{
    //     //     header_txt.text = "Message";
    //     // }
    //     // if (parameter.ContainsKey(StaticKeys.PARAMETER_MESSAGE))
    //     // {
    //     //     detail_txt.text = parameter[StaticKeys.PARAMETER_MESSAGE].ToString();
    //     // }
    //     // if (parameter.ContainsKey(StaticKeys.PARAMETER_HEADER))
    //     // {
    //     //     header_txt.text = parameter[StaticKeys.PARAMETER_HEADER].ToString();
    //     // }
    //     // if (parameter.ContainsKey(StaticKeys.PARAMETER_SOCKETLISTENER))
    //     // {
    //     //     Debug.Log("PARAMETER_SOCKETLISTENER");
    //     // }
    //     // if (parameter.ContainsKey(StaticKeys.PARAMETER_MESSAGE))
    //     // {
    //     //     detail_txt.text = parameter[StaticKeys.PARAMETER_MESSAGE].ToString();
    //     // }
    //     // if (parameter.ContainsKey(StaticKeys.PARAMETER_TYPE))
    //     // {
    //     //     // prices = (List<ConvertPrice>)parameter["purchaseData"];
    //     //     if (parameter[StaticKeys.PARAMETER_TYPE].ToString() == StaticKeys.POPUP_TYPE_YESNO)
    //     //     {
    //     //         object_ok.SetActive(false);
    //     //         object_yesno.SetActive(true);
    //     //     }
    //     //     else
    //     //     {
    //     //         object_ok.SetActive(true);
    //     //         object_yesno.SetActive(false);
    //     //     }
    //     // }
    //     // if (parameter.ContainsKey(StaticKeys.PARAMETER_ERRORCODE))
    //     // {
    //     //     LincolnText.Get("error_"+parameter[StaticKeys.PARAMETER_ERRORCODE].ToString(), parameter[StaticKeys.PARAMETER_ERRORCODE].ToString(), (string text) => {
    //     //         detail_txt.text = text;
    //     //     });
    //     // }
    //     // if (parameter.ContainsKey(StaticKeys.ACTION_YES)) yes_callback = (Action)parameter[StaticKeys.ACTION_YES];
    //     // if (parameter.ContainsKey(StaticKeys.ACTION_NO)) no_callback = (Action)parameter[StaticKeys.ACTION_NO];
    //     // if (parameter.ContainsKey(StaticKeys.ACTION_OK)) ok_callback = (Action)parameter[StaticKeys.ACTION_OK];
    //     // b_ok.onClick.AddListener(() =>
    //     // {
    //     //     if (ok_callback != null) ok_callback.Invoke();
    //     //     Dispose();
    //     // });
    //     // b_yes.onClick.AddListener(() =>
    //     // {
    //     //     if (yes_callback != null) yes_callback.Invoke();
    //     //     Dispose();
    //     // });
    //     // b_no.onClick.AddListener(() =>
    //     // {
    //     //     if (no_callback != null) no_callback.Invoke();
    //     //     Dispose();
    //     // });
    //     // SocketCluster.OnConnected += SocketCluster_OnConnected;
    // }
    public override void OnDestroy()
    {
        Dispose();
    }
    // public override void Dispose()
    // {
    //     if(this.gameObject!= null)
    //         Destroy(this.gameObject);
    // }

    public override void OnCreated()
    {
        throw new NotImplementedException();
    }
}
