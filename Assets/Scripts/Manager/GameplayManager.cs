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
    public static Subject<Unit> OnGameEnd = new Subject<Unit>();
    public ReactiveProperty<int> totalRound = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> round = new ReactiveProperty<int>(0);
    public void Init(){

    }
    public void SetTotalRound(int _totalRound){
        totalRound.Value =_totalRound-1;
        
    }
    public void IncreaseRound(){
        round.Value ++;
        if(round.Value > totalRound.Value)
            OnGameEnd.OnNext(default);
            // Game End
    }
    public void ResetGame(){
        round.Value = 0;
        OnRestartGame.OnNext(default);
    }

    //PlayerREgister When Cross Finish Line
    public static void RegisterLocalPlayerFinish(long ticktime){
        Debug.Log("RegisterLocalPlayerfinish "+ticktime);
        //set time in playerindexprofiledata
        var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        var playersIndexHash = roomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        var targetPlayer = PhotonNetwork.LocalPlayer;
        Debug.Assert(playersIndexHash.ContainsKey(targetPlayer.UserId));
        var playerProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playersIndexHash[targetPlayer.UserId].ToString());
        playerProfileData.playerFinishTime = ticktime.ToString();
        var json = JsonConvert.SerializeObject(playerProfileData);
        playersIndexHash[targetPlayer.UserId] = json;
        Debug.Log("changed "+playersIndexHash[targetPlayer.UserId]);
        PhotonNetwork.CurrentRoom.SetCustomProperties(playersIndexHash);
    }
}
