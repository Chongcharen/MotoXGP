using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameDataManager : MonoSingleton<GameDataManager>
{
    public List<MapLocationData> mapLocationDatas;
    public string currentMapChoose = "Mountain01";
    public void Init(){
        mapLocationDatas = new List<MapLocationData>();
    }
    public void SaveMapData(List<MapLocationData> _data){
        mapLocationDatas.Clear();
        mapLocationDatas = _data.ToList();
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
