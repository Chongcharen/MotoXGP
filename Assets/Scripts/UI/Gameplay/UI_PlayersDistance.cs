using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using UniRx;
using System.Linq;
public class UI_PlayersDistance : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField]GameObject root;
    [SerializeField]Transform transformParent;
    [SerializeField]GameObject playerSliderPrefab;
    [SerializeField]Slider playerSlider;
    [SerializeField]Color[] colorRank;
    [SerializeField]Image[] bgImageRank;
    [SerializeField]Text[] textRank;
    Dictionary<string,PlayerSliderPrefab> dic_playerSlider;
    Dictionary<string,PlayerDistanceData> dic_playerDistance;

    PlayerDistanceData myDistanceData,nextPlayerDistanceData;
    PlayerSliderPrefab mySliderPrefab;

    
    float startPoint,finishPoint;
    //List<PlayerDistanceData> playerDistanceList = new List<PlayerDistanceData>();
    string localUserId;
    void Awake()
    {
        GameNetwork.OnSetDirection.Subscribe(pos =>{
            startPoint = pos.x;
            finishPoint = pos.y;
            CreateSlider();
        });
    }
    void CreateSlider(){
        dic_playerSlider = new Dictionary<string, PlayerSliderPrefab>();
        dic_playerDistance = new Dictionary<string, PlayerDistanceData>();
        Hashtable property = PhotonNetwork.CurrentRoom.CustomProperties;
        Hashtable playerData = property[RoomPropertyKeys.PLAYER_DATA] as Hashtable;
        Hashtable playerIndexData = property[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        //Debug.Log("player index conatin >"+property.ContainsKey(RoomPropertyKeys.PLAYER_INDEX));
        //Debug.Log("playerIndexData conatin >"+property.ContainsKey(RoomPropertyKeys.PLAYER_DATA));
        foreach (var playerIndex in playerIndexData)
        {
            Debug.Log(string.Format("index key {0} vale {1}",playerIndex.Key,playerIndex.Value));
            var slider = Instantiate(playerSliderPrefab,Vector3.zero,Quaternion.identity,transformParent);
            slider.name = playerIndex.Key.ToString();
            slider.transform.localPosition = Vector3.zero;
            PlayerSliderPrefab sliderPrefab = slider.GetComponent<PlayerSliderPrefab>();
            sliderPrefab.GetSlider().minValue = startPoint;
            sliderPrefab.GetSlider().maxValue = finishPoint;
            
            dic_playerSlider.Add(playerIndex.Key.ToString(),sliderPrefab);
            var playerDistanceData = new PlayerDistanceData{
                playerIndex = int.Parse(playerIndex.Value.ToString()),
                userID = playerIndex.Key.ToString(),
                playerName = playerIndex.Key.ToString(),
                distance = startPoint
            };
            dic_playerDistance.Add(playerIndex.Key.ToString(),playerDistanceData);
            if(playerIndex.Key.ToString() == PhotonNetwork.LocalPlayer.NickName){
                localUserId = PhotonNetwork.LocalPlayer.NickName;
                myDistanceData = playerDistanceData;
                mySliderPrefab = sliderPrefab;
            }
            sliderPrefab.SetUpData(playerData,colorRank[playerDistanceData.playerIndex]);
        }
        if(mySliderPrefab != null)
            mySliderPrefab.transform.parent.SetAsLastSibling();
    }

    // Update is called once per frame
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged){
        //Debug.Log("OnRoomPropertiesUpdate "+propertiesThatChanged.Count);

        //Foreach ซ้อนกันเยอะ เอาออกไป อยุ่ hash roomproperty ดีมั้ย?
        foreach (var property in propertiesThatChanged)
        {
            
            if(property.Key.ToString() == RoomPropertyKeys.PLAYER_DATA){
                //Debug.Log(string.Format("property key {0} vale {1}",property.Key,property.Value));
                var playerData = property.Value as Hashtable;
                foreach (var data in playerData)
                {
                    //Debug.Log(string.Format("data key {0} vale {1}",data.Key,data.Value));
                    var keyName = data.Key.ToString();
                    if(dic_playerSlider.ContainsKey(keyName)){
                        var distance = System.Convert.ToSingle(data.Value.ToString());
                        dic_playerSlider[keyName].SetValue(distance);
                        SetDistance(keyName,distance);
                        SetPlayerRanking();
                    }
                }
            }
        }
    }
    void SetPlayerRanking(){
        //Debug.Log("SetplayerRanking************************************************");
        dic_playerDistance = dic_playerDistance.OrderByDescending(key => key.Value.distance).ToDictionary(k =>k.Key , v =>v.Value);
        var myranking = 0;
        var currentRanking = 0;

        for (int i = 0; i < dic_playerDistance.Count; i++)
        {   
            var playerDistanceData = dic_playerDistance.ElementAtOrDefault(i).Value;
            if(playerDistanceData == null)return;
            if(!bgImageRank[i].gameObject.activeSelf)  
                bgImageRank[i].gameObject.SetActive(true);
            bgImageRank[i].color = colorRank[playerDistanceData.playerIndex];
            textRank[i].text = (i+1)+". "+playerDistanceData.playerName;
            if(playerDistanceData.playerName == localUserId){
                myranking = i;
                if(i>0)
                    nextPlayerDistanceData = dic_playerDistance.ElementAtOrDefault(i-1).Value;
                else
                    nextPlayerDistanceData = null;

                if(nextPlayerDistanceData != null && (nextPlayerDistanceData.distance - playerDistanceData.distance) < 10){
                    mySliderPrefab.OverTaking();
                }else{
                    mySliderPrefab.CloseOverTake();
                }
            }
        }
    }
    void SetDistance(string key , float _value){
        // if(!dic_playerDistance.ContainsKey(key)){
        //     dic_playerDistance.Add(key,_value);
        // }
        if(!dic_playerDistance.ContainsKey(key)){
            Debug.LogError("Not found key "+key+" in dic_playerdistance");
        }
        dic_playerDistance[key].distance = _value;
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps){
        //targetPlayer.CustomProperties
    }
    
}
public class PlayerDistanceData{
    public int playerIndex;
    public string userID;
    public string playerName;
    public float distance;
}
