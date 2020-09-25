using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using Newtonsoft.Json;
using TMPro;
public class HierachyMng : MonoBehaviour, IPointerEnterHandler, IDragHandler, IPointerDownHandler, IEndDragHandler
{

    [SerializeField]Canvas mapCanvas;
    [SerializeField]GameObject mapSwipeObjectPrefab; 
    [SerializeField]Transform content;
    [SerializeField]Button b_EnterLevel;
    [SerializeField]Button b_forest,b_desert,b_beach;
    [SerializeField]Toggle t_forest,t_desert,t_sea;
    [SerializeField]ToggleGroup toggleGroup;
    [SerializeField]TMP_InputField input_nos;
    //Mockupdata
     MapMockupData mapMockupData;

    //Content Settings-------------------------------------------------------------------------------------------
    public Sprite Background;
    public RectTransform ScrollContent;
    public List<Transform> Page;
    public List<bool> PageShow;
    public Vector2 LayoutPosCenter;
    public Vector2 LayoutPosL;
    public Vector2 LayoutPosR;
    //public List<int> listPageSort;
    //-----------------------------------------------------------------------------------------------------------

    public float Element_Width;
    public float Element_Height;
    public float Element_Margin;
    public float Element_Scale;

    //int ChildrenNum;

    public int currentFocusIndex = 0;//should not more than Page.Count
    public float m_fCurrentFocusIndex = 0;
    public float slideSensitive = 1;
    public Vector2 startPos;
    public Vector2 LastPos;
    public Vector2 TouchPos;

    public float Transition_In;
    public float Transition_Out;

    public int indexL;
    public int indexR;
    public int rangeL = 1;
    public int rangeR = 1;

    public int currentL;
    public int currentR;

    public int extendR = 3;
    public float extendOffsetX = 100;
    public float extendOffsetZ = 50;

    bool startDrag = false;
    List<int> indexExtend = new List<int>();

    bool isRunning = false;
    int themeIndex = 0;
    int levelStar = 0;
    bool firstInit = true;

    // Use this for initialization
    void Start () {
        mapCanvas.ObserveEveryValueChanged(c => mapCanvas.enabled).Subscribe(active =>{
            if(active){
                SetupMapChoose();
                //Init();
            }
            else{
                ClearChildren();
                isRunning = false;
                firstInit = true;
            }
        });
        b_EnterLevel.OnClickAsObservable().Subscribe(_=>{

            if(string.IsNullOrEmpty(input_nos.text))
                input_nos.text = "3";
            levelStar = Page[currentFocusIndex].GetComponent<MapSwipeObjectPrefab>().GetStar+1;
            print(Depug.Log("Level "+levelStar,Color.blue));

            // ExitGames.Client.Photon.Hashtable roomOptions = new ExitGames.Client.Photon.Hashtable();
            // roomOptions.Add(RoomOptionKey.MAP_THEME,themeIndex);
            // roomOptions.Add(RoomOptionKey.MAP_STAGE,currentFocusIndex);
           
            //PhotonNetworkConsole.Instance.JoinRandomRoom(roomOptions);
            // ProtocolRoomProperty roomProperties = new ProtocolRoomProperty();
            // roomProperties.stage = currentFocusIndex;
            // roomProperties.theme = themeIndex;
            // roomProperties.level =  Page[currentFocusIndex].GetComponent<MapSwipeObjectPrefab>().GetStar;

            var roomProperties = new Bolt.Photon.PhotonRoomProperties();
            roomProperties.AddRoomProperty(RoomOptionKey.MAP_THEME,currentFocusIndex);
            roomProperties.AddRoomProperty(RoomOptionKey.MAP_STAGE,themeIndex);
            roomProperties.AddRoomProperty(RoomOptionKey.MAP_LEVEL,levelStar);
            roomProperties.IsOpen = true;
            roomProperties.IsVisible = true;
            roomProperties.CustomRoomPropertiesInLobby.Add(RoomOptionKey.MAP_THEME);
            roomProperties.CustomRoomPropertiesInLobby.Add(RoomOptionKey.MAP_STAGE);
            roomProperties.CustomRoomPropertiesInLobby.Add(RoomOptionKey.MAP_LEVEL);
            
            GameDataManager.Instance.SetUpGameLevel(themeIndex,currentFocusIndex,levelStar,System.Convert.ToInt32(input_nos.text));
            BoltLobbyNetwork.Instance.JoinRandomSession(roomProperties);
            PageManager.Instance.CloseMap();
            
            //mockup input_nos
            
            //ClearPage();
            
            Debug.Log("thene => "+themeIndex);
            Debug.Log("stage "+currentFocusIndex);
        });
        b_forest.OnClickAsObservable().Subscribe(_=>{
            themeIndex = 0;
            Init();
        });
        b_desert.OnClickAsObservable().Subscribe(_=>{
            themeIndex = 1;
            Init();
        });
        b_beach.OnClickAsObservable().Subscribe(_=>{
            themeIndex = 2;
            Init();
        });
        t_forest.onValueChanged.AddListener(active =>{
            if(!active)return;
            themeIndex = 0;
            Init();
        });
        t_desert.onValueChanged.AddListener(active =>{
            if(!active)return;
            themeIndex = 1;
            Init();
        });
        t_sea.onValueChanged.AddListener(active =>{
            if(!active)return;
            themeIndex = 2;
            Init();
        });
        //ChildrenNum = Page.Count;

        ////Flip all sibling index
        //for (int normalIndex = 0; normalIndex < Page.Count; normalIndex++)
        //{
        //    int newIndex = (Page.Count - 1) - (normalIndex);
        //    if (newIndex >= Page.Count)
        //        newIndex -= Page.Count;
        //    Page[normalIndex].GetComponent<Transform>().SetSiblingIndex(newIndex);
        //}

        
    }
    void SetupMapChoose(){
        GUIDebug.Log("SetupMapChoose state "+ GameDataManager.Instance.gameLevel.stage);
        GUIDebug.Log("SetupMapChoose  theme count"+ GameDataManager.Instance.GameLevelData.gameThemesData.Count);
        m_fCurrentFocusIndex = GameDataManager.Instance.gameLevel.stage;
        switch(GameDataManager.Instance.gameLevel.theme){
            case 0:
                if(t_forest.isOn)
                    Init();
                else
                    t_forest.isOn = true;
            break;
            case 1:
                if(t_desert.isOn)
                    Init();
                else
                    t_desert.isOn = true;
            break;
        }
    }
    void Init(){
        Debug.Log("themeIndex "+themeIndex);
        GenerateMapChoice();
        ShowHierachy();
        
    }
    void ClearPage(){
        for (int i = 0; i < Page.Count; i++)
        {
            if(Page[i] != null)
                Destroy(Page[i].gameObject);
        }
        Page.Clear();
    }
    void GenerateMapChoice(){
         GUIDebug.Log("GenerateMapChoice");
        ClearPage();
        MapMockupData mapMockupData = new MapMockupData();
        mapMockupData.Generatedata();
        //var serialized = JsonConvert.SerializeObject(mapMockupData.choiceMockupData);
        //JsonUtility.FromJson<GameLevelData>(jsonString);
        //var serialized = JsonUtility.ToJson(mapMockupData.choiceMockupData);//JsonConvert.SerializeObject(mapMockupData.choiceMockupData);
        var themeData = GameDataManager.Instance.GameLevelData.gameThemesData[themeIndex];
        var texture_map = Resources.Load<Texture2D>("Image/Forest");
        var img_sprite = Sprite.Create(texture_map,new Rect(0,0,texture_map.width,texture_map.height),new Vector2(0.5f,0.5f));
        GUIDebug.Log("themeData.gameStages.Count "+themeData.gameStages.Count);
        for (int i = 0; i < themeData.gameStages.Count; i++)
        {
            var go = Instantiate(mapSwipeObjectPrefab,Vector3.zero,Quaternion.identity,content);
            go.GetComponent<MapSwipeObjectPrefab>().Setup(themeData.gameStages[i],img_sprite);
             GUIDebug.Log("go "+go.name);
            if(firstInit && i == GameDataManager.Instance.gameLevel.stage){
                go.GetComponent<MapSwipeObjectPrefab>().SelectStar(GameDataManager.Instance.gameLevel.level);
                firstInit = false;
            }
            go.gameObject.SetActive(true);
            go.transform.localRotation = Quaternion.Euler(0,-13,0);
            Page.Add(go.transform);
        }
    }
    void ShowHierachy(){
        for (int b = 0; b < Page.Count; b++)
        {
            Page[b].GetComponent<RectTransform>().sizeDelta = new Vector2(Element_Width, Element_Height);
            //Page[b].GetComponent<RectTransform>().localPosition = new Vector3((2 * b + 3) * Element_Width / 2 + (2 * b + 3) * Element_Margin, 0, 10);
            PageShow.Add(false);
        }

        //MockUpdata 
        cal();
        fetchHierachy();
        isRunning = true;
    }
	// Update is called once per frame
	void Update () {
        if(!isRunning)return;
        fetchHierachy();
        indexExtend.Clear();
        for (int b = 0; b < Page.Count; b++)
        {
            PageShow[b] = false;
        }
        
        for (int b = currentFocusIndex; b <= currentFocusIndex + (extendR+rangeR); b++)
        {
            int tmp = b;
            if (tmp >= Page.Count)
                tmp = tmp % Page.Count;
            PageShow[tmp] = true;
        }
        for (int i = 1; i <= extendR+rangeR; i++)
        {
            indexExtend.Add((currentFocusIndex+i)%Page.Count);
        }

        for (int b = currentFocusIndex; b >= currentFocusIndex - rangeL; b--)
        {
            int tmp = b;
            if (tmp < 0)
                tmp = Page.Count + tmp;
            PageShow[tmp] = true;
        }

        for (int b = 0; b < Page.Count; b++)
        {
            if (b != currentFocusIndex)
            {
                
                if (PageShow[b])
                {
                    Page[b].transform.localScale = Vector2.Lerp(Page[b].transform.localScale, new Vector2(1, 1), Transition_Out);
                    //check L or R
                    if(currentL == b){
                        Page[b].transform.localPosition = Vector2.Lerp(Page[b].transform.localPosition, LayoutPosL, Transition_Out);
                    }
                }
                else
                {
                    Page[b].transform.localScale = new Vector2(0, 0);
                    Page[b].transform.localPosition = Vector2.Lerp(Page[b].transform.localPosition, LayoutPosCenter, Transition_Out);
                }
                Page[b].GetComponent<MapSwipeObjectPrefab>().DisabledInterActive();
            }
            else
            {
                Page[b].transform.localScale = Vector2.Lerp(Page[b].transform.localScale, new Vector2(Element_Scale, Element_Scale), Transition_In);
               // Page[b].interactable = true;
                Page[b].GetComponent<MapSwipeObjectPrefab>().b_Enter.interactable = true;
                Page[b].transform.localPosition = Vector2.Lerp(Page[b].transform.localPosition, LayoutPosCenter, Transition_In);
                Page[b].GetComponent<MapSwipeObjectPrefab>().EnabledInterActive();
            }
        }

        for (int i = 0; i < indexExtend.Count; i++)
        {
            Page[indexExtend[i]].transform.localPosition = Vector3.Lerp(Page[indexExtend[i]].transform.localPosition, new Vector3(LayoutPosR.x + (extendOffsetX*(i+1)),0,extendOffsetZ*(i+1)), Transition_Out);
        }

        if (EventSystem.current.IsPointerOverGameObject() == this)
        {
         //   Debug.Log("on %s",this);
        }
    }

    void fetchHierachy()
    {
        int currentSibIdx = (Page.Count - 1);
        int countingAssign = 0;
        
        for (int normalIndex = currentFocusIndex; countingAssign < Page.Count; normalIndex++, countingAssign++)
        {
            if(normalIndex >= Page.Count)
                normalIndex -= Page.Count;

            Page[normalIndex].GetComponent<Transform>().SetSiblingIndex(currentSibIdx);
            currentSibIdx--;
        }      
    }
    public void cal()
    {
        bool minus = false;
        if (m_fCurrentFocusIndex < 0)
        {
            minus = true;
            m_fCurrentFocusIndex *= -1;//convert to plus
        }
        m_fCurrentFocusIndex = m_fCurrentFocusIndex % Page.Count;//clamp in rage of page
        if (minus)//loop from minus
        {
            m_fCurrentFocusIndex = Page.Count - m_fCurrentFocusIndex;
        }
        currentFocusIndex = Mathf.RoundToInt(m_fCurrentFocusIndex);
        if (currentFocusIndex == Page.Count)//in case raw val 3.5 and rounded to 4 (it'd be 0)
            currentFocusIndex = 0;

        indexL = currentFocusIndex - (rangeL+1);
        if (indexL < 0)//loop
            indexL = Page.Count + indexL;

        currentL = currentFocusIndex - (rangeL);
        if (currentL < 0)//loop
            currentL = Page.Count + currentL;

        indexR = currentFocusIndex + (rangeR+1);
       // Debug.Log("indexR = "+indexL);
        if (indexR >= Page.Count)
            indexR %= Page.Count;

        currentR = currentFocusIndex + (rangeR);
        //Debug.Log("currentR = "+currentR);
        if (currentR >= Page.Count)
            currentR %= Page.Count;
    }
    private Vector2 pointerOffset;
    private RectTransform canvasRectTransform;
    public RectTransform panelRectTransform;

    void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.transform as RectTransform;
            panelRectTransform = transform/*.parent*/ as RectTransform;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        //panelRectTransform.SetAsLastSibling();
        // ติด ิีะะนื หน้า ถ้าหากกดโดน ทำให้การคำนวณผิด
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);

        Debug.Log("OnPointerDown");
        Debug.Log("pointerOffset");
        Debug.Log(pointerOffset);
        startPos = data.position;

        LastPos = startPos;
        Debug.Log("PointerDown laspos "+LastPos);

    }

    public void OnDrag(PointerEventData data)
    {
        if (panelRectTransform == null)
            return;
        Vector2 pointerPostion = data.position;
        TouchPos = pointerPostion;

        if(startDrag)
            m_fCurrentFocusIndex -= (pointerPostion.x - LastPos.x)* slideSensitive;
        else
            startDrag = true;
        LastPos.x = pointerPostion.x;//keep lastest pos
        cal();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        bool minus = false;
       // Debug.Log("before "+m_fCurrentFocusIndex);
        if (m_fCurrentFocusIndex < 0)
        {
            minus = true;
            m_fCurrentFocusIndex *= -1;//convert to plus
        }
        m_fCurrentFocusIndex = m_fCurrentFocusIndex % Page.Count;//clamp in rage of page
       
       // Debug.Log("after "+m_fCurrentFocusIndex);
        if (minus)//loop from minus
        {
            m_fCurrentFocusIndex = Page.Count - m_fCurrentFocusIndex;
        }

        currentFocusIndex = Mathf.RoundToInt(m_fCurrentFocusIndex);

        if (currentFocusIndex == Page.Count)//in case raw val 3.5 and rounded to 4 (it'd be 0)
            currentFocusIndex = 0;

        m_fCurrentFocusIndex = currentFocusIndex;
        startDrag = false;
    }
    public Vector3[] canvasCorners;
    Vector2 ClampToWindow(PointerEventData data)
    {
        Vector2 rawPointerPosition = data.position;

        canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);
        Debug.Log(canvasCorners);

        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
        return newPointerPosition;
    }
    void ClearChildren(){
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
    }

    

    
}
