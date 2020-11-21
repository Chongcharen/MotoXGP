using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
public class UI_Profile : UIDisplay
{
    [SerializeField]Button b_back;
    [SerializeField]Button b_share_id;
    [SerializeField]TextMeshProUGUI txt_playfabId;
    [SerializeField]TextMeshProUGUI txt_displayName;
    [SerializeField]RawImage img_avatar;
    bool hasObserve = false;
    private void Start()
    {
        id = UIName.PROFILE;
        UI_Manager.RegisterUI(this);
        b_back.OnClickAsObservable().Subscribe(_=>{
            UI_Manager.OpenUI(UIName.LOBBY);
        }).AddTo(this);
        b_share_id.OnClickAsObservable().Subscribe(_=>{
            NativeShare.ShareResultCallback p = (result,target) =>
            {
                Debug.Log("callback"+result);
                Debug.Log("target "+target);
                
            };
            new NativeShare().SetSubject("Share MotoXGP ID with")
            .SetText(PlayFabController.Instance.PlayFabId)
            .SetTitle("Share MotoXGP ID")
            .SetCallback(p)
            .Share();
        }).AddTo(this);
        PlayFabController.Instance.playerProfileModel.AsObservable().Subscribe(p=>{
            if( p == null&&hasObserve)return;
             PlayFabController.Instance.playerProfileModel.Value.ObserveEveryValueChanged(v =>v.DisplayName).Subscribe(_=>{
                txt_displayName.text = PlayFabController.Instance.playerProfileModel.Value.DisplayName;
                txt_playfabId.text = PlayFabController.Instance.playerProfileModel.Value.PlayerId;
            }).AddTo(this);

            PlayFabController.Instance.playerProfileModel.Value.ObserveEveryValueChanged(i => i.AvatarUrl).Subscribe(_=>{
                    StaticCoroutine.DoCoroutine(ImageManager.Instance.LoadImage(_,texture =>{
                        img_avatar.texture = texture;
                    }));
                }).AddTo(this);
            PlayFabController.Instance.playerProfileModel.Value.ObserveEveryValueChanged(i => i.PlayerId).Subscribe(_=>{
                    StaticCoroutine.DoCoroutine(ImageManager.Instance.LoadImage(_,texture =>{
                        txt_playfabId.text = _;
                    }));
                }).AddTo(this);
            hasObserve = true;
        }).AddTo(this);
    }
}
