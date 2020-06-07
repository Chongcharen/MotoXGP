using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Achievement
{
    public int race = 0;
    public int win = 0;
    public int lost = 0;

    public int lowGear = 0;
    public int jump = 0;
    public int nitro = 0;
    public int crash = 0;
    public int gold = 0;
    public int enegy = 0;
    public int frontFlip = 0;
    public int backFlip = 0;
    public float liftWheel = 0;
    public string mapId = "";
    public List<BestTime> listBestTime = new List<BestTime>();
}
[System.Serializable]
public class BestTime
{
    public string mapId = "";
    public float[] bestTime = new float[5] { 1000, 1000, 1000, 1000, 1000 };
}

public enum TypeQuest
{
    Map,
    FrontFlip,
    BackFlip,
    Win,
    Gold,
    Enegy,
    BestTime,
    Crash,
    Nitro,
    LiftWheel,
    BikeLevel,
    LowGear,
    Race,
}

public class ConditionQuest : MonoBehaviour {
    
    [System.Serializable]
    public class Quest
    {
        public string conditionText = "";
        public string Id = "";
        public int wantedValue = 0;
        public TypeQuest typeQuest = TypeQuest.Map;
    }

    public ItemQuest itemQuestPrefab;
    public Transform parentItem;

    public List<Quest> listQuest = new List<Quest>();
    public List<Quest> currentQuest = new List<Quest>();
    public List<ItemQuest> itemQuest = new List<ItemQuest>();
    
    public GameObject succesObj;
    public int amountQuest = 3;

    private void Start()
    {
        succesObj.SetActive(false);
        itemQuestPrefab.gameObject.SetActive(false);

        SetQuest();
    }
    private void SetQuest()
    {
        List<Quest> questWant = new List<Quest>();
        questWant = listQuest;
        for (int i = questWant.Count; i > amountQuest; i--)
        {
            int indexQuest = Random.Range(0, questWant.Count);
            questWant.RemoveAt(indexQuest);
        }
        foreach (var item in questWant)
        {
            var itemQuest = Instantiate(itemQuestPrefab, parentItem);
            itemQuest.itemText.text = item.conditionText;
            itemQuest.gameObject.SetActive(true);
        }
        currentQuest = questWant;
    }

    public void OnCheckDairyQuest()
    {
        foreach (var item in listQuest)
        {
            if (CheckAchievementDairyQuest(item))
            {
                Debug.Log(item.conditionText);
            }
        }
    }

    private bool CheckAchievementDairyQuest(Quest quest)
    {
        var gm = GameManagerQuest.Instance;
        var dairyCollectStat = GameManagerQuest.Instance.myAccount.dairyCollectStat;
        switch (quest.typeQuest)
        {   
            case TypeQuest.Map:
                if(gm.currentPlayMapId == quest.Id) //
                {
                    return true;
                }
                break;
            case TypeQuest.FrontFlip:
                if (dairyCollectStat.frontFlip >= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.BackFlip:
                if (dairyCollectStat.backFlip >= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.Win:
                if (dairyCollectStat.win >= quest.wantedValue && dairyCollectStat.mapId == quest.Id)
                {
                    return true;
                }
                break;
            case TypeQuest.Gold:
                if (dairyCollectStat.gold >= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.Enegy:
                if (dairyCollectStat.enegy >= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.BestTime:
                if (gm.dicBestTime[gm.currentPlayMapId].bestTime[gm.mapLevel] < quest.wantedValue && quest.Id == gm.currentPlayMapId)
                {
                    return true;
                }
                break;
            case TypeQuest.Crash:
                if (dairyCollectStat.crash <= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.Nitro:
                if (dairyCollectStat.nitro >= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.LiftWheel:
                if (dairyCollectStat.liftWheel >= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.Race:
                if (dairyCollectStat.race >= quest.wantedValue)
                {
                    return true;
                }
                break;
            case TypeQuest.LowGear:
                if (dairyCollectStat.lowGear >= quest.wantedValue)
                {
                    return true;
                }
                break;
            default:
                return false;
        }
        return false;
    }

    private IEnumerator SuccesQuest()
    {
        succesObj.SetActive(true);
        yield return new WaitForSeconds(4);
        succesObj.SetActive(false);
    }
}
