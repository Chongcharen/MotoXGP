using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class UI_UpdateWheel : MonoBehaviour
{
    public static Subject<float> OnUpdateSpring = new Subject<float>();
    public static Subject<float> OnUpdateDamper = new Subject<float>();
    public static Subject<float> OnUpdateChoke = new Subject<float>();

     public Slider slider_spring,slider_damper,slider_choke;
   public TMP_InputField input_spring,input_damper,input_visualChoke;
   public TextMeshProUGUI spring_txt,damper_txt,choke_txt;

   private void Start()
   {
     slider_spring.OnValueChangedAsObservable().Subscribe(_ =>{
          spring_txt.text = _.ToString();
          OnUpdateSpring.OnNext(_);
     }).AddTo(this);   
     slider_damper.OnValueChangedAsObservable().Subscribe(_ =>{
          damper_txt.text = _.ToString();
           OnUpdateDamper.OnNext(_);
     }).AddTo(this);
     slider_choke.OnValueChangedAsObservable().Subscribe(_ =>{
          choke_txt.text = _.ToString();
           OnUpdateChoke.OnNext(_);
     }).AddTo(this);  
   }
   public void UpdateSpring(){
       if(string.IsNullOrEmpty(input_spring.text))return;
        OnUpdateSpring.OnNext(System.Convert.ToSingle(input_spring.text));
   }
   public void UpdateDamper(){
       if(string.IsNullOrEmpty(input_damper.text))return;
        OnUpdateDamper.OnNext(System.Convert.ToSingle(input_damper.text));
   }
   public void UpdateChoke(){
        if(string.IsNullOrEmpty(input_visualChoke.text))return;
        OnUpdateChoke.OnNext(System.Convert.ToSingle(input_visualChoke.text));
   }
}
