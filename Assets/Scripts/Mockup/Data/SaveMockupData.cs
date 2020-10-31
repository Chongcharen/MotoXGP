using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
public static class SaveMockupData
{
    static PlayerCustomizeData playerCustomizeData;
    public static PlayerCustomizeData GetEquipment{
        get{
            Load();
            return playerCustomizeData;
        }
    }
    public static void SaveEquipment(int equipmentID , string model_name,string texture_name){
        Load();
        var key = playerCustomizeData.playerEquipmentMapper.ElementAt(equipmentID).Key;
        PlayerEquipedData equipedData = playerCustomizeData.playerEquipmentMapper.ElementAt(equipmentID).Value;
        equipedData.model_name = model_name;
        equipedData.texture_name = texture_name;
        playerCustomizeData.playerEquipmentMapper[key] = equipedData;
        UnityEngine.Debug.Log(Depug.Log("equipment saveed! id"+equipmentID+"model "+equipedData.model_name + "texture = "+equipedData.texture_name,Color.green));
        Save();
    }
    static void Save(){
        if(playerCustomizeData.playerEquipmentMapper == null){
            NewPlayerCustomData();
        }
        var json = JsonConvert.SerializeObject(playerCustomizeData);
        Debug.Log("json save = "+json);
        PlayerPrefs.SetString("playercustom",json);
    }
    static void Load(){
        var jsonLoad = PlayerPrefs.GetString("playercustom");
        Debug.Log("json load = "+jsonLoad);
        if(string.IsNullOrEmpty(jsonLoad))
            NewPlayerCustomData();
        else
            playerCustomizeData = JsonConvert.DeserializeObject<PlayerCustomizeData>(jsonLoad);
    }
    static void NewPlayerCustomData(){
        playerCustomizeData = new PlayerCustomizeData();
        playerCustomizeData.playerEquipmentMapper = new Dictionary<string, PlayerEquipedData>();
        var helmetDefault = GameDataManager.Instance.equipmentData.data[EquipmentKeys.HELMET][0];
        var suitDefault = GameDataManager.Instance.equipmentData.data[EquipmentKeys.SUIT][0];
        var gloveDefault = GameDataManager.Instance.equipmentData.data[EquipmentKeys.GLOVE][0];
        var bootDefault = GameDataManager.Instance.equipmentData.data[EquipmentKeys.BOOT][0];
        playerCustomizeData.playerEquipmentMapper.Add(EquipmentKeys.HELMET,new PlayerEquipedData{model_name = helmetDefault.model_name,texture_name = helmetDefault.texture_name});
        playerCustomizeData.playerEquipmentMapper.Add(EquipmentKeys.SUIT,new PlayerEquipedData{model_name = suitDefault.model_name,texture_name = suitDefault.texture_name});
        playerCustomizeData.playerEquipmentMapper.Add(EquipmentKeys.GLOVE,new PlayerEquipedData{model_name = gloveDefault.model_name,texture_name = gloveDefault.texture_name});
        playerCustomizeData.playerEquipmentMapper.Add(EquipmentKeys.BOOT,new PlayerEquipedData{model_name = bootDefault.model_name,texture_name = bootDefault.texture_name});
    }
    
}
[System.Serializable]
public struct PlayerCustomizeData{
    public Dictionary<string,PlayerEquipedData> playerEquipmentMapper;
}
[System.Serializable]
public struct PlayerEquipedData{
    public string model_name;
    public string texture_name;
}
