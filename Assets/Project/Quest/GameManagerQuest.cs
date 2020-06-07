using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class Account
{

    public string playerName = "Player";
    public string bikeId = "1001";

    public Achievement dairyCollectStat;
    public Achievement accountCollectStat;
}

public class GameManagerQuest : MonoBehaviour {
    public static GameManagerQuest Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAccount = new Account();
        }
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString();
    }
    public ScriptableAccount AccountScriptable;
    public Account myAccount;
    public Dictionary<string, BestTime> dicBestTime = new Dictionary<string, BestTime>();

    public Text timerText;
    public string currentPlayMapId;
    public int mapLevel = 0;

    private void Start()
    {
        myAccount = AccountScriptable.GetScriptable();
        if(myAccount.accountCollectStat.listBestTime.Count > 0)
            dicBestTime = myAccount.accountCollectStat.listBestTime.ToDictionary(v => v.mapId, v => v);
        
    }
    private void SetScriptable()
    {
        myAccount.accountCollectStat.listBestTime = dicBestTime.Values.ToList();
        AccountScriptable.SetScriptable(myAccount);
    }
    public void SetAccountAchievement(Achievement ac1, Achievement ac2)
    {
        ac1.backFlip += ac2.backFlip;
        ac1.crash += ac2.crash;
        ac1.enegy += ac2.enegy;
        ac1.frontFlip += ac2.frontFlip;
        ac1.gold += ac2.gold;
        ac1.nitro += ac2.nitro;
        ac1.win += ac2.win;
        ac1.liftWheel = ac2.liftWheel;
        ac1.mapId = currentPlayMapId;
    }
    private float timer = 0;
    public void SetBestTime()
    {
        if (!dicBestTime.ContainsKey(currentPlayMapId))
        {
            dicBestTime.Add(currentPlayMapId, new BestTime());
            dicBestTime[currentPlayMapId].mapId = currentPlayMapId;
            dicBestTime[currentPlayMapId].bestTime[mapLevel] = timer;
        }
        if (dicBestTime[currentPlayMapId].bestTime[mapLevel] > timer)
        {
            dicBestTime[currentPlayMapId].bestTime[mapLevel] = timer;
        }
    }

    private void OnApplicationQuit()
    {
        SetScriptable();
    }
}
