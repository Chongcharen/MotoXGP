using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using UniRx;
public class UI_Gameplay : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI time_txt;
    [SerializeField]TextMeshProUGUI speed_txt;
    bool stopTimer = true;
    bool isEnd = false;
    long playerTime;
    float timeElapsed;
    string totalTimer;
    TimeSpan timeSpan;
    void Start(){
        GameplayManager.OnGameEnd.Subscribe(_=>{
            isEnd = _;
            OnStopTimer();
        }).AddTo(this);
        GameplayManager.Instance.elapsedTime.ObserveEveryValueChanged(t =>t.Value).Subscribe(_timeElapsed =>{
            timeElapsed = _timeElapsed;
        }).AddTo(this);
        GameplayManager.Instance.isStart.ObserveEveryValueChanged(s =>s.Value).Subscribe(start =>{
            stopTimer = !start;
        }).AddTo(this);
        BikeBoltSystem.OnShowSpeed.Subscribe(speed =>{
            speed_txt.text = speed.ToString();
        }).AddTo(this);
    }

    private void Update() {
        if(stopTimer)return;
        timeSpan = System.TimeSpan.FromSeconds((Time.time-timeElapsed));
        time_txt.text = timeSpan.ToString(@"mm\:ss\:fff");
    }
    void OnStopTimer(){
        Debug.Log("StopTimer");
        stopTimer =true;
        totalTimer = time_txt.text;
        GameplayManager.RegisterLocalPlayerFinish(timeSpan.Ticks);
    }
}
