using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UniRx;

namespace DevAhead.Data{
public class SpreadSheet
    {
        public Subject<string> OnDownloadComplete = new Subject<string>();
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
        int currentLevelIndex;

        string[] headerKeys;
        int[] headerIndexs;
        public SpreadSheet(string _documentKey){
            documentKey = _documentKey;
        }
        public void Download(){
            //headerKeys = _headerKeys.ToArray();
            //headerIndexs = new int[headerKeys.Length];
            spreadsheetDownloadUrl = addressExport+documentKey+exportFormat;
            spreadsheetViewUrl = addressView+documentKey+viewFormat;

            Debug.Log("spreadsheetDownloadUrl "+spreadsheetDownloadUrl);
            //1
            var downloadHandler = new DownloadHandlerBuffer();
            var webRequest = new UnityWebRequest(spreadsheetDownloadUrl, "GET", downloadHandler, null);
            var ops = webRequest.SendWebRequest();
            ops.completed += (obj) =>
            {
                if(downloadHandler.isDone)
                    OnDownloadComplete.OnNext(downloadHandler.text);
            };
        }
    }
}