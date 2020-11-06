using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UdpKit;
using Newtonsoft.Json;
public class BikeEquipmentToken : IProtocolToken{
    public Dictionary<string,BikeEquipedData> bikeEquipmentMapper;
    //public PlayerCustomizeData player
    public void Read(UdpPacket packet)
    {
        var json = packet.ReadString();
        bikeEquipmentMapper = JsonConvert.DeserializeObject<Dictionary<string,BikeEquipedData>>(json);
    }

    public void Write(UdpPacket packet)
    {
        var json = JsonConvert.SerializeObject(bikeEquipmentMapper);
        packet.WriteString(json);
    }
}