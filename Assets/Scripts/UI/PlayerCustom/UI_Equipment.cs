using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
public class UI_Equipment : MonoBehaviour
{
    [SerializeField]Button b_open_equipment;
    [SerializeField]GameObject equipment_window;
    public Toggle[] toggles;

    void Start(){
        b_open_equipment.OnClickAsObservable().Subscribe(_=>{
            equipment_window.SetActive(!equipment_window.activeSelf);
        }).AddTo(this);
        foreach (var item in toggles)
        {
            item.OnValueChangedAsObservable().Subscribe(_=>{
                if(!_)return;
                var index = Array.IndexOf(toggles,item);
                equipment_window.SetActive(true);
            }).AddTo(this);
        }
    
    }
}
