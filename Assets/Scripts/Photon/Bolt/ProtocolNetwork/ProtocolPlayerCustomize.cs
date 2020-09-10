using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UdpKit;
public class ProtocolPlayerCustomize : IProtocolToken
{
    // Start is called before the first frame update

    // Update is called once per frame
    public int bike_body_id = 0;
    public int bike_texture_id = 0;
    public void Read(UdpPacket packet)
    {
        bike_body_id = packet.ReadInt();
        bike_texture_id = packet.ReadInt();
    }

    public void Write(UdpPacket packet)
    {
        packet.WriteInt(bike_body_id);
        packet.WriteInt(bike_texture_id);
    }
}
