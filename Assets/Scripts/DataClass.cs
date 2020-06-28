using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClass 
{
}
public class EndPointData{
    public int instanceId;
    public bool isPass = false;
    public Collider collider;
    public float position;

}
public struct ChatMessageData{
    
    public string channelName;
    public ChannelType channelType;
    public string[] senders;
    public object[] messages;
}
//PlayerData in playerResult when play end on finishLine
public struct PlayerGameResultData{
    public string nation;
    public string playerName;
    public string playerAvatar;
    public long playerFinishTime;
   // public 
}
//playerData in GamePlay
public class PlayerIndexProfileData{
    public int index;
    public string userId;
    public string nickName;
    public string profileModel;
    public string colorCode;
    
    public string nation;
    public string playerFinishTime;
}
public class PlayerDistanceData{
    public int playerIndex;
    public string userID;
    public string playerName;
    public float distance;
}
