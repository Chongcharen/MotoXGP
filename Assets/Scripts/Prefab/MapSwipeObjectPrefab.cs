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
    [SerializeField]Image img_fade;
    [SerializeField]Image[] startImages;

    bool isBlock = true;

    MapChoiceData choiceData;
    private void Start() {
        b_Enter.OnClickAsObservable().Subscribe(_=>{
            //EnterLevel
        });
    
    }
    public void Setup(MapChoiceData _data){
        choiceData = _data;
        map_name_txt.text = choiceData.mapType;
        var level = choiceData.mapLevel +1;
        var mapLevelName = level < 10 ? "0"+level : level.ToString();
        map_level_txt.text = mapLevelName;
        map_detail_txt.text = choiceData.mapDetail;
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
