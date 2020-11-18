using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class UI_Login : UIDisplay
{
    [Header("PlayfabLogin")]
    [SerializeField]Button b_facebook_login;
    [SerializeField]Button b_google_login;
    [SerializeField]Button b_guest_login;
    [Header("Photon Login")]
    [SerializeField]TMP_InputField input_name;
    [SerializeField]Button b_connect;

    void Awake(){
        if(PhotonNetworkConsole.Instance.isConnected){
            if(root != null)
                root.gameObject.SetActive(false);
            PageManager.Instance.OpenLobby();
        }
        
    }
    void Start(){
        Debug.Log("BoltNetwork.IsConnected "+BoltNetwork.IsConnected);
        id = UIName.LOGIN;
        UI_Manager.RegisterUI(this);
        if(PlayFabController.Instance.IsLogin){
            OpenLobby();
        }
        // b_connect.OnClickAsObservable().Subscribe(_=>{
        //     if(string.IsNullOrEmpty(input_name.text))return;
        //     PhotonNetworkConsole.Instance.Connect(input_name.text);
        // });
        // GPGSAuthentication.OnGoogleLoginSuccess.Subscribe( _=>{
        //     b_google_login.interactable = false;
        //     if(!_)
        //         b_google_login.gameObject.SetActive(false);
        // });
        PhotonNetworkConsole.OnConnectedServer.Subscribe(connected =>{
            OpenLobby();
        }).AddTo(this);
        
        // b_google_login.OnClickAsObservable().Subscribe(_=>{
        //     PlayFabController.Instance.LoginWithGoogle();
        // });
        b_facebook_login.OnClickAsObservable().Subscribe(_=>{
            PlayFabController.Instance.LoginWithFacebook();
        }).AddTo(this);
        b_guest_login.OnClickAsObservable().Subscribe(_=>{
            PlayFabController.Instance.LoginWithDeviceId();
        });
        PlayFabController.OnPlayFabLoginComplete.Subscribe(_=>{
            AddressableManager.Instance.Init();
            //BoltLobbyNetwork.Instance.Connect();
        }).AddTo(this);
        AddressableManager.OnDownloadDependenciesCompleted.Subscribe(_=>{
            BoltLobbyNetwork.Instance.Connect();
        }).AddTo(this);
        PlayFabController.OnLogout.Subscribe(_=>{
            root.gameObject.SetActive(true);
        }).AddTo(this);

    }
    void OpenLobby(){
        if(root != null)
                root.gameObject.SetActive(false);
            PageManager.Instance.OpenLobby();
    }
}
