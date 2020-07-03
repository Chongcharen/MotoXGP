using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using DG.Tweening;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
public enum ChannelType{
    World,Room,Whisper
}
public class ChatManager : MonoBehaviourPunCallbacks, IChatClientListener
{
    public static Subject<ChatMessageData> OnGetChatMessage = new Subject<ChatMessageData>();
    static ChatManager instance;
    public static ChatManager Instance{
        get{
            if(instance == null){
                var findInstance = GameObject.Find("ChatManager");
                if(findInstance != null)
                    Destroy(findInstance);
                var go = new GameObject("ChatManager",typeof(ChatManager));
                instance = go.GetComponent<ChatManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    public ChatClient chatClient;
    protected internal AppSettings chatAppSettings;
    //public List<string> chatChannelList = new List<string>();
    public ReactiveCollection<string> chatChannelList = new ReactiveCollection<string>();
    
    public ChannelType currentChannelType;
    Photon.Chat.AuthenticationValues authenticationValues;
    public string currentChannel;
    public string currentRoomChannel;
    public string playerName;
    public void Init(){
        #if PHOTON_UNITY_NETWORKING
                this.chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        #endif
        currentChannel = "World";
        AddChannelList("World");
        currentChannelType = ChannelType.World;
        Connect();
    }
    // public override void OnConnectedToMaster(){
    //     ConnectChatServer();
    // }
    public void Connect(){
        this.chatClient = new ChatClient(this);
        #if !UNITY_WEBGL
                this.chatClient.UseBackgroundWorkerForSending = true;
        #endif
        authenticationValues = new Photon.Chat.AuthenticationValues(PhotonNetwork.LocalPlayer.UserId);
        playerName = PhotonNetwork.NickName;
        chatClient.Connect(this.chatAppSettings.AppIdChat, "1.0", authenticationValues);
    }
    void IChatClientListener.OnConnected()
    {
        chatClient.Subscribe(new string[] {"World"});
    }
    public void SendChatMessage(string inputMessage)
    {
        if(string.IsNullOrEmpty(inputMessage))return;
        chatClient.PublishMessage(currentChannel, inputMessage);
    }
    public void AddChannelList(string channelName){
        if(!chatChannelList.Contains(channelName))
            chatChannelList.Add(channelName);
    }
    public void RemoveChatChannelList(string channelName){
        if(chatChannelList.Contains(channelName))
            chatChannelList.Remove(channelName);
    }
    public void SubScribeChannel(string _channel){
        AddChannelList(_channel);
        chatClient.Subscribe(new string[]{_channel});
    }
    public void UnsubscribeChannel(string _channel){
        if(string.IsNullOrEmpty(_channel))return;
        RemoveChatChannelList(_channel);
        chatClient.Unsubscribe(new string[]{_channel});
    }
    
    public void SwithChannelType(ChannelType type){
         if(currentChannelType == type)return;
        currentChannelType = type;
        switch(type){
            case ChannelType.World :
                currentChannel = "World";
                break;
            case ChannelType.Room :
                currentChannel = currentRoomChannel;
                break;
        }
    }
    
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        var messageData = new ChatMessageData{
            channelType = (channelName == currentRoomChannel && channelName != "World") ? ChannelType.Room : ChannelType.World,
            channelName = channelName,
            senders = senders,
            messages = messages
        };
        OnGetChatMessage.OnNext(messageData);
    }
    public void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }
    }
    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnDisconnected()
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
    }

    

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {

    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }
    void OnDestroy()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }
    // public void OnEnable()
    // {
    //     PhotonNetwork.AddCallbackTarget(this);
    // }

    // public void OnDisable()
    // {
    //     if (this.chatClient != null)
    //     {
    //         this.chatClient.Disconnect();
    //     }
    //     PhotonNetwork.RemoveCallbackTarget(this);
    // }
}


