using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using UniRx;
public class PhotonVoiceConsole : MonoBehaviourPunCallbacks
{
    static PhotonVoiceConsole _instance;
    [SerializeField]GameObject voicePrefab;
    private GameObject voiceObject;
    private PhotonVoiceView voiceView;
    private Recorder recorder;

    public static PhotonVoiceConsole Instance{
        get{
            if(_instance == null){
                _instance = new GameObject("PhotonVoiceConsole",typeof(PhotonVoiceConsole)).GetComponent<PhotonVoiceConsole>();
            }
            return _instance;
        }
    }
    public void Init(){
        Debug.Log("Photon voiceconsole init");
    }
    private void Awake() {
        GameController.OnMicActive.Subscribe(active =>{
            recorder.TransmitEnabled = active;
        });
    }
    public override void OnJoinedRoom(){
        CreateVoiceView();
    }
    public void CreateVoiceView(){
        DestroyVoiceObject();
        voiceObject = PhotonNetwork.Instantiate("Photon/VoicePrefab",Vector3.one,Quaternion.identity);
        voiceView = voiceObject.GetComponent<PhotonVoiceView>();
        recorder = PhotonVoiceNetwork.Instance.PrimaryRecorder;
    }
    public override void OnLeftRoom(){
        DestroyVoiceObject();
    }
    public override void OnDisconnected(DisconnectCause cause){
        DestroyVoiceObject();
    }

    void DestroyVoiceObject(){
        if(voiceObject == null)return;
        Destroy(voiceObject);
        voiceObject = null;
    }
    
}
