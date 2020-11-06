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
public class UI_lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_joinRandomRoom,b_mapviewer,b_playerCustom,b_shop;
    [SerializeField]Image[] bikeImages;
    [SerializeField]float[] bikeStartScale;
    [SerializeField]float[] bikeStopScale;
    [SerializeField]Vector3[] bike0Path;
    [SerializeField]Vector3[] bike1Path;
    [SerializeField]Vector3[] bike2Path;
    public List<Vector3[]> pathList;

    void Start(){
        pathList = new List<Vector3[]>();
        pathList.Add(bike0Path);pathList.Add(bike1Path);pathList.Add(bike2Path);
        b_joinRandomRoom.OnClickAsObservable().Subscribe(_=>{
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
            PageManager.Instance.OpenShop();
        }).AddTo(this);
    }
    void StartBikeAnimation(){
       // Sequence sequence = DOTween.Sequence();
        bikeImages[0].rectTransform.anchoredPosition = pathList[0][0];
        bikeImages[0].rectTransform.DOLocalPath(pathList[0],1,PathType.CatmullRom,PathMode.Full3D).OnComplete(()=>{
            //bikeImages[0].rectTransform.anchoredPosition = pathList[0][0];
        });
    }
    public override void OnJoinedRoom(){
        root.gameObject.SetActive(false);
        PageManager.Instance.OpenRoom();
    }
    
}
