using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Starter : MonoBehaviour
{
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    // static void OnLoad(){
    //     Debug.Log("******************Initial******************");
    //     SceneFlow.Instance.StartScene();
    //     Application.targetFrameRate = 60;
    //     PhotonVoiceConsole.Instance.Init();
    //     PlayFabController.Instance.Init();
    //     SceneManager.LoadScene(SceneName.LOBBY);
    //     // PhotonController.Instance.Init();
    //     // LobbyController.Instance.Init();
    //     // RoomController.Instance.Init();
    //     Debug.Log("Onload !!!");
    // }

    private void Start() {
        Debug.Log("******************Initial******************");
        SceneFlow.Instance.StartScene();
        Application.targetFrameRate = 60;
        PhotonVoiceConsole.Instance.Init();
        PlayFabController.Instance.Init();
        SceneManager.LoadScene(SceneName.LOBBY);
        // PhotonController.Instance.Init();
        // LobbyController.Instance.Init();
        // RoomController.Instance.Init();
        Debug.Log("Onload !!!");
    }
}
