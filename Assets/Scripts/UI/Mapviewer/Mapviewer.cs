using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Mapviewer : MonoBehaviour
{
    string mapName;
    void Start(){
        UI_Mapviewer.OnGenerateMap.Subscribe(_mapName =>{
            mapName = _mapName;
            GenerateMap();
        }).AddTo(this);
    }
    void GenerateMap(){
        MapModelGenerator.Instance.GenerateMapByName(mapName);
    }
}
