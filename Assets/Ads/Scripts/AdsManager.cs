using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoSingleton<AdsManager>,IUnityAdsListener 
{
    public static string GooglePlay_ID = "3896893";
    public static string Apple_ID = "3896892";
    string myPlacementId = "rewardedVideo";
    string placementVideo = "video";
    string placementrewardVideo = "rewardedVideo";
    string placemen_tinterstitial_video = "interstitialvideo";
    string placement_banner= "banner";
    public bool testMode = true;
    bool isInit = false;
    public void Init(){
        if(isInit)return;
        Advertisement.AddListener(this);
        Advertisement.Initialize(GooglePlay_ID,testMode);
        isInit = true;
    }
    // Update is called once per frame
    public void ShowInterStatialAds(){
        if(Advertisement.IsReady(placemen_tinterstitial_video))
            Advertisement.Show(placemen_tinterstitial_video);
    }
    public void ShowRewardVideo(){
         Debug.Log("show rewardvideo "+placementrewardVideo);
         Debug.Log(Advertisement.IsReady(placementrewardVideo));
        if(Advertisement.IsReady(placementrewardVideo))
        {
            Debug.Log("show rewardvideo "+placementrewardVideo);
            Advertisement.Show(placementrewardVideo);
        }
    }
    public void ShowVideo(){
        if(Advertisement.IsReady(placementVideo))
            Advertisement.Show(placementVideo);
    }
    public void Showbanner(){
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placement_banner);
    }
    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
       // throw new System.NotImplementedException();
       Debug.Log("OnUnityAdsDidFinish "+placementId+"result "+showResult);
       switch(showResult){
           case ShowResult.Finished:
            CheckAdsReward(placementId);
           break;
           case ShowResult.Skipped:
           break;
           case ShowResult.Failed:
           break;
       }
    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
       // throw new System.NotImplementedException();
       Debug.Log("OnUnityAdsReady "+placementId);
       
    }
    void CheckAdsReward(string placementId){
        if(placementId == placementrewardVideo){
            Popup_Reward.Launch("x 888");
        }
    }
    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
       // throw new System.NotImplementedException();
       Debug.Log("OnUnityAdsDidError "+message);
    }

    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
       // throw new System.NotImplementedException();
       Debug.Log("OnUnityAdsDidError "+placementId);
       if(placementId == myPlacementId){

       }
    }
    private void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
    
}
