using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using UnityEngine.U2D;
using TMPro;
public class UI_Equipment : MonoBehaviour
{
    [SerializeField]Button b_clearScreen;
    [SerializeField]GameObject equipment_window;
    [SerializeField]GridLayoutGroup layoutGroup;
    public Vector2 suit_layout;
    public Vector2 standard_layout;
    [SerializeField]Transform equipment_content;
    [SerializeField]GameObject equipment_prefab;
    [SerializeField]GameObject equipment_suit_prefab;
    [SerializeField]CinemachineVirtualCamera virtualCamera;
    [SerializeField]GameObject[] ui_panels;
    [SerializeField]GameObject ui_bike;
    [SerializeField]Transform character_object;
    [SerializeField]Transform character_equipement_view;
    CinemachineTransposer CinemachineTransposer;
    CinemachineComposer CinemachineComposer;
    public float character_screenX_center = 0.5f;
    public float character_screenX_shift_left = 0.3f;
    public Toggle[] toggles;
    [Header("Bike Toggles")]
    public Toggle[] toggles_bike;
    public GameObject[] bike_status;
    public TextMeshProUGUI[] txt_bike_index;
    public ToggleGroup equipment_toggle_group;
    int equipmentIndex=-1;
    int bikeEquipmentIndex = 0;
    bool isClearScreen = false;
    string equipmentKey = "";

    void Start(){

        CinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        CinemachineComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        equipment_window.ObserveEveryValueChanged(w =>w.activeSelf).Subscribe(_=>{
            if(_){
                virtualCamera.Follow = character_equipement_view;
                virtualCamera.LookAt = character_equipement_view;
                //CinemachineComposer.
                //DOTween.To(()=> this.CinemachineTransposer.m_FollowOffset.x,x => this.CinemachineTransposer.m_FollowOffset.x = x , character_screenX_shift_left,0.5f).SetAutoKill();
            }else
            {
                virtualCamera.Follow = character_object;
                virtualCamera.LookAt = character_object;
                //DOTween.To(()=> this.CinemachineTransposer.m_FollowOffset.x,x => this.CinemachineTransposer.m_FollowOffset.x = x , character_screenX_center,0.5f).SetAutoKill();
            }
        }).AddTo(this);

        b_clearScreen.OnClickAsObservable().Subscribe(_=>{
            isClearScreen = !isClearScreen;

            Debug.Log("isclearScreen "+isClearScreen);
            ui_panels.All(c => { c.SetActive(!isClearScreen); return true; }); // ทำไมต้อง return true?
            if(isClearScreen){
                toggles.All(t => {t.isOn = false;return true;});
                virtualCamera.Follow = character_object;
                ClearEquipmentWindow();
            }
            else{
                equipment_window.SetActive(false);
            }
        }).AddTo(this);
        foreach (var item in toggles)
        {
            item.OnValueChangedAsObservable().Subscribe(_=>{
                var index = Array.IndexOf(toggles,item);
                ui_bike.SetActive(index == 5);
                if(!_){
                    if(equipmentIndex == index)
                        equipment_window.SetActive(_);
                    return;
                }
                equipment_window.SetActive(_);
                equipmentIndex = index;
                if(index < 5){
                    var equipmentElement = GameDataManager.Instance.equipmentData.data.ElementAtOrDefault(equipmentIndex);
                    SetupEquipmentWindow(equipmentElement);
                }else
                {
                    Debug.Log("bikeEquipmentIndex "+bikeEquipmentIndex);
                    var bikeEquipmentElement = GameDataManager.Instance.bikeEquipmentData.data.ElementAtOrDefault(bikeEquipmentIndex);
                    SetupBikeEquipmentWindow(bikeEquipmentElement);
                }
            }).AddTo(this);
        }
        foreach (var item in toggles_bike)
        {
            item.OnValueChangedAsObservable().Subscribe(_=>{
                bikeEquipmentIndex = Array.IndexOf(toggles_bike,item);
                 Debug.Log("bikeEquipmentIndex Changed.."+bikeEquipmentIndex);
                bike_status[bikeEquipmentIndex].SetActive(_);
                txt_bike_index[bikeEquipmentIndex].color = _ ? Color.black : Color.white;
                if(!_)return;
                var equipmentElement = GameDataManager.Instance.bikeEquipmentData.data.ElementAtOrDefault(bikeEquipmentIndex);
                SetupBikeEquipmentWindow(equipmentElement);
            }).AddTo(this);
        }
        bikeEquipmentIndex = 0;
    }
    async void SetupEquipmentWindow(KeyValuePair<string,List<PartEquipmentData>> equipmentData){
            ClearEquipmentWindow();
            Debug.Log("equipmentData Key "+equipmentData.Key);
            var atlastKey = AddressableKeys.LABEL_ATLAS+"/equipment_"+equipmentData.Key+".spriteatlas";
            Debug.Log("atlastKey "+atlastKey);
            var atlasSprite = await AddressableManager.Instance.LoadObject<SpriteAtlas>(atlastKey);
            if(atlasSprite == null)return;
            try{
                if(equipmentData.Value.Count <= 0)return;
                foreach (var item in equipmentData.Value)
                {
                    var go = Instantiate(equipment_prefab,equipment_content);
                    go.transform.localScale = Vector3.one;
                    go.GetComponent<EquipmentIconPrefab>().Setup(equipmentIndex,atlasSprite,item,equipment_toggle_group);
                }
            }catch(Exception e){
                Debug.Log("Error exception "+e.Message);
            }

        }
        async void SetupBikeEquipmentWindow(KeyValuePair<string,List<PartBikeEquipmentData>> bikeEquipmentData){
             ClearEquipmentWindow();
            Debug.Log("equipmentData Key "+bikeEquipmentData.Key);
            var atlastKey = AddressableKeys.LABEL_ATLAS+"/equipment_"+bikeEquipmentData.Key+".spriteatlas";
            Debug.Log("atlastKey "+atlastKey);
            var atlasSprite = await AddressableManager.Instance.LoadObject<SpriteAtlas>(atlastKey);
            if(atlasSprite == null)return;
            try{
                if(bikeEquipmentData.Value.Count <= 0)return;
                foreach (var item in bikeEquipmentData.Value)
                {
                    var go = Instantiate(equipment_prefab,equipment_content);
                    go.transform.localScale = Vector3.one;
                    go.GetComponent<EquipmentIconPrefab>().Setup(equipmentIndex,atlasSprite,item,equipment_toggle_group);
                }
            }catch(Exception e){
                Debug.Log("Error exception "+e.Message);
            }
        }
        void ClearEquipmentWindow(){
            for (int i = 0; i < equipment_content.childCount; i++)
            {
                Destroy(equipment_content.GetChild(i).gameObject);
            }
            
        }
}
