using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using System.Linq;
using PlayFab.AuthenticationModels;
using UniRx;
using PlayFab.DataModels;
using System;
using Photon.Pun;
using Photon.Realtime;

public enum LoginMode{
    Facebook,Google,Guest
}
public class PlayFabController : MonoSingleton<PlayFabController>
{
    public static Subject<Unit> OnPlayFabLoginComplete = new Subject<Unit>();
    public static Subject<GetPhotonAuthenticationTokenResult> OnGetPhotonAuthenticationTokenResult = new Subject<GetPhotonAuthenticationTokenResult>();
    public static Subject<Unit> OnLogout = new Subject<Unit>();
    [SerializeField]string playfabTitleId;
    public string PlayFabId = string.Empty;
    string entityId = string.Empty;
    string entityType = string.Empty;
    public string entityToken = string.Empty;
    public bool IsLogin = false;
    LoginMode loginMode;
    public PlayerProfileModel playerProfileModel2;
    public ReactiveProperty<PlayerProfileModel> playerProfileModel = new ReactiveProperty<PlayerProfileModel>();
    void Start(){
        if(string.IsNullOrEmpty(PlayFabSettings.TitleId)){
            PlayFabSettings.TitleId = "BB6FF";
        }
        playfabTitleId = PlayFabSettings.TitleId;
        // LoginWithFacebook();
        FacebookManager.OnLoginFBCompleted.Subscribe(token =>{
            LoginPlayfabWithFBAccount(token);
        }).AddTo(this);
        FacebookManager.OnFBInit.Subscribe(_=>{
            FacebookManager.Instance.Login();
        });
        #if UNITY_ANDROID
        GPGSAuthentication.OnGoogleLoginSuccess.Subscribe(success =>{
            if(success)
                LoginPlayfabWithGoogleAccount();
        });
        #endif
    }
    public void Init(){

    }
    public void LoginWithFacebook(){
        loginMode =  LoginMode.Facebook;
        FacebookManager.Instance.Init();
    }
    #if UNITY_ANDROID
    public void LoginWithGoogle(){
        Debug.Log("LoginWithGoogle");
        loginMode =  LoginMode.Google;
        GPGSAuthentication.Instance.Init();
    }
    #endif
    public void LoginWithDeviceId(){
        loginMode =  LoginMode.Guest;
        LoginPlayFabWithDeviceID();
    }
    #if UNITY_ANDROID
    void LoginPlayfabWithGoogleAccount(){
        var request = new LoginWithGoogleAccountRequest{
                            TitleId = PlayFabSettings.TitleId,
                            ServerAuthCode = GPGSAuthentication.platform.GetServerAuthCode(),
                            PlayerSecret = GPGSAuthentication.Instance.playerSecrect,
                            CreateAccount = true
                        };        
        PlayFabClientAPI.LoginWithGoogleAccount(request,RequestPhotonToken,OnPlayFabError);
    }
    #endif
    void LoginPlayfabWithFBAccount(Facebook.Unity.AccessToken token){
        Debug.Log("LoginPlayfabWithFBAccount "+token);
        var request = new LoginWithFacebookRequest{AccessToken = token.TokenString};
        request.TitleId = PlayFabSettings.TitleId;
        request.CreateAccount = true;
        PlayFabClientAPI.LoginWithFacebook(request,RequestPhotonToken,OnPlayFabError);
    }
    void LoginPlayFabWithDeviceID(){
        var request = new LoginWithAndroidDeviceIDRequest{AndroidDeviceId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true};
        PlayFabClientAPI.LoginWithAndroidDeviceID(request,RequestPhotonToken,OnPlayFabError);
    }
    void OnLogin(LoginResult result){
        // ใช้ requestphotontoken แทน
        Debug.Log("onlogin "+result);
        Debug.Log("newlycreated "+result.NewlyCreated);
        PlayFabId = result.PlayFabId;
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;
        entityToken = result.EntityToken.EntityToken;
        
        Debug.Log("entityid "+entityId);
        Debug.Log("entityType "+entityType);
        Debug.Log("result playfab id "+result.PlayFabId);
      // SetUserData();
      // GetPlayerProfile(PlayFabId);
    }
    //request authenticate with photon before login complete
    private void RequestPhotonToken(LoginResult result) {
        Debug.Log("RequestPhotontoken "+result.EntityToken);
        Debug.Log("RequestPhotontoken "+result.PlayFabId);
        Debug.Log("RequestPhotontoken "+result.SessionTicket);
        Debug.Log("newlycreated "+result.NewlyCreated);

        PlayFabId = result.PlayFabId;
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;
        entityToken = result.EntityToken.EntityToken;

        // ถ้า newlycreated ให้ไป Update profile ผู้เล่นเบื้องต้นก่อน จาก แหล่งที่ login
        if(result.NewlyCreated)
            UpdateProfileWithNewPlayer();
        else
            GetPhotonAuthenticationToken();
        
        
        
    }
    void GetPhotonAuthenticationToken(){
        Debug.Log("GetPhotonAuthenticationToken");
        Debug.Log("PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime ");
        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
        },AuthenticateWithPhoton, OnPlayFabError);
    }
    void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult tokenResult){

            Debug.Log("Photon token acquired: " + tokenResult.PhotonCustomAuthenticationToken + "  Authentication complete.");

            var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };

            //We add "username" parameter. Do not let it confuse you: PlayFab is expecting this parameter to contain player PlayFab ID (!) and not username.
            customAuth.AddAuthParameter("username", PlayFabId);    // expected by PlayFab custom auth service

            //We add "token" parameter. PlayFab expects it to contain Photon Authentication Token issues to your during previous step.
            customAuth.AddAuthParameter("token", tokenResult.PhotonCustomAuthenticationToken);

            Debug.Log("USerid in custom auth "+customAuth.UserId);
            //We finally tell Photon to use this authentication parameters throughout the entire application.
            PhotonNetwork.AuthValues = customAuth;
            GetPlayerProfile(PlayFabId);
            OnGetPhotonAuthenticationTokenResult.OnNext(tokenResult);
            
    }
    void OnPlayFabError(PlayFabError error){
        Debug.Log("PlayFabError "+error);
    }
    public void Logout(){
        OnLogout.OnNext(default);
    }
    public void SetEntityData(){
        var data = new Dictionary<string, object>()
        {
            {"Health", 100},
            {"Mana", 10000}
        };
        var dataList = new List<SetObject>()
        {
            new SetObject()
            {
                ObjectName = "PlayerData",
                DataObject = data
            },
            // A free-tier customer may store up to 3 objects on each entity
        };

        PlayFabDataAPI.SetObjects(new SetObjectsRequest()
        {
            Entity = new PlayFab.DataModels.EntityKey {Id = entityId, Type = entityType}, // Saved from GetEntityToken, or a specified key created from a titlePlayerId, CharacterId, etc
            Objects = dataList,
        }, (setResult) => {
            Debug.Log(setResult.ProfileVersion);
            TestGetEntity();
        }, OnPlayFabError);
    }

    public void TestGetEntity(){
        Dictionary<string,ObjectResult> objs = new Dictionary<string, ObjectResult>();
        var getRequest = new GetObjectsRequest {Entity = new PlayFab.DataModels.EntityKey {Id = entityId, Type = entityType}};
        PlayFabDataAPI.GetObjects(getRequest,
            result => { 
                objs = result.Objects; 
                  Debug.Log("objs "+objs.Count);
            },
            OnPlayFabError
        );
      
    }
    void SetUserData() {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
            Data = new Dictionary<string, string>() {
                {"Ancestor", "Arthur"},
                {"Successor", "Fred"}
            }
        },
        result =>{ 
            Debug.Log("Successfully updated user data");
            GetUserData();
        },
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void GetUserData(){

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(){PlayFabId = PlayFabId,Keys = null},UserdataSuccess,OnPlayFabError);
    }
    void UpdateDisplayName(string newDisplayname) {
        PlayFabClientAPI.UpdateUserTitleDisplayName( new UpdateUserTitleDisplayNameRequest {
            DisplayName = "Bavaria"
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    void GetPlayerProfile(string playFabId) {
        Debug.Log("Getplayerprofile "+playFabId);
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints() {
                ShowDisplayName = true,
                ShowAvatarUrl = true,
                ShowLastLogin = true
            }
        },
        result => {
                // Debug.Log("The player's profile data is: " + result.PlayerProfile.PlayerId);
                Debug.Log("The player's DisplayName data is: " + result.PlayerProfile.DisplayName);
                Debug.Log("The player's AvatarUrl data is: " + result.PlayerProfile.AvatarUrl);
                playerProfileModel.Value = result.PlayerProfile;
                playerProfileModel2 = result.PlayerProfile;
                PhotonNetwork.NickName = playerProfileModel.Value.DisplayName;
                IsLogin = true;
                Debug.Log("Change Nickname "+PhotonNetwork.NickName);
                OnPlayFabLoginComplete.OnNext(default);
            },
        error => Debug.LogError(error.GenerateErrorReport()));
    }
    void UserdataSuccess(GetUserDataResult userdataResult){
        //example and test this
        // Debug.Log("userdataResult "+userdataResult.Data.Count);
        // Debug.Log("userdataResult "+userdataResult.CustomData);
        // Debug.Log(""+userdataResult.Request.ToJson());
        // foreach (var item in userdataResult.Data)
        // {
        //     Debug.Log(string.Format(" key {0} value {1}",item.Key,item.Value.Value));
        // }
        //PlayFabController.OnPlayfabLoginComplete.OnNext(userdataResult);
    }
    void TestCloudScript(){
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "helloWorld", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudHelloWorld, OnErrorShared);
    }

    private void OnErrorShared(PlayFabError obj)
    {
        //throw new NotImplementedException();
    }

    private void OnCloudHelloWorld(ExecuteCloudScriptResult result)
    {
        Debug.Log("result "+result.FunctionResult);
        Debug.Log("result "+result.CustomData);
        Debug.Log("result "+result.FunctionResult.ToString());
        // Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        //object wrapper = JsonWrapper.SerializeObject(result.FunctionResult);
        var dic = new Dictionary<string,object>();
        dic.Add("aaa",1);
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    void UpdateProfileWithNewPlayer(){
        var displayName = string.Empty;
        var imageUrl = string.Empty;
        if(loginMode == LoginMode.Facebook){
            //UpdateDisplayName(FacebookManager.Instance.profileData.name);
            displayName = FacebookManager.Instance.profileData.name;
            imageUrl = FacebookManager.Instance.profileData.picture.data.url;
        }else if(loginMode == LoginMode.Google){
           // GPGSAuthentication.platform.Authenticate()
           displayName = "Google_"+PlayFabId;//GPGSAuthentication.Instance.localUser.userName;
           imageUrl = "";//GPGSAuthentication.Instance.localUser.image.
        }else if(loginMode == LoginMode.Guest){
            displayName = "Guest_"+PlayFabId;
            imageUrl = "";
        }
        print(Depug.Log("Updateprofile withnewplayer : "+displayName,Color.green));
        PlayFabClientAPI.UpdateAvatarUrl(new UpdateAvatarUrlRequest{
            ImageUrl = imageUrl
        },result =>{
             Debug.Log("avatar url change to " + result.ToJson());
        },error => Debug.LogError(error.GenerateErrorReport()));

        PlayFabClientAPI.UpdateUserTitleDisplayName( new UpdateUserTitleDisplayNameRequest {
            DisplayName = displayName
        }, result => {
            Debug.Log("The player's display name is now: " + result.DisplayName);
            GetPhotonAuthenticationToken();
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }


    public void UpdateDataToPhotonPlayer(){
         //Test Add Modile In playerHashtable
         Debug.Log("UpdUpdateDataToPhotonPlayerate");
        ExitGames.Client.Photon.Hashtable playerProfilemodel_hashtable = new ExitGames.Client.Photon.Hashtable();
        playerProfilemodel_hashtable.Add(PlayerPropertiesKey.PLAYFAB_PROFILE,playerProfileModel.Value.ToJson());
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProfilemodel_hashtable);
    }
}
