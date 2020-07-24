using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UniRx;
public class Starter : MonoBehaviour
{
    public static Subject<string> OnNotification = new Subject<string>();
    MapDataDownloader mapDataDownloader;
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
        Debug.Log("aaaaaaaaaaabbbbb");
        Debug.Log("******************Initial******************");
        SceneFlow.Instance.StartScene();
        Application.targetFrameRate = 60;
        PhotonVoiceConsole.Instance.Init();
        PlayFabController.Instance.Init();
        GameDataManager.Instance.Init();
        DownloadData();
        //SceneManager.LoadScene(SceneName.LOBBY);
        // PhotonController.Instance.Init();
        // LobbyController.Instance.Init();
        // RoomController.Instance.Init();
        Debug.Log("Onload !!!");
    }
    void DownloadData(){
        OnNotification.OnNext("DownloadMapData");
        mapDataDownloader = new MapDataDownloader();
        mapDataDownloader.Start();
        mapDataDownloader.downloadComplete += OnMapDownlodComplete;
    }

    private void OnMapDownlodComplete(List<MapLocationData> mapLocationDataList)
    {
        OnNotification.OnNext("DownloadMapData Complete..");
        mapDataDownloader.downloadComplete -= OnMapDownlodComplete;
        GameDataManager.Instance.SaveMapData(mapLocationDataList);
        mapDataDownloader.Dispose();
        mapDataDownloader = null;
        LoadLevelData();
        //SceneManager.LoadScene(SceneName.LOBBY);
    }

    void LoadLevelData(){
        OnNotification.OnNext("Download LevelData..");
        Debug.Log("Application.persistentDataPath "+Application.streamingAssetsPath);
        var filePath = Path.Combine(Application.streamingAssetsPath,FilePath.GAME_LEVEL_DATA);
        string jsonString;

        if(Application.platform == RuntimePlatform.Android) //Need to extract file from apk first
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            jsonString= reader.text;
        }
        else
        {
            jsonString= GameUtil.GetTextFromFile(filePath);
        }


        //OnNotification.OnNext(path);
        //var jsonData = GameUtil.GetTextFromFile(path);
        Debug.Log(jsonString);
        Dictionary<string,object> gameleveldata = JsonConvert.DeserializeObject<Dictionary<string,object>>(jsonString);
        GameLevelData levelData = JsonConvert.DeserializeObject<GameLevelData>(jsonString);
        GameDataManager.Instance.SetUpGameLeveldata(levelData);
        OnNotification.OnNext("LevelData download complete prepareTo Login");
        SceneManager.LoadScene(SceneName.LOBBY);
        Debug.Log("leveldata "+levelData.version);
        Debug.Log("leveldata gameThemes"+levelData.gameThemesData.Count);
        Debug.Log("dic count "+gameleveldata.Count);
        foreach (var data in levelData.gameThemesData)
        {
            Debug.Log(string.Format("themename {0} stageLevel {1}",data.themeName,data.gameStages.Count));
        }
        //Debug.Log("gameleveldata "+gameleveldata.gameThemes);
        
    }
}
