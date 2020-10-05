using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using DevAhead.Data;
using UniRx;
public class GameDataManager : MonoSingleton<GameDataManager>
{
    public List<MapLocationData> mapLocationDatas;
    public GameConfigData gameConfigData;
    public EquipmentData equipmentData;
    GameLevelData gameLevelData;
    public GameLevel gameLevel; //level for choose map
    

    public void Init(){
        gameLevel = new GameLevel();
        gameLevel.theme = 0;
        gameLevel.stage = 0;
        gameLevel.level = 0;
        gameLevel.nosCount = 3;
        mapLocationDatas = new List<MapLocationData>();

        SpreadSheetGameConfig.OnUpdateGameConfigData.Subscribe(_ =>{
            gameConfigData = _;
            Debug.Log("gameConfigData "+gameConfigData.photonNetworkConfig.gameVersion);
            Debug.Log("gameConfigData "+gameConfigData.photonNetworkConfig.sendRate);
        }).AddTo(this);
    }
    public void SetUpGameLevel(int themeIndex,int stageIndex,int levelIndex,int _nosCount){
        gameLevel.theme = themeIndex;gameLevel.stage = stageIndex;gameLevel.level = levelIndex;
        gameLevel.gameStageData =  gameLevelData.gameThemesData[gameLevel.theme].gameStages[gameLevel.stage];
        gameLevel.nosCount = _nosCount;
    }
    public void SetUpGameConfigData(string jsonData){
        //gameConfigData = JsonConvert.DeserializeObject<GameConfigData>(jsonData);
        Debug.Log("jsondata "+jsonData);
        // var spreadSheetConverter = new SpreadSheetDataConverter();
        // gameConfigData = spreadSheetConverter.ConvertData<GameConfigData>(jsonData,new string[]{SpreadSheetKeys.HEADER_NAME,SpreadSheetKeys.HEADER_DATA,SpreadSheetKeys.HEADER_VALE});
        // Debug.Log("gameversion "+gameConfigData.photonNetworkConfig.gameVersion);
    }
    public void SaveMapData(List<MapLocationData> _data){
        mapLocationDatas.Clear();
        mapLocationDatas = _data.ToList();
    }
    //gamelevel
    public void SetupGameLeveldata(GameLevelData _data){
        gameLevelData = _data;
    }
    public void SetupEquipmentData(EquipmentData _data){
        equipmentData = _data;
    }
    public GameLevelData GameLevelData{get{return gameLevelData;}}
    public string GetStageName(){
        return gameLevelData.gameThemesData[gameLevel.theme].gameStages[gameLevel.stage].themeName+gameLevelData.gameThemesData[gameLevel.theme].gameStages[gameLevel.stage].stageName;
    }
    public GameStageData GetGamelevelByName(string mapName){
       
       GameStageData data = null;
        foreach (var themeData in gameLevelData.gameThemesData)
        {
            var query = from stage in themeData.gameStages
            where stage.themeName+stage.stageName == mapName
            select stage;

            foreach (var item in query)
            {
                data = item;
                break;
            }
        }
        Debug.Log("levelbyname "+data.themeName);
        Debug.Log("stage name "+data.stageName);
        return data;
       // gameLevelData.gameThemesData.Find(data =>data)
    }
    public MapLocationData GetLevelMap(string _mapName){
       // Debug.Log("Getlevelmap "+_mapName);
        foreach (var item in mapLocationDatas)
        {
            //Debug.Log("map name "+item.mapName);
            Debug.Log(item.mapName == _mapName);
        }
        var finder = mapLocationDatas.Find(data => data.mapName == _mapName);
        //Debug.Log("Finder "+finder);
        //Debug.Assert(finder != null,"Mapdata not found in name "+_mapName);
        return finder;
    }
    void OnApplicationQuit(){
        if(gameObject != null){
            Destroy(gameObject);
        }
    }
}
