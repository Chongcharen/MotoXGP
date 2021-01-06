﻿using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class UI_Login : UIDisplay
{
    [Header("PlayfabLogin")]
    [SerializeField]Image fader;
    [SerializeField]Button b_facebook_login;
    [SerializeField]Button b_google_login;
    [SerializeField]Button b_guest_login;
    [Header("Photon Login")]
    [SerializeField]TMP_InputField input_name;
    [SerializeField]Button b_connect;

    void Awake(){
        if(PhotonNetworkConsole.Instance.isConnected){
            // if(root != null)
            //     root.gameObject.SetActive(false);
            //PageManager.Instance.OpenLobby();
           OpenLobbyWithDelay(2);
        }
        
    }
    private void OnEnable()
    {
         fader.enabled = true;
    }
    void Start(){
        Debug.Log("BoltNetwork.IsConnected "+BoltNetwork.IsConnected);
        Debug.Log("BoltNetwork.IsRunning "+BoltNetwork.IsRunning);
        Debug.Log("PlayFabController.Instance.IsLogin "+PlayFabController.Instance.IsLogin);
        id = UIName.LOGIN;
        UI_Manager.RegisterUI(this);
        if(PlayFabController.Instance.IsLogin){
            // if(!BoltNetwork.IsConnected){
            //     BoltLobbyNetwork.Instance.Connect();
            // }else
            // {
            //     OpenLobbyWithDelay(2);
            // }
            if(BoltNetwork.IsRunning){
                // if(BoltNetwork.IsConnected){
                //     OpenLobbyWithDelay(2);
                // }
                // else{
                //    Popup_Loading.Launch();
                //    //BoltLobbyNetwork.Instance.Connect();
                // }
                OpenLobbyWithDelay(2);
            }else{
                if(!BoltNetwork.IsConnected){
                    Popup_Loading.Launch();
                }
            }
            
           
        }else{
            fader.enabled = false;
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
            OpenLobbyWithDelay(2);
        }).AddTo(this);
        
        // b_google_login.OnClickAsObservable().Subscribe(_=>{
        //     PlayFabController.Instance.LoginWithGoogle();
        // });
        b_facebook_login.OnClickAsObservable().Subscribe(_=>{
            Popup_Loading.Launch();
            PlayFabController.Instance.LoginWithFacebook();
            var parameter = new Dictionary<string,object>();
            parameter.Add(PopupKeys.PARAMETER_POPUP_HEADER,"Logging...");
            parameter.Add(PopupKeys.PARAMETER_MESSAGE,"Logging Please Wait");
            Popup_Login.Launch(parameter);
        }).AddTo(this);
        b_guest_login.OnClickAsObservable().Subscribe(_=>{
            Popup_Loading.Launch();
            PlayFabController.Instance.LoginWithDeviceId();
            var parameter = new Dictionary<string,object>();
            parameter.Add(PopupKeys.PARAMETER_POPUP_HEADER,"Logging...");
            parameter.Add(PopupKeys.PARAMETER_MESSAGE,"Logging Please Wait");
            Popup_Login.Launch(parameter);
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
    // void OpenLobby(){
    //     Debug.Log("Open Lobby");
    //     UI_Manager.OpenUI(UIName.LOBBY);
    //     // if(root != null)
    //     //         root.gameObject.SetActive(false);
    //     //     PageManager.Instance.OpenLobby();
    // }
    async void OpenLobbyWithDelay(int millisecoundDelay){
        await Task.Delay(millisecoundDelay);
         UI_Manager.OpenUI(UIName.LOBBY);
    }
}
