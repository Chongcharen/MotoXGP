
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Newtonsoft.Json;
using ExitGames.Client.Photon;
using DG.Tweening;
using TMPro;
using System.Collections;

public class UI_Ready : MonoBehaviour
{
    [SerializeField]GameObject root;
    [SerializeField]Image image_panel;
    [SerializeField]TextMeshProUGUI countdown_txt;
    bool isPlayed = false;
    void Awake(){
        GameCallback.OnGameReady.Subscribe(raceCountdown=>{
            countdown_txt.text = raceCountdown.Message; 
            countdown_txt.DOKill();
            countdown_txt.transform.DOScale(Vector3.zero,0);
            countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).SetAutoKill();
            if(raceCountdown.RaceStart){
                StartCoroutine(DelayCloseUI());
            }
        }).AddTo(this);
    }
    IEnumerator DelayCloseUI(){
        yield return new WaitForSeconds(1);
        countdown_txt.text = "";
        root.gameObject.SetActive(false);
    }
    void Ready(){
       // GameplayManager.Instance.PlayerReady(PhotonNetwork.LocalPlayer.UserId);
        image_panel.enabled = false;
    }
    // void StartCountDown(){
    //     if(GameplayManager.Instance.isEnd.Value)return;
    //     countdown_txt.text = "3";
    //     countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0);

    //     Sequence sequence = DOTween.Sequence();
    //     sequence.Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
    //         countdown_txt.text = "2";
    //     })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0))
    //     .Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
    //         countdown_txt.text = "1";
    //     })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0))
    //     .Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
    //         countdown_txt.text = "Go!";
    //         isPlayed = true;
    //         GameplayManager.Instance.StartGame();
    //     })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0))
    //     .Append(countdown_txt.transform.DOScale(new Vector3(1.3f,1.3f,1.3f),1).SetEase(Ease.InOutCirc).OnComplete(()=>{
    //         countdown_txt.text = "";
    //     })).Append(countdown_txt.transform.DOScale(new Vector3(0f,0f,0f),0));
    //     sequence.Play();
    // }
}
