using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
public class UI_Starter : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI detail_txt;

    void Start(){
        Starter.OnNotification.Subscribe(text =>{
            detail_txt.text = text;
        }).AddTo(this);
    }
}
