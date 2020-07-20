using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModelGenerator : MonoSingleton<MapModelGenerator>
{
    MapLocationData mapLocationData;

    List<GameObject> mapList;
    int poolCount = 1;

    public Vector3 localSpawnPosition;
    public Vector3[] spawnPointsPosition;
    public float startPosition;
    public float finishPosition;
    void Start(){
    }
    public void GenerateMap(){
        mapList = new List<GameObject>();
        mapLocationData = GameDataManager.Instance.GetLevelMap(GameDataManager.Instance.currentMapChoose);
        
        GenerateTerrain();
        GenerateObject();
        GetSpawnPoint();
    }
    void GenerateTerrain(){
        var mapRoot = new GameObject("MapRoot");
        poolCount = mapLocationData.startPositionDatas.Count;
        ObjectPool.Instance.CreatePool("Map/Moutain/Terrain",poolCount);
        foreach (var positionData in mapLocationData.startPositionDatas)
        {
            var prefab = ObjectPool.Instance.GetObjectFormPool("Map/Moutain/Terrain");
            if(prefab != null){
                prefab.gameObject.SetActive(true);
                prefab.transform.SetParent(mapRoot.transform);
                prefab.transform.position = positionData;
                prefab.transform.rotation = Quaternion.Euler(0,180,0);
                mapList.Add(prefab);
            }
        }
    }
    public void GenerateObject(){
        var environment = new GameObject("Environment");
        environment.transform.position = Vector3.zero;
        foreach (var objectData in mapLocationData.objectLocationDatas)
        {
            ObjectPool.Instance.CreatePool("Map/Moutain/"+objectData.prefabName,1);
            var prefab = ObjectPool.Instance.GetObjectFormPool("Map/Moutain/"+objectData.prefabName);
            if(prefab != null){
                prefab.gameObject.SetActive(true);
                prefab.transform.SetParent(environment.transform);
                prefab.transform.position = objectData.position;
                prefab.transform.rotation = Quaternion.Euler(0,180,0);
                mapList.Add(prefab);
            }
        }
    }

    private void GetSpawnPoint()
    {
        if(mapList.Count <= 0)return;
        var spawnGroup = mapList[0].GetComponent<SpawnGroup>();
        localSpawnPosition = spawnGroup.GetLocalSpawnPosition();
        spawnPointsPosition = new Vector3[spawnGroup.GetSpawnPoints().Length];
        for (int i = 0; i < spawnPointsPosition.Length; i++)
        {
            spawnPointsPosition[i] = spawnGroup.GetSpawnPoints()[i].position;
        }
    }

    

}
