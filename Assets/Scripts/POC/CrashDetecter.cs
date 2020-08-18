using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class CrashDetecter : MonoBehaviour
{
    public static Subject<Vector3> OnCrash = new Subject<Vector3>();
    public static Subject<Unit> OnBump = new Subject<Unit>();
    void Start(){

    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == TagKeys.GROUND || other.tag == TagKeys.ROAD){
           Debug.Log("Crash");
           OnCrash.OnNext(transform.position);
       }
       //pillar use for monolith and pillar
       if(other.tag == TagKeys.PILLAR){
           OnCrash.OnNext(transform.position);
           OnBump.OnNext(default);
       }
    }
}
