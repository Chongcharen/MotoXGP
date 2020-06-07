using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class UI_Login : MonoBehaviour
{
    [SerializeField]GameObject root;
    [SerializeField]TMP_InputField input_name;
    [SerializeField]Button b_connect;

    void Start(){
        b_connect.OnClickAsObservable().Subscribe(_=>{
            if(string.IsNullOrEmpty(input_name.text))return;
            PhotonNetworkConsole.Instance.Connect(input_name.text);
        });
        PhotonNetworkConsole.OnConnectedServer.Subscribe(connected =>{
            root.gameObject.SetActive(false);
            PageManager.Instance.OpenLobby();
        });
    }
}
