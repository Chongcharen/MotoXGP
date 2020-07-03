using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class CrashDetecter : MonoBehaviour
{
    public static Subject<Vector3> OnCrash = new Subject<Vector3>();
    void Start(){

    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == TagKeys.GROUND || other.tag == TagKeys.ROAD){
           Debug.Log("Crash");
           OnCrash.OnNext(transform.position);
       }
    }
}
