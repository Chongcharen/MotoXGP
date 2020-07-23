
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
    [SerializeField]Transform playerStartPoint;
    [SerializeField]Transform[] spawnPoint;
    [SerializeField]Transform finishPoint;
    [SerializeField]GameObject prefab;
    [SerializeField]SmoothFollow cameraFollow;
    [SerializeField]Kino.Motion motionBlur;
    public float startPosition;
   // public float totalDirection;
    //player
    Transform playerTransform;
    void Start(){
        //ObjectPool.Instance.CreatePool("POC/BikeV5.7",10);
        startPosition = spawnPoint[0].position.x;
        //var go = PhotonNetwork.Instantiate("POC/BikeV5.8",spawnPoint[PhotonNetwork.LocalPlayer.ActorNumber].position,Quaternion.Euler(0,90,0));
        var go = PhotonNetwork.Instantiate("POC/BikeV5.8",spawnPoint[PhotonNetwork.LocalPlayer.ActorNumber].position,Quaternion.Euler(0,90,0));
        go.name = PhotonNetwork.LocalPlayer.NickName;
        go.GetComponent<AbikeChopSystem>().SetController(true);
        go.GetComponent<PhotonSmoothSyncMovement>().fixPostionZ = go.transform.position.z;
        go.transform.position = new Vector3(go.transform.position.x,go.transform.position.y,playerStartPoint.position.z);
        go.transform.GetComponent<AbikeChopSystem>().startPosition = go.transform.position;
        playerTransform = go.transform;
        cameraFollow.target = go.transform.GetComponent<AbikeChopSystem>().targetForCameraLook;
        cameraFollow.playerCrash = go.transform.GetComponent<AbikeChopSystem>().crash;
        go.GetComponent<BoostSystem>().SetUpMotion(motionBlur);
        //GameplayManager.Instance.PlayerReady(PhotonNetwork.LocalPlayer.UserId);
    }
    // void PlayerReady(){
    //     var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties as Hashtable;
    //     var playerindex = roomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
    //     if(playerindex.ContainsKey())
    // }
    void Update(){
       //totalDirection = finishPoint.position.x- playerTransform.position.x;
    }
    
}
