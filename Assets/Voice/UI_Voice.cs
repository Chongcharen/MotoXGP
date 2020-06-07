using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Photon.Voice.Unity;
using Photon.Realtime;
using Photon.Pun;
public class UI_Voice : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject ui_voice_display;
    [SerializeField]Toggle t_debugEcho,t_transmit,t_mute;

    void Start()
    {
        PhotonConsole.OnJoinRoom.Subscribe(success =>{
            ui_voice_display.SetActive(success);
        });
        t_transmit.OnValueChangedAsObservable().Subscribe(active =>{
            PhotonConsole.Instance.SetTransmit(active);
        });
        t_debugEcho.OnValueChangedAsObservable().Subscribe(active =>{
            PhotonConsole.Instance.SetEchoMode(active);
        });
        t_mute.OnValueChangedAsObservable().Subscribe(active =>{
            PhotonConsole.Instance.SetMute(active);
        });
    }
    
}
