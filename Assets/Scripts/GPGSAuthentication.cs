using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UniRx;
#if UNITY_ANDROID
public class GPGSAuthentication : MonoSingleton<GPGSAuthentication>
{
    //Start is called before the first frame update
    
    public static PlayGamesPlatform platform;
    public static Subject<bool> OnGoogleLoginSuccess = new Subject<bool>();
    public string playerSecrect = "hcJqvoLS0g_Oq00o2lEagzL-";
    public ILocalUser localUser;
    public void Init(){
        {
       // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.
       if(platform == null){
           PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestEmail().RequestIdToken().RequestServerAuthCode(true).Build ();
            PlayGamesPlatform.InitializeInstance (config);
            PlayGamesPlatform.DebugLogEnabled = true;
           platform = PlayGamesPlatform.Activate();
           
       }
       Debug.Log("Social.Active.localUser.authenticated "+Social.Active.localUser.authenticated);
       Social.Active.localUser.Authenticate(success =>{
                Debug.Log("Authenticate success "+success);
                Debug.Log("Auth ? = "+platform.GetServerAuthCode());
                localUser = Social.Active.localUser;
                OnGoogleLoginSuccess.OnNext(success);
                
            });  
        //    if(Social.Active.localUser.authenticated){
        //        Social.Active.localUser.Authenticate(success =>{
        //             Debug.Log("Authenticate success "+success);
        //             Debug.Log("Auth ? = "+platform.GetServerAuthCode());
        //             OnGoogleLoginSuccess.OnNext(success);
        //         });  
        //    }else{
        //        OnGoogleLoginSuccess.OnNext(true);
        //    }
        }
       
    }
}
#endif