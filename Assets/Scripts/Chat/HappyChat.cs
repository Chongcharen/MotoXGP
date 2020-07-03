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
public class HappyChat : MonoBehaviour, IChatClientListener
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
        this.chatClient.Connect(this.chatAppSettings.AppIdChat, "1.0", new Photon.Chat.AuthenticationValues(UserName));

    }
    void IChatClientListener.OnConnected()
    {
        Debug.Log("4488");
        //throw new System.NotImplementedException();
    }
    public void SendMessageFromInput(){
        // SendChatMessage(InputFieldChat.text);
        // InputFieldChat.text = "";
    }
    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
           SendMessageFromInput();
        }
    }
    private void SendChatMessage(string inputMessage)
    {
        if(string.IsNullOrEmpty(inputMessage))return;
        chatClient.PublishMessage("channelA", inputMessage);
    }
    public void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }

        // check if we are missing context, which means we got kicked out to get back to the Photon Demo hub.
       /* if (this.StateText == null)
        {
            Destroy(this.gameObject);
            return;
        }

        this.StateText.gameObject.SetActive(this.ShowState); // this could be handled more elegantly, but for the demo it's ok.*/
    }

    void IChatClientListener.OnDisconnected()
    {
       
    }

    void IChatClientListener.OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        string username ="";
        for (int i = 0; i < senders.Length; i++)
        {
            username = string.Format("<color=#35ADF8>{0}</color> : ", senders[i]);
            msgs = string.Format("{0}{1}\n", username, messages[i]);
        }
        //chat_txt.text += msgs;
    }

    void IChatClientListener.OnPrivateMessage(string sender, object message, string channelName)
    {
       
    }

    void IChatClientListener.OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
      
    }

    void IChatClientListener.OnSubscribed(string[] channels, bool[] results)
    {
       
    }

    void IChatClientListener.OnUnsubscribed(string[] channels)
    {
      
    }

    void IChatClientListener.OnUserSubscribed(string channel, string user)
    {
       
    }

    void IChatClientListener.OnUserUnsubscribed(string channel, string user)
    {
      
    }
    public void OpenChat()
    {
       // chatObject.SetActive(!chatObject.activeSelf);
    }
    //Chat input
  
    void OnDestroy()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }
    // void OnDisable()
    // {
    //     this.chatClient.Disconnect();
    // }

    
}
