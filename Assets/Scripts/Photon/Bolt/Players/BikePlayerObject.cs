using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikePlayerObject
{
    public BoltEntity character;
    public BoltConnection connection;
    public PlayerProfileToken profileToken;
    
    public int index;
    public bool IsServer
    {
        get { return connection == null; }
    }

    public bool IsClient
    {
        get { return connection != null; }
    }
}
