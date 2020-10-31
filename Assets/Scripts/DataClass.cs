using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClass 
{
}
[System.Serializable]
public class EndPointData{
    public int instanceId;
    public bool isPass = false;
    public Collider collider;
    public float position;

}
public class CameraPointData{
    public int instanceId;
    public bool isPass = false;
    public Collider collider;
    public float position;
    public Quaternion rotation;

}
[System.Serializable]
public struct ChatMessageData{
    
    public string channelName;
    public ChannelType channelType;
    public string[] senders;
    public object[] messages;
}
//PlayerData in playerResult when play end on finishLine
[System.Serializable]
public struct PlayerGameResultData{
    public string nation;
    public string playerName;
    public string playerAvatar;
    public long playerFinishTime;
   // public 
}
//playerData in GamePlay
[System.Serializable]
public class PlayerIndexProfileData{
    public int index;
    public string userId;
    public string nickName;
    public string profileModel;
    public string colorCode;
    
    public string nation;
    public string playerFinishTime;
    public bool ready = false;
}
public class PlayerDistanceData{
    public int playerIndex;
    public string userID;
    public string playerName;
    public float distance;
}

//Mockup Map Data
[System.Serializable]
public class MapChoiceData{
    public string mapType;
    public int mapLevel;
    public string mapName;
    public string mapDetail;
}



[System.Serializable]
public class GameLevel{
    public int theme;
    public int stage;
    public int level;
    public int nosCount;
    public GameStageData gameStageData;
}
//GameLeveldata for choose map
[System.Serializable]
public class GameLevelData{
    public string version;
    public List<GameThemeData> gameThemesData;
}
[System.Serializable]
public class GameThemeData{
    public string themeName;
    public List<GameStageData> gameStages;
}
[System.Serializable]
public class GameStageData{
    public string themeName;
    public string stageName;
    public string detail;
    public int stage;
}
[System.Serializable]
public class MapLocationData{
    public string mapName;
    public List<Vector3> startPositionDatas;
    public List<ObjectLocationData> objectTerrainDatas;
    public List<ObjectLocationData> objectLocationDatas;
    
}
[System.Serializable]
public struct ObjectLocationData{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
}

// Data form sheet
[System.Serializable]
public class GameConfigData{
    public PhotonNetworkConfigData photonNetworkConfig;
    public DataPath dataPath;
}
[System.Serializable]
public class PhotonNetworkConfigData{
    public string gameVersion;
    public int sendRate;
    public int serializationRate;
    public float keepAliveInBackground;
}
[System.Serializable]
public class DataPath{
    public string equipment;
}

//PlayerData
[System.Serializable]
public class PlayerBikeData{
    public int bikeId;
    public int bikeTextureId;
    public int runningTrack;
    public long playerFinishTime;
}
[System.Serializable]
public class PartEquipmentData{
    public int id;
    public string icon_name;
    public string model_name;
    public string texture_name;
    public int price;
    public bool locked;
}
[System.Serializable]
public class EquipmentData{
    public Dictionary<string,List<PartEquipmentData>> data;
}
[System.Serializable]
public struct EquipmentTrack{
    public int id;
    public string model_name;
    public string texture_name;
}