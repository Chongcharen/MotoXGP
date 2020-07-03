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
    bool stopTimer = false;
    long playerTime;
    string totalTimer;
    TimeSpan timeSpan;
    void Start(){
        GameplayManager.OnGameEnd.Subscribe(_=>{
            OnStopTimer();
        }).AddTo(this);
    }

    private void Update() {
        if(stopTimer)return;
        timeSpan = System.TimeSpan.FromSeconds(Time.time);
        time_txt.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
    void OnStopTimer(){
        Debug.Log("StopTimer");
        stopTimer =true;
        totalTimer = time_txt.text;
        GameplayManager.RegisterLocalPlayerFinish(timeSpan.Ticks);
    }
}
