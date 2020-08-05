using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
public class MapSwipeObjectPrefab : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TextMeshProUGUI map_name_txt,map_level_txt,map_detail_txt;
    public Button b_Enter;
    public Image img_map;
    [SerializeField]Image img_fade;
    [SerializeField]Image[] starImages;

    bool isBlock = true;

    GameStageData gameStageData;
    private void Start() {
        b_Enter.OnClickAsObservable().Subscribe(_=>{
            //EnterLevel
        });
    
    }
    public void Setup(GameStageData _data,Sprite _sprite){
        gameStageData = _data;
        map_name_txt.text = gameStageData.themeName;
        img_map.sprite = _sprite;
       // img_map.main = Sprite.Create(_texture,img_map.sprite.rect,new Vector2(0.5f,0.5f));
        //var level = gameStageData.mapLevel +1;
        //var mapLevelName = level < 10 ? "0"+level : level.ToString();
        map_level_txt.text = gameStageData.stageName;
        map_detail_txt.text = gameStageData.detail;
    }
    public void EnabledInterActive(){
        if(!isBlock)return;
        isBlock = false;
        b_Enter.interactable = true;
        img_fade.DOFade(0,0);
    }
    public void DisabledInterActive(){
        if(isBlock)return;
        isBlock = true;
        b_Enter.interactable = false;
        img_fade.DOFade(0.7f,0);
    }
}
