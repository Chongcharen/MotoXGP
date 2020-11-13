using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class Popup_Reward : Popup
{
    public Image itemImage;
    public TextMeshProUGUI detail_txt;
    static string message;
    public Button b_ok;
     public static Popup_Reward Launch(string data){
        message = data;
        var prefab = Resources.Load<Popup_Reward>("Popup_Reward");
        return Instantiate<Popup_Reward>(prefab);
    }
     public void ClosePopup(){
        Destroy(this.gameObject);
    }

    public override void OnCreated()
    {
        detail_txt.text = message;
        b_ok.OnClickAsObservable().Subscribe(_=>{
            Dispose();
        }).AddTo(this);
    }

    public override void OnShow()
    {

    }

    public override void OnDestroy()
    {

    }
}
