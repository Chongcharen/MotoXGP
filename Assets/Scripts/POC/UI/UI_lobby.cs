using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Photon.Realtime;
using Photon.Pun;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UI_lobby : UIDisplay
{
    [Header("Navigator To UI")]
    [SerializeField]Button b_friend;
    [SerializeField]Button b_shop;
    [Header(".")]
    [SerializeField]Button b_joinRandomRoom,b_mapviewer,b_playerCustom;
    [SerializeField]Button b_share;
    [SerializeField]Button b_share_text;
    [SerializeField]Image[] bikeImages;
    [SerializeField]float[] bikeStartScale;
    [SerializeField]float[] bikeStopScale;
    [SerializeField]Vector3[] bike0Path;
    [SerializeField]Vector3[] bike1Path;
    [SerializeField]Vector3[] bike2Path;
    public List<Vector3[]> pathList;

    void Start(){
        id = UIName.LOBBY;
        UI_Manager.RegisterUI(this);
        pathList = new List<Vector3[]>();
        pathList.Add(bike0Path);pathList.Add(bike1Path);pathList.Add(bike2Path);
        b_joinRandomRoom.OnClickAsObservable().Subscribe(_=>{
            //UI_Manager.OpenUI(UIName.ROOM);
            PageManager.Instance.OpenMap();
            //PhotonNetworkConsole.Instance.JoinRandomRoom(null);
        });
        root.ObserveEveryValueChanged(r =>r.gameObject.activeSelf).Subscribe(active =>{
            //Animate Bike when lobby active
            if(active)
                 StartBikeAnimation();
        });
        b_mapviewer.OnClickAsObservable().Subscribe(_=>{
            SceneManager.LoadScene(SceneName.MAP_VIEWER);
        }).AddTo(this);
        b_playerCustom.OnClickAsObservable().Subscribe(_=>{
            Debug.Log("BoltNetwork.IsConnected "+BoltNetwork.IsConnected);
            //SceneManager.LoadScene(SceneName.PLAYER_CUSTOM);
            SceneDownloadAsset.LoadScene(SceneName.PLAYER_CUSTOM,
                                            new List<string>{AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.HELMET,
                                            AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.GLOVE,
                                            AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.SUIT,
                                            AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.BOOT
                                        });
        }).AddTo(this);

        b_shop.OnClickAsObservable().Subscribe(_=>{
           // PageManager.Instance.OpenShop();
           UI_Manager.OpenUI(UIName.SHOP);
        }).AddTo(this);
        b_friend.OnClickAsObservable().Subscribe(_=>{
            UI_Manager.OpenUI(UIName.FRIENDS);
        }).AddTo(this);

        b_share.OnClickAsObservable().Subscribe(_=>{

            NativeShare.ShareResultCallback p = (result,target) =>
            {
                Debug.Log("callback"+result);
                Debug.Log("target "+target);
            };
            new NativeShare().SetSubject("Share MotoXGP Game")
            .SetText("https://play.google.com/store/apps/details?id=com.garena.game.kgth")
            .SetTitle("title")
            .SetCallback(p)
            .Share();

        }).AddTo(this);

        b_share_text.OnClickAsObservable().Subscribe(_=>{
             NativeShare.ShareResultCallback p = (result,target) =>
            {
                Debug.Log("callback"+result);
                Debug.Log("target "+target);
                
            };
            new NativeShare().SetSubject("Share MotoXGP ID with")
            .SetText(PlayFabController.Instance.PlayFabId)
            .SetTitle("Share MotoXGP ID")
            .SetCallback(p)
            .Share();

        }).AddTo(this);
        
    }
    void StartBikeAnimation(){
       // Sequence sequence = DOTween.Sequence();
        bikeImages[0].rectTransform.anchoredPosition = pathList[0][0];
        bikeImages[0].rectTransform.DOLocalPath(pathList[0],1,PathType.CatmullRom,PathMode.Full3D).OnComplete(()=>{
            //bikeImages[0].rectTransform.anchoredPosition = pathList[0][0];
        });
    }
    // public override void OnJoinedRoom(){
    //     root.gameObject.SetActive(false);
    //     PageManager.Instance.OpenRoom();
    // }
    
}
