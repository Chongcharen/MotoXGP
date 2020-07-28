using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;
using UniRx;

namespace DevAhead.Data{
    public class SpreadSheetDataConverter : IDisposable
    {
        public static Subject<int> OnCompleted = new Subject<int>();
        public const string SPLIT_REX = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        public const string LINE_SPLIT_REX = @"\r\n|\n\r|\n|\r";
        public string[] headerKeys;
        public int[] headerIndexs;

        public void Dispose()
        {
            
        }


        // int[] headerIndexs;
        // int[] headerData;
        // Dictionary<string,int> headerIndexData = new Dictionary<string, int>();
        // Dictionary<string,object> baseData = new Dictionary<string, object>();
        // Dictionary<string,object> targetDictionary = new Dictionary<string, object>();
        // public T ConvertData<T>(string jsonCsv,string[] _headerKeys){
        //     headerKeys = _headerKeys;
        //     headerIndexs = new int[headerKeys.Length];
        //     headerData = new int[headerKeys.Length];
        //     var lines = Regex.Split(jsonCsv, LINE_SPLIT_REX);
        //     var header = Regex.Split(lines[0], SPLIT_REX);
        //     for (int i = 0; i < header.Length; i++)
        //     {
        //         Debug.Log("header = "+header[i]);
        //     }
        //     for (int i = 0; i < headerIndexs.Length; i++)
        //     {
        //         headerData[i] = System.Array.FindIndex(header,(item) => {return item == headerKeys[i];});
        //         Debug.Log("header key "+headerKeys[i]+ " data "+headerData[i]);
        //         if(!headerIndexData.ContainsKey(headerKeys[i]))
        //             headerIndexData.Add(headerKeys[i],headerData[i]);
        //     }
        //     for (var i = 1; i < lines.Length; i++){
        //         var values = Regex.Split(lines[i], SPLIT_REX);//3 column
        //         foreach (var item in headerIndexData)
        //         {
        //             Debug.Log("item.key "+item.Key +"vale "+item.Value);
        //             if(string.IsNullOrEmpty(item.Key)){
        //                 if(item.Value == 0){
        //                     targetDictionary = new Dictionary<string, object>(); 
        //                     if(!baseData.ContainsKey(item.Key))
        //                         baseData.Add(item.Key,targetDictionary);
        //                     continue;
        //                 }
        //                 targetDictionary.Add(item.Key,item.Value);
        //             }
        //         }
        //     }
        //    var classInstance = GetObject<T>(baseData);
        //    return classInstance;
        // }
        // T GetObject<T>(Dictionary<string,object> dict)
        // {
        //     Type type = typeof(T);
        //     var obj = Activator.CreateInstance(type);

        //     foreach (var kv in dict)
        //     {
        //         type.GetProperty(kv.Key).SetValue(obj, kv.Value);
        //     }
        //     return (T)obj;
        // }
    }
}
