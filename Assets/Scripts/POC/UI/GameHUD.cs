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
using UnityEngine.SceneManagement;

public class GameHUD : MonoBehaviourPunCallbacks
{
    public static Subject<Unit> OnResetPosition = new Subject<Unit>();
    public static Subject<Unit> OnRestartPosition = new Subject<Unit>();
    public static Subject<Unit> OnLowerGear = new Subject<Unit>();
    [SerializeField]Button b_reset,b_restart,b_mic;
    [SerializeField]Button b_accel;
    [SerializeField]Button b_back;
    [SerializeField]TextMeshProUGUI nos_txt,fps_txt,speed_txt;
    [SerializeField]Image[] images_wifi;
    [Header("Nos")]
    [SerializeField]Image image_nos;
    [SerializeField]Image image_timeNod;

    [Header("lower gear")]
    [SerializeField]float time_accel_limit = 0.5f;
    [SerializeField]int acceletor_gear_limit=2;
    [SerializeField]Image[] img_lower_gear;
    [SerializeField]float gear_cooldown=2;
    public ReactiveProperty<int> gear_count = new ReactiveProperty<int>(3);
    float timer_accel_touch;
    int acceletor_gear_count;
    
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
        gear_count.ObserveEveryValueChanged(g => g.Value).Subscribe(_=>{
            if(_<3){
                img_lower_gear[_].DOFillAmount(1,gear_cooldown).SetAutoKill();
                Observable.Timer(TimeSpan.FromSeconds(gear_cooldown)).Subscribe(timer=>{
                    gear_count.Value ++;
                }).AddTo(this);
            }
           // print(Depug.Log("Gear Count "+gear_count,Color.yellow));
        });

        b_back.OnClickAsObservable().Subscribe(_=>{
            PhotonNetworkConsole.Instance.LeaveRoom();
            ObjectPool.Instance.Dispose();
            SceneManager.LoadScene(SceneName.LOBBY);
        });
        b_restart.OnClickAsObservable().Subscribe(_=>{
            // Debug.Log("restart Click");
            OnRestartPosition.OnNext(default);
        }).AddTo(this);
        GameController.OnMicActive.Subscribe(active =>{
            b_mic.interactable = active;
        }).AddTo(this);
        AbikeChopSystem.OnBoostChanged.Subscribe(value =>{
            nos_txt.text = value.ToString();
        }).AddTo(this);
        AbikeChopSystem.OnBoostTime.Subscribe(boostTime =>{
            MakeAnimationBoosted(boostTime);
        }).AddTo(this);
        AbikeChopSystem.OnBoostDelay.Subscribe(delayTime =>{
            image_nos.DOFillAmount(0,0).SetAutoKill();
            image_nos.DOFillAmount(1,delayTime).SetAutoKill();
        }).AddTo(this);
        AbikeChopSystem.OnShowSpeed.Subscribe(_=>{
            speed_txt.text = ((int)_).ToString();
        }).AddTo(this);
    }
    private void Update() {
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

    public void AcceletorLowerGear(){
       // Debug.Log("AcceletorLowerGear ");
        if(acceletor_gear_count <= 0){
            acceletor_gear_count++;
        }else{
            if(Time.time - timer_accel_touch <= time_accel_limit){
                acceletor_gear_count++;
            }
        }
        //Debug.Log("acceletor_gear_count "+acceletor_gear_count);
        timer_accel_touch = Time.time; 
        //Debug.Log("timer_accel_touch "+timer_accel_touch);
        if(acceletor_gear_count >= acceletor_gear_limit){
            if(gear_count.Value >=3){
                gear_count.Value = 0;
                //Debug.Log("tgear_count "+gear_count.Value);
                OnLowerGear.OnNext(default);
                img_lower_gear[0].DOFillAmount(0,0.1f);
                img_lower_gear[1].DOFillAmount(0,0.2f);
                img_lower_gear[2].DOFillAmount(0,0.3f);
            }
            acceletor_gear_count = 0;
        }
        Observable.Timer(TimeSpan.FromSeconds(time_accel_limit)).Subscribe(_=>{
                    acceletor_gear_count = 0;
        }).AddTo(this);
    }
}


