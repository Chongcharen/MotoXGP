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

    string gamePath;
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

        gamePath = "Map/"+GameDataManager.Instance.gameLevel.gameStageData.themeName+"/";
        var mapRoot = new GameObject("MapRoot");
        poolCount = mapLocationData.startPositionDatas.Count;
        var prefabTerrain = GameDataManager.Instance.gameLevel.gameStageData.themeName+""+GameDataManager.Instance.gameLevel.gameStageData.stageName;
        var terrainResource = Resources.Load<GameObject>(gamePath+prefabTerrain);
        var templateTerrain = Instantiate(terrainResource);

        if(templateTerrain != null){
            templateTerrain.gameObject.SetActive(true);
            templateTerrain.transform.SetParent(mapRoot.transform);
            templateTerrain.transform.position = Vector3.zero;
            templateTerrain.transform.rotation = Quaternion.Euler(0,180,0);
        }
        var environment = new GameObject("Environment");
        environment.transform.position = Vector3.zero;
        environment.transform.SetParent(templateTerrain.transform);

        

        

        foreach (var objectData in mapLocationData.objectTerrainDatas)
        {
            var objectPath = gamePath+objectData.prefabName;
            ObjectPool.Instance.CreatePool(objectPath,1);
            var prefab = ObjectPool.Instance.GetObjectFormPool(objectPath);
            if(prefab != null){
                prefab.gameObject.SetActive(true);
                prefab.transform.SetParent(environment.transform);
                prefab.transform.position = objectData.position;
                prefab.transform.rotation = Quaternion.Euler(0,180,0);
            }
        }

        foreach (var positionData in mapLocationData.startPositionDatas)
        {
            var terrainInstance = Instantiate(templateTerrain);
            terrainInstance.transform.SetParent(mapRoot.transform);
            terrainInstance.transform.position = positionData;
            mapList.Add(terrainInstance);
        }
        templateTerrain.gameObject.SetActive(false);
        terrainResource.gameObject.SetActive(false);
        Destroy(templateTerrain);
        Resources.UnloadAsset(terrainResource);
        
    }
    public void GenerateObject(){
        var environment = new GameObject("BaseEnvironmentObject");
        environment.transform.position = Vector3.zero;
        foreach (var objectData in mapLocationData.objectLocationDatas)
        {
            var objectPath = gamePath+objectData.prefabName;
            ObjectPool.Instance.CreatePool(objectPath,1);
            var prefab = ObjectPool.Instance.GetObjectFormPool(objectPath);
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
