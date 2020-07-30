using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class MapManager : MonoBehaviour {

    public static Subject<Vector2> OnSetDirection = new Subject<Vector2>();
    public static Subject<Quaternion> OnCameraRotate = new Subject<Quaternion>();
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

    public Vector3 localSpawnPosition;
    public Vector3[] spawnPointsPosition;

    [Header("Info")]
    public string mapId;
    public int level = 1;
    [Header("Zone")]
    Dictionary<int,Vector3> respawnData;
    Dictionary<int,EndPointData> endpointData;
    Dictionary<int,Quaternion> camerapointData;
    public Vector3 respawnPosition;
    public bool isDeadzone = false;

    public float startPoint;
    public float finishPoint;

    void Awake(){
        Instance = this;
    }
    private void Start()
    {
        ObjectPool.Instance.Init();
        GameplayManager.Instance.Init();
        Init();
        
        
    }

    public void Init()
    {
        endpointData = new Dictionary<int, EndPointData>();
        camerapointData = new Dictionary<int, Quaternion>();
        respawnData = new Dictionary<int, Vector3>();
        MapModelGenerator.Instance.GenerateMap();
        localSpawnPosition = MapModelGenerator.Instance.localSpawnPosition;
        spawnPointsPosition = MapModelGenerator.Instance.spawnPointsPosition.ToArray();
        MapModelGenerator.Instance.Dispose();
        SetDeadZone();
        SetCameraZone();
        SubscribeEvent();
        GetComponent<UI_PlayersDistance>().enabled = true;
        GetComponent<GameNetwork>().enabled = true;
        PhotonVoiceConsole.Instance.CreateVoiceView();
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
        //Debug.Log("GetEndpoint");
        Debug.Assert(endpointData.ContainsKey(endPointId));
        var endpointRawdata = endpointData[endPointId];
        if(!endpointRawdata.isPass){
            endpointRawdata.isPass = true;
            endpointRawdata.collider.enabled = false;
            GameplayManager.Instance.IncreaseRound();
        }
    }
    public void GetZone(int zoneInstanceID){
        // Debug.Log("Respawn Count "+respawnData.Count);
        // Debug.Log("Getzone "+zoneInstanceID);
        Debug.Assert(respawnData.ContainsKey(zoneInstanceID),"get responseZone");
        respawnPosition = respawnData[zoneInstanceID];
    }
    public void GetCameraZone(int cameraInstanceID){
        Debug.Assert(camerapointData.ContainsKey(cameraInstanceID),"get responseZone");
        OnCameraRotate.OnNext(camerapointData[cameraInstanceID]);
    }
    public void PassDeadZone(bool _isdeadzone){
        isDeadzone = _isdeadzone;
    }
    private void SetDeadZone(){
        endpointData.Clear();
        //Debug.Log("SetDeadZone==========> ");
        GameObject[] objZone = GameObject.FindGameObjectsWithTag(TagKeys.Zone);//zoneObject.GetComponentsInChildren<Transform>();
        //Debug.Log("OBJ Zone length "+objZone.Length);
        foreach (var gameObj in objZone)
        {
            var trans = gameObj.GetComponentsInChildren<Transform>();
            Debug.Log("Child length "+trans.Length);
            foreach (var item in trans)
            {
                if(item.gameObject.tag == TagKeys.ZonePoint){
                    var deadZoneTarget = item.Find(deadZone);
                    var respawnTarget = item.Find(respawnZone);
                    //Debug.Log("deadZoneTarget = "+deadZoneTarget.GetInstanceID());
                    //Debug.Log("respawnTarget = "+respawnTarget.position);
                    if(!respawnData.ContainsKey(deadZoneTarget.GetInstanceID()))
                        respawnData.Add(deadZoneTarget.GetInstanceID(),respawnTarget.position);
                }
                if(item.gameObject.tag == TagKeys.ENDPOINT){
                    var endpoint = item.gameObject.GetComponent<Collider>();
                    // print(Depug.Log("Get in object name "+item.name,Color.blue));
                    // print(Depug.Log("instanceid "+item.transform.GetInstanceID(),Color.red));
                    if(!endpointData.ContainsKey(item.transform.GetInstanceID())){
                        var rawdata = new EndPointData{
                            instanceId = item.transform.GetInstanceID(),
                            isPass = false,
                            collider = endpoint,
                            position = item.position.x
                        };
                        //print(Depug.Log("endpoint raw data "+rawdata.position,Color.white));
                        endpointData.Add(item.transform.GetInstanceID(),rawdata);
                    }  
                }
            }
        }
        //Debug.Log("SetDeadZone 1 ");
        var sort = from enrty in endpointData orderby enrty.Value.position ascending select enrty;
        var firstEndPoint = sort.ElementAtOrDefault(0);
        var lastEndpoint = sort.ElementAtOrDefault(sort.Count()-1);
        if(lastEndpoint.Value != null && firstEndPoint.Value != null){
            startPoint = firstEndPoint.Value.position;
            finishPoint = lastEndpoint.Value.position;
            //OnSetDirection.OnNext(new Vector2(firstEndPoint.Value.position,lastEndpoint.Value.position));
        }else
        {
            Debug.LogError("Cannot found firstEndpoint or last endpoint");
        }
        //Debug.Log("SetDeadZone 2  Endpoint DAta "+endpointData.Count);
        GameplayManager.Instance.SetTotalRound(endpointData.Count);

        // foreach (var item in respawnData)
        // {
        //     Debug.Log(string.Format("Key {0} Value {1}",item.Key,item.Value));
        // }
        // Debug.Log("TotalCount "+respawnData.Count);
    }
    void SetCameraZone(){
        camerapointData.Clear();
        GameObject[] objZone = GameObject.FindGameObjectsWithTag(TagKeys.CAMERAZONE);//zoneObject.GetComponentsInChildren<Transform>();
        //Debug.Log("OBJ Zone length "+objZone.Length);
        foreach (var gameObj in objZone)
        {
            var trans = gameObj.GetComponentsInChildren<Transform>();
            foreach (var cameraTrans in trans)
            {
                if(!camerapointData.ContainsKey(cameraTrans.GetInstanceID())){
                    Quaternion rotate = Quaternion.Euler(cameraTrans.rotation.eulerAngles.x,cameraTrans.rotation.eulerAngles.y-180,cameraTrans.rotation.eulerAngles.z);
                    Debug.Log("Rotate =========+++++++ "+rotate);
                    Debug.Log("Rotate =========+++++++ "+cameraTrans.rotation.eulerAngles.x);
                    Debug.Log("Rotate =========+++++++ "+cameraTrans.rotation.eulerAngles.y);
                    Debug.Log("Rotate =========+++++++ "+cameraTrans.rotation.eulerAngles.z);
                    camerapointData.Add(cameraTrans.GetInstanceID(),rotate);
                }
            }

        }
    }

   
    
}
