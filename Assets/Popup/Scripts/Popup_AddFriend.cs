using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class Popup_AddFriend : Popup
{
     [SerializeField] TextMeshProUGUI detail_txt,name_txt;
     [SerializeField] Button b_close, b_yes, b_no;
     [SerializeField]Image img_player;
     Action yes_callback, no_callback, ok_callback;
     static CT_AddFriendRequest staticAddFriendRequest;
     public static Popup_AddFriend Launch(CT_AddFriendRequest _parameter)
    {
        staticAddFriendRequest = _parameter;
        var prefab = Resources.Load<Popup_AddFriend>("popup_AddFriend");
        return Instantiate<Popup_AddFriend>(prefab);
    }
    public override void OnCreated()
    {
        b_yes.OnClickAsObservable().Subscribe(_=>{
            // RestMethod.AcceptFriend(staticAddFriendRequest.uidFrom,staticAddFriendRequest.uidTo,()=>{
            //     PhotonMessage.UpdateFriends();
            //     Dispose();
            // });
        });
        b_no.OnClickAsObservable().Subscribe(_=>{
            Dispose();
        });
    }
    public override void OnShow()
    {
    //   ImageManager.Instance.LoadFromURL(staticAddFriendRequest.playerRequest_imgURL,img_player);
    //   detail_txt.text = staticAddFriendRequest.playerRequest_name+" ต้องการขอเป็นเพื่อนกับคุณ";
    //   name_txt.text = staticAddFriendRequest.playerRequest_name;
    }   
    public override void OnDestroy()
    {

    }

    
}
