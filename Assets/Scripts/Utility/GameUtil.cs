using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using PlayFab.ClientModels;
using Newtonsoft.Json;
public static class GameUtil
{
    public static void SetHashTableProperty(ExitGames.Client.Photon.Hashtable hashtable,string key,object vale){
        if(hashtable.ContainsKey(key)){
            hashtable[key] = vale;
        }else{
            hashtable.Add(key,vale);
        }
    }
    public static PlayerProfileModel ConvertToPlayFabPlayerProfilemodel(string json){
        return JsonConvert.DeserializeObject<PlayerProfileModel>(json);
    }
}
