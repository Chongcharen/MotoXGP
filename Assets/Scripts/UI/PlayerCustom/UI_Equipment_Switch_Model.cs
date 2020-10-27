using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UI_Equipment_Switch_Model : MonoBehaviour
{
    [SerializeField]GameObject player_obj;
    [SerializeField]GameObject bike_obj;
    [SerializeField]Vector3 player_solo_transform;
    [SerializeField]Vector3 bike_solo_transform;
    [SerializeField]Vector3 player_duo_transform;
    [SerializeField]Vector3 bike_duo_transform;

    [SerializeField]Toggle t_player_solo;
    [SerializeField]Toggle t_bike_solo;
    [SerializeField]Toggle t_duo;

    private void Start()
    {
        t_player_solo.OnValueChangedAsObservable().Subscribe(_=>{
            ChangePlayerSolo();
        }).AddTo(this);  
        t_bike_solo.OnValueChangedAsObservable().Subscribe(_=>{
            ChangeBikeSolo();
        }).AddTo(this); 
        t_duo.OnValueChangedAsObservable().Subscribe(_=>{
            ChangeDuo();
        }).AddTo(this);   
    }
    void ChangePlayerSolo(){

    }
    void ChangeBikeSolo(){

    }
    void ChangeDuo(){

    }
}
