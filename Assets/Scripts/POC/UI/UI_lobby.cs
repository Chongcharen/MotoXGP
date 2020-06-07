using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Photon.Realtime;
using Photon.Pun;
public class UI_lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_joinRandomRoom;

    void Start(){
        b_joinRandomRoom.OnClickAsObservable().Subscribe(_=>{
            PhotonNetworkConsole.Instance.JoinRandomRoom(null);
        });
        root.ObserveEveryValueChanged(r =>r.gameObject.activeSelf).Subscribe(active =>{

            Debug.Log("UI _lobby Active "+active);
        });
    }
    public override void OnJoinedRoom(){
        root.gameObject.SetActive(false);
        PageManager.Instance.OpenRoom();
    }
    
}
