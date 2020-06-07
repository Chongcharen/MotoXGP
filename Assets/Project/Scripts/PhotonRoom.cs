using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System;

public class PhotonRoom : MonoBehaviourPunCallbacks ,IInRoomCallbacks {

	public static PhotonRoom room;
    public MultiplayerSetting mps;

    private PhotonView pv;

    public bool isGameLoaded;
    public int currentScene;

    private Player[] photonPlayers;


    public int playersInRoom;
    public int myNumberInRoom;

    public int playersInGame;

    private bool readyToCount;
    private bool readyToStart;

    public float startingTime;
    public float lessThanMaxPlayers;

    private float atMaxPlayer;
    private float timeToStart;

    internal string mapId;



    private void Awake()
    {
        if (room != null)
        {
            Destroy(gameObject);
            return;
        }
        room = this;
        DontDestroyOnLoad(gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        mps = MultiplayerSetting.Instance;
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayer = 3;
        timeToStart = startingTime;
    }

    public override void OnJoinedRoom()
    {
        var lm = LobbyManager.lm;
        base.OnJoinedRoom();
        if (PhotonNetwork.IsMasterClient)
            lm.startBtnObj.SetActive(true);
        else
            lm.startBtnObj.SetActive(false);

        lm.roomObj.SetActive(true);
        lm.mapObj.SetActive(false);

        Debug.Log("In a Room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;

        mapId = PhotonNetwork.CurrentRoom.CustomProperties["MapId"].ToString();

        var cpt = PhotonNetwork.LocalPlayer.CustomProperties;
        cpt.Add("Number", myNumberInRoom);
        PhotonNetwork.LocalPlayer.SetCustomProperties(cpt);

        SetupModelRoom();

        if (mps.delayStart)
        {
            Debug.Log("Display out of max");
            if (playersInRoom >= 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == mps.maxPlayer)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
                //countStartText.gameObject.SetActive(true);
            }
        }
    }

    private void SetupModelRoom()
    {
        if (LobbyManager.lm != null)
        {
            var lm = LobbyManager.lm;
            var ib = InfoBike.Instance;
            var ic = InfoCharacter.Instance;

            for (int i = 0; i < 4; i++)
            {
                lm.playerSlot[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < photonPlayers.Length; i++)
            {
                lm.playerSlot[i].gameObject.SetActive(true);
                if (photonPlayers[i] != null)
                {
                    var cpt = photonPlayers[i].CustomProperties;
                    
                    lm.modelsManager[i].SetAllTexture(
                        ib.GetBodyObjHigh(ib.dicSticker[cpt["Body"].ToString()].prefabId),
                        ib.GetBodyTextureHigh(cpt["Body"].ToString()),
                        ic.GetHelmetObjHigh(ic.dicTextureHelmetInfo[cpt["Helmet"].ToString()].prefabId),
                        ic.GetHelmetTextureHigh(cpt["Helmet"].ToString()),
                        ic.GetSuitTextureHigh(cpt["Suit"].ToString()),
                        ic.GetGlovesTextureHigh(cpt["Gloves"].ToString()),
                        ic.GetBootsTextureHigh(cpt["Boots"].ToString())
                        );

                    lm.playersNameText[i].text = photonPlayers[i].NickName;
                }
            }
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        Debug.Log("new player join");


        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;

        SetupModelRoom();
        if (mps.delayStart)
        {
            Debug.Log("Display max players possible" + playersInRoom + ":" + mps.maxPlayer);
            if (playersInRoom > 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == mps.maxPlayer)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    private void Update()
    {
        if (mps.delayStart)
        {
            if (playersInRoom == 1)
                RestartTimer();
            if (!isGameLoaded)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
                else if (readyToCount)
                {
                    Debug.Log("start to the players" + timeToStart);
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                if (timeToStart <= 0)
                {
                    StartGame();
                }
            }
        }
    }

    public void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(mps.muliplayerScene);
    }

    private void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 3;
        readyToCount = false;
        readyToStart = false;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        Debug.Log("FinLoadScene");
        if (currentScene == mps.muliplayerScene)
        {
            GameSetup.gs.Init();
            isGameLoaded = true;

            if (mps.delayStart)
            {
                Debug.Log("Fin_LoadGame");
                pv.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                RPC_CreatePlayer();
                Debug.Log("Cre");
            }
        }
    }
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            pv.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        Debug.Log("RPCCreatePre");
        var player = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Gameplay", "PlayerPrefab"), Vector3.zero ,Quaternion.identity,0);
        player.GetComponent<PhotonPlayer>().Init();
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        PhotonNetwork.LeaveRoom();
        if(currentScene != mps.menuScene)
            PhotonNetwork.LoadLevel(mps.menuScene);

        if (!PhotonNetwork.InRoom && LobbyManager.lm != null)
        {
            LobbyManager.lm.InitLocalPlayer();
            LobbyManager.lm.roomObj.SetActive(false);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;

        SetupModelRoom();
        playersInGame--;
    }
}
