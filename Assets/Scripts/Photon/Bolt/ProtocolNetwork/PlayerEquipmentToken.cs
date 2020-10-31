using System.Collections;
using System.Collections.Generic;
using Bolt;
using UdpKit;
using UnityEngine;
using Newtonsoft.Json;
public class PlayerEquipmentToken : IProtocolToken
{
    public Dictionary<string,PlayerEquipedData> playerEquipmentMapper;
    //public PlayerCustomizeData player
    public void Read(UdpPacket packet)
    {
        var json = packet.ReadString();
        playerEquipmentMapper = JsonConvert.DeserializeObject<Dictionary<string,PlayerEquipedData>>(json);
    }

    public void Write(UdpPacket packet)
    {
        var json = JsonConvert.SerializeObject(playerEquipmentMapper);
        packet.WriteString(json);
    }
}
