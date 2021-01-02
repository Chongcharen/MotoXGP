using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Newtonsoft.Json;
using System.IO;
public class UI_UpdateWheel : MonoBehaviour
{
    public static Subject<float> OnUpdateSpring = new Subject<float>();
    public static Subject<float> OnUpdateDamper = new Subject<float>();
    public static Subject<float> OnUpdateChoke = new Subject<float>();

   [Header("Main UI")]
   public Button b_Setting;
   public Button b_Rigidbody_setting;
   public Button b_Engine_setting;
   public Button b_FrontWheel_setting;
   public Button b_RearWheel_setting;
   public Button b_bike_preset;
   public Button b_Landing_Setting;
   public Button b_close;
  
   [Header("back")]
   public Button b_back_rigidbody;
   public Button b_back_engine;
   public Button b_back_frontWheel;
   public Button b_back_rearWheel;
   public Button b_back_preset;
   public GameObject ui_main,ui_main_setting,ui_rigidbody,ui_Engine,ui_frontWheel,ui_rearWheel,ui_preset,ui_landing;
   #region Landing
   [Header("Landing")]
   public Slider slider_landing_maxFall;
   public Slider slider_landing_maxSpeed;
   public Slider slider_landing_minFall;
   public Slider slider_landing_minSpeed;
   public Toggle t_landing_active;

   public TextMeshProUGUI txt_landing_maxFall;
   public TextMeshProUGUI txt_landing_maxSpeed;
   public TextMeshProUGUI txt_landing_minFall;
   public TextMeshProUGUI txt_landing_minSpeed;
   public Button b_landing_default;
   public Button b_landing_save;
   public Button b_landing_load;
   public Button b_back_landing;

   #endregion
   #region Rigidbody
   [Header("RigidBody")]
   public Slider slider_rigidbody_mass;
   public Slider slider_rigidbody_drag;
   public Slider slider_rigidbody_angulardrag;
   public TextMeshProUGUI txt_rigidbody_mass;
   public TextMeshProUGUI txt_rigidbody_drag;
   public TextMeshProUGUI txt_rigidbody_angularDrag;
   public Button b_rigidbody_default;
   public Button b_rigidbody_save;
   public Button b_rigidbody_load;
   #endregion
   #region Engine
   [Header("Engine")]
   public Slider slider_max_torque;
   public Slider slider_max_velocity;
   public Slider slider_nos_velcity;
   public Slider slider_jump_power;
   public Slider slider_roate_bike_power;
   public Slider slider_animaion_choke;
   public Slider slider_decrease_torque;


   public TextMeshProUGUI txt_max_torque;
   public TextMeshProUGUI txt_max_velocity;
   public TextMeshProUGUI txt_nos_velcity;
   public TextMeshProUGUI txt_jump_power;
   public TextMeshProUGUI txt_roate_bike_power;
   public TextMeshProUGUI txt_animation_choke;
   public TextMeshProUGUI txt_decrease_torque;
   
   public Button b_engine_default;
   public Button b_engine_save;
   public Button b_engine_load;
   #endregion
   #region FrontWheel
     [Header("FrontWheel Setting")]
     //wheel
     public Slider slider_fw_mass;
     public Slider slider_fw_wheelDampingRate;
     public Slider slider_fw_suspensionDistance;
     public Slider slider_fw_forceApppointDistance;
     //Suspension Spring
     public Slider slider_fw_spring;
     public Slider slider_fw_damper;
     public Slider slider_fw_targetPositon;
     //Forward friction
     public Slider slider_fw_extremumSlip;
     public Slider slider_fw_extremumValue;
     public Slider slider_fw_asymptoteSlip;
     public Slider slider_fw_asymptoteValue;
     public Slider slider_fw_stiffness;
     public TextMeshProUGUI txt_fw_mass;
     public TextMeshProUGUI txt_fw_wheelDampingRate;
     public TextMeshProUGUI txt_fw_suspensionDistance;
     public TextMeshProUGUI txt_fw_forceApppointDistance;
     public TextMeshProUGUI txt_fw_spring;
     public TextMeshProUGUI txt_fw_damper;
     public TextMeshProUGUI txt_fw_targetPositon;
     public TextMeshProUGUI txt_fw_extremumSlip;
     public TextMeshProUGUI txt_fw_extremumValue;
     public TextMeshProUGUI txt_fw_asymptoteSlip;
     public TextMeshProUGUI txt_fw_asymptoteValue;
     public TextMeshProUGUI txt_fw_stiffness;

     public Button b_fw_default;
     public Button b_fw_save;
     public Button b_fw_load;
#endregion
   #region RearWheel
     [Header("RearWheel Setting")]
     //wheel
     public Slider slider_rw_mass;
     public Slider slider_rw_wheelDampingRate;
     public Slider slider_rw_suspensionDistance;
     public Slider slider_rw_forceApppointDistance;
     //Suspension Spring
     public Slider slider_rw_spring;
     public Slider slider_rw_damper;
     public Slider slider_rw_targetPositon;
     //Forward friction
     public Slider slider_rw_extremumSlip;
     public Slider slider_rw_extremumValue;
     public Slider slider_rw_asymptoteSlip;
     public Slider slider_rw_asymptoteValue;
     public Slider slider_rw_stiffness;

     public TextMeshProUGUI txt_rw_mass;
     public TextMeshProUGUI txt_rw_wheelDampingRate;
     public TextMeshProUGUI txt_rw_suspensionDistance;
     public TextMeshProUGUI txt_rw_forceApppointDistance;
     public TextMeshProUGUI txt_rw_spring;
     public TextMeshProUGUI txt_rw_damper;
     public TextMeshProUGUI txt_rw_targetPositon;
     public TextMeshProUGUI txt_rw_extremumSlip;
     public TextMeshProUGUI txt_rw_extremumValue;
     public TextMeshProUGUI txt_rw_asymptoteSlip;
     public TextMeshProUGUI txt_rw_asymptoteValue;
     public TextMeshProUGUI txt_rw_stiffness;
     public Button b_rw_default;
     public Button b_rw_save;
     public Button b_rw_load;
     #endregion

   bool settingActive = false;
   private void Start()
   {
        //BikePreset
        ViewBikePreset.OnSelectBikePreset.Subscribe(preset =>{
             UpdateBikeUIData(preset);
             OnUpdateRigidbodySetting.OnNext(preset.rigidbody);
             OnUpdateBikeEngineSetting.OnNext(preset.engineSetting);
             OnUpdateFrontWheelSetting.OnNext(preset.wheelColliderSettings[0]);
             OnUpdateRearWheelSetting.OnNext(preset.wheelColliderSettings[1]);
             OnUpdateLandingSetting.OnNext(preset.landing);
        }).AddTo(this);
        //

        b_close.OnClickAsObservable().Subscribe(_=>{
             settingActive = false;
             ActiveSetting(settingActive);
        }).AddTo(this);
        b_Setting.OnClickAsObservable().Subscribe(_ =>{
             settingActive = !settingActive;
          ActiveSetting(settingActive);
        }).AddTo(this);
        b_Rigidbody_setting.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(false);
             ui_rigidbody.gameObject.SetActive(true);
        }).AddTo(this);
        b_Engine_setting.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(false);
             ui_Engine.gameObject.SetActive(true);
        }).AddTo(this);
        b_FrontWheel_setting.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(false);
             ui_frontWheel.gameObject.SetActive(true);
        }).AddTo(this);
        b_RearWheel_setting.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(false);
             ui_rearWheel.gameObject.SetActive(true);
        }).AddTo(this);
        b_bike_preset.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(false);
             ui_preset.gameObject.SetActive(true);
        }).AddTo(this);
        b_Landing_Setting.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(false);
             ui_landing.gameObject.SetActive(true);
        }).AddTo(this);
        b_back_rigidbody.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(true);
             ui_rigidbody.gameObject.SetActive(false);
        }).AddTo(this);
        b_back_engine.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(true);
             ui_Engine.gameObject.SetActive(false);
        }).AddTo(this);
        b_back_frontWheel.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(true);
             ui_frontWheel.gameObject.SetActive(false);
        }).AddTo(this);
        b_back_rearWheel.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(true);
             ui_rearWheel.gameObject.SetActive(false);
        }).AddTo(this);
        b_back_preset.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(true);
             ui_preset.gameObject.SetActive(false);
        }).AddTo(this);
        b_back_landing.OnClickAsObservable().Subscribe(_=>{
             ui_main_setting.gameObject.SetActive(true);
             ui_landing.gameObject.SetActive(false);
        }).AddTo(this);

     BikeBoltSystem.OnUpdateBikeSettingData.Subscribe(data =>{
          UpdateBikeUIData(data);
          LoadRigidBody();
          LoadEngine();
          LoadFrontWheel();
          LoadRearWheel();
          LoadBikePreset();
          LoadLanding();
     }).AddTo(this);
     //EngineSubscribe
     #region Landing Slider
     slider_landing_maxFall.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateLandingMaxFall.OnNext(_);
          txt_landing_maxFall.text = _.ToString();
     }).AddTo(this);
     slider_landing_maxSpeed.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateLandingMaxSpeed.OnNext(_);
          txt_landing_maxSpeed.text = _.ToString();
     }).AddTo(this);
     slider_landing_minFall.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateLandingMinFall.OnNext(_);
          txt_landing_minFall.text = _.ToString();
     }).AddTo(this);
     slider_landing_minSpeed.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateLandingMinSpeed.OnNext(_);
          txt_landing_minSpeed.text = _.ToString();
     }).AddTo(this);
     t_landing_active.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateLandingActive.OnNext(_);
     }).AddTo(this);
     b_landing_default.OnClickAsObservable().Subscribe(_=>{
          LoadDefaultLanding();
     }).AddTo(this);
     b_landing_load.OnClickAsObservable().Subscribe(_=>{
          LoadLanding();
     }).AddTo(this);
     b_landing_save.OnClickAsObservable().Subscribe(_=>{
          SaveLanding();
     }).AddTo(this);
     #endregion
     #region Rigidbody Slider
     slider_rigidbody_mass.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateRigidbodyMass.OnNext(_);
          txt_rigidbody_mass.text = _.ToString();
     }).AddTo(this);
     slider_rigidbody_drag.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateRigidbodyDrag.OnNext(_);
          txt_rigidbody_drag.text = _.ToString();
     }).AddTo(this);
     slider_rigidbody_angulardrag.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateRigidbodyAngularDarg.OnNext(_);
          txt_rigidbody_angularDrag.text = _.ToString();
     }).AddTo(this);
     b_rigidbody_default.OnClickAsObservable().Subscribe(_=>{
          LoadDefaultRigidBody();
     }).AddTo(this);
     b_rigidbody_load.OnClickAsObservable().Subscribe(_=>{
          LoadRigidBody();
     }).AddTo(this);
     b_rigidbody_save.OnClickAsObservable().Subscribe(_=>{
          SaveRigidBody();
     }).AddTo(this);
     #endregion
     #region Engine Slider
     slider_max_torque.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateMaxTorque.OnNext(_);
          txt_max_torque.text = _.ToString();
     }).AddTo(this);
     slider_max_velocity.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateMaxVelocity.OnNext(_);
          txt_max_velocity.text = Mathf.CeilToInt(_*3.6f).ToString();
     }).AddTo(this);
     slider_nos_velcity.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateNosVelocity.OnNext(_);
          txt_nos_velcity.text =  Mathf.CeilToInt(_*3.6f).ToString();
     }).AddTo(this);
     slider_jump_power.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateJumpPower.OnNext(_);
          txt_jump_power.text = _.ToString();
     }).AddTo(this);
     slider_roate_bike_power.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateRotateBikePower.OnNext(_);
          txt_roate_bike_power.text = _.ToString();
     }).AddTo(this);
     slider_animaion_choke.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateAnimationChoke.OnNext(_);
          txt_animation_choke.text = _.ToString();
     }).AddTo(this);
     slider_decrease_torque.OnValueChangedAsObservable().Subscribe(_=>{
          OnupdateDecreaseTorque.OnNext(_);
          txt_decrease_torque.text = _.ToString();
     }).AddTo(this);
     b_engine_default.OnClickAsObservable().Subscribe(_=>{
          LoadDefaultEngine();
     }).AddTo(this);
     b_engine_load.OnClickAsObservable().Subscribe(_=>{
          LoadEngine();
     }).AddTo(this);
     b_engine_save.OnClickAsObservable().Subscribe(_=>{
          SaveEngine();
     }).AddTo(this);
     #endregion
     //FrontWheel
     #region ForwardWheel Slider
     slider_fw_mass.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateWheelMass.OnNext(Tuple.Create(0,_));
          txt_fw_mass.text = _.ToString();
     }).AddTo(this);
     slider_fw_wheelDampingRate.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateWheelDampingRate.OnNext(Tuple.Create(0,_));
          txt_fw_wheelDampingRate.text = _.ToString();
     }).AddTo(this);
     slider_fw_suspensionDistance.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateWheelSuspensionDistance.OnNext(Tuple.Create(0,_));
          txt_fw_suspensionDistance.text = _.ToString();
     }).AddTo(this);
     slider_fw_forceApppointDistance.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelForceApppointDistance.OnNext(Tuple.Create(0,_));
         txt_fw_forceApppointDistance.text = _.ToString();
     }).AddTo(this);
     slider_fw_spring.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelSpring.OnNext(Tuple.Create(0,_));
          txt_fw_spring.text = _.ToString();
     }).AddTo(this);
     slider_fw_damper.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelDamper.OnNext(Tuple.Create(0,_));
         txt_fw_damper.text = _.ToString();
     }).AddTo(this);
     slider_fw_targetPositon.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelTargetPosition.OnNext(Tuple.Create(0,_));
         txt_fw_targetPositon.text = _.ToString();
     }).AddTo(this);
     slider_fw_extremumSlip.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelExtremumSlip.OnNext(Tuple.Create(0,_));
         txt_fw_extremumSlip.text = _.ToString();
     }).AddTo(this);
     slider_fw_extremumValue.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelExtremumValue.OnNext(Tuple.Create(0,_));
         txt_fw_extremumValue.text = _.ToString();
     }).AddTo(this);
     slider_fw_asymptoteSlip.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelAsymptoteSlip.OnNext(Tuple.Create(0,_));
          txt_fw_asymptoteSlip.text = _.ToString();
     }).AddTo(this);
     slider_fw_asymptoteValue.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelAsymptoteValue.OnNext(Tuple.Create(0,_));
         txt_fw_asymptoteValue.text = _.ToString();
     }).AddTo(this);
     slider_fw_stiffness.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelStiffness.OnNext(Tuple.Create(0,_));
         txt_fw_stiffness.text =_.ToString();
     }).AddTo(this);
     b_fw_default.OnClickAsObservable().Subscribe(_=>{
          LoadDefaultFrontWheel();
     }).AddTo(this);
     b_fw_load.OnClickAsObservable().Subscribe(_=>{
          LoadFrontWheel();
     }).AddTo(this);
     b_fw_save.OnClickAsObservable().Subscribe(_=>{
          SaveFrontWheel();
     }).AddTo(this);
     #endregion
     //rearwheel
     #region RearWheel Slider
     slider_rw_mass.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateWheelMass.OnNext(Tuple.Create(1,_));
          txt_rw_mass.text = _.ToString();
     }).AddTo(this);
     slider_rw_wheelDampingRate.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateWheelDampingRate.OnNext(Tuple.Create(1,_));
          txt_rw_wheelDampingRate.text =_.ToString();
     }).AddTo(this);
     slider_rw_suspensionDistance.OnValueChangedAsObservable().Subscribe(_=>{
          OnUpdateWheelSuspensionDistance.OnNext(Tuple.Create(1,_));
          txt_rw_suspensionDistance.text = _.ToString();
     }).AddTo(this);
     slider_rw_forceApppointDistance.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelForceApppointDistance.OnNext(Tuple.Create(1,_));
         txt_rw_forceApppointDistance.text = _.ToString();
     }).AddTo(this);
     slider_rw_spring.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelSpring.OnNext(Tuple.Create(1,_));
         txt_rw_spring.text = _.ToString();
     }).AddTo(this);
     slider_rw_damper.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelDamper.OnNext(Tuple.Create(1,_));
         txt_rw_damper.text = _.ToString();
     }).AddTo(this);
     slider_rw_targetPositon.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelTargetPosition.OnNext(Tuple.Create(1,_));
         txt_rw_targetPositon.text = _.ToString();
     }).AddTo(this);
     slider_rw_extremumSlip.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelExtremumSlip.OnNext(Tuple.Create(1,_));
         txt_rw_extremumSlip.text = _.ToString();
     }).AddTo(this);
     slider_rw_extremumValue.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelExtremumValue.OnNext(Tuple.Create(1,_));
          txt_rw_extremumValue.text = _.ToString();
     }).AddTo(this);
     slider_rw_asymptoteSlip.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelAsymptoteSlip.OnNext(Tuple.Create(1,_));
         txt_rw_asymptoteSlip.text = _.ToString();
     }).AddTo(this);
     slider_rw_asymptoteValue.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelAsymptoteValue.OnNext(Tuple.Create(1,_));
         txt_rw_asymptoteValue.text = _.ToString();
     }).AddTo(this);
     slider_rw_stiffness.OnValueChangedAsObservable().Subscribe(_=>{
         OnUpdateWheelStiffness.OnNext(Tuple.Create(1,_));
         txt_rw_stiffness.text = _.ToString();
     }).AddTo(this);
     b_rw_default.OnClickAsObservable().Subscribe(_=>{
          LoadDefaultRearWheel();
     }).AddTo(this);
     b_rw_load.OnClickAsObservable().Subscribe(_=>{
          LoadRearWheel();
     }).AddTo(this);
     b_rw_save.OnClickAsObservable().Subscribe(_=>{
          SaveRearWheel();
     }).AddTo(this);
     #endregion
   }
   #region Delegate
   //Landing
   public static Subject<float> OnUpdateLandingMaxFall = new Subject<float>();
   public static Subject<float> OnUpdateLandingMaxSpeed = new Subject<float>();
   public static Subject<float> OnUpdateLandingMinFall = new Subject<float>();
   public static Subject<float> OnUpdateLandingMinSpeed = new Subject<float>();
   public static Subject<bool> OnUpdateLandingActive = new Subject<bool>();
   //Rigidbody
   public static Subject<float> OnUpdateRigidbodyMass = new Subject<float>();
   public static Subject<float> OnUpdateRigidbodyDrag = new Subject<float>();
   public static Subject<float> OnUpdateRigidbodyAngularDarg = new Subject<float>();
   //engine
   public static Subject<float> OnUpdateMaxTorque = new Subject<float>();
   public static Subject<float> OnUpdateMaxVelocity = new Subject<float>();
   public static Subject<float> OnUpdateNosVelocity = new Subject<float>();
   public static Subject<float> OnUpdateJumpPower = new Subject<float>();
   public static Subject<float> OnUpdateRotateBikePower = new Subject<float>();
   public static Subject<float> OnUpdateAnimationChoke = new Subject<float>();
   public static Subject<float> OnupdateDecreaseTorque = new Subject<float>();
   //Wheel
   public static Subject<Tuple<int,float>> OnUpdateWheelMass = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelDampingRate = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelSuspensionDistance = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelForceApppointDistance = new Subject<Tuple<int, float>>();
   //Suspension Spring
   public static Subject<Tuple<int,float>> OnUpdateWheelSpring = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelDamper = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelTargetPosition = new Subject<Tuple<int, float>>();
   //Forward friction 
   public static Subject<Tuple<int,float>> OnUpdateWheelExtremumSlip = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelExtremumValue = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelAsymptoteSlip = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelAsymptoteValue = new Subject<Tuple<int, float>>();
   public static Subject<Tuple<int,float>> OnUpdateWheelStiffness = new Subject<Tuple<int, float>>();

   //UpdateSet
   public static Subject<RigidbodyTuner> OnUpdateRigidbodySetting = new Subject<RigidbodyTuner>();
   public static Subject<BikeEngineSetting> OnUpdateBikeEngineSetting =new Subject<BikeEngineSetting>();
   public static Subject<WheelColliderSetting> OnUpdateFrontWheelSetting = new Subject<WheelColliderSetting>();
   public static Subject<WheelColliderSetting> OnUpdateRearWheelSetting = new Subject<WheelColliderSetting>();
   public static Subject<LandingTunner> OnUpdateLandingSetting = new Subject<LandingTunner>();
   #endregion
   BikeSettingMappingData defaultBikeSettingData;
   void UpdateBikeUIData(BikeSettingMappingData data){
        Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<UpdateBikeUIDAta>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> "+data);
        print(Depug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<UpdateBikeUIDAta>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>",Color.white));
        Debug.Log("landing data "+data.landing);
        //engine
        UpdateRigidBodyUI(data.rigidbody);
        UpdateEngineUI(data.engineSetting);
        UpdateFrontWheelUI(data.wheelColliderSettings[0]);
        UpdateRearWheelUI(data.wheelColliderSettings[1]);
        UpdateLandingUI(data.landing);
        defaultBikeSettingData = data;
     //    var presets = new List<BikeSettingMappingData>();
     //    defaultBikeSettingData = data;
     //    presets.Add(data);
     //    presets.Add(data);
     //    var json = JsonConvert.SerializeObject(presets);

     //    Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
     //    Debug.Log("JSON = "+json);
   }
   public void ActiveSetting(bool active){
        ui_main.gameObject.SetActive(active);
        if(!active){
             ui_main_setting.SetActive(true);
             ui_Engine.gameObject.SetActive(false);
             ui_frontWheel.gameObject.SetActive(false);
             ui_rearWheel.gameObject.SetActive(false);
             ui_preset.gameObject.SetActive(false);
             ui_landing.SetActive(false);
        }
   }
   void LoadDefaultLanding(){
        Debug.Log("landing "+defaultBikeSettingData.landing);
        OnUpdateLandingSetting.OnNext(defaultBikeSettingData.landing);
        UpdateLandingUI(defaultBikeSettingData.landing);
   }
   void LoadLanding(){
         if(string.IsNullOrEmpty(PlayerPrefs.GetString("landing_tuner")))return;
        var landingTuner = JsonConvert.DeserializeObject<LandingTunner>(PlayerPrefs.GetString("landing_tuner"));
        OnUpdateLandingSetting.OnNext(landingTuner);
        UpdateLandingUI(landingTuner);
   }
   void SaveLanding(){
         var landingTuner = new LandingTunner(){
              maxFall = slider_landing_maxFall.value,
              maxSpeed = slider_landing_maxSpeed.value,
              minFall = slider_landing_minFall.value,
              minSpeed = slider_landing_minSpeed.value,
              active = t_landing_active.isOn
        };
        var json = JsonConvert.SerializeObject(landingTuner);
        PlayerPrefs.SetString("landing_tuner",json);
   }
   void UpdateLandingUI(LandingTunner tuner){
        slider_landing_maxFall.SetValueWithoutNotify(tuner.maxFall);
        slider_landing_maxSpeed.SetValueWithoutNotify(tuner.maxSpeed);
        slider_landing_minFall.SetValueWithoutNotify(tuner.minFall);
        slider_landing_minSpeed.SetValueWithoutNotify(tuner.minSpeed);
        t_landing_active.isOn = tuner.active;
        txt_landing_maxFall.text = tuner.maxFall.ToString();
        txt_landing_maxSpeed.text = tuner.maxSpeed.ToString();
        txt_landing_minFall.text = tuner.minFall.ToString();
        txt_landing_minSpeed.text = tuner.minSpeed.ToString();
   }
   void LoadDefaultRigidBody(){
        OnUpdateRigidbodySetting.OnNext(defaultBikeSettingData.rigidbody);
        UpdateRigidBodyUI(defaultBikeSettingData.rigidbody);
   }
   void LoadRigidBody(){
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("rigidbody_tuner")))return;
        var rb = JsonConvert.DeserializeObject<RigidbodyTuner>(PlayerPrefs.GetString("rigidbody_tuner"));
        OnUpdateRigidbodySetting.OnNext(rb);
        UpdateRigidBodyUI(rb);
   }
   void SaveRigidBody(){
        var rb = new Rigidbody(){
             mass = slider_rigidbody_mass.value,
             drag = slider_rigidbody_drag.value,
             angularDrag = slider_rigidbody_angulardrag.value
        };
        var json = JsonConvert.SerializeObject(rb);
        PlayerPrefs.SetString("rigidbody_tuner",json);
   }
   void UpdateRigidBodyUI(RigidbodyTuner rb){
        slider_rigidbody_mass.SetValueWithoutNotify(rb.mass);
        slider_rigidbody_drag.SetValueWithoutNotify(rb.drag);
        slider_rigidbody_angulardrag.SetValueWithoutNotify(rb.angularDrag);

        txt_rigidbody_mass.text = Mathf.CeilToInt(rb.mass).ToString();
        txt_rigidbody_drag.text = rb.drag.ToString();
        txt_rigidbody_angularDrag.text = rb.angularDrag.ToString();
   }
   void LoadDefaultEngine(){
        OnUpdateBikeEngineSetting.OnNext(defaultBikeSettingData.engineSetting);
        UpdateEngineUI(defaultBikeSettingData.engineSetting);
   }
   void LoadEngine(){
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("engine_tuner")))return;
        var engine = JsonConvert.DeserializeObject<BikeEngineSetting>(PlayerPrefs.GetString("engine_tuner"));
        OnUpdateBikeEngineSetting.OnNext(engine);
        UpdateEngineUI(engine);
   }
   void SaveEngine(){
        var engine = new BikeEngineSetting{
             engine_max_torque = slider_max_torque.value,
             engine_max_velocity = slider_max_velocity.value,
             engine_nos_velocity = slider_nos_velcity.value,
             engine_jump_power = slider_jump_power.value,
             engine_rotate_bike_power = slider_roate_bike_power.value,
             engine_animation_choke = slider_animaion_choke.value,
             engine_decrease_torque = slider_decrease_torque.value
        };
        var json = JsonConvert.SerializeObject(engine);
        PlayerPrefs.SetString("engine_tuner",json);
   }
   void UpdateEngineUI(BikeEngineSetting engineSetting){
        slider_max_torque.SetValueWithoutNotify(engineSetting.engine_max_torque);
        slider_max_velocity.SetValueWithoutNotify(engineSetting.engine_max_velocity);
        slider_nos_velcity.SetValueWithoutNotify(engineSetting.engine_nos_velocity);
        slider_jump_power.SetValueWithoutNotify(engineSetting.engine_jump_power);
        slider_roate_bike_power.SetValueWithoutNotify(engineSetting.engine_rotate_bike_power);
        slider_animaion_choke.SetValueWithoutNotify(engineSetting.engine_animation_choke);
        slider_decrease_torque.SetValueWithoutNotify(engineSetting.engine_decrease_torque);
        txt_max_torque.text = engineSetting.engine_max_torque.ToString();
        txt_max_velocity.text = Mathf.CeilToInt(engineSetting.engine_max_velocity*3.6f).ToString();
        txt_nos_velcity.text =  Mathf.CeilToInt(engineSetting.engine_nos_velocity*3.6f).ToString();
        txt_jump_power.text = engineSetting.engine_jump_power.ToString();
        txt_roate_bike_power.text = engineSetting.engine_rotate_bike_power.ToString();
        txt_animation_choke.text = engineSetting.engine_animation_choke.ToString();
        txt_decrease_torque.text = engineSetting.engine_decrease_torque.ToString();
   }

   void LoadDefaultFrontWheel(){
        OnUpdateFrontWheelSetting.OnNext(defaultBikeSettingData.wheelColliderSettings[0]);
        UpdateFrontWheelUI(defaultBikeSettingData.wheelColliderSettings[0]);
   }
   void LoadFrontWheel(){
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("frontwheel_tuner")))return;
        var frontWheel = JsonConvert.DeserializeObject<WheelColliderSetting>(PlayerPrefs.GetString("frontwheel_tuner"));
        OnUpdateFrontWheelSetting.OnNext(frontWheel);
        UpdateFrontWheelUI(frontWheel);
   }
   void UpdateFrontWheelUI(WheelColliderSetting wheelSetting){
       slider_fw_mass.SetValueWithoutNotify(wheelSetting.mass);
        slider_fw_wheelDampingRate.SetValueWithoutNotify(wheelSetting.wheelDampingRate);
        slider_fw_suspensionDistance.SetValueWithoutNotify(wheelSetting.suspensionDistance);
        slider_fw_forceApppointDistance.SetValueWithoutNotify(wheelSetting.forceApppointDistance);
          //Suspension Spring
          slider_fw_spring.SetValueWithoutNotify(wheelSetting.spring);
          slider_fw_damper.SetValueWithoutNotify(wheelSetting.damper);
          slider_fw_targetPositon.SetValueWithoutNotify(wheelSetting.targetPosition);
          //Forward friction
          slider_fw_extremumSlip.SetValueWithoutNotify(wheelSetting.extremumSlip);
          slider_fw_extremumValue.SetValueWithoutNotify(wheelSetting.extremumValue);
          slider_fw_asymptoteSlip.SetValueWithoutNotify(wheelSetting.asymptoteSlip);
          slider_fw_asymptoteValue.SetValueWithoutNotify(wheelSetting.asymptoteValue);
          slider_fw_stiffness.SetValueWithoutNotify(wheelSetting.stiffness);

          txt_fw_mass.text = wheelSetting.mass.ToString();
          txt_fw_wheelDampingRate.text = wheelSetting.wheelDampingRate.ToString();
          txt_fw_suspensionDistance.text = wheelSetting.suspensionDistance.ToString();
          txt_fw_forceApppointDistance.text = wheelSetting.forceApppointDistance.ToString();

          txt_fw_spring.text = wheelSetting.spring.ToString();
          txt_fw_damper.text = wheelSetting.damper.ToString();
          txt_fw_targetPositon.text = wheelSetting.targetPosition.ToString();

          txt_fw_extremumSlip.text = wheelSetting.extremumSlip.ToString();
          txt_fw_extremumValue.text = wheelSetting.extremumValue.ToString();
          txt_fw_asymptoteSlip.text = wheelSetting.asymptoteSlip.ToString();
          txt_fw_asymptoteValue.text = wheelSetting.asymptoteValue.ToString();
          txt_fw_stiffness.text = wheelSetting.stiffness.ToString();
   }
   void SaveFrontWheel(){
         var frontWheel = new WheelColliderSetting{
             mass = slider_fw_mass.value,
             wheelDampingRate = slider_fw_wheelDampingRate.value,
             suspensionDistance = slider_fw_suspensionDistance.value,
             forceApppointDistance = slider_fw_forceApppointDistance.value,
             spring = slider_fw_spring.value,
             damper = slider_fw_damper.value,
             targetPosition = slider_fw_targetPositon.value,
             extremumSlip = slider_fw_extremumSlip.value,
             extremumValue = slider_fw_extremumValue.value,
             asymptoteSlip = slider_fw_asymptoteSlip.value,
             asymptoteValue = slider_fw_asymptoteValue.value,
             stiffness = slider_fw_stiffness.value
        };
        var json = JsonConvert.SerializeObject(frontWheel);
        PlayerPrefs.SetString("frontwheel_tuner",json);
   }
   void LoadDefaultRearWheel(){
         OnUpdateRearWheelSetting.OnNext(defaultBikeSettingData.wheelColliderSettings[1]);
         UpdateRearWheelUI(defaultBikeSettingData.wheelColliderSettings[1]);
   }
   void LoadRearWheel(){
         if(string.IsNullOrEmpty(PlayerPrefs.GetString("rearwheel_tuner")))return;
        var rearWheel = JsonConvert.DeserializeObject<WheelColliderSetting>(PlayerPrefs.GetString("rearwheel_tuner"));
        OnUpdateRearWheelSetting.OnNext(rearWheel);
        UpdateRearWheelUI(rearWheel);
   }
   void UpdateRearWheelUI(WheelColliderSetting wheelSetting){
       slider_rw_mass.SetValueWithoutNotify(wheelSetting.mass);
        slider_rw_wheelDampingRate.SetValueWithoutNotify(wheelSetting.wheelDampingRate);
        slider_rw_suspensionDistance.SetValueWithoutNotify(wheelSetting.suspensionDistance);
        slider_rw_forceApppointDistance.SetValueWithoutNotify(wheelSetting.forceApppointDistance);
          //Suspension Spring
          slider_rw_spring.SetValueWithoutNotify(wheelSetting.spring);
          slider_rw_damper.SetValueWithoutNotify(wheelSetting.damper);
          slider_rw_targetPositon.SetValueWithoutNotify(wheelSetting.targetPosition);
          //Forward friction
          slider_rw_extremumSlip.SetValueWithoutNotify(wheelSetting.extremumSlip);
          slider_rw_extremumValue.SetValueWithoutNotify(wheelSetting.extremumValue);
          slider_rw_asymptoteSlip.SetValueWithoutNotify(wheelSetting.asymptoteSlip);
          slider_rw_asymptoteValue.SetValueWithoutNotify(wheelSetting.asymptoteValue);
          slider_rw_stiffness.SetValueWithoutNotify(wheelSetting.stiffness);

          txt_rw_mass.text = wheelSetting.mass.ToString();
          txt_rw_wheelDampingRate.text = wheelSetting.wheelDampingRate.ToString();
          txt_rw_suspensionDistance.text = wheelSetting.suspensionDistance.ToString();
          txt_rw_forceApppointDistance.text = wheelSetting.forceApppointDistance.ToString();

          txt_rw_spring.text = wheelSetting.spring.ToString();
          txt_rw_damper.text = wheelSetting.damper.ToString();
          txt_rw_targetPositon.text = wheelSetting.targetPosition.ToString();

          txt_rw_extremumSlip.text = wheelSetting.extremumSlip.ToString();
          txt_rw_extremumValue.text = wheelSetting.extremumValue.ToString();
          txt_rw_asymptoteSlip.text = wheelSetting.asymptoteSlip.ToString();
          txt_rw_asymptoteValue.text = wheelSetting.asymptoteValue.ToString();
          txt_rw_stiffness.text = wheelSetting.stiffness.ToString();
   }
   void SaveRearWheel(){
          var rearWheel = new WheelColliderSetting{
             mass = slider_rw_mass.value,
             wheelDampingRate = slider_rw_wheelDampingRate.value,
             suspensionDistance = slider_rw_suspensionDistance.value,
             forceApppointDistance = slider_rw_forceApppointDistance.value,
             spring = slider_rw_spring.value,
             damper = slider_rw_damper.value,
             targetPosition = slider_rw_targetPositon.value,
             extremumSlip = slider_rw_extremumSlip.value,
             extremumValue = slider_rw_extremumValue.value,
             asymptoteSlip = slider_rw_asymptoteSlip.value,
             asymptoteValue = slider_rw_asymptoteValue.value,
             stiffness = slider_rw_stiffness.value
        };
        var json = JsonConvert.SerializeObject(rearWheel);
        PlayerPrefs.SetString("rearwheel_tuner",json);
   }

   void LoadBikePreset(){
       
        
   }
}
public class BikeSettingMappingData{
     public RigidbodyTuner rigidbody;
     public LandingTunner landing;
    public BikeEngineSetting engineSetting;
    public WheelColliderSetting[] wheelColliderSettings;

}
public class LandingTunner{
     public float maxFall;
     public float maxSpeed;
     public float minFall;
     public float minSpeed;
     public bool active;
}
public class RigidbodyTuner{
     public float mass;
     public float drag;
     public float angularDrag;
}
public class BikeEngineSetting{
     public float engine_max_torque;
     public float engine_max_velocity;
     public float engine_nos_velocity;
     public float engine_jump_power;
     public float engine_rotate_bike_power;
     public float engine_animation_choke;
     public float engine_decrease_torque;
}
public class WheelColliderSetting{
      //wheel
    public float mass;
    public float wheelDampingRate;
    public float suspensionDistance;
    public float forceApppointDistance;

    //Suspension Spring
    public float spring;
    public float damper;
    public float targetPosition;
    // Forward friction
    public float extremumSlip;
    public float extremumValue;
    public float asymptoteSlip;
    public float asymptoteValue;
    public float stiffness;
}