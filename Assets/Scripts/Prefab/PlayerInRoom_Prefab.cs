
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Photon.Realtime;
using PlayFab.ClientModels;
using ExitGames.Client.Photon;
public class PlayerInRoom_Prefab : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playername_text;
    [SerializeField]Image player_avatar,bg_image,image_flag_nation;
    [SerializeField]Button b_chat,b_kick;
    public string userId;
    public Color playerColor;
    PlayerProfileModel profilemodel;
    void Start(){
        // b_chat.OnClickAsObservable().Subscribe(_=>{

        // });
        // b_kick.OnClickAsObservable().Subscribe(_=>{

        // });
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
}
