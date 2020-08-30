using System.Collections.Generic;
using System.Linq.Expressions;

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using ExitGames.Client.Photon;
using UniRx;
using System.Linq;
using ExitGames.Client.Photon;
using PlayFab.ClientModels;
using Newtonsoft.Json;

// ตอนนี้ Userid ยังใช้ไมไ่ด้เพราะ ยังไมไ่ด้มีการเซ็ต user เลยต้องใช้ nickname ไปก่อน
public class UI_Room : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_leave,b_playGame;
    [SerializeField]Transform contentTransform;
    [SerializeField]PlayerInRoom_Prefab[] playersData;
    [SerializeField]Color[] playerColor;
    List<PlayerInRoom_Prefab> players = new List<PlayerInRoom_Prefab>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UI Room start");
        b_leave.OnClickAsObservable().Subscribe(_=>{
            //PhotonNetwork.LeaveRoom(); 
            // if(BoltNetwork.IsServer){
            //     BoltLobbyNetwork.Instance.Shutdown(ConnectionType.Disconnect);
            // }else{
                
            // }
            BoltLobbyNetwork.Instance.Shutdown(ConnectionType.Disconnect);
        }).AddTo(this);
        b_playGame.OnClickAsObservable().Subscribe(_=>{
            PrepareTostartGame();
            // Start To gameplay Scene
        }).AddTo(this);
        root.ObserveEveryValueChanged(r =>r.gameObject.activeSelf).Subscribe(active =>{
            Debug.Log("UI_Room Active "+active);
            b_playGame.gameObject.SetActive(BoltNetwork.IsServer);
        }).AddTo(this);
        PlayerInRoom_Prefab.OnDestroyed.Subscribe(player =>{
            RemovePlayer(player);
        }).AddTo(this);
    }
    /*
        sample Node =>   RoomHashtable
                            playerHashtable
                            playerindexHashtable
    */
    public void AddPlayer(PlayerInRoom_Prefab player){
        if(player == null)return;
        if(players.Contains(player))return;
        
        players.Add(player);
        player.transform.SetParent(contentTransform,false);
        player.transform.SetAsLastSibling();
        player.SetColor(playerColor[players.Count]);
    }
    public void RemovePlayer(PlayerInRoom_Prefab _player){
        if(players.Contains(_player))
            players.Remove(_player);
        
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetColor(playerColor[i]);
        }
    }
    public void UpdatePlayerInroom(){
        ClearData();
        Debug.Log("Updatepayerinroom ");
        //get playerIndex data 
        Hashtable roomPropertyData = new Hashtable();
        Hashtable playerIndexHashTable = PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        if(playerIndexHashTable ==  null)
            playerIndexHashTable = new Hashtable();
            
        var index = 0;
        var playerIndex =  PhotonNetwork.CurrentRoom.Players.OrderBy(key => key.Key).ToDictionary(k =>k.Key , v =>v.Value);
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(string.Format("Player {0} vale {1} id {2}",item.Key,item.Value,item.Value.UserId));
        }
        foreach (var player in playerIndex)
        {
            // Debug.Log("Index "+index);
            // Debug.Log("player value "+player.Value);
            // Debug.Log("playersData data "+playersData);
            // Debug.Log("playersData data count"+playersData.Length);
            playersData[index].SetData(player.Value,playerColor[index]);
            playersData[index].gameObject.SetActive(true);
            PlayerIndexProfileData playerIndexProfileData;
            if(!playerIndexHashTable.ContainsKey(player.Value.UserId)){
                var playerProfileJson = player.Value.CustomProperties[PlayerPropertiesKey.PLAYFAB_PROFILE] as string;
                Debug.Log("playerProfileJson "+playerProfileJson);
                Debug.Assert(!string.IsNullOrEmpty(playerProfileJson));
                playerIndexProfileData = new PlayerIndexProfileData{
                    index = index,
                    nickName = player.Value.NickName,
                    profileModel = playerProfileJson,
                    userId = player.Value.UserId,
                    nation = "thai",
                    colorCode = ColorUtility.ToHtmlStringRGBA(playerColor[index])
                };
                playerIndexHashTable.Add(player.Value.UserId,playerProfileJson);
            }else{
                var profileJson =  playerIndexHashTable[player.Value.UserId] as string;
                playerIndexProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(profileJson);
            }
            playerIndexProfileData.index = index;
            playerIndexProfileData.colorCode = ColorUtility.ToHtmlStringRGBA(playerColor[index]);
            playerIndexHashTable[player.Value.UserId] = JsonConvert.SerializeObject(playerIndexProfileData);
            GameUtil.SetHashTableProperty(roomPropertyData,RoomPropertyKeys.PLAYER_INDEX,playerIndexHashTable);
            index ++;
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomPropertyData);
        b_playGame.gameObject.SetActive(PhotonNetwork.CurrentRoom.masterClientId == PhotonNetwork.LocalPlayer.ActorNumber);
    }
    void PrepareTostartGame(){
        // Hashtable data = new Hashtable(); // data Hash in customproperty (RoomHashtableData)
        // Hashtable playerDatas = new Hashtable(); 
        // Hashtable playerIndex = new Hashtable();
        // //Set UserID เอานะ
        // if(data.ContainsKey(RoomPropertyKeys.PLAYER_INDEX)){
        //     data[RoomPropertyKeys.PLAYER_INDEX] = null;
        // }
        // data.Add(RoomPropertyKeys.PLAYER_DATA,playerDatas);
        // //data.Add(RoomPropertyKeys.GAME_START,true);
        // PhotonNetwork.CurrentRoom.SetCustomProperties(data);
        // PhotonNetwork.CurrentRoom.IsVisible = false;

        //PhotonNetwork.LoadLevel(SceneName.GAMEPLAY);
        BoltNetwork.LoadScene(SceneName.GAMEPLAY);
        //PhotonNetwork.CurrentRoom.SetCustomProperties();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged){
        foreach (var item in propertiesThatChanged)
        {
            Debug.Log(string.Format("Change Key : {0} , Value {1}",item.Key,item.Value));
            if(item.Key.ToString() == RoomPropertyKeys.PLAYER_DATA){
                ExitGames.Client.Photon.Hashtable playerDataHash = item.Value as ExitGames.Client.Photon.Hashtable;
                Debug.Log("playerdata hash count "+playerDataHash.Count);
                foreach (var playerData in playerDataHash)
                {
                    Debug.Log(string.Format("Playerdata key : {0} , value : {1}",playerData.Key,playerData.Value));
                }
            }
            if(item.Key.ToString() == RoomPropertyKeys.PLAYER_INDEX){
                ExitGames.Client.Photon.Hashtable playerIndex_hash = item.Value as ExitGames.Client.Photon.Hashtable;
                foreach (var index in playerIndex_hash)
                {
                     Debug.Log(string.Format("playerIndex_hash Key : {0} , Value {1}",item.Key,item.Value));
                }
            }
        }
        var playerindexHashtable = PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        if(playerindexHashtable != null){
             Debug.Log("playerindexHashtable total "+playerindexHashtable);
        }

        if(propertiesThatChanged.ContainsKey(RoomPropertyKeys.GAME_START)){
            if((bool)propertiesThatChanged[RoomPropertyKeys.GAME_START])
                PhotonNetwork.LoadLevel(SceneName.GAMEPLAY);
        }
         
    }
    // public override void OnLeftRoom(){
    //     if(root != null)
    //         root.gameObject.SetActive(false);
    //     PageManager.Instance.OpenLobby();
    // }
    //other joined room
    // public override void OnPlayerEnteredRoom(Player newPlayer){
    //     Debug.Log("playerenterroom ++++"+newPlayer.UserId);
    //     Debug.Log("PlayerCount ++++"+PhotonNetwork.CurrentRoom.PlayerCount);
    //     PhotonNetwork.CurrentRoom.AddPlayer(newPlayer);
    //     UpdatePlayerInroom();
    // }
    // public override void OnPlayerLeftRoom(Player otherPlayer){
    //     UpdatePlayerInroom();
    // }
    // // local joined Room;
    // public override void OnJoinedRoom(){
    //     PhotonNetwork.CurrentRoom.AddPlayer(PhotonNetwork.LocalPlayer);
    //     UpdatePlayerInroom();
    // }
    
    void ClearData(){
        Debug.Log("clear data ");
        for (int i = 0; i < playersData.Length; i++)
        {
            //Destroy(content.GetChild(i).gameObject);
            playersData[i].gameObject.SetActive(false);
        }
    }
    // void Update(){
    //     Debug.Log(PhotonNetwork.LevelLoadingProgress);
    // }
}
public class PlayerIndexRank{
    public int index;
    public string nickName;
}
