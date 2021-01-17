using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class UI_Preset : MonoBehaviour
{
     public GameObject viewBikePresetPrefab;
    public GameObject content;
    List<BikeSettingMappingData> datas;
    private async void Start()
    {
        // content.ObserveEveryValueChanged(c =>content.activeSelf).Subscribe(active =>{
        //     ClearContent();
        //     if(active){

        //     }
        // }).AddTo(this);    
         var presetFile = await AddressableManager.Instance.LoadObject<TextAsset>("Text/BikeTunerPreset.txt");
         datas = JsonConvert.DeserializeObject<List<BikeSettingMappingData>>(presetFile.text);
         SetupBikePreset();
    }
    private async void OnEnable()
    {
        // var filePath = Path.Combine(Application.streamingAssetsPath,FilePath.BIKE_TUNER_PRESET_DATA);
        // Debug.Log("firepath "+filePath);
        // string jsonString = "";
        // if(Application.platform == RuntimePlatform.Android) //Need to extract file from apk first
        // {
        //     WWW reader = new WWW(filePath);
        //     while (!reader.isDone) { }

        //     jsonString= reader.text;
        // }
        // else
        // {
        //     jsonString= GameUtil.GetTextFromFile(filePath);
        // }
        // print(Depug.Log("Json = "+jsonString,Color.red));
        // datas = JsonConvert.DeserializeObject<List<BikeSettingMappingData>>(jsonString);
       
        // datas = JsonConvert.DeserializeObject<List<BikeSettingMappingData>>(presetFile.text);
        // SetupBikePreset();
         //var presetFile = await AddressableManager.Instance.LoadObject<TextAsset>("Text/BikeTunerPreset");
         //datas = JsonConvert.DeserializeObject<List<BikeSettingMappingData>>(presetFile.text);
         //SetupBikePreset();
    }
    void SetupBikePreset(){
        Debug.Log("SetupBikePreset!!!!!!!!!!!!!!!!!!!!!!!");
        var index = 0;
        foreach (var item in datas)
        {
            var go = Instantiate(viewBikePresetPrefab,Vector3.zero,Quaternion.identity,content.transform);
            go.gameObject.SetActive(true);
            go.GetComponent<ViewBikePreset>().Setup(item,index);
            index ++;
        }
    }
}


