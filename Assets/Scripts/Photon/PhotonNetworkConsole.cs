using System;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ExitGames.Client.Photon;
using System.IO;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

[RequireComponent(typeof(LobbyController))]
public class PhotonNetworkConsole : MonoBehaviourPunCallbacks
{
    static PhotonNetworkConsole instance;
    public string gameVersion;
    string nickName;
    public static Subject<bool> OnConnectedServer = new Subject<bool>();
    public static Subject<Unit> OnPhotonVoiceCreated = new Subject<Unit>();
    public static Subject<Unit> OnJoinLobby = new Subject<Unit>();
    public bool isConnected = false;
    public LobbyController lobbyController;
    public ChatManager chatManager;
    PhotonVoiceView playerVoiceView;
    Recorder recorder;
    public ClientState state;
    string[] friends_uid;

    public string lastRoomName = string.Empty;
    public string roomPassword = string.Empty;

    public static PhotonNetworkConsole Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("PhotonNetworkConsole", typeof(PhotonNetworkConsole)).GetComponent<PhotonNetworkConsole>();
                DontDestroyOnLoad(instance.gameObject);
                instance.Init();
            }
            return instance;
        }
    }
    
    private void Start()
    {
        ACustom.RegisterTypes();

    }
    private void Init()
    {
        
        //PhotonNetwork.ServerAddress
        lobbyController = instance.gameObject.GetComponent<LobbyController>();
        //chatManager = ChatManager.Instance;
    }
    //Connect with photon authentication
    public void Connect(){
        PhotonNetwork.GameVersion = GameDataManager.Instance.gameConfigData.photonNetworkConfig.gameVersion;
        PhotonNetwork.SendRate = GameDataManager.Instance.gameConfigData.photonNetworkConfig.sendRate;
        PhotonNetwork.SerializationRate = GameDataManager.Instance.gameConfigData.photonNetworkConfig.serializationRate;
        PhotonNetwork.KeepAliveInBackground = GameDataManager.Instance.gameConfigData.photonNetworkConfig.keepAliveInBackground;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    //Connect without photon authentication ( login with inputname)
    public void Connect(string _nickName,string userid = "")
    {
        nickName = _nickName;
        Debug.Log("PhotonNetwork.AuthValues.UserId"+PhotonNetwork.AuthValues.UserId);
        if(string.IsNullOrEmpty(userid) && string.IsNullOrEmpty(PhotonNetwork.AuthValues.UserId)){
            userid = _nickName;
            AuthenticationValues authValue = new AuthenticationValues(userid);
            PhotonNetwork.AuthValues = authValue;
        }
        //Debug.Log("Connect "+_nickName+"user id "+userid);
       
        PhotonNetwork.NickName = _nickName;
        PhotonNetwork.ConnectUsingSettings();
    
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        //Debug.Log("Onjoin Lobby "+PhotonNetwork.NetworkClientState);
        OnJoinLobby.OnNext(Unit.Default);
        //PhotonNetwork.ReconnectAndRejoin();
        PhotonNetwork.NetworkStatisticsReset();
    }
    private void Update() {
        state = PhotonNetwork.NetworkClientState;
    }
    public void UpdateFriendsUID(string[] uidArray){
        friends_uid = uidArray.ToArray();
        FindFriendsInfo();
    }
    public void FindFriendsInfo(){
            if(!PhotonNetwork.IsConnectedAndReady)return;
            if(friends_uid != null && friends_uid.Length > 0){
                PhotonNetwork.FindFriends(friends_uid);
            }
    }
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        base.OnFriendListUpdate(friendList);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps){
        Debug.Log("Player Properties Update !!!!!!");
        if(targetPlayer == PhotonNetwork.LocalPlayer){
            Debug.Log("localplayer update ");

        }
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.LogWarning("on OnConnectedToMaster");
        isConnected = true;
        OnConnectedServer.OnNext(true);
        PhotonNetwork.JoinLobby();
        ChatManager.Instance.Init();
        PlayFabController.Instance.UpdateDataToPhotonPlayer();
        StartCoroutine(TestPlayer());
        // Debug.Log("Currenly room "+PhotonNetwork.CountOfRooms);
       // PhotonNetwork.LeaveRoom(false);
//        PhotonNetwork.FindFriends(friends_uid);
        //StartCoroutine(RejoinLastRoom());
    }
    IEnumerator TestPlayer(){
        yield return new WaitForSeconds(1);
        TestPlayerProfile();
    }
    void TestPlayerProfile(){
        foreach (var item in PhotonNetwork.LocalPlayer.CustomProperties)
        {
            Debug.Log(string.Format("key {0} vale {1}",item.Key,item.Value));

        }
    }
    public override void OnLeftRoom(){
        Debug.Log("PhotonNetworkConsole OnLeftRoom");
    }
    IEnumerator RejoinLastRoom(){
        yield return new WaitForSeconds(3);
        //Debug.Log("rejoinroom "+lastRoomName);
        if(!string.IsNullOrEmpty(lastRoomName)){
           JoinRoom(lastRoomName,null);
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }
    
    public override void OnConnected()
    {
        base.OnConnected();
        Debug.LogWarning("on connected");
        //OnConnectedServer.OnNext(true);
        
        //PhotonNetwork.LeaveRoom(true);
    }
    public void CreateRoom(ExitGames.Client.Photon.Hashtable roomOption){
        lobbyController.CreateRoom(roomOption);
    }
    public void JoinRandomRoom(ExitGames.Client.Photon.Hashtable roomOptions){
        lobbyController.JoinRandomRoom(roomOptions);
    }
    public void JoinRoom(string roomName,ExitGames.Client.Photon.Hashtable roomOptions){
        lobbyController.JoinPrivateRoom(roomName,roomOptions);
    }
    public void RejoinRoom(){
        PhotonNetwork.ReconnectAndRejoin();
    }
    public void LeaveRoom(){
        Debug.Log("LeaveRoom !!!!!");
        PhotonNetwork.LeaveRoom(false);
    }
    #region  VoiceSetting
    public void SetUpPhotonVoiceView(PhotonVoiceView voiceView){
        playerVoiceView = voiceView;
        recorder = PhotonVoiceNetwork.Instance.PrimaryRecorder;
        OnPhotonVoiceCreated.OnNext(Unit.Default);
    }
    public void SetMute(bool active){
        if(recorder == null)return;
        recorder.TransmitEnabled = active;
    }
    public void SetSpeaker(bool active){
        if(recorder == null)return;
        AudioListener.volume = active ? 1 : 0; 
    }
    #endregion

    private void OnApplicationFocus(bool focus)
    {
       // Debug.Log("Focus ? " + focus);
    }
}

static internal class ACustom
{
    internal static void RegisterTypes()
    {
        //  PhotonPeer.RegisterType(Type customType, byte code, SerializeMethod serializeMethod, DeserializeMethod deserializeMethod)
        //PhotonPeer.RegisterType(typeof(Vector2), (byte)'W', SerializeVector2, DeserializeVector2);
        PhotonPeer.RegisterType(typeof(CustomSerialization), (byte)'A', CustomSerialization.Serialize, CustomSerialization.Deserialize);
        PhotonPeer.RegisterType(typeof(CT_PlayerDeckUpdate), (byte)'C',CT_PlayerDeckUpdate.Serialize, CT_PlayerDeckUpdate.Deserialize);
        PhotonPeer.RegisterType(typeof(CT_RequestDeckUpdate),(byte)'D',CT_RequestDeckUpdate.Serialize,CT_RequestDeckUpdate.Deserialize);
        PhotonPeer.RegisterType(typeof(CT_PlayerSummary),(byte)'E',CT_PlayerSummary.Serialize,CT_PlayerSummary.Deserialize);
        PhotonPeer.RegisterType(typeof(CT_UserStaticData),(byte)'F',CT_UserStaticData.Serialize,CT_UserStaticData.Deserialize);
        PhotonPeer.RegisterType(typeof(CT_AddFriendRequest),(byte)'G',CT_UserStaticData.Serialize,CT_UserStaticData.Deserialize);
        
    }
}
[System.Serializable]
public class CustomSerialization
{
    public byte Id { get; set; }
    public static byte[] Serialize(object customType)
    {
        var c = (CustomSerialization)customType;
        return new byte[] { c.Id};
    }
    public static object Deserialize(byte[] bytes)
    {
        var result = new CustomSerialization { Id = bytes[0] };
        return result;
    }
}

[System.Serializable]
public class CT_PlayerDeckUpdate

{
    public int actorNr;
    public int cards_length;
    public int ranks_length;
    public int higherCards_length;
    public byte[] cards;
    public byte[] ranks;
    public byte[] higherCards;
    public byte[] swapCard;
    public bool isRule;
    public static byte[] Serialize(object o)
    {
        CT_PlayerDeckUpdate customType = o as CT_PlayerDeckUpdate;
        if (customType == null) { return null; }
        using (var s = new MemoryStream())
        {
            using (var bw = new BinaryWriter(s))
            {
                bw.Write(customType.actorNr);
                bw.Write(customType.cards_length);
                bw.Write(customType.ranks_length);
                bw.Write(customType.higherCards_length);
                bw.Write(customType.cards);
                bw.Write(customType.ranks);
                bw.Write(customType.higherCards);
                bw.Write(customType.swapCard);
                bw.Write(customType.isRule);
                return s.ToArray();
            }
        }
    }
    public static object Deserialize(byte[] bytes)
    {
        CT_PlayerDeckUpdate customObject = new CT_PlayerDeckUpdate();
        using (var s = new MemoryStream(bytes))
        {
            using (var br = new BinaryReader(s))
            {
                customObject.actorNr = br.ReadInt32();
                customObject.cards_length = br.ReadInt32();
                customObject.ranks_length = br.ReadInt32();
                customObject.higherCards_length = br.ReadInt32();
                customObject.cards = br.ReadBytes(customObject.cards_length);
                customObject.ranks = br.ReadBytes(customObject.ranks_length);
                customObject.higherCards = br.ReadBytes(customObject.higherCards_length);
                customObject.swapCard = br.ReadBytes(2);
                customObject.isRule = br.ReadBoolean();
            }
        }
        return customObject;
    }
}

[System.Serializable]
public class CT_RequestDeckUpdate{
    public byte[] cards;
    public byte[] swapCard; //0 from 1 to
    public static byte[] Serialize(object o){
        CT_RequestDeckUpdate customType = o as CT_RequestDeckUpdate;
        using (var s = new MemoryStream())
        {
            using (var bw = new BinaryWriter(s))
            {
                bw.Write(customType.cards);
                bw.Write(customType.swapCard);
                return s.ToArray();
            }
        }
    }
    public static object Deserialize(byte[] bytes){
CT_RequestDeckUpdate customObject = new CT_RequestDeckUpdate();
        using (var s = new MemoryStream(bytes))
        {
            using (var br = new BinaryReader(s))
            {
                customObject.cards = br.ReadBytes(8);
                customObject.swapCard = br.ReadBytes(2);
            }
        }
        return customObject;
    }
}
[System.Serializable]
    public class CT_PlayerSummary
    {
        public string actor;
        public string imageURL;
        public bool isRule;
        public bool winAllPlayer;
        public bool loseAllPlayer;
        public int pointTotal;
        public long currencyAmount;
        public byte gameCurrency;
        public static byte[] Serialize(object o)
        {
            CT_PlayerSummary customType = o as CT_PlayerSummary;
            if (customType == null) { return null; }
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    bw.Write(customType.actor);
                    bw.Write(customType.imageURL);
                    bw.Write(customType.isRule);
                    bw.Write(customType.winAllPlayer);
                    bw.Write(customType.loseAllPlayer);
                    bw.Write(customType.pointTotal);
                    bw.Write(customType.currencyAmount);
                    bw.Write(customType.gameCurrency);
                    return s.ToArray();
                }
            }
        }
        public static object Deserialize(byte[] bytes)
        {
            CT_PlayerSummary customObject = new CT_PlayerSummary();
            using (var s = new MemoryStream(bytes))
            {
                using (var br = new BinaryReader(s))
                {
                    customObject.actor = br.ReadString();
                    customObject.imageURL = br.ReadString();
                    customObject.isRule = br.ReadBoolean();
                    customObject.winAllPlayer = br.ReadBoolean();
                    customObject.loseAllPlayer = br.ReadBoolean();
                    customObject.pointTotal = br.ReadInt32();
                    customObject.currencyAmount = br.ReadInt64();
                    customObject.gameCurrency = br.ReadByte();
                }
            }
            return customObject;
        }
    } 
    [System.Serializable]
    public class CT_UserStaticData{
        public string displayName;
        public string imageurl;
        public string uid;
        public int seatIndex;
        public static byte[] Serialize(object o)
        {
            CT_UserStaticData customType = o as CT_UserStaticData;
            if (customType == null) { return null; }
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    //bw.Write(customType._id);
                    bw.Write(customType.displayName);
                    bw.Write(customType.imageurl);
                    bw.Write(customType.uid);
                    bw.Write(customType.seatIndex);
                    return s.ToArray();
                }
            }
        }
        public static object Deserialize(byte[] bytes)
        {
            CT_UserStaticData customObject = new CT_UserStaticData();
            using (var s = new MemoryStream(bytes))
            {
                using (var br = new BinaryReader(s))
                {
                    //customObject._id = br.ReadString();
                    customObject.displayName = br.ReadString();
                    customObject.imageurl = br.ReadString();
                    customObject.uid = br.ReadString();
                    customObject.seatIndex = br.ReadInt32();
                }
            }
            return customObject;
        }
    }
     [System.Serializable]
    public class CT_AddFriendRequest{
        public string uidTo;
        public string uidFrom;
        public string playerRequest_name;
        public string playerRequest_imgURL;
        public static byte[] Serialize(object o)
        {
            CT_AddFriendRequest addFriendRequest = o as CT_AddFriendRequest;
            if (addFriendRequest == null) { return null; }
            using (var s = new MemoryStream())
            {
                using (var bw = new BinaryWriter(s))
                {
                    //bw.Write(customType._id);
                    bw.Write(addFriendRequest.uidTo);
                    bw.Write(addFriendRequest.uidFrom);
                    bw.Write(addFriendRequest.playerRequest_name);
                    bw.Write(addFriendRequest.playerRequest_imgURL);
                    return s.ToArray();
                }
            }
        }
        public static object Deserialize(byte[] bytes)
        {
            CT_AddFriendRequest customObject = new CT_AddFriendRequest();
            using (var s = new MemoryStream(bytes))
            {
                using (var br = new BinaryReader(s))
                {
                    //customObject._id = br.ReadString();
                    customObject.uidTo = br.ReadString();
                    customObject.uidFrom = br.ReadString();
                    customObject.playerRequest_name = br.ReadString();
                    customObject.playerRequest_imgURL = br.ReadString();
                }
            }
            return customObject;
        }
    }