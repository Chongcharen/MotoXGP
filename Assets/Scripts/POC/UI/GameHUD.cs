using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using DG.Tweening;
public class GameHUD : MonoBehaviourPunCallbacks
{
    public static Subject<Unit> OnResetPosition = new Subject<Unit>();
    public static Subject<Unit> OnRestartPosition = new Subject<Unit>();
    [SerializeField]Button b_reset,b_restart,b_mic;
    [SerializeField]TextMeshProUGUI nos_txt,fps_txt;
    [SerializeField]Image[] images_wifi;
    [SerializeField]Image image_nos,image_timeNod;
    public float deltaTime;
    
    ///TestGame
    [Header("GameDebug")]
    [SerializeField]Button b_restartGame;

    void Start(){
        // b_restartGame.OnClickAsObservable().Subscribe(_=>{
           
        //     GameplayManager.Instance.ResetGame();
        // });
        // b_reset.OnClickAsObservable().Subscribe(_=>{
        //     OnResetPosition.OnNext(default);
        // });
        b_restart.OnClickAsObservable().Subscribe(_=>{
             Debug.Log("restart Click");
            OnRestartPosition.OnNext(default);
        });
        GameController.OnMicActive.Subscribe(active =>{
            b_mic.interactable = active;
        });
        AbikeChopSystem.OnBoostChanged.Subscribe(value =>{
            nos_txt.text = value.ToString();
        });
        AbikeChopSystem.OnBoostTime.Subscribe(boostTime =>{
            MakeAnimationBoosted(boostTime);
        });
    }
    void Update(){
        if(PhotonNetwork.GetPing() <= 60){
            SetPing(Color.green,Color.green,Color.green);
        }else if(PhotonNetwork.GetPing() > 60 && PhotonNetwork.GetPing() <= 90){
            SetPing(Color.yellow,Color.yellow,Color.white);
        }
        else{
            SetPing(Color.red,Color.white,Color.white);
        }
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fps_txt.text = Mathf.Ceil (fps).ToString ();
    }

    void MakeAnimationBoosted(float time){
        image_timeNod.DOFillAmount(1,time).OnComplete(()=>{
            image_timeNod.DOFillAmount(0,.5f);
        });
        if(image_nos != null){
            image_nos.transform.DOShakePosition(time,10,10,180);
        }
    }

    void SetPing(Color firstColor,Color secondColor,Color thirthColor){
        images_wifi[0].color = firstColor;
        images_wifi[1].color = secondColor;
        images_wifi[2].color = thirthColor;
    }
}


