using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : MonoBehaviour {

    [Header("Name Obj")]
    public string roadName = "Road";
    public string lastSpawnPointName = "LastSpawnPoint";
    public string endPointName = "EndPoint";
    public string fallName = "FallArea";
    public string respawnPointFallName = "RespawnPointFall";
    public string spawnGroup = "SpawnGroup";


    [Header("Object")]
    public GameObject roadPrefab;
    public Transform currentLastSpawnPoint;
    public Transform currentEndPoint;
    public Transform[] spawnPoints;

    [Header("Info")]
    public string mapId;
    public int level = 1;

    private void Start()
    {
       // if (PhotonRoom.room == null)
            //Init();
           // Init();
    }

    public void Init()
    {
        Transform road = transform.Find(roadName);
        roadPrefab = road.gameObject;
        currentEndPoint = road.transform.Find(endPointName);
        currentLastSpawnPoint = road.transform.Find(lastSpawnPointName);
        SetFallArea(road);
        SetLevelMap();
        spawnPoints = transform.Find(spawnGroup).GetComponentsInChildren<Transform>();
        GameSetup.gs.spawnPoints = spawnPoints;
    }

    private void SetLevelMap()
    {
        for (int i = 0; i < level; i++)
        {
            GameObject roadObj = Instantiate(roadPrefab, currentLastSpawnPoint.position, Quaternion.identity, roadPrefab.transform.parent);
            currentLastSpawnPoint = roadObj.transform.transform.Find(lastSpawnPointName);
            Destroy(currentEndPoint.gameObject);
            currentEndPoint = roadObj.transform.Find(endPointName);
        }
    }

    private void SetFallArea(Transform value)
    {
        Transform[] objs = value.GetComponentsInChildren<Transform>();
        foreach (var obj in objs)
        {
            if (obj.name == fallName)
            {
                var fm = obj.gameObject.AddComponent<FallManager>();
                fm.respawnPointName = respawnPointFallName;
            }
        }
    }
}
