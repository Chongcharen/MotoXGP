
using UnityEngine;
using UniRx;
using ExitGames.Client.Photon;
using System.IO;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    //[SerializeField]Transform playerStartPoint;
    //[SerializeField]Transform[] spawnPoint;
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
        ExitGames.Client.Photon.Hashtable roomData;
        ExitGames.Client.Photon.Hashtable playerIndex;
        //Find index player
        roomData = PhotonNetwork.CurrentRoom.CustomProperties as ExitGames.Client.Photon.Hashtable;
        playerIndex = PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.PLAYER_INDEX] as ExitGames.Client.Photon.Hashtable;
        var playerProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playerIndex[PhotonNetwork.LocalPlayer.UserId].ToString());
        //
        startPosition = MapManager.Instance.localSpawnPosition.x;//spawnPoint[0].position.x;
        Debug.Log("-----------------------------------");
        var go = PhotonNetwork.Instantiate("POC/BikePlayer_100620",MapManager.Instance.spawnPointsPosition[playerProfileData.index],Quaternion.Euler(0,90,0));
        go.name = PhotonNetwork.LocalPlayer.NickName;
        go.GetComponent<PhotonCustomTransformView>().fixPositionZ = go.transform.position.z;
        go.transform.position = new Vector3(go.transform.position.x,go.transform.position.y,MapManager.Instance.localSpawnPosition.z);
        go.transform.GetComponent<AbikeChopSystem>().startPosition = go.transform.position;
        go.transform.GetComponent<AbikeChopSystem>().BoostLimit = GameDataManager.Instance.gameLevel.nosCount;
        playerTransform = go.transform;
        cameraFollow.target = go.transform.GetComponent<AbikeChopSystem>().targetForCameraLook;
        cameraFollow.playerCrash = go.transform.GetComponent<AbikeChopSystem>().crash;
        go.GetComponent<BoostSystem>().SetUpMotion(motionBlur);
        //go.GetComponent<BikeCustomize>().RandomBike();


        var playerComponent = go.GetComponent<PlayerComponents>();
        //GameplayManager.Instance.PlayerReady(PhotonNetwork.LocalPlayer.UserId);
    }
    // void PlayerReady(){
    //     var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties as Hashtable;
    //     var playerindex = roomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
    //     if(playerindex.ContainsKey())
    // }
    
}
