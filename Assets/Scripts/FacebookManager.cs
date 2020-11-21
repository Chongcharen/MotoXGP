using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using UniRx;
using Newtonsoft.Json;
public class FacebookManager : MonoSingleton<FacebookManager>
{
    public static Subject<AccessToken> OnLoginFBCompleted = new Subject<AccessToken>();
    public static Subject<Unit> OnFBInit = new Subject<Unit>();

    public AccessToken accessToken;
    public FBProfileData profileData;
    public void Init(){
       if(!FB.IsInitialized)
            FB.Init(InitCallback,OnHideUnity);
        else{
            FB.ActivateApp();
            OnFBInit.OnNext(default);
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
       if(isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void InitCallback()
    {
        if(FB.IsInitialized){
            FB.ActivateApp();
            OnFBInit.OnNext(default);
        }
    }

    public void Login(){
        Depug.Log("Facebook Login ",Color.green);
        var perms = new List<string>(){"public_profile", "email","user_friends"};
        FB.LogInWithReadPermissions(perms, AuthCallback);
        //FB.LogInWithPublishPermissions(perms,AuthCallback);
    }
    private void AuthCallback (ILoginResult result) {
    if (FB.IsLoggedIn) {
        accessToken = Facebook.Unity.AccessToken.CurrentAccessToken;
        Debug.Log("accesstoken userid "+accessToken.UserId);
        FB.API("/me?fields=id,name,picture",HttpMethod.GET,OnGetProfileCallback);
        //OnLoginFBCompleted.OnNext(accessToken);
        foreach (string perm in accessToken.Permissions) {
            Debug.Log(perm);
        }
    } else {
        Debug.Log("User cancelled login");
    }
}

    private void OnGetProfileCallback(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error) && !result.Cancelled) {
            Debug.Log("rawresult "+result.RawResult);
            profileData = JsonConvert.DeserializeObject<FBProfileData>(result.RawResult);
            Debug.Log("data "+profileData.name);
            OnLoginFBCompleted.OnNext(accessToken);
        }
    }
    //  private IEnumerator fetchProfilePic (string url) {
    //     WWW www = new WWW(url);
    //     yield return www;
    //     this.profilePic = www.texture;

    //     //Construct a new Sprite
    //     Sprite sprites = new Sprite();     

    //     //Create a new sprite using the Texture2D from the url. 
    //     //Note that the 400 parameter is the width and height. 
    //     //Adjust accordingly

    //     sprite = Sprite.Create(www.texture, new Rect(0, 0, 50 ,50), Vector2.zero);  
    //     sprites = Sprite.Create(www.texture, new Rect(0, 0, 50 ,50), Vector2.zero);  

    // }
}

public class FBProfileData{
    public string id;
    public string name;
    public FBPicture picture;
}
public class FBPicture{
    public FBPictureData data;
}
public class FBPictureData{
    public int height;
    public int width;
    public bool is_silhouette;
    public string url; 
}