using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;
using PlayFab.ClientModels;

public class UI_Friends : UIDisplay
{
    public Transform content;
    public GameObject FriendDetailPrefab;
    public Button b_back,b_clear_input,b_search;
    public TMP_InputField input_searchName;
    public Toggle t_friend,t_friendRequest,t_addFriend;
    public FriendListKey friendMode = FriendListKey.Friend;

    private void Start()
    {
        id = UIName.FRIENDS;
        UI_Manager.RegisterUI(this);

        root.ObserveEveryValueChanged(r => r.activeSelf).Subscribe(active=>{
            if(!active)return;
            FriendAPI.GetFriendLists(result =>UpdateFriendLists(result));
        }).AddTo(this);

        b_back.OnClickAsObservable().Subscribe(_=>{
            UI_Manager.OpenUI(UIName.LOBBY);
        }).AddTo(this);
        t_friend.onValueChanged.AddListener(_=>{
             if(!_)return;
            friendMode = FriendListKey.Friend;
            FriendAPI.GetFriendLists(result =>UpdateFriendLists(result));
        });
        t_friendRequest.onValueChanged.AddListener(_=>{
             if(!_)return;
            friendMode = FriendListKey.FriendRequest;
            FriendAPI.GetFriendLists(result =>{
                UpdateFriendRequestProfile(result);
            });
        });
        t_addFriend.onValueChanged.AddListener(_=>{
            b_clear_input.interactable = _;
            b_search.interactable = _;
            input_searchName.interactable = _;
            if(!_)return;
            GameUtil.ClearContent(content);
        });
        b_clear_input.OnClickAsObservable().Subscribe(_=>{
            input_searchName.text = "";
        }).AddTo(this);
        b_search.OnClickAsObservable().Subscribe(_=>{
            if(string.IsNullOrEmpty(input_searchName.text))return;
            friendMode = FriendListKey.FriendAdd;
            FriendAPI.GetPlayerProfile(input_searchName.text,result =>UpdatePlayerProfile(result));
        }).AddTo(this);
    }
    void UpdatePlayerProfile(PlayerProfileModel profile){
        GameUtil.ClearContent(content);
        var go = Instantiate(FriendDetailPrefab,Vector3.zero,Quaternion.identity,content);
        go.GetComponent<FriendDetailDisplay>().Setup(profile,friendMode);
    }
    void UpdateFriendRequestProfile(GetFriendsListResult result){
        GameUtil.ClearContent(content);
        foreach (var friend in result.Friends)
        {
            if(!friend.Tags.Contains(FriendTags.REQUESTEE))continue;
             var go = Instantiate(FriendDetailPrefab,Vector3.zero,Quaternion.identity,content);
            go.GetComponent<FriendDetailDisplay>().Setup(friend.Profile,FriendListKey.FriendRequest);
        }
       
    }
    void UpdateFriendLists(GetFriendsListResult result){
        GameUtil.ClearContent(content);
        foreach (var friend in result.Friends)
        {
            if(!friend.Tags.Contains(FriendTags.CONFIRMED))continue;
             var go = Instantiate(FriendDetailPrefab,Vector3.zero,Quaternion.identity,content);
            go.GetComponent<FriendDetailDisplay>().Setup(friend.Profile,FriendListKey.Friend);
        }
    }
}
