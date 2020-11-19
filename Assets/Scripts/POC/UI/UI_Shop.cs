using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
public class UI_Shop : UIDisplay
{
    [SerializeField]Button b_back;
    // Start is called before the first frame update
    void Start()
    {
        id = UIName.SHOP;
        UI_Manager.RegisterUI(this);
        b_back.OnClickAsObservable().Subscribe(_=> {
            //PageManager.Instance.OpenLobby();
            UI_Manager.OpenUI(UIName.LOBBY);
        }).AddTo(this);
    }

}
