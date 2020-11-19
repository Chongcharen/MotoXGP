using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab.ClientModels;
using UniRx;
public class FriendDetailDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TextMeshProUGUI txt_displayName;
    [SerializeField]RawImage rawImg_avatar;
    [SerializeField]Button b_remove_friend;
    [SerializeField]Button b_add_friend;
    [SerializeField]Button b_chat;
    [SerializeField]Button b_profile;
    public PlayerProfileModel profileModel;
    FriendListKey friendListMode;
    FriendInfo friendInfo;
    private void Start()
    {
        b_add_friend.OnClickAsObservable().Subscribe(_=>{
            if(friendListMode == FriendListKey.FriendAdd){
                FriendAPI.MakeFriendRequest(profileModel.PlayerId,success =>{
                     if(success)
                        Destroy(this.gameObject);
                });
            }else if(friendListMode == FriendListKey.FriendRequest){
                FriendAPI.AcceptFriend(profileModel.PlayerId,success =>{
                    if(success)
                        Destroy(this.gameObject);
                });
            }
        }).AddTo(this);
        b_remove_friend.OnClickAsObservable().Subscribe(_=>{
             FriendAPI.RejectFriend(profileModel.PlayerId,success =>{
                    if(success)
                        Destroy(this.gameObject);
                });
        }).AddTo(this);
        b_profile.OnClickAsObservable().Subscribe(_=>{
            Debug.Log("showprofile");
        }).AddTo(this);
        b_chat.OnClickAsObservable().Subscribe(_=>{

        }).AddTo(this);
    }
    public void Setup(PlayerProfileModel _model,FriendListKey mode){
        profileModel = _model;
        friendListMode = mode;
        txt_displayName.text = profileModel.DisplayName;
        DownloadImageAvatar(profileModel.AvatarUrl);
        switch(mode){
            case FriendListKey.Friend:
                SetupFriend();
            break;
            case FriendListKey.FriendAdd:
                SetupFindPlayer();
            break;
            case FriendListKey.FriendRequest:
                SetupFriendRequest();
            break;
        }
    }
    void DownloadImageAvatar(string url){
        Debug.Log("download Iamge "+url);
        if(string.IsNullOrEmpty(url))return;
        StaticCoroutine.DoCoroutine(ImageManager.Instance.LoadImage(url,texture =>{
            rawImg_avatar.texture = texture;
        }));
    }
    void SetupFindPlayer(){
        b_chat.gameObject.SetActive(false);
        b_remove_friend.gameObject.SetActive(false);
    }
    void SetupFriendRequest(){
        b_chat.gameObject.SetActive(false);
    }
    void SetupFriend(){
        b_add_friend.gameObject.SetActive(false);
    }
}
