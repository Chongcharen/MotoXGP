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
using UdpKit.Platform.Photon;
using Newtonsoft.Json;
using Facebook.MiniJSON;
public enum ConnectionType{
    Server,Client,ToCreateServer,ToClient,ToServer,Disconnect,Session
}
public class BoltLobbyNetwork : GlobalEventListener
{
    public static Subject<Unit> OnBoltConnected = new Subject<Unit>();
    public static Subject<Unit> OnJoinSession = new Subject<Unit>();
    public static Subject<Unit> OnSessionEndProgress = new Subject<Unit>();
    static BoltLobbyNetwork _instance;
    public BoltConnection playerConnection;
    public ConnectionType connectionType = ConnectionType.Disconnect;
    public static BoltLobbyNetwork Instance{
        get{
            if(_instance == null){
                var go = new GameObject("BoltSession",typeof(BoltLobbyNetwork));
                _instance = go.GetComponent<BoltLobbyNetwork>();
                DontDestroyOnLoad(go);
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
        BoltNetwork.RegisterTokenClass<PlayerProfileToken>();
        BoltNetwork.RegisterTokenClass<BikeEquipmentToken>();
    }

    public void Connect(){
        Debug.Log("Connect-++++++++++++++++++++");
        var parameter = new Dictionary<string,object>();
        parameter.Add(PopupKeys.PARAMETER_POPUP_HEADER,"Connect Server");
        parameter.Add(PopupKeys.PARAMETER_MESSAGE,"Connect Client");
        //Popup_BoltConnect.Launch(parameter);
        BoltLauncher.StartClient();
    }
    public void JoinRandomSession(Bolt.Photon.PhotonRoomProperties roomProperties){
        UdpSessionFilter filter =new UdpSessionFilter();
        filter.FillMode = UdpSessionFillMode.Random;
        filter[RoomOptionKey.MAP_THEME] = GameDataManager.Instance.gameLevel.theme;
        filter[RoomOptionKey.MAP_STAGE] = GameDataManager.Instance.gameLevel.stage;
        filter[RoomOptionKey.MAP_LEVEL] = GameDataManager.Instance.gameLevel.level;

        // var playerCustomize = new ProtocolPlayerCustomize();//connectionToken
        // playerCustomize.bike_body_id = 2;
        // playerCustomize.bike_texture_id = 5;

        var playerData = new PlayerProfileToken();
        //playerData.playerBikeData.playerFinishTime = 99999999;
        playerData.playerProfileModel = PlayFabController.Instance.playerProfileModel.Value;
        playerData.RandomBikeData();
        Debug.Log("playerdata "+playerData.playerBikeData);
        Debug.Log("model "+playerData.playerProfileModel);
        Debug.Log("filter "+filter);
        //BoltMatchmaking.JoinRandomSession(filter,playerData);
        BoltMatchmaking.JoinRandomSession(filter,playerData);
        CallPopupForWaiting("ระบบกำลัง ค้นหาห้อง โปรดรอสักครู่");
    }
    void CallPopupForWaiting(string messageDetail){
        Popup_Loading.Launch();
        var parameter = new Dictionary<string,object>();
        parameter.Add(PopupKeys.PARAMETER_POPUP_HEADER,"JoinRoom");
        parameter.Add(PopupKeys.PARAMETER_MESSAGE,messageDetail);
        Popup_JoinGameRoom.Launch(parameter);
    }
    public void JoinSession(UdpKit.UdpSession session){

        BoltMatchmaking.JoinSession(session);
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
        var playerProfiles = new Dictionary<int,PlayerProfileToken>();
        //var json = JsonUtility.ToJson(playerProfiles);
        var json = JsonConvert.SerializeObject(playerProfiles);
        //var json = Json.Serialize(playerProfiles);
        Debug.Log("json "+json);
        customToken.AddRoomProperty(RoomOptionKey.PLAYERS_RANK,json);
        customToken.IsOpen = true;
        customToken.IsVisible = true;
         foreach (var item in playerProfiles)
        {
            Debug.LogFormat("Key : {0} , Value : {1}",item.Key,item.Value);
            GUIDebug.Log("Entity Attached !!!!!! 1111111 Key : "+item.Key+" , Value : "+item.Value);
        }
        // customToken[RoomOptionKey.MAP_THEME] = GameDataManager.Instance.gameLevel.theme;
        // customToken[RoomOptionKey.MAP_STAGE] = GameDataManager.Instance.gameLevel.stage;
        // customToken[RoomOptionKey.MAP_LEVEL] = GameDataManager.Instance.gameLevel.level;
        // PhotonRoomProperties roomtoken = new PhotonRoomProperties();
        //     roomtoken.IsOpen = true; // set if the room will be open to be joined
        //     roomtoken.IsVisible = true; // set if the room will be visible
        //     roomtoken.AddRoomProperty("t", 1); // custom property
        //     roomtoken.AddRoomProperty("m", 2); // cu
        // var playerCustom = new ProtocolPlayerCustomize(); // connectionToken 
        // playerCustom.bike_body_id = 2;
        // playerCustom.bike_texture_id = 3;
        var playerData = new PlayerProfileToken();
        //playerData.playerBikeData.playerFinishTime = 99999999;
        playerData.playerProfileModel = PlayFabController.Instance.playerProfileModel.Value;
        playerData.RandomBikeData();
        // token เป็น playerdata แล้ว join ห้องเดียวกันไมไ่ด้ งง
        
        BoltMatchmaking.CreateSession(
            sessionID: matchName,token:customToken,null,customToken
        );
        CallPopupForWaiting("ระบบกำลัง สร้างห้อง โปรดรอสักครู่");
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
        Debug.Log("----------SessionCreatedOrUpdated--------"+session);
        
        // var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);//1
        // entity.TakeControl();//3
        print(Depug.Log("HostData "+session.HostData.Length,Color.blue));
        print(Depug.Log("HostData "+session.HostObject,Color.blue));
        
        //print(Depug.Log("HostData "+ps.Properties.Count,Color.blue));

        PhotonSession photonSession = BoltMatchmaking.CurrentSession as PhotonSession;
        photonSession.Properties["t"] = "Bavaria";
        foreach (var ss in photonSession.Properties) {
            Debug.LogFormat("Key : {0} , Value : {1}",ss.Key,ss.Value);
           // GUIDebug.Log("Key : "+ss.Key+" , Value : "+ss.Value);
        }
        OnSessionEndProgress.OnNext(default);
        // print(Depug.Log("LobbyServerCallback connection ",Color.white));
        // print(Depug.Log("LobbyServerCallback Server "+BoltNetwork.Server,Color.white));
        // print(Depug.Log("LobbyServerCallback ConnectToken "+BoltNetwork.Server.ConnectToken,Color.white));
        // print(Depug.Log("LobbyServerCallback AcceptToken "+BoltNetwork.Server.AcceptToken,Color.white));
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
         OnSessionEndProgress.OnNext(default);
    }
    public override void BoltStartFailed(UdpKit.UdpConnectionDisconnectReason disconnectReason){
        Debug.Log("BoltStartFailed .............. "+disconnectReason);
         OnSessionEndProgress.OnNext(default);
    }
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason){
        Debug.Log("BoltShutdownBegin    ************** "+disconnectReason);
        if(connectionType == ConnectionType.ToCreateServer){
            BoltLauncher.StartServer(); 
        }else{
            BoltLauncher.StartClient();
        }
         OnSessionEndProgress.OnNext(default);
    }
    public override void ConnectFailed(UdpKit.UdpEndPoint endpoint, IProtocolToken token){
        Debug.Log("0000000000000000   Connect Failed 0000000000000"+endpoint.Address);
         OnSessionEndProgress.OnNext(default);
    }
    public override void Disconnected(BoltConnection connection){
        Debug.Log("Disconnected");
         OnSessionEndProgress.OnNext(default);
        
    }
    public override void SessionCreationFailed(UdpKit.UdpSession session, UdpKit.UdpSessionError errorReason){
        Debug.Log("SessionCreationFailed ............"+session+" reason "+errorReason);
         OnSessionEndProgress.OnNext(default);
    }
    #endregion

    public void Dispose(){
        if(gameObject != null)
            Destroy(gameObject);
    }
}
