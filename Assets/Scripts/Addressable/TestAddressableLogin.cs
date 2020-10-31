using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class TestAddressableLogin : MonoBehaviour
{
    [SerializeField]Button b_login;
    private void Start()
    {
        b_login.OnClickAsObservable().Subscribe(_=>{
            Debug.Log("login");
            PlayFabController.Instance.LoginWithDeviceId();
        }).AddTo(this);
        PlayFabController.OnPlayFabLoginComplete.Subscribe(_=>{
            Debug.Log("PlayFabController complete");
             AddressableManager.Instance.Init();
        }).AddTo(this);
    }
}
