using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using System.Linq;
using UdpKit;
using UniRx;

[BoltGlobalBehaviour(BoltNetworkModes.Server, @SceneName.LOBBY,@SceneName.PLAYER_CUSTOM)]
public class LobbyServerCallback : GlobalEventListener{

    public static Subject<Unit> OnBoltStart = new Subject<Unit>();
    public static Subject<BoltEntity> OnJoinSession = new Subject<BoltEntity>();
    void Awake(){
        print(Depug.Log("CreateServerPlayer ",Color.white));
        //BikePlayerRegistry.CreateServerPlayer();
    }
     public override void BoltStartBegin(){
        //register protocolToken 
        print(Depug.Log("BoltStartBegin RegisterTokenClass",Color.blue));
        BoltNetwork.RegisterTokenClass<ProtocolRoomProperty>();
        BoltNetwork.RegisterTokenClass<Bolt.Photon.PhotonRoomProperties>();
        BoltNetwork.RegisterTokenClass<ProtocolPlayerCustomize>();
        BoltNetwork.RegisterTokenClass<PlayerProfileToken>();
        BoltNetwork.RegisterTokenClass<PlayerEquipmentToken>();
        BoltNetwork.RegisterTokenClass<BikeEquipmentToken>();
        OnBoltStart.OnNext(default);
    }
     public override void SessionCreatedOrUpdated(UdpKit.UdpSession session){
        print(Depug.Log("----------SessionCreatedOrUpdated--------"+session,Color.blue));
        print(Depug.Log("----------session.Source--------"+session.Source,Color.blue));
        print(Depug.Log("----------session.id--------"+session.Id,Color.blue));
        print(Depug.Log("----------session.HostName--------"+session.HostName,Color.blue));
        print(Depug.Log("----------session.HostData--------"+session.HostData,Color.blue));
        print(Depug.Log("----------session.IsDedicatedServer--------"+session.IsDedicatedServer,Color.blue));

        print(Depug.Log("BoltNetwork.Server "+BoltNetwork.Server,Color.white));
        print(Depug.Log("BoltNetwork.ScopeMode "+BoltNetwork.ScopeMode,Color.white));
        print(Depug.Log("BoltNetwork.UdpSocket "+BoltNetwork.UdpSocket,Color.white));
        print(Depug.Log("BoltNetwork.Server "+BoltNetwork.Server,Color.white));

        /// test protocol token only
        /// 
        // if(!BoltNetwork.IsServer)return;
        // var bikePlayerObject = BikePlayerRegistry.CreateClientPlayer(BoltNetwork.Server);
        // Debug.Log("Server UdpSocket "+BoltNetwork.UdpSocket);
        // Debug.Log("Server UserToken "+BoltNetwork.UdpSocket.UserToken);
        // Debug.Log("AllPlayers ----> "+BikePlayerRegistry.AllPlayers.Count());
        // Debug.Log("AllPlayerReadys ----> "+BikePlayerRegistry.playersReady.Count);
        // Debug.Log("AcceptToken "+bikePlayerObject.connection);
        // Debug.Log("ConnectToken "+bikePlayerObject.connection);
        // Debug.Log("BoltNetwork.Server "+BoltNetwork.Server);
        // Debug.Log("BoltNetwork.Server.connectionID "+BoltNetwork.Server);
        // //BoltLobbyNetwork.Instance.JoinSession(session);
        // var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);//1
        // entity.TakeControl();
        // var playerInroom = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
        //     playerInroom.SetupProfileModel(PlayFabController.Instance.playerProfileModel.Value);
        //     playerInroom.SetupPlayer(!BoltNetwork.IsServer);
        //     playerInroom.SetupBikePlayerObject(bikePlayerObject);
        //     //playerInroom.SetupPlayer(entity,bikePlayerObject.IsClient);

        // print(Depug.Log("All players "+BikePlayerRegistry.AllPlayers.Count(),Color.blue));
        // if(BoltNetwork.IsServer){
        //     var logEvent = LogEvent.Create(GlobalTargets.Everyone);
        //     logEvent.Message = "Server Take control!!!!!!";
        //     logEvent.Send();
        //     //BoltNetwork.CreateStreamChannel()
        // }
    }
    public override void Connected(BoltConnection connection){
        print(Depug.Log("LSC Connected "+connection,Color.blue));
        
        if(!BoltNetwork.IsServer)return;
        // var bikePlayerObject = BikePlayerRegistry.CreateClientPlayer(connection);
        // print(Depug.Log("LSC bikePlayerObject is Cleint "+bikePlayerObject.IsClient,Color.blue));
        // print(Depug.Log("AcceptToken "+connection.AcceptToken,Color.blue));
        // print(Depug.Log("ConnectToken "+connection.ConnectToken,Color.blue));
        // print(Depug.Log("bikePlayerObject AcceptToken"+bikePlayerObject.connection.AcceptToken,Color.blue));
        // print(Depug.Log("bikePlayerObject "+bikePlayerObject.connection.ConnectToken,Color.blue));
        // if(bikePlayerObject.IsClient){
        //     // var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);
        //     // entity.AssignControl(bikePlayerObject.connection);
        //     // var playerInroom = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
        //     // var token = bikePlayerObject.connection.ConnectToken as PlayerProfileToken;
        //     // playerInroom.SetupProfileModel(token.playerProfileModel);
        //     // playerInroom.SetupPlayer(bikePlayerObject.IsClient);
        // }
    }
    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
        {
           Debug.Log("Connectionrequest "+endpoint+ " token "+token);
        }
    public override void SceneLoadLocalDone(string scene, IProtocolToken token){
        print(Depug.Log("SceneLoadLocalDone ",Color.white));
        print(Depug.Log("token "+token,Color.white));
        print(Depug.Log("BoltNetwork.Server "+BoltNetwork.Server,Color.white));
        print(Depug.Log("BoltNetwork.ScopeMode "+BoltNetwork.ScopeMode,Color.white));
        print(Depug.Log("BoltNetwork.UdpSocket "+BoltNetwork.UdpSocket,Color.white));
        // print(Depug.Log("BoltNetwork.UserData "+BoltNetwork.Server.UserData,Color.white));
        // print(Depug.Log("BoltNetwork.ConnectToken "+BoltNetwork.Server.ConnectToken,Color.white));
        // print(Depug.Log("BoltNetwork.AcceptToken "+BoltNetwork.Server.AcceptToken,Color.white));
        // print(Depug.Log("BoltNetwork.DisconnectToken "+BoltNetwork.Server.DisconnectToken,Color.white));
        var profileToken = token as PlayerProfileToken;
        Debug.Log("profileToken "+profileToken);
        
        var playerData = new PlayerProfileToken();
        //playerData.playerBikeData.playerFinishTime = 99999999;
        playerData.playerProfileModel = PlayFabController.Instance.playerProfileModel.Value;
        playerData.RandomBikeData();
        //playerData.playerBikeData.bikeCustomize = SaveMockupData.GetBikeEquipment;


        if(!BoltNetwork.IsServer)return;
        var bikePlayerObject = BikePlayerRegistry.CreateClientPlayer(BoltNetwork.Server,playerData);
        var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);//1

        bikePlayerObject = BikePlayerRegistry.GetBikePlayer(BoltNetwork.Server);
        Debug.Log("bikeplayerobject "+bikePlayerObject);
       // Debug.Log("bikeCustomize "+bikePlayerObject.profileToken.playerBikeData.bikeCustomize);
        //bikePlayerObject.profileToken.playerBikeData.bikeCustomize.bikeEquipmentMapper
        entity.TakeControl();
        var playerInroom = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
            playerInroom.SetupProfileModel(PlayFabController.Instance.playerProfileModel.Value);
            playerInroom.SetupPlayer(!BoltNetwork.IsServer);
            playerInroom.SetupBikePlayerObject(bikePlayerObject);

        print(Depug.Log("All players "+BikePlayerRegistry.AllPlayers.Count(),Color.blue));
        if(BoltNetwork.IsServer){
            var logEvent = LogEvent.Create(GlobalTargets.Everyone);
            logEvent.Message = "Server Take control!!!!!!";
            logEvent.Send();
        }
    }
    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token){
        print(Depug.Log("SceneLoadRemoteDone connection ID : "+connection.ConnectionId,Color.white));
        print(Depug.Log("PlyerProfile Token "+token,Color.blue));
        var profileToken = token as PlayerProfileToken;
        Debug.Log("profileToken "+profileToken);
        var bikePlayerObject = BikePlayerRegistry.CreateClientPlayer(connection,profileToken);
        print(Depug.Log("LSC bikePlayerObject is Cleint "+bikePlayerObject.IsClient,Color.blue));
        print(Depug.Log("AcceptToken "+connection.AcceptToken,Color.blue));
        print(Depug.Log("ConnectToken "+connection.ConnectToken,Color.blue));
        print(Depug.Log("bikePlayerObject AcceptToken"+bikePlayerObject.connection.AcceptToken,Color.blue));
        print(Depug.Log("bikePlayerObject "+bikePlayerObject.connection.ConnectToken,Color.blue));
        if(bikePlayerObject.IsClient){
            var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);
            entity.AssignControl(bikePlayerObject.connection);
            var playerInroom = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
            var playerProfileToken = token as PlayerProfileToken;
            playerInroom.SetupProfileModel(playerProfileToken.playerProfileModel);
            playerInroom.SetupPlayer(bikePlayerObject.IsClient);
            playerInroom.SetupBikePlayerObject(bikePlayerObject);
            
        }
    }
   
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpKit.UdpConnectionDisconnectReason disconnectReason){
        print(Depug.Log("----------BoltShutdownBegin--------",Color.red));
        if(BoltNetwork.IsServer){
            BikePlayerRegistry.Dispose();
        }
        BoltLauncher.StartClient();
        print(Depug.Log("-BikePlayerRegistry count- "+BikePlayerRegistry.AllPlayers.Count(),Color.red));
    }

    public override void Disconnected(BoltConnection connection){
        
        print(Depug.Log("---Someone Disconnected----"+connection.ConnectionId,Color.red));
        print(Depug.Log("---Someone Disconnected----"+connection.ConnectionId,Color.red));
        BikePlayerRegistry.RemovePlayer(connection);
        
    }
}
