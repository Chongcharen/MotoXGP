using System.Security.Cryptography.X509Certificates;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UniRx;
using DevAhead.Data;
using System.Linq;
using UnityEngine.Networking;

public class Starter : MonoBehaviour
{
    public static Subject<string> OnNotification = new Subject<string>();
    Dictionary<int,bool> loaderSheetData = new Dictionary<int, bool>();
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
        GUIDebug.Instance.Init();
        GUIDebug.Log("GUIDebug init");
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
        SpreadSheetDataConverter.OnCompleted.Subscribe(id =>{
            CheckCopmpletedData(id);
        });
    }
    void DownloadData(){
        OnNotification.OnNext("DownloadMapData");
        var spreadSheetGameconfig = new SpreadSheetGameConfig();
        spreadSheetGameconfig.Start();
        loaderSheetData.Add(spreadSheetGameconfig.GetHashCode(),false);
        mapDataDownloader = new MapDataDownloader();
        mapDataDownloader.Start();
        mapDataDownloader.downloadComplete += OnMapDownlodComplete;
        print(Depug.Log("Loadcustomdata ",Color.blue));
        

    }

    private void OnMapDownlodComplete(List<MapLocationData> mapLocationDataList)
    {
        OnNotification.OnNext("DownloadMapData Complete..");
        mapDataDownloader.downloadComplete -= OnMapDownlodComplete;
        GameDataManager.Instance.SaveMapData(mapLocationDataList);
        mapDataDownloader.Dispose();
        mapDataDownloader = null;
        LoadLevelData();
        LoadGoogleEquipmentData();
    }
    void LoadGoogleEquipmentData(){
        var helmetData = new CustomDataDownloader(SpreadSheetKeys.EQUIPMENT,SpreadSheetKeys.GID_HELMET);
        var suitData = new CustomDataDownloader(SpreadSheetKeys.EQUIPMENT,SpreadSheetKeys.GID_SUIT);
        var gloveData = new CustomDataDownloader(SpreadSheetKeys.EQUIPMENT,SpreadSheetKeys.GID_GLOVE);
        var bootData = new CustomDataDownloader(SpreadSheetKeys.EQUIPMENT,SpreadSheetKeys.GID_BOOT);
       // Depug.Log("spreadsheetDownloadUrl "+spreadsheetDownloadUrl,Color.green);
        EquipmentData equipmentData = new EquipmentData();
        equipmentData.data = new Dictionary<string, List<PartEquipmentData>>();
        GameDataManager.Instance.SetupEquipmentData(equipmentData);
        helmetData.Start();
        helmetData.downloadComplete += jsonString =>{
            Debug.Log("json "+jsonString);
            var equipmentDataList = JsonConvert.DeserializeObject<List<PartEquipmentData>>(jsonString);
            for (int i = 0; i < equipmentDataList.Count; i++)
            {
                Debug.Log(equipmentDataList[i].icon_name);
            }
            equipmentData.data.Add(EquipmentKeys.HELMET,equipmentDataList);
            suitData.Start();
            helmetData.Dispose();
            
        };
       
        suitData.downloadComplete += jsonString =>{
            
            var equipmentDataList = JsonConvert.DeserializeObject<List<PartEquipmentData>>(jsonString);
            for (int i = 0; i < equipmentDataList.Count; i++)
            {
                Debug.Log(equipmentDataList[i].icon_name);
            }
            equipmentData.data.Add(EquipmentKeys.SUIT,equipmentDataList);
            gloveData.Start();
            suitData.Dispose();
        };
        gloveData.downloadComplete += jsonString =>{
            Debug.Log("gloveData json "+jsonString);
            var equipmentDataList = JsonConvert.DeserializeObject<List<PartEquipmentData>>(jsonString);
            for (int i = 0; i < equipmentDataList.Count; i++)
            {
                Debug.Log(equipmentDataList[i].icon_name);
            }
            equipmentData.data.Add(EquipmentKeys.GLOVE,equipmentDataList);
            bootData.Start();
            gloveData.Dispose();
        };
        bootData.downloadComplete += jsonString =>{
            Debug.Log("gloveData json "+jsonString);
            var equipmentDataList = JsonConvert.DeserializeObject<List<PartEquipmentData>>(jsonString);
            for (int i = 0; i < equipmentDataList.Count; i++)
            {
                Debug.Log(equipmentDataList[i].icon_name);
            }
            equipmentData.data.Add(EquipmentKeys.BOOT,equipmentDataList);
            bootData.Dispose();

            var jsonequipment = JsonConvert.SerializeObject(equipmentData);
            Debug.Log("jsonequipment "+jsonequipment);
            GameDataManager.Instance.SetupEquipmentData(equipmentData);
        };
        
        
       
    }
    void LoadEquipmentData(){
        OnNotification.OnNext("Download LoadEquipmentData..");
        Debug.Log("Application.persistentDataPath "+Application.streamingAssetsPath);
        var filePath = Path.Combine(Application.streamingAssetsPath,FilePath.GAME_EQUIPMENT_DATA);
        string jsonString = "";
        OnNotification.OnNext("jsonString "+filePath);
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

        Debug.Log("equipmentdata "+jsonString);
        EquipmentData equipmentData = JsonConvert.DeserializeObject<EquipmentData>(jsonString);
        GameDataManager.Instance.SetupEquipmentData(equipmentData);
        Debug.Log("COUJT "+GameDataManager.Instance.equipmentData.data.Count);
        foreach (var item in GameDataManager.Instance.equipmentData.data)
        {
            Debug.Log(item.Key);
        }
    }

    void LoadLevelData(){
        OnNotification.OnNext("Download LevelData..");
        Debug.Log("Application.persistentDataPath "+Application.streamingAssetsPath);
        var filePath = Path.Combine(Application.streamingAssetsPath,FilePath.GAME_LEVEL_DATA);
        string jsonString;
        OnNotification.OnNext("jsonString "+filePath);
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
        OnNotification.OnNext("jsonString .."+jsonString);
        Debug.Log(jsonString);
        
        // Dictionary<string,object> gameleveldata = JsonConvert.DeserializeObject<Dictionary<string,object>>(jsonString);
        // GameLevelData levelData = JsonConvert.DeserializeObject<GameLevelData>(jsonString);

        Dictionary<string,object> gameleveldata = JsonConvert.DeserializeObject<Dictionary<string,object>>(jsonString);//JsonUtility.FromJson<Dictionary<string,object>>(jsonString); //JsonConvert.DeserializeObject<Dictionary<string,object>>(jsonString);
        GameLevelData levelData = JsonConvert.DeserializeObject<GameLevelData>(jsonString);//JsonUtility.FromJson<GameLevelData>(jsonString);//JsonConvert.DeserializeObject<GameLevelData>(jsonString);
        GameDataManager.Instance.SetupGameLeveldata(levelData);

        
        OnNotification.OnNext("LevelData download complete prepareTo Login");
        CheckCopmpletedData(-1);
        // Debug.Log("leveldata "+levelData.version);
        // Debug.Log("leveldata gameThemes"+levelData.gameThemesData.Count);
        // Debug.Log("dic count "+gameleveldata.Count);
        // foreach (var data in levelData.gameThemesData)
        // {
        //     Debug.Log(string.Format("themename {0} stageLevel {1}",data.themeName,data.gameStages.Count));
        // }
        //Debug.Log("gameleveldata "+gameleveldata.gameThemes);
    }

    void CheckCopmpletedData(int instanceID){
        Debug.Log("CheckCompletedData ");
        Debug.Log("all sheet download "+loaderSheetData.All(data =>data.Value == true));
        OnNotification.OnNext("level data "+GameDataManager.Instance.GameLevelData);
        //.OnNext("level data "+GameDataManager.Instance.GameLevelData.gameThemesData[0].gameStages.Count);
        if(loaderSheetData.ContainsKey(instanceID)){
            loaderSheetData[instanceID] = true;
        }
        if(GameDataManager.Instance.GameLevelData == null)return;
        if(!loaderSheetData.All(data =>data.Value == true))return;
        SceneManager.LoadScene(SceneName.LOBBY);
    }
}
