using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ExitGames.Client.Photon;
using System.IO;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField]Transform[] spawnPoint;
    [SerializeField]GameObject prefab;
    [SerializeField]SmoothFollow cameraFollow;
    void Start(){
        var go = PhotonNetwork.Instantiate("POC/Bike",spawnPoint[PhotonNetwork.LocalPlayer.ActorNumber].position,Quaternion.Euler(0,90,0));
        go.GetComponent<AbikeChopSystem>().isControll = true;
        go.GetComponent<PhotonSmoothSyncMovement>().fixPostionZ = go.transform.position.z;
        go.transform.position = new Vector3(go.transform.position.x,go.transform.position.y,spawnPoint[0].position.z);
        go.transform.GetComponent<AbikeChopSystem>().startPosition = go.transform.position;
        PhotonVoiceConsole.Instance.CreateVoiceView();
        cameraFollow.target = go.transform;
    }
    
}
