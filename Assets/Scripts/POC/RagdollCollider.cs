﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class RagdollCollider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Collider[] colliders;
    [SerializeField]PhysicMaterial material;
    
    void Start()
    {
        
        colliders  = GetComponentsInChildren<Collider>();
        foreach(Collider coll in colliders){
            //coll.enabled = false;
            coll.material = material;
            var rigidbody = coll.gameObject.GetComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.None;
            coll.GetComponent<Rigidbody>().mass = 0;
            coll.GetComponent<Rigidbody>().drag = 0;
            coll.GetComponent<Rigidbody>().angularDrag = 0;
            //rigidbody.angularDrag = 10;
            //rigidbody.drag = 1;
            //rigidbody.useGravity = false;
            //coll.GetComponent<Rigidbody>().mass = 0;
            //coll.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ;
        
        }
        CrashDetecter.OnCrash.Subscribe(_=>{
             foreach(Collider coll in colliders){
                var rigidbody = coll.GetComponent<Rigidbody>();
                rigidbody.mass = 50;
                rigidbody.drag = 1f;
                rigidbody.angularDrag = 0.05f;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.sleepThreshold = 0;
             }
        });
        AbikeChopSystem.OnReset.Subscribe(_=>{
            foreach(Collider coll in colliders){
                var rigidbody = coll.GetComponent<Rigidbody>();
                rigidbody.mass = 0;
                rigidbody.drag = 0;
                rigidbody.angularDrag = 0;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.sleepThreshold = 0.005f;
                rigidbody.WakeUp();
             }
        });
    }

}
