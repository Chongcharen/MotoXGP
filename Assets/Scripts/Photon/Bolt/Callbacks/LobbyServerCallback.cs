using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using System.Linq;
using UdpKit;

[BoltGlobalBehaviour(SceneName.LOBBY)]
public class LobbyServerCallback : GlobalEventListener{
    void Awake(){
        print(Depug.Log("CreateServerPlayer ",Color.white));
        //BikePlayerRegistry.CreateServerPlayer();
    }
    public override void Connected(BoltConnection connection){
        if(!BoltNetwork.IsServer)return;
        print(Depug.Log("LobbyServerCallback connection ",Color.white));
        print(Depug.Log("LobbyServerCallback AcceptToken "+connection.AcceptToken,Color.white));
        print(Depug.Log("LobbyServerCallback ConnectToken "+connection.ConnectToken,Color.white));
        var bikePlayerObject = BikePlayerRegistry.CreateClientPlayer(connection);
        print(Depug.Log("LobbyServerCallback connection "+bikePlayerObject,Color.white));
        if(bikePlayerObject.IsClient){
            var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);
            entity.AssignControl(bikePlayerObject.connection);
        }
    }
    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
        {
           Debug.Log("Connectionrequest "+endpoint+ " token "+token);
        }
    public override void SceneLoadLocalDone(string scene, IProtocolToken token){
        print(Depug.Log("SceneLoadLocalDone ",Color.white));
    }
    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token){
        print(Depug.Log("SceneLoadRemoteDone "+connection.ConnectionId,Color.white));
    }
    public override void SessionCreatedOrUpdated(UdpKit.UdpSession session){
        print(Depug.Log("----------SessionCreatedOrUpdated--------"+session,Color.blue));

        /// test protocol token only
        /// 
        if(!BoltNetwork.IsServer)return;
        BikePlayerRegistry.CreateClientPlayer(BoltNetwork.Server);
        Debug.Log("Server UdpSocket "+BoltNetwork.UdpSocket);
        Debug.Log("Server UserToken "+BoltNetwork.UdpSocket.UserToken);
        Debug.Log("AllPlayers ----> "+BikePlayerRegistry.AllPlayers.Count());
        Debug.Log("AllPlayerReadys ----> "+BikePlayerRegistry.playersReady.Count);
        var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);//1
        entity.TakeControl();
        
        print(Depug.Log("All players "+BikePlayerRegistry.AllPlayers.Count(),Color.blue));
        if(BoltNetwork.IsServer){
            var logEvent = LogEvent.Create(GlobalTargets.Everyone);
            logEvent.Message = "Server Take control!!!!!!";
            logEvent.Send();
            //BoltNetwork.CreateStreamChannel()
        }
    }
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpKit.UdpConnectionDisconnectReason disconnectReason){
        print(Depug.Log("----------BoltShutdownBegin--------",Color.red));
        if(BoltNetwork.IsServer){
            BikePlayerRegistry.Dispose();
        }
        print(Depug.Log("-BikePlayerRegistry count- "+BikePlayerRegistry.AllPlayers.Count(),Color.red));
    }

    public override void Disconnected(BoltConnection connection){
        print(Depug.Log("---Someone Disconnected----"+connection.ConnectionId,Color.red));
        BikePlayerRegistry.RemovePlayer(connection);
    }
}
