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
        //Debug.Log()
        
        mapLocationData = GameDataManager.Instance.GetLevelMap(GameDataManager.Instance.GetStageName());
        Debug.Log("----------Level "+GameDataManager.Instance.gameLevel.theme);
        Debug.Log("----------stage "+GameDataManager.Instance.gameLevel.stage);
        GenerateTerrain();
        GenerateObject();
        GetSpawnPoint();
    }
    public void GenerateMapByName(string mapName){
        mapList = new List<GameObject>();
        mapLocationData = GameDataManager.Instance.GetLevelMap(mapName);
        GameDataManager.Instance.gameLevel.gameStageData = GameDataManager.Instance.GetGamelevelByName(mapName);
        GenerateTerrain();
        GenerateObject();
        GetSpawnPoint();
    }
    void GenerateTerrain(){
        ClearMap();
        var mapRoot = new GameObject("MapRoot");
        poolCount = mapLocationData.startPositionDatas.Count;
        var prefabTerrain = GameDataManager.Instance.gameLevel.gameStageData.themeName+""+GameDataManager.Instance.gameLevel.gameStageData.stageName;
        Debug.Log("prefabTerrain "+prefabTerrain);
        ObjectPool.Instance.CreatePool("Map/Forest/"+prefabTerrain,poolCount);
        foreach (var positionData in mapLocationData.startPositionDatas)
        {
            var prefab = ObjectPool.Instance.GetObjectFormPool("Map/Forest/"+prefabTerrain);
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
            Debug.Log("Prefab name "+objectData.prefabName);
            ObjectPool.Instance.CreatePool("Map/Forest/"+objectData.prefabName,1);
            var prefab = ObjectPool.Instance.GetObjectFormPool("Map/Forest/"+objectData.prefabName);
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

    void ClearMap(){
        var root = GameObject.Find("MapRoot");
        var environment = GameObject.Find("Environment");
        if(root != null)
            Destroy(root.gameObject);
        if(environment != null)
            Destroy(environment.gameObject);

        ObjectPool.Instance.ClearPool();
    }

    

}
