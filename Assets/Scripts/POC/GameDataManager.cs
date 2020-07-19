using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameDataManager : MonoSingleton<GameDataManager>
{
    public List<MapLocationData> mapLocationDatas;
    public void Init(){
        mapLocationDatas = new List<MapLocationData>();
    }
    public void SaveMapData(List<MapLocationData> _data){
        mapLocationDatas.Clear();
       mapLocationDatas = _data.ToList();
    }
}
