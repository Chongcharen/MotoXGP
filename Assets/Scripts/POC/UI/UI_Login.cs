﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class UI_Login : MonoBehaviour
{
    [SerializeField]GameObject root;
    [Header("PlayfabLogin")]
    [SerializeField]Button b_facebook_login;
    [SerializeField]Button b_google_login;
    [SerializeField]Button b_guest_login;
    [Header("Photon Login")]
    [SerializeField]TMP_InputField input_name;
    [SerializeField]Button b_connect;

    void Start(){
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
            if(root != null)
                root.gameObject.SetActive(false);
            PageManager.Instance.OpenLobby();
        });
        
        // b_google_login.OnClickAsObservable().Subscribe(_=>{
        //     PlayFabController.Instance.LoginWithGoogle();
        // });
        b_facebook_login.OnClickAsObservable().Subscribe(_=>{
            PlayFabController.Instance.LoginWithFacebook();
        });
        b_guest_login.OnClickAsObservable().Subscribe(_=>{
            PlayFabController.Instance.LoginWithDeviceId();
        });
        PlayFabController.OnPlayFabLoginComplete.Subscribe(_=>{
           // root.gameObject.SetActive(false);
          // PhotonNetworkConsole.Instance.Connect(input_name.text);\
            PhotonNetworkConsole.Instance.Connect();
        });
        PlayFabController.OnLogout.Subscribe(_=>{
            root.gameObject.SetActive(true);
        });

    }
}
