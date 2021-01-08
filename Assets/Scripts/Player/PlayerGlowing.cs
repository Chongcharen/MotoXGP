using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
public class PlayerGlowing : MonoBehaviour
{
    public SkinnedMeshRenderer[] mats;
    //public float[] rimlightValue;
    private void Start()
    {
        // rimlightValue = new float[mats.Length];
        // for (var i = 0; i < mats.Length; i++)
        // {
        //     Debug.Log("Rimlight ................"+mats[i].material.GetFloat("_RimlightShow"));
            
        //     rimlightValue[i] = mats[i].material.GetFloat("_RimlightShow");
        //     mats[i].material.SetFloat("_RimlightShow",1);
        // }
        // BikeBoltSystem.OnControllGained.Subscribe(_=>{
        //     if(!_)return;
        //     ShowRimlight();
        // }).AddTo(this);
    }

    public void ShowRimlight(){
        for (var i = 0; i < mats.Length; i++)
        {
           // rimlightValue[i] = mats[i].material.GetFloat("_RimlightShow");
            mats[i].material.SetFloat("_RimlightShow",0);
        }
    }

    public async void CloseRimlight(){
        await Task.Delay(5000);
        for (var i = 0; i < mats.Length; i++)
        {
            //rimlightValue[i] = mats[i].material.GetFloat("_RimlightShow");\
            var rimlightValue =  mats[i].material.GetFloat("_RimlightShow");
            var materialTarget =  mats[i].material;
            DOTween.To(()=> rimlightValue, x=> rimlightValue = x, 1, 3).OnUpdate(()=>{
               Debug.Log("Rimlight Changed ..... "+rimlightValue);
               materialTarget.SetFloat("_RimlightShow",rimlightValue);
            }).SetAutoKill();
            //mats[i].material.SetFloat("_RimlightShow",1);
            
        }
    }
}
