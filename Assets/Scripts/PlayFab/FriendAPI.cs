using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UniRx;
using System;

public static class FriendAPI
{
    public static Subject<List<FriendInfo>> OnFriendUpdate = new Subject<List<FriendInfo>>();
    public static PlayFabAuthenticationContext AuthenticationContext;
    public static ReactiveProperty<List<FriendInfo>> friendInfos = new ReactiveProperty<List<FriendInfo>>();
    #region GetFriend
    public static void GetPlayerProfile(string playFabId,Action<PlayerProfileModel> OnGetPlayerProfileResult) {
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints() {
                ShowDisplayName = true,
                ShowAvatarUrl = true,
            }
        },
        result =>{
            OnGetPlayerProfileResult(result.PlayerProfile);
        },
        error => Debug.LogError(error.GenerateErrorReport()));
    }
    public static void SearchFriendRequest(Action<ExecuteCloudScriptResult> resultCallback = null){
         PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
            FunctionName = "SearchFriendsRequest",
            //FunctionParameter = new {inputValue = "Bavaria"},
            FunctionParameter = new { PlayFabId = PlayFabController.Instance.PlayFabId, Page = 2, Tag = "requestee" },
            GeneratePlayStreamEvent = true,
            },
            success =>{
                Debug.Log("OnSearchFreindSuccess ");
                if(resultCallback == null)return;
                resultCallback(success);
                //Debug.Log(PlayFab.PfEditor.Json.JsonWrapper.SerializeObject(success.FunctionResult));
            },OnErrorShared
        );
    }
    public static void GetFriendLists(Action<GetFriendsListResult> resutlCallback = null){
        var getFriendListRequest = new GetFriendsListRequest(){
            ProfileConstraints = new PlayerProfileViewConstraints() {
                ShowDisplayName = true,
                ShowAvatarUrl = true,
            },
            IncludeFacebookFriends = true,
            IncludeSteamFriends = true
        };
        PlayFabClientAPI.GetFriendsList(getFriendListRequest,result=>{
            //OnGetFriendResult(result);
            friendInfos.SetValueAndForceNotify(result.Friends);
            friendInfos.Value = result.Friends;
            OnFriendUpdate.OnNext(friendInfos.Value);
            if(resutlCallback != null)
                resutlCallback(result);
        },error =>{
            Debug.Log("Error");
        });
    }
    public static void SetFriendTagsRequest(string FriendPlayFabId){
        Debug.Log("Setfreindtagrequest "+FriendPlayFabId);
        var setFriendTagRequest = new SetFriendTagsRequest();
            setFriendTagRequest.FriendPlayFabId = FriendPlayFabId;
            setFriendTagRequest.Tags = new List<string>{"requestee"};
        PlayFabClientAPI.SetFriendTags(setFriendTagRequest,result =>{
            Debug.Log("setfriendtag result "+result.ToJson());
        },OnErrorShared);
    }
    // { PlayFabId: PlayFabId, FriendPlayFabId: FriendPlayFabId }
    public static void MakeFriendRequest(string FriendPlayFabId,Action<bool> callback= null){
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest(){
            FunctionName = "MakeFriendRequest",
            FunctionParameter = new {PlayFabId = PlayFabController.Instance.PlayFabId,FriendPlayFabId = FriendPlayFabId},
            GeneratePlayStreamEvent = true
        },cb =>{
            if(callback != null)
                     callback(true);
        },OnErrorShared);
    }
    // { PlayFabId: PlayFabId, FriendPlayFabId: FriendPlayFabId }
    public static void AcceptFriend(string FriendPlayFabId,Action<bool> callback= null){
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest(){
                FunctionName = "FriendsRequestConfirmed",
                FunctionParameter = new {PlayFabId = PlayFabController.Instance.PlayFabId,FriendPlayFabId = FriendPlayFabId}
            }
            ,acceptResult =>{
                Debug.Log("acceptfriend "+acceptResult.ToJson());
                if(callback != null)
                    callback(true);
            },OnErrorShared
        );
    }
    public static void RejectFriend(string FriendPlayFabId,Action<bool> callback = null){
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest(){
                FunctionName = "FriendsRequestDeclined",
                FunctionParameter = new {PlayFabId = PlayFabController.Instance.PlayFabId,FriendPlayFabId = FriendPlayFabId}
            }
            ,rejectResult =>{
                Debug.Log("rejectResult "+rejectResult.ToJson());
                if(callback != null)
                    callback(true);
            },OnErrorShared
        );
    }


    

    private static void OnErrorShared(PlayFabError obj)
    {
        //throw new NotImplementedException();
    }


    private static void OnGetFriendResult(GetFriendsListResult result)
    {
        Debug.Log($"result {result}");
        Debug.Log($"Custom Data = {result.CustomData}");
        Debug.Log($"Friend Count = {result.Friends.Count}");
        Debug.Log($"Request = {result.Request}");

        //friendInfos.Value = result.Friends;
        friendInfos.SetValueAndForceNotify(result.Friends);
        friendInfos.Value = result.Friends;
        OnFriendUpdate.OnNext(friendInfos.Value);
        Debug.Log("freind vale "+friendInfos.Value);
        foreach (var item in friendInfos.Value)
        {
            Debug.Log($"Freind ID = {item.FriendPlayFabId} name {item.TitleDisplayName}");
        }
    }
    #endregion
    #region AddFriend
    public static void AddFriend(string titleDisplayName,AddFriendMode mode){
       
        var addFriendRequest = new AddFriendRequest();
        switch(mode){
            case AddFriendMode.Email:
                addFriendRequest.FriendEmail = titleDisplayName;
            break;
            case AddFriendMode.PlayFabId:
                addFriendRequest.FriendPlayFabId = titleDisplayName;
            break;
            case AddFriendMode.TitleDisplayName:
                addFriendRequest.FriendTitleDisplayName = titleDisplayName;
            break;
            case AddFriendMode.Username:
                addFriendRequest.FriendUsername = titleDisplayName;
            break;
        }
        PlayFabClientAPI.AddFriend(addFriendRequest,result =>OnAddFriendResult(result),errorCallback=>{
            Debug.Log("Error "+errorCallback);
        });
    }

    private static void OnAddFriendResult(AddFriendResult result)
    {
        GetFriendLists();
    }
    #endregion
    #region RemoveFriend
    public static void RemoveFriend(string playfabId){
        var request = new RemoveFriendRequest();
        request.FriendPlayFabId = playfabId;
        PlayFabClientAPI.RemoveFriend(request,result =>OnRemoveFreindResult(result),errorCallback=>{});
    }

    private static void OnRemoveFreindResult(RemoveFriendResult result)
    {
        GetFriendLists();
    }
    #endregion
}
public enum AddFriendMode{
    Email = 0 , PlayFabId = 1,TitleDisplayName =2,Username = 3
}
public static class FriendTags{
    public const string REQUESTER = "requester";
    public const string REQUESTEE = "requestee";
    public const string CONFIRMED = "Confirmed";
}
public enum FriendListKey{
    Friend,FriendRequest,FriendAdd
}