using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ExitGames.Client.Photon;
using System.IO;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;
using UnityEngine.UI;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField]Transform[] spawnPoint;
    [SerializeField]Transform finishPoint;
    [SerializeField]GameObject prefab;
    [SerializeField]SmoothFollow cameraFollow;
    [SerializeField]Slider playerSlider;
    public float startPosition;
    public float totalDirection;

    public static Subject<Vector2> OnSetDirection = new Subject<Vector2>();
    //player
    Transform playerTransform;
    void Start(){
        startPosition = spawnPoint[0].position.x;
        OnSetDirection.OnNext(new Vector2(startPosition,finishPoint.position.x));
        var go = PhotonNetwork.Instantiate("POC/BikeV5.7",spawnPoint[PhotonNetwork.LocalPlayer.ActorNumber].position,Quaternion.Euler(0,90,0));
        go.name = PhotonNetwork.LocalPlayer.NickName;
        go.GetComponent<AbikeChopSystem>().SetController(true);
        go.GetComponent<PhotonSmoothSyncMovement>().fixPostionZ = go.transform.position.z;
        go.transform.position = new Vector3(go.transform.position.x,go.transform.position.y,spawnPoint[0].position.z);
        go.transform.GetComponent<AbikeChopSystem>().startPosition = go.transform.position;
        PhotonVoiceConsole.Instance.CreateVoiceView();
        playerTransform = go.transform;
        cameraFollow.target = go.transform.GetComponent<AbikeChopSystem>().targetForCameraLook;
        totalDirection = finishPoint.position.x- playerTransform.position.x;
        playerSlider.minValue = go.transform.position.x;
        playerSlider.maxValue = totalDirection;
    }
    void Update(){
       totalDirection = finishPoint.position.x- playerTransform.position.x;
       playerSlider.value = playerTransform.position.x;
    }
    
}
