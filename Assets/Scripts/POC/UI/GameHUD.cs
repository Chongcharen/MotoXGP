using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
public class GameHUD : MonoBehaviour
{
    [SerializeField]Button b_reset,b_restart,b_mic;
    [SerializeField]TextMeshProUGUI nos_txt;
    public static Subject<Unit> OnResetPosition = new Subject<Unit>();
    public static Subject<Unit> OnRestartPosition = new Subject<Unit>();

    

    void Start(){
        b_reset.OnClickAsObservable().Subscribe(_=>{
            OnResetPosition.OnNext(default);
        });
        b_restart.OnClickAsObservable().Subscribe(_=>{
            OnRestartPosition.OnNext(default);
        });
        GameController.OnMicActive.Subscribe(active =>{
            b_mic.interactable = active;
        });
        AbikeChopSystem.OnBoostChanged.Subscribe(value =>{
            nos_txt.text = value.ToString();
        });
    }
}
