using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using UniRx;
public class PhotonConsole : MonoBehaviourPunCallbacks
{
    static PhotonConsole instance;
    public PhotonVoiceView voiceView;
    public Recorder recorder;
    
    public static Subject<bool> OnJoinRoom = new Subject<bool>();
    public static PhotonConsole Instance{
        get{
            if(instance == null){
                var go = new GameObject("Photonconsole",typeof(PhotonConsole));
                instance = go.GetComponent<PhotonConsole>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    public void QuickStart(){
        var quickName = "name"+UnityEngine.Random.Range(0,999);
        Connect(quickName,"0");
    }
    public void Connect(string _nickName,string userid)
    {
        //AuthenticationValues authValue = new AuthenticationValues(userid);
        //PhotonNetwork.AuthValues = authValue;
        PhotonNetwork.NickName = _nickName;
        PhotonNetwork.SendRate = 10;
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.AutomaticallySyncScene = true;
        
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.LogWarning("on OnConnectedToMaster");
        // PhotonNetwork.NickName = nickName;
        // PhotonNetwork.JoinLobby();
        // Connected.OnNext(true);
        // if(friends_uid != null && friends_uid.Length > 0){
        //     Debug.Log("findFriend ");
        //     FindFriends(friends_uid);
        // }
       // 
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message){
        OnJoinRoom.OnNext(false);
        Debug.Log("joinrandomroom failed ");
        recorder = null;
        var roomName = "random"+UnityEngine.Random.Range(0,999);
        RoomOptions roomOptions = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom(roomName,roomOptions,TypedLobby.Default);
    }
    public override void OnJoinedRoom(){
        Debug.Log("OnjoinedRoom");
        var go = PhotonNetwork.Instantiate("Photon/VoicePrefab",Vector3.one,Quaternion.identity);
        voiceView = go.GetComponent<PhotonVoiceView>();
        recorder = PhotonVoiceNetwork.Instance.PrimaryRecorder;
        
        Debug.Log("recorder "+recorder);
        OnJoinRoom.OnNext(true);
    }

    public void SetEchoMode(bool active){
        if(recorder == null)return;
        recorder.DebugEchoMode = active;
    }
    public void SetMute(bool isMute){
        if(recorder == null)return;
        AudioListener.volume = isMute ? 0 : 1;
    }
    public void SetTransmit(bool isTransmit){
        if(recorder == null)return;
        recorder.TransmitEnabled = isTransmit;
    }

    //voiceview
}
