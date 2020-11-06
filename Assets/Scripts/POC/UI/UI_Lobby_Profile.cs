using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
public class UI_Lobby_Profile : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI txt_displayName;
    private void Start() {
        PlayFabController.OnPlayFabLoginComplete.Subscribe(_=>{
            print(Depug.Log("onplayfablogincomplete "+PlayFabController.Instance.playerProfileModel.DisplayName,Color.green));
            txt_displayName.text = PlayFabController.Instance.playerProfileModel.DisplayName;
        }).AddTo(this);
    }
    
}
