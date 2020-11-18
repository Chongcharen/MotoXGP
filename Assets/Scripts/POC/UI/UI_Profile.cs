using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
public class UI_Profile : MonoBehaviour
{
    [SerializeField]Button b_back;
    [SerializeField]Button b_share_id;
    [SerializeField]TextMeshProUGUI txt_playfabId;
    [SerializeField]TextMeshProUGUI txt_displayName;
    [SerializeField]RawImage img_avatar;
    bool hasObserve = false;
    private void Start()
    {
        b_back.OnClickAsObservable().Subscribe(_=>{

        }).AddTo(this);
        b_share_id.OnClickAsObservable().Subscribe(_=>{

        }).AddTo(this);
        PlayFabController.Instance.playerProfileModel.AsObservable().Subscribe(p=>{
            if( p == null&&hasObserve)return;
             PlayFabController.Instance.playerProfileModel.Value.ObserveEveryValueChanged(v =>v.DisplayName).Subscribe(_=>{
                txt_displayName.text = PlayFabController.Instance.playerProfileModel.Value.DisplayName;
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
