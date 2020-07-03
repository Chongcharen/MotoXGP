using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField]GameObject display_login,display_lobby,display_room;

    [Header("Map")]
    [SerializeField]Canvas MapCanvas;
    [SerializeField]Camera MapCamera;

    [Header("Game")]
    [SerializeField]Canvas GameCanvas;

    public static PageManager Instance;
    void Awake(){
        Instance = this;
        SceneFlow.Instance.StartScene();
    }
    public void OpenLobby(){
        display_lobby.SetActive(true);
    }
    public void OpenRoom(){
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
