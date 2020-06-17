using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using ExitGames.Client.Photon;
using UniRx;
public class LobbyController : MonoBehaviourPunCallbacks
{
    ExitGames.Client.Photon.Hashtable customProperty = new ExitGames.Client.Photon.Hashtable();
    List<Photon.Realtime.RoomInfo> roomList = new List<Photon.Realtime.RoomInfo>();

    public static Subject<string> OnFindRoom = new Subject<string>();
    public int maxPlayer = 6;
    int multiplayerSceneIndex =1;
    Photon.Realtime.RoomInfo roomTarget;
    public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
    {
        PhotonNetwork.AutomaticallySyncScene = true; //Makes it so whatever scene the master client has loaded is the scene all other clients will load
        //quickStartButton.interactable = true;
    }

    public void JoinRandomRoom(ExitGames.Client.Photon.Hashtable roomOptions) //Paired to the Quick Start button
    {
        Debug.Log("JoinrandomRoom");
        customProperty = roomOptions;
        PhotonNetwork.JoinRandomRoom(customProperty,0);
    }
    
    public void JoinPrivateRoom(string roomName , ExitGames.Client.Photon.Hashtable roomOptions){

    }
    public void EnterPassword(string password)
    {
        Debug.Log("enter password  "+password);
        if (roomTarget == null) return;
    }
    public override void OnJoinedRoom() //Callback function for when we successfully create or join a room.
    {
        
        PhotonNetworkConsole.Instance.lastRoomName = PhotonNetwork.CurrentRoom.Name;
        //RoomManager.Instance.lastRoomName = RoomManager.Instance.currentRoom.Name;
        //StartGame();
        
    }
    public override void OnJoinRoomFailed(short returnCode, string message){
        Debug.LogError("Joinroomfailed "+message);
    }
    private void StartGame() //Function for loading into the multiplayer scene.
    {
         //UI_HOME_Manager.Instance.CloseHomeScene();
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsMasterClient)
        {
           // PhotonNetwork.LoadLevel(multiplayerSceneIndex);
            //because of AutoSyncScene all players who join the room will also be loaded into the multiplayer scene.
           // PageStack.Instance.CurrentSceneSwitch(SceneName.GamePlay);
        }
    }
    // public override void OnJoinRandomFailed(short returnCode, string message) //Callback function for if we fail to join a rooom
    // {
    //     Debug.Log("OnJoinRandomFailed "+returnCode +"message "+message );
    //     CreateRoom(customProperty);
    // }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(string.Format("OnJoinRandomFailed++ code {0} message {1}", returnCode, message));
        JoinRandom();
    }
    public void JoinRandom()
    {
        var roomName = "name 888";
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOption,TypedLobby.Default);
    }

    public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> _roomList)
    {
        base.OnRoomListUpdate(_roomList);
        roomList = _roomList.ToList();
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        // Debug.Log("onroomPropertiesupdate ");
        // foreach (var item in propertiesThatChanged)
        // {
        //     Debug.Log("item "+item.Key + "value "+item.Value);
        // }
    }
    public void CreateRoom(ExitGames.Client.Photon.Hashtable _customProperty = null) //trying to create our own room
    {
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayer };
        roomOps.PlayerTtl = -1;
        roomOps.EmptyRoomTtl = 6000000;
        //roomOps.Plugins = new string[] {"MyFirstPlugin"}; // call plugin on create game
        // _customProperty.Add(RoomOptionKey.HostID,UserManager.Instance.userData.Value.uid);
        // if(_customProperty != null){
        //     string[] Property;
        //     roomOps.CustomRoomProperties = _customProperty;
        //     roomOps.IsVisible = true;
        //     if(_customProperty.ContainsKey(RoomOptionKey.Password)){
        //         Property = new string[] {RoomOptionKey.Password};
        //     }else{
        //         Property = new string[] {RoomOptionKey.CurrencyAmount,RoomOptionKey.GameCurrency};
        //     }
        //     if(_customProperty.ContainsKey(RoomOptionKey.RoomTime)){
        //         roomOps.PlayerTtl = -1;//(int)_customProperty[RoomOptionKey.RoomTime];
        //         roomOps.EmptyRoomTtl = 6000000 ;
                
        //     }
        //     roomOps.CustomRoomPropertiesForLobby = Property;
        // }
        roomOps.PublishUserId = true;
        Debug.Log("!!!!!!!!!!!!!!!!!CreateRoom");
        PhotonNetwork.CreateRoom( "aaaa", roomOps,TypedLobby.Default); //attempting to create a new room
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message) //callback function for if we fail to create a room. Most likely fail because room name was taken.
    {
        Debug.Log("Failed to create room... trying again "+returnCode +"message "+message );
        CreateRoom(); //Retrying to create a new room with a different name.
    }

    public void QuickCancel() //Paired to the cancel button. Used to stop looking for a room to join.
    {
       // quickCancelButton.SetActive(false);
       // quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    public void Reconnect()
    {
        PhotonNetwork.ReconnectAndRejoin();
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    
}
