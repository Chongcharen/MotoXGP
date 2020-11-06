using System.Linq.Expressions;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Bolt.Matchmaking;
using ExitGames.Client.Photon;
using Bolt;

public class UI_GameResult : MonoBehaviour
{
    [SerializeField]GameObject root;
    [SerializeField]List<PlayerRankGameplay> playerRanks;
    [SerializeField]Button b_leaveRoom;
    Dictionary<string,PlayerRankGameplay> playerRanksBinding;
    public List<PlayerIndexProfileData> playerDataSort = new List<PlayerIndexProfileData>();
    void Awake(){
        
    }
    void Start(){
        GameplayManager.OnGameEnd.Subscribe(_=>{
            root.gameObject.SetActive(true);
        }).AddTo(this);
        b_leaveRoom.OnClickAsObservable().Subscribe(_=>{
            //PhotonNetworkConsole.Instance.LeaveRoom();
            //BoltMatchmaking.CurrentSession.
            BoltLauncher.Shutdown();
            ObjectPool.Instance.Dispose();
            //SceneManager.LoadScene(SceneName.LOBBY);
            SceneDownloadAsset.LoadScene(SceneName.LOBBY);
        }).AddTo(this);
        
        GameCallback.OnPlayerRanksUpdate.Subscribe(BoltEntities =>{
            UpdatePlayerRanking(BoltEntities);
        }).AddTo(this);
        GameCallback.OnplayerRankJsonUpdate.Subscribe(json =>{
            UpdateJsonPlayerRanking(json);
        }).AddTo(this);
    }
    void UpdateJsonPlayerRanking(string jsonData){
        var playerProfilesDic = JsonConvert.DeserializeObject<Dictionary<int,PlayerProfileToken>>(jsonData); 
        playerProfilesDic = playerProfilesDic.OrderBy(p =>p.Value.playerBikeData.playerFinishTime).ToDictionary(k => k.Key,v =>v.Value);
        for (int i = 0; i < playerProfilesDic.Count; i++)
        {
            playerRanks[i].gameObject.SetActive(true);
            playerRanks[i].SetUp(playerProfilesDic.ElementAt(i).Value);
        }
    }
    void UpdatePlayerRanking(List<BoltEntity> entities){
        //print(Depug.Log("-------------->UpdatePlayerRanking "+JSonprofileData,Color.green));
        //GUIDebug.Log("--------------------->UpdatePlayerRanking 1111 "+JSonprofileData.ToString());
        //var playerProfilesDic = JsonConvert.DeserializeObject<Dictionary<int,PlayerProfileToken>>(JSonprofileData); 
        //var playerProfilesDic = Json.Deserialize(JSonprofileData) as Dictionary<int,PlayerProfileToken>;
        var sort = entities.OrderByDescending(e => ((PlayerProfileToken)e.AttachToken).playerBikeData.playerFinishTime).ToList();
        // playerProfilesDic = playerProfilesDic.OrderByDescending(p =>p.Value.playerBikeData.playerFinishTime).ToDictionary(k => k.Key,v =>v.Value);
        // for (int i = 0; i < playerProfilesDic.Count; i++)
        // {
        //     playerRanks[i].gameObject.SetActive(true);
        //     playerRanks[i].SetUp(playerProfilesDic.ElementAt(i).Value);
        //     Debug.Log(playerProfilesDic.ElementAt(i));
        // }
        for (int i = 0; i < sort.Count; i++)
        {
            playerRanks[i].gameObject.SetActive(true);
            playerRanks[i].SetUp(sort[i].AttachToken as PlayerProfileToken);
        }
    }
    // void UpdatePlayerRanking(string JSonprofileData){
        
    //     Debug.Log("UpdatePlayerRAnking " +JSonprofileData);
    //     var playerIndexProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(JSonprofileData);
    //     var findPlayer = playerDataSort.FirstOrDefault(p => p.userId == playerIndexProfileData.userId);
    //     if(string.IsNullOrEmpty(playerIndexProfileData.playerFinishTime))return;
    //     if(findPlayer == null){
    //         Debug.Log("not findplayer "+playerIndexProfileData.nickName);
    //         playerDataSort.Add(playerIndexProfileData);
    //     }else{
    //         Debug.Log("findPlayer "+findPlayer.nickName);
    //         findPlayer = playerIndexProfileData;
    //     }
    //     Debug.Log("playerDataSort count "+playerDataSort.Count);
    //     playerDataSort = playerDataSort.OrderBy(p => p.playerFinishTime).ToList();
    //     for (int i = 0; i < playerDataSort.Count; i++)
    //     {
    //         playerRanks[i].gameObject.SetActive(true);
    //         playerRanks[i].SetUp(playerDataSort[i]);
    //     }
    // }
}
