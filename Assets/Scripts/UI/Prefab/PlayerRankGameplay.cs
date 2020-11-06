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
    //PlayerIndexProfileData profileData;
    PlayerProfileToken profileData;
    PlayerProfileModel profileModel;
    public void SetUp(PlayerProfileToken _data){
        profileData = _data;
        Debug.Log("Setup "+_data);
        GUIDebug.Log("--------------------->PlayerRankGameplay Setup 222222222 "+_data);
        //Debug.Assert(!profileData.playerProfileModel);
        //Debug.Log("profileData.playerBikeData.playerFinishTime "+profileData.playerBikeData.playerFinishTime);
        profileModel = profileData.playerProfileModel;
        player_name_txt.text = profileModel.DisplayName;
        

        // var TimeSpan = System.TimeSpan.FromTicks(System.Convert.ToInt64(profileData.playerBikeData.playerFinishTime));
        //     var timeText = TimeSpan.ToString(@"mm\:ss\:fff");
        //     if(!timeText.Equals("00:00:000")){
        //         player_time_txt.text = timeText;
        //     }else{
        //         player_time_txt.text = "Driving";
        //     }


        if(profileData.playerBikeData.playerFinishTime >= 999999999999999999){
             player_time_txt.text = "Driving";
        }else
        {
            var TimeSpan = System.TimeSpan.FromTicks(System.Convert.ToInt64(profileData.playerBikeData.playerFinishTime));
            var timeText = TimeSpan.ToString(@"mm\:ss\:fff");
            if(!timeText.Equals("00:00:000")){
                player_time_txt.text = timeText;
            }else{
                player_time_txt.text = "Driving";
            }
        }
    }
}
