using System.Linq.Expressions;
using System.Xml.Linq;
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
using DG.Tweening;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using Newtonsoft.Json;
public class MotoChat : MonoBehaviour 
{
    public ReactiveProperty<bool> isFold = new ReactiveProperty<bool>(true);

    //Fordebug only!!
    [SerializeField]GameObject chatTextPrefab;
    [SerializeField]RectTransform topBarRectTransform,chatBarRectTransform;
    [Header("Input")]
    [SerializeField]TMP_InputField input_txt;
    [SerializeField]Button b_sendMesage,b_fold;

    [Header("Unfold")]
    [SerializeField]float chatbarMaximumHeight = 880;
    [SerializeField]float topbarUnfoldPosition = 1000;

    [Header("Fold")]
    [SerializeField]float chatbarMinimumHeight = 100;
    [SerializeField]float topbarFoldPosition = 220;

    [Header("Content")]
    [SerializeField]Transform chatRoomContent;
    [SerializeField]Transform chatWorldContent;
    [SerializeField]int messageLimit = 30;
    [SerializeField]int delemessageCount = 5;
    [Header("ChatBoxObject")]
    [SerializeField]GameObject worldChatBox,roomChatBox;
    [Header("PlayerNameInbox")]
    [SerializeField]Color playerColor = Color.yellow;
    Dictionary<string,Transform> channelContentTransform = new Dictionary<string, Transform>();
    Dictionary<string,GameObject> channelChatBox = new Dictionary<string, GameObject>();


    //Switch Channel 
    ToggleGroup toggleGroup;
    public Toggle worldToggle,roomToggle,wisperToggle;


    //ChatClient and Channel;    
    List<string> chatChannelList = new List<string>();
    string currentChannel;
    string currentRoomChannel;
    string playerName;
    void Start(){
        channelContentTransform.Add("World",chatWorldContent);
        channelChatBox.Add("World",worldChatBox);
        isFold.ObserveEveryValueChanged(f =>f.Value).Subscribe(fold =>{
            Fold(fold);
        });
        b_sendMesage.OnClickAsObservable().Subscribe(_=>{
            SendMessageFromInput();
        });
        b_fold.OnClickAsObservable().Subscribe(_=>{
            isFold.Value = !isFold.Value;
        });
        ChatManager.Instance.currentChannel.ObserveEveryValueChanged(c => c).Subscribe(_=>{
        }).AddTo(this);
        ChatManager.Instance.currentRoomChannel.ObserveEveryValueChanged(r => r).Subscribe(_=>{
            UpdateRoomChannel();
        }).AddTo(this);
        ChatManager.Instance.chatChannelList.ObserveAdd().Subscribe(channelAdd =>{
        }).AddTo(this);
        ChatManager.Instance.chatChannelList.ObserveRemove().Subscribe(channelRemove =>{
        }).AddTo(this);
        ChatManager.OnGetChatMessage.Subscribe( messagedata =>{
            OnGetMessagesData(messagedata);
        }).AddTo(this);
        worldToggle.OnValueChangedAsObservable().Subscribe(active =>{
            if(active)
                ChatManager.Instance.SwithChannelType(ChannelType.World);
        }).AddTo(this);
        roomToggle.OnValueChangedAsObservable().Subscribe(active =>{
            if(active)
                ChatManager.Instance.SwithChannelType(ChannelType.Room);
        }).AddTo(this);
        wisperToggle.OnValueChangedAsObservable().Subscribe(active =>{
            if(active)
                ChatManager.Instance.SwithChannelType(ChannelType.Whisper);
        }).AddTo(this);
        ChatManager.Instance.currentChannel.ObserveEveryValueChanged(current => ChatManager.Instance.currentChannel).Subscribe(current =>{
            ChangeChannelChat(current);
        }).AddTo(this);
        //toggleGroup.ObserveEveryValueChanged(t => toggleGroup.se)
    }
    void ChangeChannelChat(string channelName){
        CloseAllChannel();
        if(channelChatBox.ContainsKey(channelName))
            channelChatBox[channelName].SetActive(true);
    }
    void CloseAllChannel(){
        foreach (var channel in channelChatBox.Values)
        {
            channel.SetActive(false);
        }
    }
    void UpdateRoomChannel(){
        if(channelContentTransform.ContainsValue(chatRoomContent)){
            var roomkey = channelContentTransform.FirstOrDefault(x => x.Value == chatRoomContent).Key;
            if(!string.IsNullOrEmpty(roomkey)){
                ClearMessage(roomkey);
                channelContentTransform.Remove(roomkey);
            }
            if(channelChatBox.ContainsKey(roomkey))
                channelChatBox.Remove(roomkey);
        }
        if(PhotonNetwork.CurrentRoom == null)return;
        if(string.IsNullOrEmpty(PhotonNetwork.CurrentRoom.Name))return;
       
        if(!channelContentTransform.ContainsKey(PhotonNetwork.CurrentRoom.Name))
            channelContentTransform.Add(PhotonNetwork.CurrentRoom.Name,chatRoomContent);

        if(!channelChatBox.ContainsKey(PhotonNetwork.CurrentRoom.Name))
            channelChatBox.Add(PhotonNetwork.CurrentRoom.Name,roomChatBox);

        switch(ChatManager.Instance.currentChannelType){
            case ChannelType.Room :
                 roomToggle.isOn = true;
                break;
            case ChannelType.World:
                worldToggle.isOn = true;
                break;
        }
        ChangeChannelChat(ChatManager.Instance.currentChannel);
    }
    public void Fold(bool _fold){
        topBarRectTransform.DOAnchorPosY(_fold ? topbarFoldPosition : topbarUnfoldPosition,0.5f).SetAutoKill();
        chatBarRectTransform.DOSizeDelta(new Vector2(460,_fold ? chatbarMinimumHeight : chatbarMaximumHeight),0.5f).SetAutoKill();
    }

    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
           SendMessageFromInput();
        }
    }
    public void SendMessageFromInput(){
        ChatManager.Instance.SendChatMessage(input_txt.text);
        input_txt.text = "";
    }
    void OnGetMessagesData(ChatMessageData data)
    {
        if(!this.channelContentTransform.ContainsKey(data.channelName))return;
        string msgs = "";
        var senderName = "";
        string username ="";
        var channelContentTransform = this.channelContentTransform[data.channelName];
        string playerColorCode = ColorUtility.ToHtmlStringRGBA(playerColor);
        
        
        for (int i = 0; i < data.senders.Length; i++)
        {
            senderName = data.senders[i];
            var chatPrefab = Instantiate(chatTextPrefab);
            if(data.channelType == ChannelType.Room){
                
                var playerindexProperties = PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.PLAYER_INDEX] as ExitGames.Client.Photon.Hashtable;
                if(!playerindexProperties.ContainsKey(data.senders[i]))continue;
                var playerIndexProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playerindexProperties[data.senders[i]].ToString());
                senderName = playerIndexProfileData.nickName;
                if(playerIndexProfileData != null && !string.IsNullOrEmpty(playerIndexProfileData.colorCode))
                    playerColorCode = playerIndexProfileData.colorCode;
            }else{
                var targetPlayer = PhotonNetwork.PlayerList.FirstOrDefault(p =>p.UserId == data.senders[i]);
                if(targetPlayer != null)
                    senderName = PhotonNetwork.PlayerList.FirstOrDefault(p =>p.UserId == data.senders[i]).NickName;
            }
            username = string.Format("<color=#{0}>{1}</color> : ",playerColorCode, senderName);
            msgs = string.Format("{0}{1}", username, data.messages[i]);
            chatPrefab.GetComponent<TextMeshProUGUI>().text = msgs;
            chatPrefab.transform.SetParent(channelContentTransform);
            chatPrefab.transform.localPosition = Vector3.zero;
            chatPrefab.transform.localScale = Vector3.one;

            if(channelContentTransform.childCount >= messageLimit){
                for (int j = 0; j < delemessageCount; i++)
                {
                    Destroy(channelContentTransform.GetChild(0));
                }
            }
        }
    }
    void ClearMessage(string channelName){
        if(!channelContentTransform.ContainsKey(channelName))return;
        var channelContent =  channelContentTransform[channelName];
        for (int i = 0; i < channelContent.childCount; i++)
        {
            Destroy(channelContent.GetChild(i).gameObject);
        }
    }
    void OnEnable(){
        foreach (var item in channelContentTransform)
        {
            ClearMessage(item.Key);
        }
        UpdateRoomChannel();
    }
    void OnDisable(){
        isFold.Value = true;
        topBarRectTransform.DOAnchorPosY(topbarFoldPosition,0).SetAutoKill();
        chatBarRectTransform.DOSizeDelta(new Vector2(460,chatbarMinimumHeight),0).SetAutoKill();
    }
}
