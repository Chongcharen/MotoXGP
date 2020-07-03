using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotoUiGameplay : MonoBehaviour
{
    public static MotoUiGameplay Instance { get; protected set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //gm = GameplayManager.Instance;
    }

    public GameObject[] panels;
    public Dictionary<string, GameObject> dicPanel = new Dictionary<string, GameObject>();

    public Motorcycle_Controller mcc;

    [Header("Score")]
    public GameObject scorePanel; 
    public Text orderText;
    public Text orderTextshadow;

    //public Text fpsText;

    //public Text timerText;
    //public Text countDownStartGameText;

    //public GameObject spawnTestBtn;

    public InputPad accelerateInput;
    public InputPad breakInput;
    public InputPad rollLeftInput;
    public InputPad rollRightInput;
    public InputPad jumpInput;
    
    /*
    [Header("UI Animation Active")]
    //public AnimController boostBtnAnim;


    public float value;

    public bool pressStart = false;
    public bool isStartTimer = false;
    private bool nitroReady = false;

    [Header("UI Player Ranking")]
    public Transform playerRanking;
    [SerializeField]
    public List<Transform> listPlayerRanking = new List<Transform>();
    [SerializeField]
    private List<RectTransform> listIconPlayer = new List<RectTransform>();
    private Vector3 startPos;
    private float distanceEndPoint;
    private float sizeWidthRanking;

    [SerializeField] Image nosReload;

    [Header("UI Accelerate Btn")]
    public GameObject[] gearBikeObjs;
    public Image progressGearBike;
        */
    //private GameplayManager gm;
    /*
    public void SetPlayerRanking(MapManager map)
    {
        startPos = map.spawnPointGroup.position;
        Vector3 endPos = map.currentEndPoint.position;
        distanceEndPoint = Vector3.Distance(startPos, endPos);
    }
    */
    public void SetPosUI()
    {    
      //  sizeWidthRanking = listIconPlayer[0].GetComponent<RectTransform>().sizeDelta.x;
    }

    private void Update()
    {
        if (!mcc)
            return;
        /*
        if (!gm.isPlatformMobile)
        {
            /*
            if (Input.GetKeyDown(KeyCode.E))
                BoostGear();
            if (Input.GetAxisRaw("Vertical") > 0)
                mcc.accelerate = true;
            else
                mcc.accelerate = false;

            if (Input.GetAxisRaw("Vertical") < 0)
                mcc.brake = true;
            else
                mcc.brake = false;

            if (Input.GetAxisRaw("Horizontal") < 0)
                mcc.left = true;
            else
                mcc.left = false;

            if (Input.GetAxisRaw("Horizontal") > 0)
                mcc.right = true;
            else
                mcc.right = false;

            if (mcc.left || mcc.right)
                mcc.leftORright = true;
            if (Input.GetKey(KeyCode.F))
                mcc.Jump();
            if (Input.GetKeyDown(KeyCode.R))
                mcc.ForceBoost(60f);
                */
                /*
        }
        else
        {
            ControllerBike();
        }
        */
        ControllerBike();
        /*
        for (int i = 0; i < listPlayerRanking.Count; i++)
        {
            float distancePlayer = Vector3.Distance(listPlayerRanking[i].localPosition, startPos);
            float posX = (distancePlayer * sizeWidthRanking) / distanceEndPoint*18;
            listIconPlayer[i].localPosition = new Vector2(posX, 0);
        }*/
    }

    private void ControllerBike()
    {
        /*
        if (!gm.isGameStart)
            return;
*/
        mcc.accelerate = accelerateInput.isDown;
        mcc.brake = breakInput.isDown;
        mcc.left = rollLeftInput.isDown;
        mcc.right = rollRightInput.isDown;

        if (accelerateInput.isTap)
        {
            BoostGear();
            accelerateInput.ReTapPad();
        }

        if (jumpInput.isDown)
            mcc.Jump();
    }

    public void Throttle(bool isDown)
    {
        mcc.accelerate = isDown;
    }
    public void BoostGear()
    {
        mcc.LowGearForceBike();
    }
    public void Break(bool isDown)
    {
        mcc.brake = isDown;
    }
    public void RollLeft(bool isDown)
    {

        mcc.left = isDown;
    }
    public void RollRight(bool isDown)
    {
        mcc.right = isDown;
    }

    public void OnClickJunp()
    {
        mcc.Jump();
    }


    public void SetupScorePanel(int order)
    {
        orderText.text = order.ToString();
        orderTextshadow.text = order.ToString();
    }
}
