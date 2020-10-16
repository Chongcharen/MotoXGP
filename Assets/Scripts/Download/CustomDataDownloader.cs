using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
public class CustomDataDownloader : IDisposable
{
    public delegate void OnDownloadComplete (string json);
    public event OnDownloadComplete downloadComplete;
    public string documentKey = "1sxH20vXha8_DsUIoTR9m4-NchUrVatE_92oyVIzotAA";
    public string addressView = "https://docs.google.com/spreadsheets/d/";
    public string viewFormat = "/edit#gid=0";
    public string addressExport = "https://docs.google.com/feeds/download/spreadsheets/Export?key=";  
    public string exportFormat = "&exportFormat=csv&gid=0";

    //https://docs.google.com/spreadsheets/d/1sxH20vXha8_DsUIoTR9m4-NchUrVatE_92oyVIzotAA/edit?usp=sharing

    string spreadsheetDownloadUrl = "";
    string spreadsheetViewUrl = "";
     const string SPLIT_REX = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    const string LINE_SPLIT_REX = @"\r\n|\n\r|\n|\r";
    const string LANGUAGE_HEADER_PREFIX = "lang-";
    string[] headerKeys;
    int[] headerIndexs;

    public void Dispose()
    {
        throw new NotImplementedException();
    }
    public void Start(){
        Download(null);
    }
    public void Download(string[] _headerKeys){

           // headerKeys = _headerKeys.ToArray();
            //headerIndexs = new int[headerKeys.Length];
            spreadsheetDownloadUrl = addressExport+documentKey+exportFormat;
            spreadsheetViewUrl = addressView+documentKey+viewFormat;
            Debug.Log("spreadsheetDownloadUrl "+spreadsheetDownloadUrl);
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
        Debug.Log("Load customdata "+rawCsv);
        Debug.Assert(!string.IsNullOrEmpty(rawCsv),"Map data not Found");
        var lines = Regex.Split(rawCsv, LINE_SPLIT_REX);
        var header = Regex.Split(lines[0], SPLIT_REX);
        for (int i = 0; i < header.Length; i++)
            {
                        Debug.Log(header[i]);
            }
        for (int i = 0; i < lines.Length; i++)
        {
             Debug.Log("line _ "+lines[i]);
        }

        List<Dictionary<string,object>> categoryList = new List<Dictionary<string, object>>();
        
        for (int j = 1; j < lines.Length; j++)
            {
                Dictionary<string,object> customObject = new Dictionary<string, object>();
                var property = Regex.Split(lines[j], SPLIT_REX);
                for (int i = 0; i < header.Length; i++)
                {
                   customObject.Add(header[i],property[i]);
                }
                categoryList.Add(customObject);
            }

        var json = JsonConvert.SerializeObject(categoryList);
        downloadComplete(json);
    }
}
