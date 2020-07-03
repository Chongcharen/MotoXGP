using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class UI_Photon : MonoBehaviour
{
    [SerializeField]Button b_quickstart;
    
    void Start()
    {
        b_quickstart.OnClickAsObservable().Subscribe(_=>{
            PhotonConsole.Instance.QuickStart();
        });
    }
}
