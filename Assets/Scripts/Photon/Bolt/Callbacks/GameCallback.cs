using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UniRx;
using System;
using System.Linq;
using Bolt.Matchmaking;
using UdpKit.Platform.Photon;
using UdpKit;
using Newtonsoft.Json;

[BoltGlobalBehaviour(SceneName.GAMEPLAY)]
public class GameCallback : GlobalEventListener
{
    public static Subject<RaceCountdown> OnGameReady = new Subject<RaceCountdown>();
    public static Subject<BoltEntity> OnEntityDetached = new Subject<BoltEntity>();
    public static Subject<BoltEntity> OnEntityAttached = new Subject<BoltEntity>();
    public static Subject<List<BoltEntity>> OnPlayerRanksUpdate = new Subject<List<BoltEntity>>();
    public static Subject<string> OnplayerRankJsonUpdate = new Subject<string>();
    public static Subject<Unit> OnCutSceneReady = new Subject<Unit>();
    BoltEntity myEntity;

    void Start(){
        PhotonSession photonSession = BoltMatchmaking.CurrentSession as PhotonSession;
        photonSession.ObserveEveryValueChanged(session => session.Properties[RoomOptionKey.PLAYERS_RANK]).Subscribe(jsonData =>{
            var json = jsonData.ToString();
            Debug.Log("--------------->33333333333333333json");
           // UpdatePlayerRanking(json);
        }).AddTo(this);
        GUIDebug.Log("22222222222222222222222222222 Session "+photonSession.Properties[RoomOptionKey.PLAYERS_RANK]);
        // photonSession.Properties.ObserveEveryValueChanged(p => p).Subscribe(p =>{
        //     GUIDebug.Log("--------------------->Photon Session Update !!!!!! "+p.ToString());
        //     Debug.Log("*************************************** "+p);
        //     //UpdatePlayerRanking(json);
        // }).AddTo(this);
        
    }
    public override void SceneLoadLocalDone(string scene, IProtocolToken token){
        if(BoltNetwork.IsServer){
            print(Depug.Log("SceneLoadLocalDone SErver... ",Color.green));
            var player = BikePlayerRegistry.GetBikePlayer(BoltNetwork.Server);
            print(Depug.Log("player "+player,Color.green));
            var playerData = new PlayerProfileToken();
            playerData.playerProfileModel = PlayFabController.Instance.playerProfileModel.Value;
            playerData.RandomBikeData();
            playerData.playerBikeData.runningTrack = player.index;
            print(Depug.Log("spawnpoint position "+MapManager.Instance.spawnPointsPosition.Count(),Color.blue));
            Debug.Log("index "+player.index);
            print(Depug.Log("------------------------------------ "+player,Color.blue));
            var positionPlayer = MapManager.Instance.spawnPointsPosition[player.index];
            var entity = BoltNetwork.Instantiate(BoltPrefabs.BikePlayer_113024,playerData,positionPlayer,Quaternion.Euler(0,90,0));
            entity.TakeControl();
            entity.GetComponent<BikeBoltSystem>().runningTrack = player.index;
           // LoadBikePlayer();
        }
    }
    public async void LoadBikePlayer(){
         print(Depug.Log("SceneLoadLocalDone SErver... ",Color.green));
            var player = BikePlayerRegistry.GetBikePlayer(BoltNetwork.Server);
            print(Depug.Log("player "+player,Color.green));
            var playerData = new PlayerProfileToken();
            playerData.playerProfileModel = PlayFabController.Instance.playerProfileModel.Value;
            playerData.RandomBikeData();
            playerData.playerBikeData.runningTrack = player.index;
            print(Depug.Log("spawnpoint position "+MapManager.Instance.spawnPointsPosition.Count(),Color.blue));
            Debug.Log("index "+player.index);
            print(Depug.Log("------------------------------------ "+player,Color.blue));
            var positionPlayer = MapManager.Instance.spawnPointsPosition[player.index];
         var bikePlayer = await AddressableManager.Instance.LoadObject<GameObject>("Bike/BikePlayer_113024.prefab");
         var entity = BoltNetwork.Instantiate(bikePlayer,playerData,positionPlayer,Quaternion.Euler(0,90,0));
             entity.TakeControl();
             entity.GetComponent<BikeBoltSystem>().runningTrack = player.index;
    }
    public override void SceneLoadRemoteDone(BoltConnection connection){
        if(!BoltNetwork.IsServer)return;
        var player = BikePlayerRegistry.GetBikePlayer(connection);
        //BikePlayerRegistry
        var raceTrackEvent = RaceTrackEvent.Create(connection);
        raceTrackEvent.TrankIndex = player.index;
        raceTrackEvent.Send();
    }
    public override void OnEvent(RaceTrackEvent evnt){
        print(Depug.Log("RaceTrackEvent "+evnt.TrankIndex,Color.green));
        var playerData = new PlayerProfileToken();
            playerData.playerProfileModel = PlayFabController.Instance.playerProfileModel.Value;
            playerData.RandomBikeData();
            playerData.playerBikeData.runningTrack = evnt.TrankIndex;
        var positionPlayer = MapManager.Instance.spawnPointsPosition[evnt.TrankIndex];
        var entity = BoltNetwork.Instantiate(BoltPrefabs.BikePlayer_113024,playerData,positionPlayer,Quaternion.Euler(0,90,0));
            entity.TakeControl();
    }
    public override void OnEvent(PlayerPositionRequest evnt){
        
        if(evnt.Request && BoltNetwork.IsServer){
            var response = PlayerPositionRequest.Create(GlobalTargets.Everyone);
            var bikePlayer = BikePlayerRegistry.GetBikePlayer(evnt.RaisedBy);
            if(bikePlayer == null)return;
            response.Index = BikePlayerRegistry.GetIndexOf(bikePlayer);
            response.Response = true;
            response.ToEntity = bikePlayer.character;
            response.SeverEntity = BikePlayerRegistry.ServerPlayer.character;
            response.Send();
        }else if(evnt.Response && BoltNetwork.IsClient){
            if(myEntity == null)return;
            if(myEntity != evnt.ToEntity)return;
            var newPosition = MapManager.Instance.spawnPointsPosition[evnt.Index];
            myEntity.transform.SetPositionAndRotation(newPosition,Quaternion.Euler(0,90,0));
        }
    }
    public override void ControlOfEntityGained(BoltEntity entity){
        print(Depug.Log("ControlOfEntityGained "+BikePlayerRegistry.AllPlayerReadys,Color.green));
        print(Depug.Log("BoltEntity "+entity.Source,Color.green));
        var playerReadyEvent = PlayerReadyEvent.Create(GlobalTargets.OnlyServer);
        playerReadyEvent.Ready = true;
        playerReadyEvent.Send();
    }
     public override void EntityAttached(BoltEntity entity)
    {
        base.EntityAttached(entity);
        if(BoltNetwork.IsClient)return;
        PhotonSession photonSession = BoltMatchmaking.CurrentSession as PhotonSession;
        var json = photonSession.Properties[RoomOptionKey.PLAYERS_RANK].ToString();
        var playerProfilesDic = JsonConvert.DeserializeObject<Dictionary<int,PlayerProfileToken>>(json);
        if(!playerProfilesDic.ContainsKey(entity.GetInstanceID())){
            PlayerProfileToken token = entity.AttachToken as PlayerProfileToken;
            playerProfilesDic.Add(entity.GetInstanceID(),token);
        }
        json = JsonConvert.SerializeObject(playerProfilesDic);
        Debug.Log("Attached Json "+json);
        photonSession.Properties[RoomOptionKey.PLAYERS_RANK] = json;
        BoltMatchmaking.UpdateSession(photonSession.Properties as IProtocolToken);
        foreach (var item in photonSession.Properties)
        {
            Debug.LogFormat("Key : {0} , Value : {1}",item.Key,item.Value);
            GUIDebug.Log("Entity Attached !!!!!! 1111111 Key : "+item.Key+" , Value : "+item.Value);
        }
        OnEntityAttached.OnNext(entity);
    }
    public override void SessionCreatedOrUpdated(UdpSession session)
    {
        base.SessionCreatedOrUpdated(session);
        Debug.Log("SessionCreatedOrUpdated "+session);
        Debug.Log("session equal "+(session.Id == BoltMatchmaking.CurrentSession.Id));
        if(session.Id != BoltMatchmaking.CurrentSession.Id)return;
        PhotonSession photonSession = session as PhotonSession;
        OnplayerRankJsonUpdate.OnNext(photonSession.Properties[RoomOptionKey.PLAYERS_RANK].ToString());
    }

    public override void OnEvent(PlayerRaceFinishEvent evnt){
        print(Depug.Log("PlayerRaceFinishEvent "+evnt,Color.green));
        //Debug.Log(evnt.RaisedBy.e)
        var tokenID = evnt.Entity.AttachToken as PlayerProfileToken;
        Debug.Log("Entity count "+BoltNetwork.Entities.Count());
        print(Depug.Log("Entity ID "+evnt.Entity.GetInstanceID(),Color.blue));
        print(Depug.Log("Entity Track "+tokenID.playerBikeData.runningTrack,Color.blue));
        print(Depug.Log("Entity playerFinishTime "+tokenID.playerBikeData.playerFinishTime,Color.blue));
        print(Depug.Log("Entity jsonToken "+evnt.JsonToken,Color.blue));


        PhotonSession photonSession = BoltMatchmaking.CurrentSession as PhotonSession;
        var json = photonSession.Properties[RoomOptionKey.PLAYERS_RANK].ToString();
        var playerProfilesDic = JsonConvert.DeserializeObject<Dictionary<int,PlayerProfileToken>>(json);
        var clientToken = JsonConvert.DeserializeObject<PlayerProfileToken>(evnt.JsonToken);
        playerProfilesDic[evnt.Entity.GetInstanceID()] = clientToken;
        json = JsonConvert.SerializeObject(playerProfilesDic);
        photonSession.Properties[RoomOptionKey.PLAYERS_RANK] = json;
        OnplayerRankJsonUpdate.OnNext(json);

        //var clientToken = evnt.Entity.AttachToken as PlayerProfileToken;

        //OnPlayerRanksUpdate.OnNext(BoltNetwork.Entities.ToList());
        if(BoltNetwork.IsClient)return;

        // PhotonSession photonSession = BoltMatchmaking.CurrentSession as PhotonSession;
        // var json = photonSession.Properties[RoomOptionKey.PLAYERS_RANK].ToString();
        // var playerProfilesDic = JsonConvert.DeserializeObject<Dictionary<int,PlayerProfileToken>>(json);
        // var clientToken = evnt.Entity.AttachToken as PlayerProfileToken;
        //playerProfilesDic[evnt.Entity.GetInstanceID()] = clientToken;
        //json = JsonConvert.SerializeObject(playerProfilesDic);
        //photonSession.Properties[RoomOptionKey.PLAYERS_RANK] = json;

        BoltMatchmaking.UpdateSession(photonSession.Properties as IProtocolToken);
        // foreach (var entity in BoltNetwork.Entities)
        // {
        //     var token = entity.AttachToken as PlayerProfileToken;
        //     Debug.Log("Time "+token.playerBikeData.playerFinishTime);
        // }
    }
    public override void OnEvent(PlayerReadyEvent evnt){
        Debug.Log("PlayerReadyEvent Receive");
        if(!BoltNetwork.IsServer)return;
        if(!evnt.Ready)return;
        BikePlayerRegistry.RegisterPlayerReady(evnt.RaisedBy);
        
        if(BikePlayerRegistry.AllPlayerReadys){
            print(Depug.Log("RegisterPlayerReady "+BikePlayerRegistry.AllPlayerReadys,Color.green));
            //Show countdown in log event!!!!
            var raceCutSceneEvent = RaceCutSceneEvent.Create(GlobalTargets.Everyone);
            raceCutSceneEvent.Send();
            CreateRaceCountdown(3);
        }
    
    }
    public override void Disconnected(BoltConnection connection) {
        BoltLog.Info("Disconnected with Token {0}", connection.DisconnectToken);
        //BoltLog.Info("Returning to main menu...");
        //Application.LoadLevel("MainMenu");
    }
    public override void EntityDetached(BoltEntity entity)
    {
        base.EntityDetached(entity);
        OnEntityDetached.OnNext(entity);
    }
    void CreateRaceCountdown(int countTime){
        
            var _update = Observable.Interval(TimeSpan.FromSeconds(1)).Take(countTime+1).Subscribe(x => // x starts from 0 and is incremented everytime the stream pushes another item.
            {
                var raceCountdownEvent = RaceCountdown.Create(GlobalTargets.Everyone);
                var countTimeResult = (countTime-x);
                if(countTimeResult<=3)
                    raceCountdownEvent.Message = (countTimeResult == 0) ? "GO!" : countTimeResult.ToString();
                raceCountdownEvent.RaceStart = (countTimeResult == 0);
                raceCountdownEvent.Send();
                print(Depug.Log("Count in "+raceCountdownEvent.Message,Color.yellow));
            }).AddTo(this);
    }
    public override void OnEvent(RaceCountdown evnt){
            OnGameReady.OnNext(evnt);
            if(evnt.RaceStart)
                GameplayManager.Instance.StartGame();
    }
    public override void OnEvent(RaceCutSceneEvent evnt){
        //OpenCutscene
        // var objectCutScene = Resources.Load<GameObject>("CutScene");
        // Instantiate(objectCutScene);
        OnCutSceneReady.OnNext(default);
    }


    public override void StreamDataProgress(BoltConnection connection, UdpChannelName channel, ulong streamID, float progress)
    {
        base.StreamDataProgress(connection, channel, streamID, progress);
    }
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        base.SessionListUpdated(sessionList);
        Debug.Log("SessionListUpdated "+sessionList);
    }
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        base.BoltShutdownBegin(registerDoneCallback, disconnectReason);
        Debug.Log("BoltShutdownBegin "+disconnectReason);
        //BoltLauncher.StartClient();
    }
}
