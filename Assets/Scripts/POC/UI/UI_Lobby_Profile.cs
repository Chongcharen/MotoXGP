using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using System.Linq;
public class UI_Lobby_Profile : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI txt_displayName;
    [SerializeField]RawImage img_avatar;
    bool hasObserve = false;
    private void Start() {
        PlayFabController.OnPlayFabLoginComplete.Subscribe(_=>{
            print(Depug.Log("onplayfablogincomplete "+PlayFabController.Instance.playerProfileModel,Color.green));
           // txt_displayName.text = PlayFabController.Instance.playerProfileModel.DisplayName;
        }).AddTo(this);
        PlayFabController.Instance.playerProfileModel.AsObservable().Subscribe(p=>{
            if( p == null&&hasObserve)return;
            print(Depug.Log("AddListener =>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ",Color.red));
             PlayFabController.Instance.playerProfileModel.Value.ObserveEveryValueChanged(v =>v.DisplayName).Subscribe(_=>{
                print(Depug.Log("Profile changed ",Color.white));
                txt_displayName.text = PlayFabController.Instance.playerProfileModel.Value.DisplayName;
            }).AddTo(this);

            PlayFabController.Instance.playerProfileModel.Value.ObserveEveryValueChanged(i => i.AvatarUrl).Subscribe(_=>{
                    StaticCoroutine.DoCoroutine(ImageManager.Instance.LoadImage(_,texture =>{
                        img_avatar.texture = texture;
                    }));
                }).AddTo(this);
            hasObserve = true;
        }).AddTo(this);
    }
    
}
