using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
namespace DevAhead.Data{
public class SpreadSheet
    {
        //public event System.Action<List<LevelData>> OnComplete;
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
        const string place = "place";
        const string word = "word";
        const string hint = "hint";
        const string level = "level";

        //List<LevelData> levels = new List<LevelData>();
        // List<StageData> places = new List<StageData>();

        // //Dictionary<int,LevelData> levels = new Dictionary<int, LevelData>();
        // List<LevelData> levels = new List<LevelData>();
        int currentLevelIndex;

        string[] headerKeys;
        int[] headerIndexs;
        //LevelData currentLevel;
        // StageData currentPlace;
        // LevelData currentLevel;
        void Start()
        {
           // Download();
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
        void PraseTranslation(string rawCsv)
        {
            // Debug.Log("CSV "+rawCsv);
            // Debug.Assert(!string.IsNullOrEmpty(rawCsv),"Map data not Found");
            // var lines = Regex.Split(rawCsv, LINE_SPLIT_REX);
            // var header = Regex.Split(lines[0], SPLIT_REX);
            // for (int i = 0; i < lines.Length; i++)
            //         {
            //             Debug.Log(lines[i]);
            //         }
            // foreach (var key in headerKeys)
            // {
            //     var keyIndex = System.Array.FindIndex(header,(item) =>{ return item == key;});
            // }
            //     if(!string.IsNullOrEmpty(rawCsv))
            //     {
            //         var lines = Regex.Split(rawCsv, LINE_SPLIT_REX);
            //         var header = Regex.Split(lines[0], SPLIT_REX);
            //         for (int i = 0; i < lines.Length; i++)
            //         {
            //             Debug.Log(lines[i]);
            //         }
            //         var levelIndex = System.Array.FindIndex(header,(item) => {return item == level;});
            //         var placeIndex = System.Array.FindIndex(header,(item)=>{return item == place; });
            //         var idIndex = System.Array.FindIndex(header, (item) => { return item == word; });
            //         var translationIndex = System.Array.FindIndex(header, (item) => { return item == hint; });
            //         if(translationIndex >= 0)
            //         {
            //             for (var i = 1; i < lines.Length; i++)
            //             {
            //                 var values = Regex.Split(lines[i], SPLIT_REX);//3 column
            //                 var id = values[idIndex]; //hint
            //                 var place = values[placeIndex]; // place
            //                 var level = values[levelIndex]; // level
            //                 if(!string.IsNullOrEmpty(level)){
            //                     LevelData levelData = new LevelData();
            //                     levelData.stages = new List<StageData>();
            //                     currentLevelIndex = Convert.ToInt32(level);
            //                     levels.Add(levelData);
            //                     currentLevel = levelData;
            //                     continue;
            //                 }
            //                 if(!string.IsNullOrEmpty(place)){
            //                     if(levels.Count <= 0)return;
            //                     StageData p = new StageData();
            //                     var placeHint = values[translationIndex];
            //                     GetHint(ref placeHint);
            //                     p.placeHint = placeHint;
            //                     Debug.Log("placehint "+p.placeHint);
            //                     p.placeName = place;
            //                     p.words = new Dictionary<string, string>();
            //                     currentLevel.stages.Add(p);
            //                     currentPlace = p;
            //                     continue;
            //                 }
            //                 if (string.IsNullOrEmpty(id))
            //                     continue;
            //                 if(currentPlace == null)continue;
            //                 var translation = values[translationIndex];
            //                 GetHint(ref translation);
            //                 if (currentPlace.words.ContainsKey(id))
            //                 {
            //                     Debug.LogError("redundant localization id found : " + id + " , please make sure their ids are unique");
            //                     continue;
            //                 }
            //                 currentPlace.words.Add(id.ToLower(),translation.ToLower());
            //             }
            //         }
            //     }
            // OnComplete(levels);
        }

        void GetHint(ref string translation){
            if (translation.StartsWith("\"") && translation.EndsWith("\"")){
                                translation = translation.Substring(1, translation.Length - 2);
                            }
                            translation = translation.Replace("\"\"", "\"");
        }
    }
}