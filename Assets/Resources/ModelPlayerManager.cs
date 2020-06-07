//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class ModelPlayerManager : MonoBehaviour
//{
//    StoreItem storeItem;

//    [Header("Prefabs Parts")]
//    [SerializeField] GameObject bikeFrame;
//    [SerializeField] GameObject[] bikeBody;
//    [Header("End...")]

//    [SerializeField] GameObject bodyGroup;
//    [SerializeField] GameObject frameGroup;
//    [SerializeField] GameObject fbGroup;
//    [SerializeField] GameObject rbGroup;
//    [SerializeField] GameObject ragdollPrefab;
//    [SerializeField] Transform spawnPointRagdoll;

//    [SerializeField] MeshRenderer framesTexture;

//    [SerializeField] Transform chokePoint;
//    [SerializeField] Transform swingarmPoint;

//    public RagdollPlayer currentRagdoll;

//    public GameObject frontWheel;
//    public GameObject rearWheel;

//    //[Header("Camera Info")]
//    //public Transform groupCamPos;
//    //public Dictionary<string, Transform> dicCamPos = new Dictionary<string, Transform>();
//    //public Transform cameraGroup;

//    public float wheelSize = 0.727233f;

    
//    void Start()
//    {
//        Time.timeScale = 1;
//        storeItem = StoreItem.si;
//        SetModelLow();
//        SetupBike();
//    }

//    void SetModelLow()
//    {
//        Debug.Log(storeItem.bikeBody);

//        Debug.Log(storeItem.bodyTexture);
//        Debug.Log(storeItem.suitTexture);
//        Debug.Log(storeItem.helmetTexture);
//        Debug.Log(storeItem.glovesTexture);
//        Debug.Log(storeItem.bootsTexture);
//    }

//    void SetupBike()
//    {
//        var frame = Instantiate(bikeFrame, frameGroup.transform.position, Quaternion.identity);
//        frame.transform.parent = frameGroup.transform;

//        var body = Instantiate(bikeBody[storeItem.useBody]);
//        body.transform.parent = bodyGroup.transform;
//        var texture = StoreItem.instance.dicSticker[storeItem.bodyTexture].lTexture;
//        body.GetComponent<Renderer>().material = body.GetComponent<Material>();

//        body.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);

//        var _frame = frame.GetComponent<Frame>();

//        _frame.SetStricker(_frame.choke, texture);
//        _frame.SetStricker(_frame.swingarm, texture);
//        _frame.SetStricker(_frame.engine, texture);
//        _frame.SetStricker(_frame.hand, texture);

//        Transform frontPos = _frame.choke.transform.GetChild(0);
//        Transform rearPos = _frame.swingarm.transform.GetChild(0);

//        _frame.choke.transform.SetParent(chokePoint);
//        _frame.swingarm.transform.SetParent(swingarmPoint);

//        var front = Instantiate(frontWheel, frontPos.position, frontPos.rotation, transform);
//        front.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);
//        var rear = Instantiate(rearWheel, rearPos.position, rearPos.rotation, transform);
//        rear.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);
//        CreateRagdoll();

//        #region
//        /*
//        mcc.frontWheel = front.GetComponent<Rigidbody>();
//        mcc.rearWheel = rear.GetComponent<Rigidbody>();

//        front.GetComponent<ConfigurableJoint>().connectedBody = mcc.frontFork;
//        rear.GetComponent<ConfigurableJoint>().connectedBody = mcc.rearFork;
//        */

//        /*
//        if (LobbyGameManager.Instance != null)
//        {
//            var frame = Instantiate(infoBike.dicFrame[frameId].prefabLow, frameGroup.transform);
//            var lgm = LobbyGameManager.Instance;
            
//             bodyId = lgm.myAccount.bodyId;
//             solidId = lgm.myAccount.solidId;
//             stickerId = lgm.myAccount.stickerId;

//            frameId = lgm.myAccount.frameId;
//            helmetId = lgm.myAccount.helmetId;
//            helmetStrickerId = lgm.myAccount.helmetStickerId;
//            suitId = lgm.myAccount.suitId;
//            gloveId = lgm.myAccount.gloveId;
//            bootsId = lgm.myAccount.bootsId;

//            var body = Instantiate(infoBike.dicBody[bodyId].prefabLow, bodyGroup.transform);

//            //var bikePart = frame.GetComponentsInChildren<Transform>();
//            var _frame = frame.GetComponent<Frame>();
//            if (infoBike.dicColorSolid.ContainsKey(solidId))
//            {
//                ColorSolid fcs = infoBike.dicColorSolid[solidId];
//                var color = new Color32((byte)fcs.r, (byte)fcs.g, (byte)fcs.b, 255);
//                body.GetComponent<Renderer>().material = infoBike.solidMaterial;
//                body.GetComponent<Renderer>().material.color = color;

//                _frame.SetSolid(_frame.choke, color);
//                _frame.SetSolid(_frame.swingarm, color);
//                _frame.SetSolid(_frame.engine, color);
//            }
//            else if (infoBike.dicSticker.ContainsKey(stickerId))
//            {
//                var texture = infoBike.dicSticker[stickerId].textureLow;
//                body.GetComponent<Renderer>().material = infoBike.strickerMaterial;
//                body.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);

//                _frame.SetStricker(_frame.choke, texture);
//                _frame.SetStricker(_frame.swingarm, texture);
//                _frame.SetStricker(_frame.engine, texture);
//                _frame.SetStricker(_frame.hand, texture);
//            }

//            var mcc = GetComponent<Motorcycle_Controller>();

//            Transform frontPos = _frame.choke.transform.GetChild(0);
//            Transform rearPos = _frame.swingarm.transform.GetChild(0);

//            _frame.choke.transform.SetParent(chokePoint);
//            _frame.swingarm.transform.SetParent(swingarmPoint);

//            var front = Instantiate(frontWheel, frontPos.position, frontPos.rotation, transform);
//            front.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);
//            var rear = Instantiate(rearWheel, rearPos.position, rearPos.rotation, transform);
//            rear.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);

//            mcc.frontWheel = front.GetComponent<Rigidbody>();
//            mcc.rearWheel = rear.GetComponent<Rigidbody>();

//            front.GetComponent<ConfigurableJoint>().connectedBody = mcc.frontFork;
//            rear.GetComponent<ConfigurableJoint>().connectedBody = mcc.rearFork;
//            //mcc.InitMoto();
//            CreateRagdoll(true);          
//    }

//        else
//        {
//            return;
//        }       */
//        #endregion
//    }

//    public void CreateRagdoll()
//    {
//        currentRagdoll = Instantiate(ragdollPrefab.GetComponent<RagdollPlayer>(), spawnPointRagdoll.position, spawnPointRagdoll.rotation, this.transform);
//        currentRagdoll.InitRagdoll();

//        var hTexture = StoreItem.instance.dicHelmet[storeItem.helmetTexture].lTexture;
//        currentRagdoll.helmet.material.SetTexture("_MainTexture", hTexture);

//        var suitTexture = StoreItem.instance.dicSuit[storeItem.suitTexture].lTexture;
//        currentRagdoll.suit.material.SetTexture("_MainTexture", suitTexture);

//        var gloveTexture = StoreItem.instance.dicGlove[storeItem.glovesTexture].lTexture;
//        currentRagdoll.gloveL.material.SetTexture("_MainTexture", gloveTexture);
//        currentRagdoll.gloveR.material.SetTexture("_MainTexture", gloveTexture);

//        var bootTexture = StoreItem.instance.dicBoot[storeItem.bootsTexture].lTexture;
//        currentRagdoll.boots.material.SetTexture("_MainTexture", bootTexture);


//        //mcc.InitMoto();
//    }

//    public void ClearRagdollPlayer()
//    {
//        Destroy(currentRagdoll.gameObject);
//    }

    
//}
