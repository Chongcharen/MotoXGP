using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;
public class ViewBikePreset : MonoBehaviour
{
    public static Subject<BikeSettingMappingData> OnSelectBikePreset = new Subject<BikeSettingMappingData>();
    public Button button; 
    public TextMeshProUGUI txt_detail;
    BikeSettingMappingData data;
    public void Setup(BikeSettingMappingData _data,int index){
        Debug.Log("Setup "+index);
        data = _data;
        txt_detail.text = "แบบ "+index;
        button.OnClickAsObservable().Subscribe(_=>{
            OnSelectBikePreset.OnNext(data);
        }).AddTo(this);
    }
}
