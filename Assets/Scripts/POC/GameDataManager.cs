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
    }
    public void SaveMapData(List<MapLocationData> _data){
        mapLocationDatas.Clear();
        mapLocationDatas = _data.ToList();
    }
    public void SetUpGameLeveldata(GameLevelData _data){
        gameLevelData = _data;
    }
    public GameLevelData GameLevelData{get{return gameLevelData;}}
    public string GetStageName(){
        return gameLevelData.gameThemesData[gameLevel.theme].gameStages[gameLevel.stage].themeName+gameLevelData.gameThemesData[gameLevel.theme].gameStages[gameLevel.stage].stageName;
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
