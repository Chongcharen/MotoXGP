using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UdpKit;

public class ProtocolRoomProperty : IProtocolToken
{
    public int theme;
    public int stage;
    public int level;
    public void Read(UdpPacket packet)
    {
        packet.WriteInt(theme);
        packet.WriteInt(stage);
        packet.WriteInt(level);
    }

    public void Write(UdpPacket packet)
    {
       theme = packet.ReadInt();
       stage = packet.ReadInt();
       level = packet.ReadInt();
    }
}
