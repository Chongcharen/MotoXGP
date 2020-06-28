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
public class UI_lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_joinRandomRoom;
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
            PhotonNetworkConsole.Instance.JoinRandomRoom(null);
        });
        root.ObserveEveryValueChanged(r =>r.gameObject.activeSelf).Subscribe(active =>{
            //Animate Bike when lobby active
            if(active)
                 StartBikeAnimation();
        });
       
    }
    void StartBikeAnimation(){
       // Sequence sequence = DOTween.Sequence();
        bikeImages[0].rectTransform.DOLocalPath(pathList[0],3,PathType.CatmullRom,PathMode.Full3D).OnComplete(()=>{
            bikeImages[0].rectTransform.anchoredPosition = pathList[0][0];
        });
    }
    public override void OnJoinedRoom(){
        root.gameObject.SetActive(false);
        PageManager.Instance.OpenRoom();
    }
    
}
