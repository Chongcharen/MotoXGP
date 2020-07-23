using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameDataManager : MonoSingleton<GameDataManager>
{
    public List<MapLocationData> mapLocationDatas;
    GameLevelData gameLevelData;
    public GameLevel gameLevel; //level for choose map
    public string currentMapChoose = "Forest01";

    public void Init(){
        gameLevel = new GameLevel();
        mapLocationDatas = new List<MapLocationData>();
    }
    public void SetUpGameLevel(int themeIndex,int stageIndex){
        gameLevel.theme = themeIndex;gameLevel.stage = stageIndex;
        gameLevel.gameStageData =  gameLevelData.gameThemesData[gameLevel.theme].gameStages[gameLevel.stage];
    }
    public void SaveMapData(List<MapLocationData> _data){
        mapLocationDatas.Clear();
        mapLocationDatas = _data.ToList();
    }

    //gamelevel
    public void SetUpGameLeveldata(GameLevelData _data){
        gameLevelData = _data;
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
        Debug.Log("Getlevelmap "+_mapName);
        foreach (var item in mapLocationDatas)
        {
            Debug.Log("map name "+item.mapName);
            Debug.Log(item.mapName == _mapName);
        }
        var finder = mapLocationDatas.Find(data => data.mapName == _mapName);
        Debug.Log("Finder "+finder);
        Debug.Assert(finder != null,"Mapdata not found in name "+_mapName);
        return finder;
    }
    
}
