using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CrashDetecter : MonoBehaviour
{
    public static Subject<Vector3> OnCrash = new Subject<Vector3>();
    public static Subject<Unit> OnBump = new Subject<Unit>();
    public static Subject<Tuple<int,Vector3>> OnPlayerCrash = new Subject<Tuple<int, Vector3>>();
    public GameObject gameObject;
    void Start(){

    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == TagKeys.GROUND || other.tag == TagKeys.ROAD){
           OnCrash.OnNext(transform.position);
           OnPlayerCrash.OnNext(Tuple.Create(gameObject.GetInstanceID(),transform.position));
       }
       //pillar use for monolith and pillar
        if(other.tag == TagKeys.PILLAR){
            OnCrash.OnNext(transform.position);
            OnPlayerCrash.OnNext(Tuple.Create(gameObject.GetInstanceID(),transform.position));
            OnBump.OnNext(default);
        }
    }
}
