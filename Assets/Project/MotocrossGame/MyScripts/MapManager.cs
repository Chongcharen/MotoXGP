using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class MapManager : MonoBehaviour {

    public static Subject<Vector2> OnSetDirection = new Subject<Vector2>();
    public static MapManager Instance;
    [Header("Name Obj")]
    public string roadName = "Road";
    public string lastSpawnPointName = "LastSpawnPoint";
    public string endPointName = "EndPoint";
    public string fallName = "FallArea";
    public string respawnPointFallName = "RespawnPointFall";
    public string spawnGroup = "SpawnGroup";

    //deadzone area
    string deadZone = "deadzone";
    string safeZone = "safezone";
    string respawnZone = "respawn";


    [Header("Object")]
    public GameObject roadPrefab;
    public Transform currentLastSpawnPoint;
    public Transform currentEndPoint;
    public Transform[] spawnPoints;

    [Header("Info")]
    public string mapId;
    public int level = 1;
    [Header("Zone")]
    Dictionary<int,Vector3> respawnData;
    Dictionary<int,EndPointData> endpointData;
    public Vector3 respawnPosition;
    public bool isDeadzone = false;

    public float startPoint;
    public float finishPoint;

    void Awake(){
        Instance = this;
    }
    private void Start()
    {
        GameplayManager.Instance.Init();
        Init();
        
    }

    public void Init()
    {
        endpointData = new Dictionary<int, EndPointData>();
        respawnData = new Dictionary<int, Vector3>();
        SetDeadZone();
        SubscribeEvent();
        GetComponent<UI_PlayersDistance>().enabled = true;
        GetComponent<GameNetwork>().enabled = true;
    }
    void SubscribeEvent(){
        respawnData.ObserveEveryValueChanged(data => data.Count).Subscribe( vale =>{
            Debug.Log("Datachange "+vale);
        }).AddTo(this);
        GameplayManager.OnRestartGame.Subscribe(_=>{
            ResetLevel();
        }).AddTo(this);
    }
    void ResetLevel(){
       endpointData  = endpointData.Select(e =>{e.Value.isPass = false; e.Value.collider.enabled = true; return e;}).ToDictionary(k => k.Key , v => v.Value);

       foreach (var item in endpointData)
       {
           Debug.Log(string.Format("ispass {0} , enabled {1}",item.Value.isPass,item.Value.collider.enabled));
       }
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
    public void GetEndPoint(int endPointId){
        Debug.Log("GetEndpoint");
        Debug.Assert(endpointData.ContainsKey(endPointId));
        var endpointRawdata = endpointData[endPointId];
        if(!endpointRawdata.isPass){
            endpointRawdata.isPass = true;
            endpointRawdata.collider.enabled = false;
            GameplayManager.Instance.IncreaseRound();
        }
    }
    public void GetZone(int zoneInstanceID){
        Debug.Log("Respawn Count "+respawnData.Count);
        Debug.Assert(respawnData.ContainsKey(zoneInstanceID),"get responseZone");
        respawnPosition = respawnData[zoneInstanceID];
    }
    public void PassDeadZone(bool _isdeadzone){
        isDeadzone = _isdeadzone;
    }
    private void SetDeadZone(){
        Debug.Log("SetDeadZone ");
        GameObject[] objZone = GameObject.FindGameObjectsWithTag(TagKeys.Zone);//zoneObject.GetComponentsInChildren<Transform>();
        foreach (var gameObj in objZone)
        {
            var trans = gameObj.GetComponentsInChildren<Transform>();
            foreach (var item in trans)
            {
                if(item.gameObject.name == "ZonePoint"){
                    var deadZoneTarget = item.Find(deadZone);
                    var respawnTarget = item.Find(respawnZone);
                    Debug.Log("deadZoneTarget = "+deadZoneTarget);
                    Debug.Log("respawnTarget = "+respawnTarget);
                    if(!respawnData.ContainsKey(deadZoneTarget.GetInstanceID()))
                        respawnData.Add(deadZoneTarget.GetInstanceID(),respawnTarget.position);
                }

                if(item.gameObject.name == endPointName){
                    var endpoint = item.gameObject.GetComponent<Collider>();
                    if(!endpointData.ContainsKey(item.transform.GetInstanceID())){
                        var rawdata = new EndPointData{
                            instanceId = item.transform.GetInstanceID(),
                            isPass = false,
                            collider = endpoint,
                            position = item.position.x
                        };
                        endpointData.Add(item.transform.GetInstanceID(),rawdata);
                    }  
                }
            }
        }
        Debug.Log("SetDeadZone 1 ");
        var sort = from enrty in endpointData orderby enrty.Value.position ascending select enrty;
        var firstEndPoint = sort.ElementAtOrDefault(0);
        var lastEndpoint = sort.ElementAtOrDefault(sort.Count()-1);
        Debug.Log("xxxxxxxxxxxxxxxxxxxxxx "+firstEndPoint+" "+lastEndpoint);
        if(lastEndpoint.Value != null && firstEndPoint.Value != null){
             Debug.Log("ccccccccccccccc "+firstEndPoint+" "+lastEndpoint);
            startPoint = firstEndPoint.Value.position;
            finishPoint = lastEndpoint.Value.position;
            //OnSetDirection.OnNext(new Vector2(firstEndPoint.Value.position,lastEndpoint.Value.position));
        }else
        {
            Debug.LogError("Cannot found firstEndpoint or last endpoint");
        }
        Debug.Log("SetDeadZone 2");
        GameplayManager.Instance.SetTotalRound(endpointData.Count);

        foreach (var item in respawnData)
        {
            //Debug.Log(string.Format("Key {0} Value {1}",item.Key,item.Value));
        }
        Debug.Log("TotalCount "+respawnData.Count);
    }

   
    
}
