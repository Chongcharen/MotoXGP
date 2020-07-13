using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab.ClientModels;
public class PlayerRankGameplay : MonoBehaviour
{
    [SerializeField]Image img_flagNation;
    [SerializeField]Image img_PlayerAvatar;
    [SerializeField]TextMeshProUGUI player_name_txt,player_time_txt;
    PlayerIndexProfileData profileData;
    PlayerProfileModel profileModel;
    public void SetUp(PlayerIndexProfileData _data){
        profileData = _data;
        Debug.Assert(!string.IsNullOrEmpty(profileData.profileModel));
        profileModel = GameUtil.ConvertToPlayFabPlayerProfilemodel(profileData.profileModel);
        player_name_txt.text = profileModel.DisplayName;
        Debug.Log(player_name_txt.text + "time "+profileData.playerFinishTime);
        var TimeSpan = System.TimeSpan.FromTicks(System.Convert.ToInt64(profileData.playerFinishTime));
        player_time_txt.text = TimeSpan.ToString(@"mm\:ss\:fff");
    }
}
