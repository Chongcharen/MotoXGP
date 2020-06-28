using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField]GameObject display_login,display_lobby,display_room;

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
}
