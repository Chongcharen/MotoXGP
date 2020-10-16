using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class MapDataDownloader : IDisposable
{
    public delegate void OnDownloadComplete (List<MapLocationData> mapLocationDataList);
    public event OnDownloadComplete downloadComplete;
    public string documentKey = "1SEHi1SfZJn5WKAjzCUn-HLecthRc5iS-F-TsHI8_e3A";
        //view format
    public string addressView = "https://docs.google.com/spreadsheets/d/";
    public string viewFormat = "/edit#gid=0";
        //exportFormat
    public string addressExport = "https://docs.google.com/feeds/download/spreadsheets/Export?key=";  
    public string exportFormat = "&exportFormat=csv&gid=0";
    string spreadsheetDownloadUrl = "";
    string spreadsheetViewUrl = "";
    //https://docs.google.com/feeds/download/spreadsheets/Export?key=1uvrUKt9BaCKnwfKchMHLyQwqKTTBPJjl1COnzymROP0&exportFormat=csv&gid=0
    const string SPLIT_REX = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    const string LINE_SPLIT_REX = @"\r\n|\n\r|\n|\r";
    const string LANGUAGE_HEADER_PREFIX = "lang-";
    string[] headerKeys;
    int[] headerIndexs;
    List<MapLocationData> mapLocationDatas = new List<MapLocationData>();

    MapLocationData targetMapLocationData; 
    public void Start(){
        string[] headers = new string[]{MapKeys.Map_Name,MapKeys.Map_Start,MapKeys.Object_Terrain,MapKeys.Object_Name,MapKeys.Position_X,MapKeys.Position_Y,MapKeys.Position_Z};
        Download(headers);
    }
    public void Download(string[] _headerKeys){

            headerKeys = _headerKeys.ToArray();
            headerIndexs = new int[headerKeys.Length];
            spreadsheetDownloadUrl = addressExport+documentKey+exportFormat;
            spreadsheetViewUrl = addressView+documentKey+viewFormat;
            //1
            var downloadHandler = new DownloadHandlerBuffer();
            var webRequest = new UnityWebRequest(spreadsheetDownloadUrl, "GET", downloadHandler, null);
            var ops = webRequest.SendWebRequest();
            ops.completed += (obj) =>
            {
                if(downloadHandler.isDone)
                    PraseTranslation(downloadHandler.text);
            };
            
        }
    void PraseTranslation(string rawCsv){
        Debug.Log("------------------------------CSV "+rawCsv);
        Debug.Assert(!string.IsNullOrEmpty(rawCsv),"Map data not Found");
        var lines = Regex.Split(rawCsv, LINE_SPLIT_REX);
        var header = Regex.Split(lines[0], SPLIT_REX);
        for (int i = 0; i < lines.Length; i++)
            {
                        //Debug.Log(lines[i]);
            }
        var levelIndex = System.Array.FindIndex(header,(item) => {return item == headerKeys[0];});
        var mapPositionIndex = System.Array.FindIndex(header,(item)=>{return item == headerKeys[1]; });
        var objectTerrainIndex = System.Array.FindIndex(header,(item)=>{return item == headerKeys[2]; });
        var objectIndex = System.Array.FindIndex(header,(item)=>{return item == headerKeys[3]; });
        var posXindex = System.Array.FindIndex(header, (item) => { return item == headerKeys[4]; });
        var posYindex = System.Array.FindIndex(header, (item) => { return item == headerKeys[5]; });
        var posZindex = System.Array.FindIndex(header, (item) => { return item == headerKeys[6]; });
        for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_REX);//3 column
                var level = values[levelIndex]; // level
                var mapStartPositon = values[mapPositionIndex]; //hint
                var objectTerrain = values[objectTerrainIndex];
                var objectName = values[objectIndex]; //hint
                var posX = values[posXindex]; // place
                var posY = values[posYindex]; // place
                var posZ = values[posZindex]; // place
                if(!string.IsNullOrEmpty(level)){
                    targetMapLocationData = new MapLocationData();
                    targetMapLocationData.mapName = level;
                   // Debug.Log("Mapname "+targetMapLocationData.mapName);
                    targetMapLocationData.startPositionDatas = new List<Vector3>();
                    targetMapLocationData.objectTerrainDatas = new List<ObjectLocationData>();
                    targetMapLocationData.objectLocationDatas = new List<ObjectLocationData>();
                    mapLocationDatas.Add(targetMapLocationData);
                    continue;
                }
                if(!string.IsNullOrEmpty(mapStartPositon)){
                    if(string.IsNullOrEmpty(posX)||string.IsNullOrEmpty(posY)||string.IsNullOrEmpty(posZ)){
                        Debug.LogError("can not found position in startPosition");
                    }else
                    {
                        var position = new Vector3(float.Parse(posX),float.Parse(posY),float.Parse(posZ));
                        //Debug.Log("====>position "+position);
                        targetMapLocationData.startPositionDatas.Add(position);
                        //Debug.Log("startposition count "+targetMapLocationData.startPositionDatas.Count);
                    }
                }
                if(!string.IsNullOrEmpty(objectName)){
                    if(string.IsNullOrEmpty(posX)||string.IsNullOrEmpty(posY)||string.IsNullOrEmpty(posZ)){
                        Debug.LogError("can not found position in startPosition");
                    }else
                    {
                        //Debug.Log(string.Format("x {0} y {1} z {0}",posX,posY,posZ));
                        var position = new Vector3(float.Parse(posX),float.Parse(posY),float.Parse(posZ));
                        var objectLocationData = new ObjectLocationData{prefabName = objectName,position = position , rotation = Quaternion.identity};
                        targetMapLocationData.objectLocationDatas.Add(objectLocationData);
                    }
                }
                if(!string.IsNullOrEmpty(objectTerrain)){
                    if(string.IsNullOrEmpty(posX)||string.IsNullOrEmpty(posY)||string.IsNullOrEmpty(posZ)){
                        Debug.LogError("can not found position in startPosition");
                    }else
                    {
                        // Debug.Log("Objectterrain ............");
                        // Debug.Log(string.Format("x {0} y {1} z {0}",posX,posY,posZ));
                        var position = new Vector3(float.Parse(posX),float.Parse(posY),float.Parse(posZ));
                        var objectLocationData = new ObjectLocationData{prefabName = objectTerrain,position = position , rotation = Quaternion.identity};
                        targetMapLocationData.objectTerrainDatas.Add(objectLocationData);
                    }
                }
            }
        downloadComplete(mapLocationDatas);
        Debug.Log("Total map "+mapLocationDatas.Count);
    }
    public void Dispose(){
       mapLocationDatas.Clear();
    }
}
