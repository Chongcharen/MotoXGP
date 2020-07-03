using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSetup : MonoBehaviour {

    private PhotonView pv;
    public Text nameText;

    public Frame _frame;

    [SerializeField] Transform bodyPoint;
    [SerializeField] Transform ragdollPoint;
    [Header("Frame")]
    [SerializeField] MeshRenderer frame;
    [SerializeField] MeshRenderer engine;
    [SerializeField] MeshRenderer hand;
    [SerializeField] MeshRenderer choke;
    [SerializeField] MeshRenderer swingarm;
    [SerializeField] MeshRenderer wheelRear;
    [SerializeField] MeshRenderer wheelFront;

    private RagdollPlayer currentRagdoll;
    private ExitGames.Client.Photon.Hashtable cpt;

    private Motorcycle_Controller mcc;
    private string[] ragId;

    private void Start()
    {
        ragId = new string[5] { "1001", "1001", "1001", "1001", "1001" };
        pv = GetComponent<PhotonView>();
        mcc = GetComponent<Motorcycle_Controller>();

        if (PhotonRoom.room == null || pv.IsMine)
        {
            //FindObjectOfType<SmoothFollow>().target = transform;
            //FindObjectOfType<MotoUiGameplay>().mcc = GetComponent<Motorcycle_Controller>();
        }
        else
        {
            mcc.rearWheel.GetComponent<SphereCollider>().material = null;
        }

        if (nameText)
            nameText.text = pv.Owner.NickName;
        
        //if (PhotonRoom.room != null)
        //{
        //    cpt = pv.Owner.CustomProperties;
        //    ragId[0] = cpt["Body"].ToString();
        //    ragId[1] = cpt["Helmet"].ToString();
        //    ragId[2] = cpt["Suit"].ToString();
        //    ragId[3] = cpt["Gloves"].ToString();
        //    ragId[4] = cpt["Boots"].ToString();
        //}
        //else
        //{
        //    ragId[0] = PlayerPrefs.GetString("Body");
        //    ragId[1] = PlayerPrefs.GetString("Helmet");
        //    ragId[2] = PlayerPrefs.GetString("Suit");
        //    ragId[3] = PlayerPrefs.GetString("Gloves");
        //    ragId[4] = PlayerPrefs.GetString("Boots");
        //}
        SetupBike();
        CreateRagdoll();
    }

    void SetupBike()
    {
        var ib = InfoBike.Instance;
        Texture texture = ib.dicSticker[ragId[0]].textureLow;
        GameObject bodyPrefab = ib.dicBody[ib.dicSticker[ragId[0]].prefabId].prefabLow;

        var body = Instantiate(bodyPrefab, bodyPoint);
        
        body.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);

        _frame.SetStricker(choke, texture);
        _frame.SetStricker(swingarm, texture);
        _frame.SetStricker(engine, texture);
        _frame.SetStricker(hand, texture);

        #region
        //Transform frontPos = _frame.choke.transform.GetChild(0);
        //Transform rearPos = _frame.swingarm.transform.GetChild(0);

        //_frame.choke.transform.SetParent(chokePoint);
        //_frame.swingarm.transform.SetParent(swingarmPoint);

        //var front = Instantiate(frontWheel, frontPos.position, frontPos.rotation, transform);
        //front.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);
        //var rear = Instantiate(rearWheel, rearPos.position, rearPos.rotation, transform);
        //rear.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);

        /*
        mcc.frontWheel = front.GetComponent<Rigidbody>();
        mcc.rearWheel = rear.GetComponent<Rigidbody>();

        front.GetComponent<ConfigurableJoint>().connectedBody = mcc.frontFork;
        rear.GetComponent<ConfigurableJoint>().connectedBody = mcc.rearFork;
        */

        /*
        if (LobbyGameManager.Instance != null)
        {
            var frame = Instantiate(infoBike.dicFrame[frameId].prefabLow, frameGroup.transform);
            var lgm = LobbyGameManager.Instance;
            
             bodyId = lgm.myAccount.bodyId;
             solidId = lgm.myAccount.solidId;
             stickerId = lgm.myAccount.stickerId;

            frameId = lgm.myAccount.frameId;
            helmetId = lgm.myAccount.helmetId;
            helmetStrickerId = lgm.myAccount.helmetStickerId;
            suitId = lgm.myAccount.suitId;
            gloveId = lgm.myAccount.gloveId;
            bootsId = lgm.myAccount.bootsId;

            var body = Instantiate(infoBike.dicBody[bodyId].prefabLow, bodyGroup.transform);

            //var bikePart = frame.GetComponentsInChildren<Transform>();
            var _frame = frame.GetComponent<Frame>();
            if (infoBike.dicColorSolid.ContainsKey(solidId))
            {
                ColorSolid fcs = infoBike.dicColorSolid[solidId];
                var color = new Color32((byte)fcs.r, (byte)fcs.g, (byte)fcs.b, 255);
                body.GetComponent<Renderer>().material = infoBike.solidMaterial;
                body.GetComponent<Renderer>().material.color = color;

                _frame.SetSolid(_frame.choke, color);
                _frame.SetSolid(_frame.swingarm, color);
                _frame.SetSolid(_frame.engine, color);
            }
            else if (infoBike.dicSticker.ContainsKey(stickerId))
            {
                var texture = infoBike.dicSticker[stickerId].textureLow;
                body.GetComponent<Renderer>().material = infoBike.strickerMaterial;
                body.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);

                _frame.SetStricker(_frame.choke, texture);
                _frame.SetStricker(_frame.swingarm, texture);
                _frame.SetStricker(_frame.engine, texture);
                _frame.SetStricker(_frame.hand, texture);
            }

            var mcc = GetComponent<Motorcycle_Controller>();

            Transform frontPos = _frame.choke.transform.GetChild(0);
            Transform rearPos = _frame.swingarm.transform.GetChild(0);

            _frame.choke.transform.SetParent(chokePoint);
            _frame.swingarm.transform.SetParent(swingarmPoint);

            var front = Instantiate(frontWheel, frontPos.position, frontPos.rotation, transform);
            front.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);
            var rear = Instantiate(rearWheel, rearPos.position, rearPos.rotation, transform);
            rear.transform.localScale = new Vector3(wheelSize, wheelSize, wheelSize);

            mcc.frontWheel = front.GetComponent<Rigidbody>();
            mcc.rearWheel = rear.GetComponent<Rigidbody>();

            front.GetComponent<ConfigurableJoint>().connectedBody = mcc.frontFork;
            rear.GetComponent<ConfigurableJoint>().connectedBody = mcc.rearFork;
            //mcc.InitMoto();
            CreateRagdoll(true);          
    }

        else
        {
            return;
        }       */
        #endregion
    }

    public void CreateRagdoll()
    {
        var ic = InfoCharacter.Instance;
        currentRagdoll = Instantiate(GameSetup.gs.ragdollPrefab.GetComponent<RagdollPlayer>(), ragdollPoint.position, ragdollPoint.rotation, transform);

        var helmet = Instantiate(ic.GetHelmetObjLow(ic.dicTextureHelmetInfo[ragId[1]].prefabId), currentRagdoll.helmetPoint.position, currentRagdoll.helmetPoint.rotation, currentRagdoll.helmetPoint);

        helmet.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].SetTexture("_Albedo", ic.GetHelmetTextureLow(ragId[1]));

        currentRagdoll.suit.materials[0].SetTexture("_Albedo", ic.GetSuitTextureLow(ragId[2]));
        currentRagdoll.gloves.materials[0].SetTexture("_Albedo", ic.GetGlovesTextureLow(ragId[3]));
        currentRagdoll.boots.materials[0].SetTexture("_Albedo", ic.GetBootsTextureLow(ragId[4]));

        currentRagdoll.InitRagdoll();
        mcc.InitMoto();
    }


    public void ClearRagdollPlayer()
    {
        Destroy(currentRagdoll.gameObject);
    }

    public void PlayerGoal()
    {
        Debug.Log("Gold");
        MotoUiGameplay.Instance.SetupScorePanel(GameSetup.gs.listPlayerGoal.Count + 1);
        pv.RPC("RPC_PlayerGoal", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void RPC_PlayerGoal()
    {
        GameSetup.gs.listPlayerGoal.Add(pv.Owner);
    }


}
