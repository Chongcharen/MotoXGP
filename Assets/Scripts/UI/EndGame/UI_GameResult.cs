using System.Linq.Expressions;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_GameResult : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject root;
    [SerializeField]List<PlayerRankGameplay> playerRanks;
    [SerializeField]Button b_leaveRoom;
    Dictionary<string,PlayerRankGameplay> playerRanksBinding;
    public List<PlayerIndexProfileData> playerDataSort = new List<PlayerIndexProfileData>();
    void Start(){
        GameplayManager.OnGameEnd.Subscribe(_=>{
            root.gameObject.SetActive(true);
        }).AddTo(this);
        b_leaveRoom.OnClickAsObservable().Subscribe(_=>{
            PhotonNetworkConsole.Instance.LeaveRoom();
            SceneManager.LoadScene(SceneName.LOBBY);
        }).AddTo(this);
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged){
        var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties as Hashtable;
        var playerIndexProperties = roomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        foreach (var property in propertiesThatChanged)
        {
            //Debug.Log(string.Format("key {0} value {1}",property.Key,property.Value));
            if(!playerIndexProperties.ContainsKey(property.Key))return;
            UpdatePlayerRanking(property.Value.ToString());
        }
    }
    void UpdatePlayerRanking(string JSonprofileData){
        
        Debug.Log("UpdatePlayerRAnking " +JSonprofileData);
        var playerIndexProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(JSonprofileData);
        var findPlayer = playerDataSort.FirstOrDefault(p => p.userId == playerIndexProfileData.userId);
        if(string.IsNullOrEmpty(playerIndexProfileData.playerFinishTime))return;
        if(findPlayer == null){
            Debug.Log("not findplayer "+playerIndexProfileData.nickName);
            playerDataSort.Add(playerIndexProfileData);
        }else{
            Debug.Log("findPlayer "+findPlayer.nickName);
            findPlayer = playerIndexProfileData;
        }
        Debug.Log("playerDataSort count "+playerDataSort.Count);
        playerDataSort = playerDataSort.OrderBy(p => p.playerFinishTime).ToList();
        for (int i = 0; i < playerDataSort.Count; i++)
        {
            playerRanks[i].gameObject.SetActive(true);
            playerRanks[i].SetUp(playerDataSort[i]);
        }
    }
}
