using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UniRx;
using System;
using System.Linq;
[BoltGlobalBehaviour(SceneName.GAMEPLAY)]
public class GameCallback : GlobalEventListener
{
    public static Subject<RaceCountdown> OnGameReady = new Subject<RaceCountdown>();
    BoltEntity myEntity;
    public override void SceneLoadLocalDone(string scene, IProtocolToken token){
        if(BoltNetwork.IsServer){
            var positionPlayer = MapManager.Instance.spawnPointsPosition[0];
            var entity = BoltNetwork.Instantiate(BoltPrefabs.BikePlayer_POC,positionPlayer,Quaternion.Euler(0,90,0));
            entity.TakeControl();
        }else
        {
           
        }
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
         var positionPlayer = MapManager.Instance.spawnPointsPosition[evnt.TrankIndex];
            var entity = BoltNetwork.Instantiate(BoltPrefabs.BikePlayer_POC,positionPlayer,Quaternion.Euler(0,90,0));
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
    public override void OnEvent(PlayerReadyEvent evnt){
        Debug.Log("PlayerReadyEvent Receive");
        if(!BoltNetwork.IsServer)return;
        if(!evnt.Ready)return;
        BikePlayerRegistry.RegisterPlayerReady(evnt.RaisedBy);
        if(BikePlayerRegistry.AllPlayerReadys){
            print(Depug.Log("RegisterPlayerReady "+BikePlayerRegistry.AllPlayerReadys,Color.green));
            //Show countdown in log event!!!!
            CreateRaceCountdown(3);
        }
    }
    
    void CreateRaceCountdown(int countTime){
            var _update = Observable.Interval(TimeSpan.FromSeconds(1)).Take(countTime+1).Subscribe(x => // x starts from 0 and is incremented everytime the stream pushes another item.
            {
                var raceCountdownEvent = RaceCountdown.Create(GlobalTargets.Everyone);
                var countTimeResult = (countTime-x);
                raceCountdownEvent.Message = (countTimeResult == 0) ? "GO!" : countTimeResult.ToString();
                raceCountdownEvent.RaceStart = (countTimeResult == 0);
                raceCountdownEvent.Send();
                print(Depug.Log("Count in "+raceCountdownEvent.Message,Color.yellow));
            }).AddTo(this);
    }
    public override void OnEvent(RaceCountdown evnt){
            OnGameReady.OnNext(evnt);
    }
}
