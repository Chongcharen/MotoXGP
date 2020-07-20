using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
public class MapDataGenerator : MonoBehaviour
{
    public string documentKey = "1tfL2zeis-h9OAwL5TJzQQ-3DYa59a75spelWFzvdHoI";
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
        void Start(){
        string[] headers = new string[]{MapKeys.Map_Name,MapKeys.Map_Start,MapKeys.Object_Name,MapKeys.Position_X,MapKeys.Position_Y,MapKeys.Position_Z};
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
        Debug.Log("CSV "+rawCsv);
        Debug.Assert(!string.IsNullOrEmpty(rawCsv),"Map data not Found");
        var lines = Regex.Split(rawCsv, LINE_SPLIT_REX);
        var header = Regex.Split(lines[0], SPLIT_REX);
        for (int i = 0; i < lines.Length; i++)
            {
                        Debug.Log(lines[i]);
            }
        var levelIndex = System.Array.FindIndex(header,(item) => {return item == headerKeys[0];});
        var mapPositionIndex = System.Array.FindIndex(header,(item)=>{return item == headerKeys[1]; });
        var objectIndex = System.Array.FindIndex(header,(item)=>{return item == headerKeys[2]; });
        var posXindex = System.Array.FindIndex(header, (item) => { return item == headerKeys[3]; });
        var posYindex = System.Array.FindIndex(header, (item) => { return item == headerKeys[4]; });
        var posZindex = System.Array.FindIndex(header, (item) => { return item == headerKeys[5]; });
        Debug.Log("levelIndex "+levelIndex);
        Debug.Log("objectIndex "+objectIndex);
        Debug.Log("posXindex "+posXindex);
        Debug.Log("posYindex "+posYindex);
        Debug.Log("posZindex "+posZindex);
        for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_REX);//3 column
                var level = values[levelIndex]; // level
                var mapStartPositon = values[objectIndex]; //hint
                var objectName = values[objectIndex]; //hint
                var posX = values[posXindex]; // place
                var posY = values[posYindex]; // place
                var posZ = values[posZindex]; // place
                Debug.Log("Level "+level);
                Debug.Log("objectName "+objectName);
                Debug.Log("posX "+posX);
                Debug.Log("posY "+posY);
                Debug.Log("posZ "+posZ);
                if(!string.IsNullOrEmpty(level)){
                    targetMapLocationData = new MapLocationData();
                    targetMapLocationData.mapName = level;
                    Debug.Log("Mapname "+targetMapLocationData.mapName);
                    targetMapLocationData.startPositionDatas = new List<Vector3>();
                    targetMapLocationData.objectLocationDatas = new List<ObjectLocationData>();
                    mapLocationDatas.Add(targetMapLocationData);
                    continue;
                }
                if(!string.IsNullOrEmpty(mapStartPositon)){
                    if(string.IsNullOrEmpty(posX)||string.IsNullOrEmpty(posY)||string.IsNullOrEmpty(posZ)){
                        Debug.LogError("can not found position in startPosition");
                    }
                    var position = new Vector3(float.Parse(posX),float.Parse(posY),float.Parse(posZ));
                    Debug.Log("====>position "+position);
                    targetMapLocationData.startPositionDatas.Add(position);
                    Debug.Log("startposition count "+targetMapLocationData.startPositionDatas.Count);
                }
                if(!string.IsNullOrEmpty(objectName)){
                    if(string.IsNullOrEmpty(posX)||string.IsNullOrEmpty(posY)||string.IsNullOrEmpty(posZ)){
                        Debug.LogError("can not found position in startPosition");
                    }
                    var position = new Vector3(float.Parse(posX),float.Parse(posY),float.Parse(posZ));
                    var objectLocationData = new ObjectLocationData{prefabName = objectName,position = position , rotation = Quaternion.identity};
                    targetMapLocationData.objectLocationDatas.Add(objectLocationData);
                }
            }

        Debug.Log("Total map "+mapLocationDatas.Count);
    }
}
