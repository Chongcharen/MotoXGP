using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class GameSetup : MonoBehaviour {

    public static GameSetup gs;

    public Transform[] spawnPoints;
    public GameObject ragdollPrefab;
    public List<Player> listPlayerGoal = new List<Player>();

    public GameObject botPrefab;

    private string mapId = "1001";
    private void OnEnable()
    {
        if (gs == null)
            gs = this;

    }
    
    public void Init()
    {
        if (PhotonRoom.room != null)
        {
            mapId = PhotonRoom.room.mapId;
        }
        //var map = Instantiate(InfoMap.Instance.dicMapInfo[mapId].prefab.GetComponent<MapManager>(), null);
        //map.Init();

        //Instantiate(botPrefab, spawnPoints[spawnPoints.Length-1].position, spawnPoints[spawnPoints.Length - 1].rotation,null);
    }

    public void DisconnectPlayer()
    {
        //StartCoroutine(DisconnectAndLoad());
        Destroy(PhotonRoom.room.gameObject);
        PhotonRoom.room.LeaveRoom();
    }
    

    //IEnumerator DisconnectAndLoad()
    //{
    //    //PhotonNetwork.Disconnect();
    //    //PhotonNetwork.LeaveRoom();
    //    //while (PhotonNetwork.InRoom)
    //    //{
    //    //    yield return null;
    //    //}
    //    //SceneManager.LoadScene(MultiplayerSetting.Instance.menuScene);
    //}
}
