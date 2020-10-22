using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using Cinemachine;
using DG.Tweening;
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
    [SerializeField]Transform character_object;
    [SerializeField]Transform character_equipement_view;
    CinemachineTransposer CinemachineTransposer;
    CinemachineComposer CinemachineComposer;
    public float character_screenX_center = 0.5f;
    public float character_screenX_shift_left = 0.3f;
    public Toggle[] toggles;
    public ToggleGroup equipment_toggle_group;
    int equipmentIndex=-1;
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
                if(!_){
                    if(equipmentIndex == index)
                        equipment_window.SetActive(_);
                    return;
                }
                equipment_window.SetActive(_);
                equipmentIndex = index;
                var equipmentElement = GameDataManager.Instance.equipmentData.data.ElementAtOrDefault(equipmentIndex);

                SetupEquipmentWindow(equipmentElement);
                // Debug.Log("equipmentElement "+equipmentElement);
                // Debug.Log("equipmentElement Key"+equipmentElement.Key);
                // Debug.Log("equipmentElement Value"+equipmentElement.Value);
                //Debug.Log(GameDataManager.Instance.equipmentData.data.el)
            }).AddTo(this);
        }

        void SetupEquipmentWindow(KeyValuePair<string,List<PartEquipmentData>> equipmentData){
            ClearEquipmentWindow();
            Debug.Log("equipmentData Key "+equipmentData.Key);
            if(equipmentIndex == 1)
                    layoutGroup.cellSize = suit_layout;
            else
                    layoutGroup.cellSize = standard_layout;
            foreach (var item in equipmentData.Value)
            {
                var prefab = equipmentIndex == 1 ? equipment_suit_prefab : equipment_prefab;
                
                var go = Instantiate(prefab,equipment_content);
                go.transform.localScale = Vector3.one;
                go.GetComponent<EquipmentIconPrefab>().Setup(equipmentIndex,"equipment_"+equipmentData.Key,item,equipment_toggle_group);
            }

        }
        void ClearEquipmentWindow(){
            for (int i = 0; i < equipment_content.childCount; i++)
            {
                Destroy(equipment_content.GetChild(i).gameObject);
            }
            
        }
    
    }
}
