using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UI_Equipment_Switch_Model : MonoBehaviour
{
    [SerializeField]GameObject player_obj;
    [SerializeField]GameObject bike_obj;
    [SerializeField]Vector3 player_solo_position;
    [SerializeField]Vector3 bike_solo_position;
    [SerializeField]Vector3 player_duo_position;
    [SerializeField]Vector3 bike_duo_position;

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
        bike_obj.gameObject.SetActive(false);
        player_obj.gameObject.SetActive(true);
        player_obj.transform.localPosition = player_solo_position;
    }
    void ChangeBikeSolo(){
        bike_obj.gameObject.SetActive(true);
        player_obj.gameObject.SetActive(false);
        bike_obj.transform.localPosition = bike_solo_position;
    }
    void ChangeDuo(){
        bike_obj.gameObject.SetActive(true);
        player_obj.gameObject.SetActive(true);
        player_obj.transform.localPosition = player_duo_position;
        bike_obj.transform.localPosition = bike_duo_position;
    }
}
