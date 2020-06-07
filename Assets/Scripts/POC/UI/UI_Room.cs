using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using ExitGames.Client.Photon;
using UniRx;
public class UI_Room : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_leave,b_playGame;
    [SerializeField]GameObject playerinroomPrefab;
    [SerializeField]Transform content;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UI Room start");
        b_leave.OnClickAsObservable().Subscribe(_=>{
            PhotonNetwork.LeaveRoom();
            
        });
        b_playGame.OnClickAsObservable().Subscribe(_=>{
            PrepareTostartGame();
            PhotonNetwork.LoadLevel("NetworkGame");
        });
        root.ObserveEveryValueChanged(r =>r.gameObject.activeSelf).Subscribe(active =>{
            Debug.Log("UI_Room Active "+active);
        });
    }
    void PrepareTostartGame(){
        
    }
    public override void OnLeftRoom(){
        root.gameObject.SetActive(false);
        PageManager.Instance.OpenLobby();
    }
    //other joined room
    public override void OnPlayerEnteredRoom(Player newPlayer){
        Debug.Log("playerenterroom ++++"+newPlayer.NickName);
        PhotonNetwork.CurrentRoom.AddPlayer(newPlayer);
        UpdatePlayerInroom();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer){
        UpdatePlayerInroom();
    }
    // local joined Room;
    public override void OnJoinedRoom(){
        UpdatePlayerInroom();
    }
    public void UpdatePlayerInroom(){
        ClearData();
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log("add player "+item.Value.NickName);
            var go = Instantiate(playerinroomPrefab,Vector3.zero,Quaternion.identity,content.transform);
            go.GetComponent<PlayerInRoom_Prefab>().SetData(item.Value.NickName);
            go.gameObject.SetActive(true);
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
