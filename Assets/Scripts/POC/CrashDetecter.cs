using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class CrashDetecter : MonoBehaviour
{
    public static Subject<Unit> OnCrash = new Subject<Unit>();
    void Start(){

    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == TagKeys.GROUND || other.tag == TagKeys.ROAD){
           Debug.Log("Crash");
           OnCrash.OnNext(default);
       }
    }
}
