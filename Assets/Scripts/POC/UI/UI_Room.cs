
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using ExitGames.Client.Photon;
using UniRx;
using System.Linq;
using ExitGames.Client.Photon;

// ตอนนี้ Userid ยังใช้ไมไ่ด้เพราะ ยังไมไ่ด้มีการเซ็ต user เลยต้องใช้ nickname ไปก่อน
public class UI_Room : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_leave,b_playGame;
    [SerializeField]GameObject playerinroomPrefab;
    [SerializeField]Transform content;
    [SerializeField]Color[] playerColor;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UI Room start");
        b_leave.OnClickAsObservable().Subscribe(_=>{
            PhotonNetwork.LeaveRoom();
            
        });
        b_playGame.OnClickAsObservable().Subscribe(_=>{
            PrepareTostartGame();
            // Start To gameplay Scene
        });
        root.ObserveEveryValueChanged(r =>r.gameObject.activeSelf).Subscribe(active =>{
            Debug.Log("UI_Room Active "+active);
        });
    }
    void PrepareTostartGame(){
        Hashtable data = new Hashtable(); // data Hash in customproperty
        Hashtable playerDatas = new Hashtable(); 
        Hashtable playerIndex = new Hashtable();
        Debug.Log("data "+data.Count);
        Debug.Log("PlayerData "+playerDatas.Count);
        Debug.Log("playerIndex "+playerIndex);
        var index = 0;
        //Set UserID เอานะ
        if(data.ContainsKey(RoomPropertyKeys.PLAYER_INDEX)){
            data[RoomPropertyKeys.PLAYER_INDEX] = null;
        }
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(string.Format("player KEY : {0} , VALUE : {1}",player.Key,player.Value));

            // Player p = player.Value;
            // ExitGames.Client.Photon.Hashtable playerProperties = p.CustomProperties;

            //SetPlayer Start Position X on Game
            Debug.Log("PlayerData "+playerDatas);
            Debug.Log("playerIndex "+playerIndex);
            Debug.Log("userid "+player.Value);

            
            if(!playerDatas.ContainsKey(player.Value.NickName)){
                //playerDatas.Add(player.Value.NickName,18); //เอาออกเพราะตอนเข้าเกม ถึงแม้ตัวรถจะวิ่งไปตำแหน่งอื่น ต้ำแหน่งที่ add มาแต่แรกก็ยังรันอยู่
            }
            if(!playerIndex.ContainsKey(player.Value.NickName)){
                playerIndex.Add(player.Value.NickName,index);
            }
            //playerDatas[player.Value.NickName] =24;
            playerIndex[player.Value.NickName] =index;
           // Debug.Log("playerIndex "+playerIndex[player.Value.UserId]);
            index++;
            // ExitGames.Client.Photon.Hashtable playerData = new ExitGames.Client.Photon.Hashtable();
            // playerData.Add("Distance",1000);
            // playerDatas.Add(player.Value.UserId, playerData); 
        }
        data.Add(RoomPropertyKeys.PLAYER_DATA,playerDatas);
        data.Add(RoomPropertyKeys.PLAYER_INDEX,playerIndex);
        data.Add(RoomPropertyKeys.GAME_START,true);
        PhotonNetwork.CurrentRoom.SetCustomProperties(data);


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
        if(propertiesThatChanged.ContainsKey(RoomPropertyKeys.GAME_START)){
            if((bool)propertiesThatChanged[RoomPropertyKeys.GAME_START])
                PhotonNetwork.LoadLevel(SceneName.GAMEPLAY);
        }
         
    }
    public override void OnLeftRoom(){
        root.gameObject.SetActive(false);
        PageManager.Instance.OpenLobby();
    }
    //other joined room
    public override void OnPlayerEnteredRoom(Player newPlayer){
        Debug.Log("playerenterroom ++++"+newPlayer.NickName);
        Debug.Log("PlayerCount ++++"+PhotonNetwork.CurrentRoom.PlayerCount);
        //PhotonNetwork.CurrentRoom.AddPlayer(newPlayer);
        UpdatePlayerInroom();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer){
        UpdatePlayerInroom();
    }
    // local joined Room;
    public override void OnJoinedRoom(){
        //PhotonNetwork.CurrentRoom.AddPlayer(PhotonNetwork.LocalPlayer);
        UpdatePlayerInroom();
    }
    public void UpdatePlayerInroom(){
        ClearData();
        var index = 0;
        var playerIndex =  PhotonNetwork.CurrentRoom.Players.OrderBy(key => key.Key).ToDictionary(k =>k.Key , v =>v.Value);

        foreach (var item in playerIndex)
        {
            var go = Instantiate(playerinroomPrefab,Vector3.zero,Quaternion.identity,content.transform);
            go.GetComponent<PlayerInRoom_Prefab>().SetData(item.Value.NickName,playerColor[index]);
            go.gameObject.SetActive(true);
            index ++;
        }
        b_playGame.gameObject.SetActive(PhotonNetwork.CurrentRoom.masterClientId == PhotonNetwork.LocalPlayer.ActorNumber);
    }
    void ClearData(){
        Debug.Log("clear data ");
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
    // void Update(){
    //     Debug.Log(PhotonNetwork.LevelLoadingProgress);
    // }
}
