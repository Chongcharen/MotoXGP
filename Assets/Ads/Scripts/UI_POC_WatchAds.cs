using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class UI_POC_WatchAds : MonoBehaviour
{
    // Start is called before the first frame update
    public Button b_ads,b_reward_video,b_interstatial_video,b_banner;
    void Start()
    {
        AdsManager.Instance.Init();
        // b_ads.OnClickAsObservable().Subscribe(_=>{
        //     AdsManager.Instance.ShowVideo();
        // }).AddTo(this);
        b_reward_video.OnClickAsObservable().Subscribe(_=>{
            AdsManager.Instance.ShowRewardVideo();
        }).AddTo(this);
        // b_interstatial_video.OnClickAsObservable().Subscribe(_=>{
        //     AdsManager.Instance.ShowInterStatialAds();
        // }).AddTo(this);
        // b_banner.OnClickAsObservable().Subscribe(_=>{
        //     AdsManager.Instance.Showbanner();
        // }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
