using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UdpKit;
using PlayFab.ClientModels;

public class PlayerProfileToken : IProtocolToken
{
    public PlayerProfileModel playerProfileModel;
    public PlayerBikeData playerBikeData;
    public void Read(UdpPacket packet)
    {
        playerProfileModel = JsonUtility.FromJson<PlayerProfileModel>(packet.ReadString());
        playerBikeData = JsonUtility.FromJson<PlayerBikeData>(packet.ReadString());
    }

    public void Write(UdpPacket packet)
    {
         packet.WriteString(playerProfileModel.ToJson());
         packet.WriteString(JsonUtility.ToJson(playerBikeData));
    }

    public void RandomBikeData(){
        playerBikeData = new PlayerBikeData();
        playerBikeData.bikeId = Random.Range(1,5);
        playerBikeData.bikeTextureId = Random.Range(1,9);
    }
}
