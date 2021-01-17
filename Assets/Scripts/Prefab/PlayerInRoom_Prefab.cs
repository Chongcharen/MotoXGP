
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Photon.Realtime;
using PlayFab.ClientModels;
using ExitGames.Client.Photon;
using Bolt;
public class PlayerInRoom_Prefab : EntityEventListener<IRoomPlayerInfoState>
{
    public static Subject<PlayerInRoom_Prefab> OnDestroyed = new Subject<PlayerInRoom_Prefab>();
    [SerializeField]TextMeshProUGUI playername_text;
    [SerializeField]RawImage player_avatar;
    [SerializeField]Image bg_image,image_flag_nation;
    [SerializeField]Button b_chat,b_kick;
    public string userId;
    public Color playerColor;
    PlayerProfileModel profilemodel;
    BikePlayerObject bikePlayerObject;
    // properties
    string avatarURL;
    string flag;
    void Start(){
        // b_chat.OnClickAsObservable().Subscribe(_=>{

        // });
        // b_kick.OnClickAsObservable().Subscribe(_=>{

        // });
    }
    public override void Attached(){
        print(Depug.Log("PlayerinRoom Attached ",Color.green));
        print(Depug.Log("state . name "+state.Name,Color.green));
        state.AddCallback("Name",()=>UpdatePlayerName());
        state.AddCallback("Avatar",()=>UpdateAvatar());
        state.AddCallback("Color",()=> playerColor = state.Color);
        state.AddCallback("Flag",()=> flag = state.Flag);
        // check bike customtoken;
        if(BoltNetwork.IsClient){
            print(Depug.Log("Get token "+entity.AttachToken,Color.cyan));
            print(Depug.Log("Get server connection "+BoltNetwork.Server,Color.cyan));
            print(Depug.Log("Get server ConnectionId "+BoltNetwork.Server.ConnectionId,Color.cyan));
            print(Depug.Log("Get server AcceptToken "+BoltNetwork.Server.AcceptToken,Color.cyan));
            print(Depug.Log("Get server ConnectToken "+BoltNetwork.Server.ConnectToken,Color.cyan));
        }
        
    }
    public override void ControlGained(){
        print(Depug.Log("ControlGained client ?"+BoltNetwork.IsClient,Color.cyan));
         print(Depug.Log("this AttachToken"+this.entity.AttachToken,Color.cyan));
          print(Depug.Log("this ControlGainedToken"+this.entity.ControlGainedToken,Color.cyan));
           print(Depug.Log("this DetachToken"+this.entity.DetachToken,Color.cyan));
       // profilemodel = PlayFabController.Instance.playerProfileModel.Value;
       // SetupPlayer();
    }
    public void UpdatePlayerName(){
        Debug.Log("UpdatePlayerName "+state.Name);
        playername_text.text = state.Name;
    }
    public void UpdateAvatar(){
        avatarURL = state.Avatar;
    }
    public void SetColor(Color _color){
        playerColor = _color;
        playername_text.color = playerColor;
    }
    public void SetData(Player playerData,Color color){
        Debug.Log("player "+playerData.CustomProperties);
        if(!playerData.CustomProperties.ContainsKey(PlayerPropertiesKey.PLAYFAB_PROFILE)){
            Debug.LogError(PlayerPropertiesKey.PLAYFAB_PROFILE + "Key not found");
            return;
        }
        var playerProfileJson = playerData.CustomProperties[PlayerPropertiesKey.PLAYFAB_PROFILE] as string;
        if(!string.IsNullOrEmpty(playerProfileJson))
            profilemodel = GameUtil.ConvertToPlayFabPlayerProfilemodel(playerProfileJson.ToString());
        else
            Debug.LogError("playerProfileJson is string empty or null");
        userId = playerData.UserId;
        playername_text.text = profilemodel.DisplayName;
        playerColor = color;
       // bg_image.color = color;
    }
    public override void Detached(){
        print(Depug.Log("Playerinroom prefab Detached",Color.red));
        OnDestroyed.OnNext(this);
    }
    // public void SetupPlayer(){
    //     print(Depug.Log("SetupPlayer "+profilemodel.DisplayName,Color.green));
    //     state.Name = profilemodel.DisplayName;
    //     b_kick.gameObject.SetActive(false);
    // }
    public void SetupProfileModel(PlayerProfileModel model){
        profilemodel = model;
        SetupPlayerAvatar();
    }
    public void SetupPlayerAvatar(){
        Debug.Log("SetupAvatar ");
        Debug.Log("AvatarURL "+profilemodel.AvatarUrl);
         StaticCoroutine.DoCoroutine(ImageManager.Instance.LoadImage(profilemodel.AvatarUrl,texture =>{
                        player_avatar.texture = texture;
                    }));
    }
    public void SetupPlayer(bool IsClient){
        state.Name = profilemodel.DisplayName + (!IsClient ? " (HOST)" : "");
         b_kick.gameObject.SetActive(IsClient);
    }
    public void SetupPlayer(BoltEntity entity,bool IsClient){
        var token = this.entity.AttachToken as PlayerProfileToken;
        Debug.Log("ControlGained Token "+entity.ControlGainedToken);
        Debug.Log("SetupPlayer Token "+token);
        Debug.Log(" entity.Source.ConnectToken "+ this.entity.AttachToken);
        profilemodel = token.playerProfileModel;
        state.Name = profilemodel.DisplayName;
        b_kick.gameObject.SetActive(BoltNetwork.IsClient);

    }
    public void SetupBikePlayerObject(BikePlayerObject _bikePlayerObject){
        bikePlayerObject = _bikePlayerObject;
    }
    // public void SetupOtherPlayer(){
    //     print(Depug.Log("SetupOtherPlayer",Color.blue));
    //     //var playerObject = BikePlayerRegistry.GetBikePlayer(entity.Source);
    //     b_kick.gameObject.SetActive(BoltNetwork.IsServer);
    // }
}
