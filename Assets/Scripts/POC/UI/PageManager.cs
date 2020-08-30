﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class PageManager : MonoBehaviour
{
    [SerializeField]GameObject display_login,display_lobby,display_room;
    public UI_Room UI_Room;

    [Header("Map")]
    [SerializeField]Canvas MapCanvas;
    [SerializeField]Camera MapCamera;

    [Header("Game")]
    [SerializeField]Canvas GameCanvas;

    public static PageManager Instance;
    void Awake(){
        Instance = this;
        SceneFlow.Instance.StartScene();
        BoltLobbyNetwork.OnBoltConnected.Subscribe(_=>{
            OpenLobby();
        }).AddTo(this);
        LobbyClientCallback.OnJoinSession.Subscribe(_=>{
            OpenRoom();
        }).AddTo(this);
        UI_Room = GetComponent<UI_Room>();
    }
    public void OpenLobby(){
        display_room.SetActive(false);
        display_lobby.SetActive(true);
    }
    public void OpenRoom(){
        display_lobby.SetActive(false);
        display_room.SetActive(true);
    }

    public void OpenMap(){
        MapCanvas.enabled =true;
        MapCamera.enabled = true;
        GameCanvas.enabled = false;
    }
    public void CloseMap(){
        MapCanvas.enabled = false;
        MapCamera.enabled = false;
        GameCanvas.enabled = true;
    }
}
