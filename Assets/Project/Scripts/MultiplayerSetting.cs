using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSetting : MonoBehaviour {

    public static MultiplayerSetting Instance;

    public bool delayStart;
    public int maxPlayer = 0;

    public int menuScene;
    public int muliplayerScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        PhotonRoom.room.mps = this;
    }
}
