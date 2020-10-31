using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevAhead.Data;
using UniRx;
using System.Text.RegularExpressions;
using System;
using Newtonsoft.Json;

public class SpreadSheetGameConfig :SpreadSheetDataConverter
{
    public static Subject<GameConfigData> OnUpdateGameConfigData = new Subject<GameConfigData>();
    Dictionary<string,object> dictionaryRef;
    Dictionary<string,object> baseData = new Dictionary<string, object>();
    string stringKey = "";
    public void Start(){
        var gameConfigSheet = new SpreadSheet(SpreadSheetKeys.GAME_CONFIG);
        gameConfigSheet.Download();
        headerKeys = new string[]{SpreadSheetKeys.HEADER_NAME,SpreadSheetKeys.HEADER_DATA,SpreadSheetKeys.HEADER_VALE};
        gameConfigSheet.OnDownloadComplete.Subscribe(JsonConfigdata =>{ 
            PraseTranslation(JsonConfigdata);
        });
    }
    void PraseTranslation(string rawCsv){
        var lines = Regex.Split(rawCsv, LINE_SPLIT_REX);
        var header = Regex.Split(lines[0], SPLIT_REX);
        var nameIndex = System.Array.FindIndex(header,(item) => {return item == headerKeys[0];});
        var dataIndex = System.Array.FindIndex(header,(item)=>{return item == headerKeys[1]; });
        var valeIndex = System.Array.FindIndex(header,(item)=>{return item == headerKeys[2]; });
        for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_REX);
                var nameClassIndex = values[nameIndex];
                var data = values[dataIndex]; // level
                var value = values[valeIndex]; 
                Debug.Log("name *******************"+nameClassIndex);
                Debug.Log("data ****************"+data);
                Debug.Log("vaslue **************"+value);
                if(!string.IsNullOrEmpty(nameClassIndex)){
                    if(!baseData.ContainsKey(nameClassIndex)){
                        var classInstance = new Dictionary<string, object>();
                        dictionaryRef = classInstance;
                        baseData.Add(nameClassIndex,dictionaryRef);
                    }
                    continue;
                }
                if(!string.IsNullOrEmpty(data)){
                    if(!dictionaryRef.ContainsKey(data))
                        dictionaryRef.Add(data,value);
                }
            }
        // var jsonSerialize = JsonConvert.SerializeObject(baseData);
        // var gameConfigData = JsonConvert.DeserializeObject<GameConfigData>(jsonSerialize);

        
        var jsonSerialize = JsonConvert.SerializeObject(baseData);
        Debug.Log("json = "+jsonSerialize);
        var gameConfigData = JsonConvert.DeserializeObject<GameConfigData>(jsonSerialize);
        
        // var jsonSerialize = JsonUtility.ToJson(baseData);
        // var gameConfigData = JsonUtility.FromJson<GameConfigData>(jsonSerialize);
        //var gameConfigData = GetObject<GameConfigData>(baseData);
        Debug.Assert(gameConfigData != null, "game config data has be null ");
        OnUpdateGameConfigData.OnNext(gameConfigData);
        OnCompleted.OnNext(this.GetHashCode());
    }

    T GetObject<T>(Dictionary<string,object> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                Debug.Log("kv.key "+kv.Key+" kv. value "+kv.Value);
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }
}
