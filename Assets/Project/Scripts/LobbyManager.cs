using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using ExitGames.Client.Photon;
using Hashtable = System.Collections.Hashtable;
using System.Linq;

public class CharacterGameplayer
{
    public int id;
    public string name;

    public CharacterGameplayer(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager lm;

    [SerializeField] ModelManager localModelManager;

    public GameObject mapObj;
    public GameObject roomObj;


    public Dictionary<Player,GameObject> dicPlayerObjs = new Dictionary<Player,GameObject>();

    public List<string> listMapName = new List<string>();

    [Header("Player Name")]
    public Text localPlayerNameText;
    public InputField inputPlayerName;
    public GameObject changeNameObj;
    

    [Header("UI ---")]
    public Transform playerListBox;
    public GameObject playerListPrefab;

    [Header("Custom")]
    public CustomListItem bikeItemList;
    public CustomListItem helmetItemList;
    public CustomListItem suitItemList;
    public CustomListItem glovesItemList;
    public CustomListItem bootsItemList;

    [Header("Map")]
    public CustomListItem forestItemList;
    public CustomListItem desertItemList;

    [Header("Room")]
    [SerializeField] internal GameObject startBtnObj;
    public GameObject [] playerSlot;
    [SerializeField] internal ModelManager[] modelsManager;
    [SerializeField] internal Text[] playersNameText;

    //private Dictionary<string, GameObject> dicMapObj = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if(lm != null)
        {
            Destroy(gameObject);
            return;
        }
        lm = this;
    }

    void Start ()
    {
        if (PlayerPrefs.GetString("Body") == "" ||
            PlayerPrefs.GetString("Helmet") == "" ||
            PlayerPrefs.GetString("Suit") == "" ||
            PlayerPrefs.GetString("Glove") == "" ||
            PlayerPrefs.GetString("Boot") == ""
            )
            SetDefaultPlayer();

        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();

        if (PlayerPrefs.GetString("PlayerName") == "")
            SetPlayerName();

        InitLocalPlayer();

        bikeItemList.InitItem(InfoBike.Instance.dicSticker);
        helmetItemList.InitItem(InfoCharacter.Instance.dicTextureHelmetInfo);
        suitItemList.InitItem(InfoCharacter.Instance.dicTextureSuitInfo);
        glovesItemList.InitItem(InfoCharacter.Instance.dicTextureGloveInfo);
        bootsItemList.InitItem(InfoCharacter.Instance.dicTextureBootsInfo);

        forestItemList.InitMap(InfoMap.Instance.dicMapInfo, "Forest");
        desertItemList.InitMap(InfoMap.Instance.dicMapInfo, "Desert");
    }

    private void SetDefaultPlayer()
    {
        PlayerPrefs.SetString("Body", "1001");
        PlayerPrefs.SetString("Helmet", "1001");
        PlayerPrefs.SetString("Suit", "1001");
        PlayerPrefs.SetString("Gloves", "1001");
        PlayerPrefs.SetString("Boots", "1001");
    }

    public void SetPlayerName()
    {
        changeNameObj.SetActive(true);
        if (inputPlayerName.text != "" && inputPlayerName.text.Length <= 14)
        {
            PlayerPrefs.SetString("PlayerName", inputPlayerName.text);
            changeNameObj.SetActive(false);
            InitLocalPlayer();
        }
    }

    public void InitLocalPlayer()
    {
        var playerName = PlayerPrefs.GetString("PlayerName");
        localPlayerNameText.text = playerName;
        PhotonNetwork.LocalPlayer.NickName = playerName;

        var cpt = new ExitGames.Client.Photon.Hashtable();
        cpt.Add("Body", PlayerPrefs.GetString("Body"));
        cpt.Add("Helmet", PlayerPrefs.GetString("Helmet"));
        cpt.Add("Suit", PlayerPrefs.GetString("Suit"));
        cpt.Add("Gloves", PlayerPrefs.GetString("Gloves"));
        cpt.Add("Boots", PlayerPrefs.GetString("Boots"));

        PhotonNetwork.LocalPlayer.SetCustomProperties(cpt);
        var ib = InfoBike.Instance;
        var ic = InfoCharacter.Instance;

        localModelManager.SetAllTexture(
            ib.GetBodyObjHigh(ib.dicSticker[PlayerPrefs.GetString("Body")].prefabId),
            ib.GetBodyTextureHigh(PlayerPrefs.GetString("Body")),
            ic.GetHelmetObjHigh(ic.dicTextureHelmetInfo[PlayerPrefs.GetString("Helmet")].prefabId),
            ic.GetHelmetTextureHigh(PlayerPrefs.GetString("Helmet")),
            ic.GetSuitTextureHigh(PlayerPrefs.GetString("Suit")),
            ic.GetGlovesTextureHigh(PlayerPrefs.GetString("Gloves")),
            ic.GetBootsTextureHigh(PlayerPrefs.GetString("Boots")));
    }
    public void SetCustomProperties(string type, object value)
    {
        PlayerPrefs.SetString(type, value.ToString());
        PhotonNetwork.LocalPlayer.CustomProperties[type] = value;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected master server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnQuickJoin()
    {
        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RandomMap();
    }
    private void RandomMap()
    {
        var dicMapInfo = InfoMap.Instance.dicMapInfo;
        var listMap = dicMapInfo.Values.ToList();
        var rd = Random.Range(0, dicMapInfo.Count - 1);
        PlayerPrefs.SetString("MapId", listMap[rd].Id);
        CreateRoom();
    }

    public void CreateRoom()
    {
        int rn = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSetting.Instance.maxPlayer };

        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add("MapId", PlayerPrefs.GetString("MapId"));

        PhotonNetwork.CreateRoom("Room" + PlayerPrefs.GetString("PlayerName")+ rn, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        RandomMap();
    }

    public void OnLeaveClick()
    {
        PhotonRoom.room.LeaveRoom();
    }

    public void OnStartClick()
    {
        PhotonRoom.room.StartGame();
    }

    //internal void AddPlayerInRoom(Player playerPhoton)
    //{
    //    var player = Instantiate(playerListPrefab, playerListBox);
    //    player.SetActive(true);
    //    player.transform.GetChild(0).GetComponent<Text>().text = playerPhoton.NickName;
    //    dicPlayerObjs.Add(playerPhoton, player);
    //}

    //public void PlayerLeaveRoom(Player playerPhoton)
    //{
    //    Destroy(dicPlayerObjs[playerPhoton]);
    //    dicPlayerObjs.Remove(playerPhoton);
    //}

    private void SetStringItemLocalPlayer()
    {
       // PhotonNetwork.SetPlayerCustomProperties("Helmet", "as");
    }

    
    public void BackMenu()
    {
        InitLocalPlayer();
    }
}