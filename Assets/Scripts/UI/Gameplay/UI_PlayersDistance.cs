using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using UniRx;
using System.Linq;
using TMPro;
using DG.Tweening;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using Bolt;
public class UI_PlayersDistance : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject root;
    [SerializeField]Transform transformParent;
    [SerializeField]GameObject playerSliderPrefab;
    [SerializeField]Slider playerSlider;
    [SerializeField]TextMeshProUGUI round_txt;
    [SerializeField]Color[] colorRank;
    [SerializeField]Image[] bgImageRank;
    [SerializeField]Text[] textRank;
    Dictionary<BoltEntity,PlayerSliderPrefab> dic_playerSlider;
    Dictionary<BoltEntity,PlayerDistanceData> dic_playerDistance;

    PlayerDistanceData myDistanceData,nextPlayerDistanceData;
    PlayerSliderPrefab mySliderPrefab;

    
    float startPoint,finishPoint;
    //List<PlayerDistanceData> playerDistanceList = new List<PlayerDistanceData>();
    string localUserId;
    void Start(){
        print(Depug.Log("PlayerDistance Start ",Color.white));
        dic_playerSlider = new Dictionary<BoltEntity, PlayerSliderPrefab>();
        dic_playerDistance = new Dictionary<BoltEntity, PlayerDistanceData>();
        startPoint = MapManager.Instance.startPoint;
        finishPoint = MapManager.Instance.finishPoint;

        Debug.Log("StartPoint "+startPoint);
        Debug.Log("Finish Point "+finishPoint);
        //CreateSlider();
        // MapManager.OnSetDirection.Subscribe(pos =>{
        //     startPoint = pos.x;
        //     finishPoint = pos.y;
        //     CreateSlider();
        // });
        GameplayManager.Instance.round.ObserveEveryValueChanged(r => r.Value).Subscribe(_=>{
            Debug.Log("Round change "+_);
            if(_<= 0 || _> GameplayManager.Instance.totalRound.Value)return;
            round_txt.text = "Round "+_+"/"+GameplayManager.Instance.totalRound.Value;
            var sequence = DOTween.Sequence();
            sequence.Append(round_txt.DOFade(1,0.5f))
            .AppendInterval(5)
            .Append(round_txt.DOFade(0,0.5f)).SetAutoKill();
        }).AddTo(this);
        // PhotonCustomTransformView.OnPlayerMovement.Subscribe(tupleValue =>{
        //         SetDistance(tupleValue.Item1,tupleValue.Item2);
        //     }).AddTo(this);
        GameCallback.OnEntityAttached.Subscribe(entity =>{
            RegisterPlayer(entity);
        });
        GameCallback.OnEntityDetached.Subscribe(entity =>{
            UnregisterPlayer(entity);
        });
    }
    void CreateSlider(){
        // Debug.Log("createSlider ");
       
        // Hashtable property = PhotonNetwork.CurrentRoom.CustomProperties;
        // Hashtable playerData = property[RoomPropertyKeys.PLAYER_DATA] as Hashtable;
        // Hashtable playerIndexData = property[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;

        // Debug.LogWarningFormat("playerindex data {0}",playerIndexData);
        // /// เกมถัดไปจากครั้งแรกมักเจอ error
        //  foreach (var playerIndex in playerIndexData)
        // {
        //     Debug.Log(string.Format("index key {0} vale {1}",playerIndex.Key,playerIndex.Value));
        //     var slider = Instantiate(playerSliderPrefab,Vector3.zero,Quaternion.identity,transformParent);
        //     Debug.Log("transformParent "+transformParent);
        //     Debug.Log("Slider "+slider);
        //     slider.name = playerIndex.Key.ToString();
        //     slider.transform.localPosition = Vector3.zero;
        //     PlayerSliderPrefab sliderPrefab = slider.GetComponent<PlayerSliderPrefab>();
        //     sliderPrefab.GetSlider().minValue = startPoint;
        //     sliderPrefab.GetSlider().maxValue = finishPoint;
        //     Debug.Assert(!dic_playerSlider.ContainsKey(playerIndex.Key.ToString()));
        //     if(dic_playerSlider.ContainsKey(playerIndex.Key.ToString())){
        //         Debug.Log("Destroy dic_playerslider");
        //         Destroy(dic_playerSlider[playerIndex.Key.ToString()].gameObject);
        //         dic_playerSlider.Remove(playerIndex.Key.ToString());
        //     }
        //     dic_playerSlider.Add(playerIndex.Key.ToString(),sliderPrefab);
        //     var playerIndexProfileData = JsonConvert.DeserializeObject<PlayerIndexProfileData>(playerIndex.Value.ToString());
        //     var playerProfileModel = GameUtil.ConvertToPlayFabPlayerProfilemodel(playerIndexProfileData.profileModel);
            

        //     var playerDistanceData = new PlayerDistanceData{
        //         playerIndex = playerIndexProfileData.index,
        //         userID = playerIndexProfileData.userId,
        //         playerName = playerProfileModel.DisplayName,
        //         distance = startPoint
        //     };
        //     if(dic_playerDistance.ContainsKey(playerIndex.Key.ToString())){
        //         Debug.Log("remove dic_playerslider");
        //         dic_playerDistance[playerIndex.Key.ToString()] = null;
        //         dic_playerDistance.Remove(playerIndex.Key.ToString());
        //     }
        //     print(Depug.Log("CheckplayerKey "+playerIndex.Key,Color.white));
        //     print(Depug.Log("dic_playerDistance null? "+dic_playerDistance,Color.white));
            
            

        //     dic_playerDistance.Add(playerIndex.Key.ToString(),playerDistanceData);
        //     print(Depug.Log("dic_playerDistance containkey? "+dic_playerDistance.ContainsKey(playerIndex.Key.ToString()),Color.green));
        //     if(playerIndex.Key.ToString() == PhotonNetwork.LocalPlayer.UserId){
        //         localUserId = PhotonNetwork.LocalPlayer.UserId;
        //         myDistanceData = playerDistanceData;
        //         mySliderPrefab = sliderPrefab;
        //     }
        //     sliderPrefab.SetUpData(playerData,colorRank[playerDistanceData.playerIndex]);
        // }
        // if(mySliderPrefab != null)
        //     mySliderPrefab.transform.parent.SetAsLastSibling();


        // print(Depug.Log("stripstringkey = "+PhotonNetwork.CurrentRoom.CustomProperties.StripToStringKeys(),Color.red));
    }
    public void RegisterPlayer(BoltEntity entity){
        var token = entity.AttachToken as PlayerProfileToken;
        var data = token.playerProfileModel;
        var slider = Instantiate(playerSliderPrefab,Vector3.zero,Quaternion.identity,transformParent);
            slider.name = data.DisplayName;
            slider.transform.localPosition = Vector3.zero;
            PlayerSliderPrefab sliderPrefab = slider.GetComponent<PlayerSliderPrefab>();
            sliderPrefab.GetSlider().minValue = startPoint;
            sliderPrefab.GetSlider().maxValue = finishPoint;
            //Debug.Assert(!dic_playerSlider.ContainsKey(playerIndex.Key.ToString()));
            // if(dic_playerSlider.ContainsKey(data.PlayerId){
            //     Debug.Log("Destroy dic_playerslider");
            //     Destroy(dic_playerSlider[data.PlayerId].gameObject);
            //     dic_playerSlider.Remove(data.PlayerId);
            // }
            dic_playerSlider.Add(entity,sliderPrefab);
             if(entity.IsControlled){
                localUserId = data.PlayerId;
               // myDistanceData = playerDistanceData;
                mySliderPrefab = sliderPrefab;
            }
            sliderPrefab.SetUpData(colorRank[token.playerBikeData.runningTrack]);
    }
    void UnregisterPlayer(BoltEntity entity){
        if(dic_playerSlider.ContainsKey(entity)){
            Destroy(dic_playerSlider[entity].gameObject);
            dic_playerSlider.Remove(entity);
        }

    }
    // Update is called once per frame
    // public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged){
    //     //Foreach ซ้อนกันเยอะ เอาออกไป อยุ่ hash roomproperty ดีมั้ย?
    //     foreach (var property in propertiesThatChanged)
    //     {
    //         Debug.Log("*****property change "+property.Key+" Value "+property);
    //         // if(property.Key.ToString() == RoomPropertyKeys.PLAYER_DATA){
    //         //     var playerData = property.Value as Hashtable;
    //         //     foreach (var data in playerData)
    //         //     {
    //         //         var keyName = data.Key.ToString();
    //         //         if(dic_playerSlider == null)return;
    //         //         if(dic_playerSlider.ContainsKey(keyName)){
    //         //             var distance = System.Convert.ToSingle(data.Value.ToString());
    //         //             dic_playerSlider[keyName].SetValue(distance);
    //         //             SetDistance(keyName,distance);
    //         //             SetPlayerRanking();
    //         //         }
    //         //     }
    //         // }
    //     }
    //}

    
    void FixedUpdate(){
        SetPlayerRanking();
        foreach (var item in dic_playerSlider)
        {
            item.Value.SetValue(item.Key.transform.position.x);
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
            textRank[i].text = playerDistanceData.playerName;
            if(playerDistanceData.playerName == localUserId){
                myranking = i;
                if(i>0)
                    nextPlayerDistanceData = dic_playerDistance.ElementAtOrDefault(i-1).Value;
                else
                    nextPlayerDistanceData = null;

                // if(nextPlayerDistanceData != null && (nextPlayerDistanceData.distance - playerDistanceData.distance) < 10){
                //     mySliderPrefab.OverTaking();
                // }else{
                //     mySliderPrefab.CloseOverTake();
                // }
            }
        }
    }

    public void SetDistance(string key , float _value){
        // if(!dic_playerDistance.ContainsKey(key)){
        //     dic_playerDistance.Add(key,_value);
        // }
        //Debug.Log("Setdistance "+key+" contain ? "+dic_playerDistance.ContainsKey(key));
        //Debug.Log("_value "+_value);
        // if(!dic_playerDistance.ContainsKey(key)){
        //     Debug.LogError("Not found key "+key+" in dic_playerdistance");
        //     return;
        // }
        // if(!dic_playerSlider.ContainsKey(key)){
        //     Debug.LogError("Not found key "+key+" in dic_playerSlider");
        //     return;
        // }
        // dic_playerDistance[key].distance = _value;
        // dic_playerSlider[key].SetValue(_value);
    }
    
    void OnDestroy(){
        //Dictionary<string,PlayerSliderPrefab> dic_playerSlider;
        //Dictionary<string,PlayerDistanceData> dic_playerDistance;
        foreach (var item in dic_playerSlider.Values)
        {
            if(item != null && item.gameObject != null)
                Destroy(item.gameObject);
        }
        foreach (var itemData in dic_playerDistance.ToDictionary(k =>k.Key , v =>v.Value))
        {
            if(dic_playerDistance[itemData.Key] != null)
                dic_playerDistance[itemData.Key] = null;
        }
        dic_playerSlider.Clear();
        dic_playerDistance.Clear();
    }

}

