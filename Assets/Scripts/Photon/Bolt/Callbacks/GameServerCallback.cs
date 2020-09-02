using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UniRx;

[BoltGlobalBehaviour(BoltNetworkModes.Server,SceneName.GAMEPLAY)]
public class GameServerCallback : GlobalEventListener
{
    void Awake(){
        print(Depug.Log("GameServerCallback Awake ",Color.white));
        print(Depug.Log("GameServerCallback Awake "+BikePlayerRegistry.AllPlayers.Count(),Color.white));
    }
    public override void SceneLoadLocalDone(string scene, IProtocolToken token){
        print(Depug.Log("GameServerCallback SceneLoadLocalDone ",Color.white));
        if(BoltNetwork.IsServer){
            var positionPlayer = MapManager.Instance.spawnPointsPosition[0];
            var entity = BoltNetwork.Instantiate(BoltPrefabs.Test_Sphere,positionPlayer,Quaternion.Euler(0,90,0));
            entity.TakeControl();
        }else{
            var positionPlayer = MapManager.Instance.spawnPointsPosition[BoltNetwork.Entities.Count()+1];
            var entity = BoltNetwork.Instantiate(BoltPrefabs.Test_Sphere,positionPlayer,Quaternion.Euler(0,90,0));
            entity.TakeControl();
        }
            //entity.GetComponent<Rigidbody>().isKinematic = false;
        
       
    }
    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token){
        // print(Depug.Log("SceneLoadRemoteDone SceneLoadLocalDone "+BoltNetwork.IsClient,Color.white));
        //  var player = BikePlayerRegistry.GetBikePlayer(connection);
        // // Debug.Log("Player ????? "+player);
        //  if(player == null)return;
        //  var position = MapManager.Instance.spawnPointsPosition[BikePlayerRegistry.GetBikePlayer(connection).index];
        //  var entity = BoltNetwork.Instantiate(BoltPrefabs.Test_Sphere,position,Quaternion.Euler(0,90,0));
        //      entity.AssignControl(connection);
        //entity.GetComponent<Rigidbody>().isKinematic = false;
        //     VirtualPlayerCamera.Instantiate();
        //     VirtualPlayerCamera.instance.FollowTarget(entity.transform);
        //     VirtualPlayerCamera.instance.LookupTarget(entity.transform);
        // if(BoltNetwork.IsClient){
        //     var position = MapManager.Instance.spawnPointsPosition[BikePlayerRegistry.GetBikePlayer(connection).index];
        //     var entity = BoltNetwork.Instantiate(BoltPrefabs.BikePlayer_POC,position,Quaternion.Euler(0,90,0));
        //     entity.AssignControl(connection);
        //     // VirtualPlayerCamera.Instantiate();
        //     // VirtualPlayerCamera.instance.FollowTarget(entity.transform);
        //     // VirtualPlayerCamera.instance.LookupTarget(entity.transform);
        // }
    }
    // public override void ControlOfEntityGained(BoltEntity entity){
    //     //playerREady when entity gained;
    //     BikePlayerRegistry.RegisterPlayerReady(entity.Source);
    //     if(BikePlayerRegistry.AllPlayerReadys){
    //         print(Depug.Log("RegisterPlayerReady "+BikePlayerRegistry.AllPlayerReadys,Color.green));
    //         //Show countdown in log event!!!!
    //         CreateRaceCountdown(3);
    //     }
    // }

    // void CreateRaceCountdown(int countTime){
    //     var raceCountdownEvent = RaceCountdown.Create();
        
    //         var _update = Observable.Interval(TimeSpan.FromSeconds(1)).Take(countTime+1).Subscribe(x => // x starts from 0 and is incremented everytime the stream pushes another item.
    //         {
    //             var countTimeResult = (countTime-x);
    //             raceCountdownEvent.Message = (countTimeResult == 0) ? "GO!" : countTimeResult.ToString();
    //             raceCountdownEvent.RaceStart = (countTimeResult == 0);
    //             raceCountdownEvent.Send();
    //             var logEvent = LogEvent.Create();
    //             logEvent.Message = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
    //             logEvent.Send();
    //             print(Depug.Log("Count in "+raceCountdownEvent.Message,Color.yellow));
    //         }).AddTo(this);
    // }
    // public override void OnEvent(RaceCountdown evnt){
    //     Debug.Log("!!!!!!!!!!!!!!!!! Onevent Racecountdown "+evnt.Message);
    // }
}
