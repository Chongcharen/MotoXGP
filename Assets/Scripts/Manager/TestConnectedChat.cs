using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class TestConnectedChat : MonoBehaviour, IChatClientListener
{
    string UserName;
    protected internal AppSettings chatAppSettings;
    public ChatClient chatClient;
    void Start()
    {
        #if PHOTON_UNITY_NETWORKING
                this.chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        #endif
        Connect();
        // b_close.OnClickAsObservable().Subscribe(_=>{
        //     chatObject.gameObject.SetActive(false);
        // });
        // b_sendMessage.OnClickAsObservable().Subscribe(_=>{
        //     SendMessageFromInput();
        // });
    }
    void IChatClientListener.DebugReturn(DebugLevel level, string message)
    {
       
    }

    void IChatClientListener.OnChatStateChange(ChatState state)
    {
       
    }
    public void Connect()
    {
        this.chatClient = new ChatClient(this);
        #if !UNITY_WEBGL
                this.chatClient.UseBackgroundWorkerForSending = true;
        #endif
        Debug.Log("this.chatAppSettings.AppIdChat "+ this.chatAppSettings.AppIdChat);
        UserName = PhotonNetwork.NickName;
        Debug.Log("UserName "+UserName);
        this.chatClient.Connect(this.chatAppSettings.AppIdChat, "1.0", new Photon.Chat.AuthenticationValues(UserName));

    }
    public void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }
    }

    void IChatClientListener.OnConnected()
    {
        Debug.Log("4488");
        //throw new System.NotImplementedException();
    }

    void IChatClientListener.OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    

    void IChatClientListener.OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
     void OnDestroy()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }
}
