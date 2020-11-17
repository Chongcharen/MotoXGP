using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UniRx;
public class MapModelGenerator : MonoSingleton<MapModelGenerator>
{
    public static Subject<Unit> OnMapLoadComplete = new Subject<Unit>();
    MapLocationData mapLocationData;

    List<GameObject> mapList;
    int poolCount = 1;

    public Vector3 localSpawnPosition;
    public Vector3[] spawnPointsPosition;
    public float startPosition;
    public float finishPosition;

    string gamePath;

    //Monolith only 
    int monolithIndex = 0;
    void Start(){
    }
    public void GenerateMap(){
        mapList = new List<GameObject>();
        //Debug.Log()
        
        mapLocationData = GameDataManager.Instance.GetLevelMap(GameDataManager.Instance.GetStageName());
        Debug.Log("----------Level "+GameDataManager.Instance.gameLevel.theme);
        Debug.Log("----------stage "+GameDataManager.Instance.gameLevel.stage);
         var assetLabels = new List<string>{"Map_"+GameDataManager.Instance.gameLevel.gameStageData.themeName};
        AssetsLoader.Load(assetLabels,()=>{
            GenerateTerrainWithAddressable();
           // GenerateObjectWithAddressable();
            //GetSpawnPoint();
        });
        //GenerateTerrain();
       // GenerateObject();
        //GetSpawnPoint();
    }
    public void GenerateMapByName(string mapName){
        mapList = new List<GameObject>();
        mapLocationData = GameDataManager.Instance.GetLevelMap(mapName);
        GameDataManager.Instance.gameLevel.gameStageData = GameDataManager.Instance.GetGamelevelByName(mapName);
        var assetLabels = new List<string>{"Map_"+GameDataManager.Instance.gameLevel.gameStageData.themeName};
        AssetsLoader.Load(assetLabels,()=>{
            GenerateTerrainWithAddressable();
           
           
        });
        // GenerateTerrain();
        // GenerateObject();
        // GetSpawnPoint();
    }
    async void GenerateTerrainWithAddressable(){
        ClearMap();
        Debug.Log("Gamedatamanger "+GameDataManager.Instance.gameLevel); 
        gamePath = "Map_"+GameDataManager.Instance.gameLevel.gameStageData.themeName+"/";
        var mapRoot = new GameObject("MapRoot");
        poolCount = mapLocationData.startPositionDatas.Count;
        var prefabTerrain = GameDataManager.Instance.gameLevel.gameStageData.themeName+""+GameDataManager.Instance.gameLevel.gameStageData.stageName;
        var terrainResource = await AddressableManager.Instance.LoadObject<GameObject>(gamePath+prefabTerrain+".prefab");
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
            print(Depug.Log("ObjectData "+objectData.prefabName,Color.blue));
            var objectPath = gamePath+objectData.prefabName;
            var objectResource = await AddressableManager.Instance.LoadObject<GameObject>(objectPath+".prefab");
            Debug.Log("objectpath "+objectPath);
            //ObjectPool.Instance.CreatePool(objectPath,1);
            if(objectResource == null){
                Debug.LogError("Noit found prefab "+objectPath+" in folder");
                continue;
            }
            var prefab = Instantiate(objectResource);
            if(prefab != null){
                prefab.gameObject.SetActive(true);
                prefab.transform.SetParent(environment.transform);
                prefab.transform.position = objectData.position;
                prefab.transform.rotation = Quaternion.Euler(0,180,0);
                Debug.Log("Prefab name "+prefab.name);
                if(prefab.tag == TagKeys.MONOLITH){
                    var animator = prefab.gameObject.GetComponent<MonolithAnimatorSetting>().time = monolithIndex;
                    monolithIndex ++;
                }
            }
        }
        
        var round = 0;
        foreach (var positionData in mapLocationData.startPositionDatas)
        {
            if(round > GameDataManager.Instance.gameLevel.level && round >1)break;
            var terrainInstance = Instantiate(templateTerrain);
            terrainInstance.transform.SetParent(mapRoot.transform);
            terrainInstance.transform.position = positionData;
            mapList.Add(terrainInstance);
            round ++;
        }
        templateTerrain.gameObject.SetActive(false);
        terrainResource.gameObject.SetActive(false);
        Destroy(templateTerrain);
        AddressableManager.Instance.ClearAllAssets();

         GenerateObjectWithAddressable();
    }
    
    async void GenerateObjectWithAddressable(){
        var environment = new GameObject("BaseEnvironmentObject");
        environment.transform.position = Vector3.zero;
        foreach (var objectData in mapLocationData.objectLocationDatas)
        {
            var objectPath = gamePath+objectData.prefabName;
            var objectResource = await AddressableManager.Instance.LoadObject<GameObject>(objectPath+".prefab");
            var prefab = ObjectPool.Instance.GetObjectFormPool(objectPath);
            if(prefab != null){
                prefab.gameObject.SetActive(true);
                prefab.transform.SetParent(environment.transform);
                prefab.transform.position = objectData.position;
                prefab.transform.rotation = Quaternion.Euler(0,180,0);
                mapList.Add(prefab);
            }
        }
         GetSpawnPoint();
    }
    void GenerateTerrain(){
        ClearMap();
        gamePath = "Map/"+GameDataManager.Instance.gameLevel.gameStageData.themeName+"/";
        Debug.Log("Gamepath "+gamePath);
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
                print(Depug.Log("Prefab name "+prefab.name,Color.green));
                if(prefab.tag == TagKeys.MONOLITH){
                    var animator = prefab.gameObject.GetComponent<MonolithAnimatorSetting>().time = monolithIndex;
                    monolithIndex ++;
                }
            }
        }
        var round = 0;
        foreach (var positionData in mapLocationData.startPositionDatas)
        {
            if(round > GameDataManager.Instance.gameLevel.level && round >1)break;
            var terrainInstance = Instantiate(templateTerrain);
            terrainInstance.transform.SetParent(mapRoot.transform);
            terrainInstance.transform.position = positionData;
            mapList.Add(terrainInstance);
            round ++;
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
        print(Depug.Log("getspwnpoint "+mapList.Count,Color.blue));
        if(mapList.Count <= 0)return;
        var spawnGroup = mapList[0].GetComponent<SpawnGroup>();
        localSpawnPosition = spawnGroup.GetLocalSpawnPosition();
        spawnPointsPosition = new Vector3[spawnGroup.GetSpawnPoints().Length];

        for (int i = 0; i < spawnPointsPosition.Length; i++)
        {
            spawnPointsPosition[i] = spawnGroup.GetSpawnPoints()[i].position;
        }
        print(Depug.Log($"Getspwnpoint complete "+spawnPointsPosition.Length,Color.blue));
        OnMapLoadComplete.OnNext(default);
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
