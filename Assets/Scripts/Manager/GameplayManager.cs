using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
public class GameplayManager : InstanceClass<GameplayManager>
{
    public static Subject<Unit> OnRestartGame = new Subject<Unit>();
    public static Subject<bool> OnGameStart = new Subject<bool>();
    public static Subject<bool> OnGameEnd = new Subject<bool>();
    public static Subject<long> OnPlayerFinishTime = new Subject<long>();
    public ReactiveProperty<int> totalRound = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> round = new ReactiveProperty<int>(0);
    public ReactiveProperty<float> elapsedTime = new ReactiveProperty<float>(0);
    public ReactiveProperty<bool> isStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isEnd = new ReactiveProperty<bool>(false);
    public void Init(){

    }
    public void SetTotalRound(int _totalRound){
        print(Depug.Log("Settotal round "+_totalRound,Color.yellow));
        totalRound.Value =_totalRound-1;
        
    }
    public void IncreaseRound(){
        Debug.Log("IncreaseRound ");
        round.Value ++;
        if(round.Value > totalRound.Value){
            isEnd.Value = true;
            OnGameEnd.OnNext(isEnd.Value);
        }
            // Game End
    }
    public void ResetGame(){
        round.Value = 0;
        OnRestartGame.OnNext(default);
    }
    public void StartGame(){
        if(isStart.Value)return;
        isStart.Value = true;
        elapsedTime.Value = Time.time;
        OnGameStart.OnNext(default);
    }

    //PlayerREgister When Cross Finish Line
    public static void RegisterLocalPlayerFinish(long ticktime){
        Debug.Log("RegisterLocalPlayerfinish "+ticktime);
        OnPlayerFinishTime.OnNext(ticktime);       
         
        //set time in playerindexprofiledata
        // var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        // var playersIndexHash = roomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        // var targetPlayer = PhotonNetwork.LocalPlayer;
        // Debug.Assert(playersIndexHash.ContainsKey(targetPlayer.UserId));
        // var playerProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playersIndexHash[targetPlayer.UserId].ToString());
        // playerProfileData.playerFinishTime = ticktime.ToString();
        // var json = JsonConvert.SerializeObject(playerProfileData);
        // playersIndexHash[targetPlayer.UserId] = json;
        // Debug.Log("changed "+playersIndexHash[targetPlayer.UserId]);
        // PhotonNetwork.CurrentRoom.SetCustomProperties(playersIndexHash);
    }
    public void PlayerReady(string userId){
        Debug.Log("PlayerReady "+userId);
        var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        var playersIndexHash = roomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        if(!playersIndexHash.ContainsKey(userId)){
            Debug.LogError("Not contain "+userId+ " in thid game");
            return;
        }
        var playerProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playersIndexHash[userId].ToString());
        playerProfileData.ready = true;
        var json = JsonConvert.SerializeObject(playerProfileData);
        playersIndexHash[userId] = json;

        roomProperties[RoomPropertyKeys.PLAYER_INDEX] = playersIndexHash;
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
    }
}
