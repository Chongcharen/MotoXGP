using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class MapMockupData
{
    public List<MapChoiceData> choiceMockupData;

    public void Generatedata(){
        choiceMockupData = new List<MapChoiceData>();
        for (int i = 0; i < 15; i++)
        {
            MapChoiceData mockupData = new MapChoiceData();
            mockupData.mapLevel = i;
            mockupData.mapType = "FOREST";
            mockupData.mapDetail = "เล่นให้ผ่านด่าน "+(i+1);
            choiceMockupData.Add(mockupData);
        }
       
        
    }
}
