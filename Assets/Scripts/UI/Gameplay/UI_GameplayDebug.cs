using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class UI_GameplayDebug : MonoBehaviour
{
    [SerializeField]Button b_increaseRound;

    void Start(){
        b_increaseRound.OnClickAsObservable().Subscribe(_=>{
            GameplayManager.Instance.IncreaseRound();
        });
    }
}
