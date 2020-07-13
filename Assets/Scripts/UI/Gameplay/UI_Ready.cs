
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Newtonsoft.Json;
using ExitGames.Client.Photon;
using DG.Tweening;
using TMPro;
public class UI_Ready : MonoBehaviourPunCallbacks
{
    [SerializeField]Button b_ready;
    [SerializeField]Image image_panel;
    [SerializeField]TextMeshProUGUI countdown_txt;
    bool isPlayed = false;
    void Start(){
        // b_ready.OnClickAsObservable().Subscribe(_=>{
        //     GameplayManager.Instance.PlayerReady(PhotonNetwork.LocalPlayer.UserId);
        //     b_ready.gameObject.SetActive(false);
        //     image_panel.enabled = false;
        // });
        
        Invoke("Ready",1);
    }
    void Ready(){
        GameplayManager.Instance.PlayerReady(PhotonNetwork.LocalPlayer.UserId);
        b_ready.gameObject.SetActive(false);
        image_panel.enabled = false;
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged){
        var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties as Hashtable;
        var playerIndexProperties = roomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        foreach (var property in propertiesThatChanged)
        {
            //Debug.Log(string.Format("key {0} value {1}",property.Key,property.Value));
            //if(!playerIndexProperties.ContainsKey(property.Key))return;
           // UpdatePlayerRanking(property.Value.ToString());
        }
        CheckPlayerReady();
    }

    void CheckPlayerReady(){
        if(isPlayed)return;
        var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        var playersIndexHash =  PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;

        Debug.Log("playerindexHash "+playersIndexHash[PhotonNetwork.LocalPlayer.UserId]);
        //Debug.Log("playerindexHash coubnt "+playersIndexHash.Count);
        Debug.Log("CheckPlayerReady "+playersIndexHash.StripToStringKeys());
        int readyCount = 0;
        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            var playerProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playersIndexHash[player.UserId].ToString());
            if(playerProfileData.ready)
                readyCount++;
        }
        if(readyCount == PhotonNetwork.CurrentRoom.Players.Count){
            Debug.Log("Let's Go to start");
            StartCountDown();
        }

        // var playerProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playersIndexHash[userId].ToString());
        // playerProfileData.ready = true;
        // var json = JsonConvert.SerializeObject(playerProfileData);
        // playersIndexHash[userId] = json;
        
    }

    void StartCountDown(){
        if(GameplayManager.Instance.isEnd.Value)return;
        countdown_txt.text = "3";
        countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
            countdown_txt.text = "2";
        })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0))
        .Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
            countdown_txt.text = "1";
        })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0))
        .Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
            countdown_txt.text = "Go!";
            isPlayed = true;
            GameplayManager.Instance.StartGame();
        })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0))
        .Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
            countdown_txt.text = "";
        })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0));
        sequence.Play();
    }
}
