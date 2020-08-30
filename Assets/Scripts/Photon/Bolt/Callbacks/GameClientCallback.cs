using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UniRx;
using System;

[BoltGlobalBehaviour(BoltNetworkModes.Client,SceneName.GAMEPLAY)]
public class GameClientCallback : GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene, IProtocolToken token){
        print(Depug.Log("GameClientCallback SceneLoadLocalDone ",Color.green));
        // if(BoltNetwork.IsClient){
        //     var entity = BoltNetwork.Instantiate(BoltPrefabs.BikePlayer_POC,new Vector3(22,5,2),Quaternion.Euler(0,90,0));
        //     entity.TakeControl();
        //     VirtualPlayerCamera.Instantiate();
        //     VirtualPlayerCamera.instance.FollowTarget(entity.transform);
        //     VirtualPlayerCamera.instance.LookupTarget(entity.transform);
        // }
    }
    // public override void OnEvent(RaceCountdown evnt){
    //     Debug.Log("!!!!!!!!!!!!!!!!! Onevent Racecountdown "+evnt.Message);
    // }
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
}
