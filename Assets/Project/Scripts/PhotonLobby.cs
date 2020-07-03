using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonLobby : LoadBalancingClient
{
    public static PhotonLobby photonLobby;
    public void CallConnect()
    {
        //this.AppId = "<your appid>";  // set your app id here
        //this.AppVersion = "1.0";  // set your app version here

        // "eu" is the European region's token
        if (!this.ConnectToRegionMaster("eu")) // can return false for errors
        {
            this.DebugReturn(DebugLevel.ERROR, "Can't connect to: " + this.CurrentServerAddress);
        }
    }
}
