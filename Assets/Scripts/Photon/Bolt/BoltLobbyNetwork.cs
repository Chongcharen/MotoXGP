using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Bolt.Photon;
using Bolt.Matchmaking;
using UdpKit;
using UniRx;

public enum ConnectionType{
    Server,Client,ToCreateServer,ToClient,ToServer,Disconnect,Session
}
public class BoltLobbyNetwork : GlobalEventListener
{
    public static Subject<Unit> OnBoltConnected = new Subject<Unit>();
    public static Subject<Unit> OnJoinSession = new Subject<Unit>();
    static BoltLobbyNetwork _instance;
    public BoltConnection playerConnection;
    ConnectionType connectionType = ConnectionType.Disconnect;
    public static BoltLobbyNetwork Instance{
        get{
            if(_instance == null){
                var go = new GameObject("BoltSession",typeof(BoltLobbyNetwork));
                _instance = go.GetComponent<BoltLobbyNetwork>();
            }
            return _instance;
        }
    }
    public void Init(){

    }
    public override void BoltStartBegin(){
        //register protocolToken 
        print(Depug.Log("BoltStartBegin RegisterTokenClass",Color.blue));
        BoltNetwork.RegisterTokenClass<ProtocolRoomProperty>();
        BoltNetwork.RegisterTokenClass<Bolt.Photon.PhotonRoomProperties>();
        BoltNetwork.RegisterTokenClass<ProtocolPlayerCustomize>();
    }

    public void Connect(){
        Debug.Log("Connect-++++++++++++++++++++");
        BoltLauncher.StartClient();
    }
    public void JoinRandomSession(Bolt.Photon.PhotonRoomProperties roomProperties){
        UdpSessionFilter filter =new UdpSessionFilter();
        filter.FillMode = UdpSessionFillMode.Random;
        filter[RoomOptionKey.MAP_THEME] = GameDataManager.Instance.gameLevel.theme;
        filter[RoomOptionKey.MAP_STAGE] = GameDataManager.Instance.gameLevel.stage;
        filter[RoomOptionKey.MAP_LEVEL] = GameDataManager.Instance.gameLevel.level;

        var playerCustomize = new ProtocolPlayerCustomize();
        playerCustomize.bike_body_id = 1;
        playerCustomize.bike_texture_id = 1;

        BoltMatchmaking.JoinRandomSession(filter,playerCustomize);
    }
    public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
    {
        base.StreamDataReceived(connection, data);
        print(Depug.Log("StreamDataReceived "+data,Color.blue));
    }
    public override void StreamDataStarted(BoltConnection connection, UdpChannelName channel, ulong streamID)
    {
        base.StreamDataStarted(connection, channel, streamID);
        print(Depug.Log("StreamDataStarted "+streamID,Color.blue));
    }
    public override void StreamDataProgress(BoltConnection connection, UdpChannelName channel, ulong streamID, float progress)
    {
        base.StreamDataProgress(connection, channel, streamID, progress);
        print(Depug.Log("StreamDataProgress "+streamID,Color.blue));
    }
    public override void StreamDataAborted(BoltConnection connection, UdpChannelName channel, ulong streamID)
    {
        base.StreamDataAborted(connection, channel, streamID);
        print(Depug.Log("StreamDataAborted "+streamID,Color.blue));
    }
    public override void BoltStartDone(){
        if(connectionType == ConnectionType.Disconnect){
            connectionType = BoltNetwork.IsClient ? ConnectionType.Client : ConnectionType.Server;
            OnBoltConnected.OnNext(default);
        }else if(connectionType == ConnectionType.ToCreateServer){
            if(BoltNetwork.IsServer)
                CreateSession();
        }
    }
    //Create Room
    public void CreateSession(){
        string matchName = Guid.NewGuid().ToString();
        Debug.Log("CreateSession !!!!!!!!!!!!!!!!!!!!!!!!!!!!!! "+matchName);
        var roomProperties = new ProtocolRoomProperty{
            theme = GameDataManager.Instance.gameLevel.theme,
            stage = GameDataManager.Instance.gameLevel.stage,
            level = GameDataManager.Instance.gameLevel.level
        };
        var customToken = new Bolt.Photon.PhotonRoomProperties();
        customToken.AddRoomProperty(RoomOptionKey.MAP_THEME,GameDataManager.Instance.gameLevel.theme);
        customToken.AddRoomProperty(RoomOptionKey.MAP_STAGE,GameDataManager.Instance.gameLevel.stage);
        customToken.AddRoomProperty(RoomOptionKey.MAP_LEVEL,GameDataManager.Instance.gameLevel.level);
        // customToken[RoomOptionKey.MAP_THEME] = GameDataManager.Instance.gameLevel.theme;
        // customToken[RoomOptionKey.MAP_STAGE] = GameDataManager.Instance.gameLevel.stage;
        // customToken[RoomOptionKey.MAP_LEVEL] = GameDataManager.Instance.gameLevel.level;
        // PhotonRoomProperties roomtoken = new PhotonRoomProperties();
        //     roomtoken.IsOpen = true; // set if the room will be open to be joined
        //     roomtoken.IsVisible = true; // set if the room will be visible
        //     roomtoken.AddRoomProperty("t", 1); // custom property
        //     roomtoken.AddRoomProperty("m", 2); // cu
        var playerCustom = new ProtocolPlayerCustomize();
        playerCustom.bike_body_id = 2;
        playerCustom.bike_texture_id = 2;
        BoltMatchmaking.CreateSession(
            sessionID: matchName,token:customToken,null,customToken
        );
    }
    
    public override void Connected(BoltConnection connection){
        //when new player(connection) has join to session
        //server only can create entity and assign control to a new player;
        //server is owner for all entity , player join can controlonly
        Debug.Log("Connected "+connection.AcceptToken);
        Debug.Log("id "+connection.ConnectionId);
        // if(BoltNetwork.IsClient){
        //     var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);
        //     entity.AssignControl(connection);
        // }
        // playerConnection = connection;
        // playerConnection.UserData = PlayFabController.Instance.playerProfileModel;
        //var model = playerConnection.UserData as PlayFab.ClientModels.PlayerProfileModel;
        //Debug.Log("UserData "+model.DisplayName);
       
    }
    public void StartGamePlay(){
        //BoltNetwork.LoadScene(SceneName.GAME_PLAY);
    }
    
    public override void SessionCreatedOrUpdated(UdpKit.UdpSession session){
        // Debug.Log("----------SessionCreatedOrUpdated--------"+session);
        // var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);//1
        // entity.TakeControl();//3
        // print(Depug.Log("All players "+BikePlayerRegistry.AllPlayers.Count(),Color.blue));
    }
    public override void EntityAttached(BoltEntity entity)
        {
            // Debug.Log("EntityAttached "+entity);
            // //2
            // SetUpEntityPlayer(entity);
            // EntityAttachedEventHandler(entity);

            // var photonPlayer = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
            // Debug.Log("Entity controled "+entity.IsControlled);
            // Debug.Log("Entity IsOwner "+entity.IsOwner);
            // Debug.Log("Entity IsControllerOrOwner "+entity.IsControllerOrOwner);
            // if (photonPlayer)
            // {
            //     if (entity.IsControlled)
            //     {
            //         photonPlayer.SetupPlayer();
            //     }
            //     else
            //     {
            //         photonPlayer.SetupOtherPlayer();
            //     }
            // }
        }
    // void SetUpEntityPlayer(BoltEntity entity){
    //     var state = entity.GetState<IRoomPlayerInfoState>();
    //     state.Name = PlayFabController.Instance.playerProfileModel.DisplayName;
    // }
    // private void EntityAttachedEventHandler(BoltEntity entity)
    //     {
    //         OnJoinSession.OnNext(default);
    //         var lobbyPlayer = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
    //         PageManager.Instance.UI_Room.AddPlayer(lobbyPlayer);
    //     }
    public void Shutdown(ConnectionType toType){
        connectionType = ConnectionType.Disconnect;
        BoltLauncher.Shutdown();
    }
    #region Boltfailed
    public override void SessionConnectFailed(UdpKit.UdpSession session, IProtocolToken token, UdpKit.UdpSessionError errorReason){
        Debug.Log("SessionConnectFailed ............"+session+" reason "+errorReason);
        
        switch (errorReason)
        {
            case UdpSessionError.GameDoesNotExist:
                    Debug.Log("Create new session");
                    //BoltLauncher.Shutdown();
                    connectionType = ConnectionType.ToCreateServer;
                    if(BoltNetwork.IsServer){
                        CreateSession();
                    }else{
                        BoltLauncher.Shutdown();
                    }
                    //CreateSession();
                break;
            default:
                Debug.Log("aaa");
                break;

        }
    }
    public override void BoltStartFailed(UdpKit.UdpConnectionDisconnectReason disconnectReason){
        Debug.Log("BoltStartFailed .............. "+disconnectReason);
    }
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason){
        Debug.Log("BoltShutdownBegin    ************** "+disconnectReason);
        if(connectionType == ConnectionType.ToCreateServer){
            BoltLauncher.StartServer();
        }else{
            BoltLauncher.StartClient();
        }
    }
    public override void ConnectFailed(UdpKit.UdpEndPoint endpoint, IProtocolToken token){
        Debug.Log("0000000000000000   Connect Failed 0000000000000"+endpoint.Address);
    }
    public override void Disconnected(BoltConnection connection){
        Debug.Log("Disconnected");
        
    }
    public override void SessionCreationFailed(UdpKit.UdpSession session, UdpKit.UdpSessionError errorReason){
        Debug.Log("SessionCreationFailed ............"+session+" reason "+errorReason);
    }
    #endregion
}
