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
    static BikeCustomizeData bikeCustomizeData;
    public static PlayerCustomizeData GetEquipment{
        get{
            Load();
            return playerCustomizeData;
        }
    }
    public static BikeCustomizeData GetBikeEquipment{
        get{
            Load();
            return bikeCustomizeData;
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
    public static void SaveBikeEquipment(int equipmentID,string model_name,string texture_name){
        Debug.Log("SAveBike !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Load();
        Debug.Log("bike data "+bikeCustomizeData.bikeEquipmentMapper.Count);
        var key = bikeCustomizeData.bikeEquipmentMapper.ElementAt(0).Key;

        BikeEquipedData bikeEquipedData = bikeCustomizeData.bikeEquipmentMapper.ElementAt(0).Value;
        bikeEquipedData.model_name = model_name;
        bikeEquipedData.texture_name = texture_name;
        bikeCustomizeData.bikeEquipmentMapper[key] = bikeEquipedData;
        UnityEngine.Debug.Log(Depug.Log("equipment saveed! id"+0+"model "+bikeEquipedData.model_name + "texture = "+bikeEquipedData.texture_name,Color.green));
        Save();
    }
    static void Save(){
        if(playerCustomizeData.playerEquipmentMapper == null){
            NewPlayerCustomData();
            NewBikeCustomData();
        }
        var json = JsonConvert.SerializeObject(playerCustomizeData);
        var bikeJson = JsonConvert.SerializeObject(bikeCustomizeData);
        Debug.Log("json save = "+json);
        PlayerPrefs.SetString("playercustom",json);
        PlayerPrefs.SetString("bikecustom",bikeJson);

    }
    static void Load(){
        var jsonLoad = PlayerPrefs.GetString("playercustom");
        var bikeJsonLoad = PlayerPrefs.GetString("bikecustom");
        Debug.Log("json load = "+jsonLoad);
        if(string.IsNullOrEmpty(jsonLoad)){
            NewPlayerCustomData();
        }
        else{
            playerCustomizeData = JsonConvert.DeserializeObject<PlayerCustomizeData>(jsonLoad);
            
        }
        if(string.IsNullOrEmpty(bikeJsonLoad))
            NewBikeCustomData();
        else
            bikeCustomizeData = JsonConvert.DeserializeObject<BikeCustomizeData>(bikeJsonLoad);

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
    static void NewBikeCustomData(){
        bikeCustomizeData = new BikeCustomizeData();
        bikeCustomizeData.bikeEquipmentMapper = new Dictionary<string, BikeEquipedData>();
        var bikeDefault = GameDataManager.Instance.equipmentData.data[EquipmentKeys.BIKE][0];
        bikeCustomizeData.bikeEquipmentMapper.Add(EquipmentKeys.BIKE,new BikeEquipedData{model_name = bikeDefault.model_name,texture_name = bikeDefault.texture_name});
    }
    
}
[System.Serializable]
public struct PlayerCustomizeData{
    public Dictionary<string,PlayerEquipedData> playerEquipmentMapper;
}
public struct BikeCustomizeData{
    public Dictionary<string,BikeEquipedData> bikeEquipmentMapper;
}
[System.Serializable]
public struct PlayerEquipedData{
    public string model_name;
    public string texture_name;
}
[System.Serializable]
public struct BikeEquipedData{
    public string model_name;
    public string texture_name;
}
