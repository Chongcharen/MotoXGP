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
        SetRagdoll();
        CrashDetecter.OnCrash.Subscribe(_=>{
             foreach(Collider coll in colliders){
                if(coll == null)continue;
                var rigidbody = coll.GetComponent<Rigidbody>();
                rigidbody.mass = 50;
                rigidbody.drag = 1f;
                rigidbody.angularDrag = 0.05f;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.sleepThreshold = 0;
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationY;
             }
        });
        AbikeChopSystem.OnReset.Subscribe(_=>{
            foreach(Collider coll in colliders){
                if(coll == null)continue;
                var rigidbody = coll.GetComponent<Rigidbody>();
                rigidbody.mass = 0;
                rigidbody.drag = 0;
                rigidbody.angularDrag = 0;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.sleepThreshold = 0.005f;
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                rigidbody.WakeUp();
             }
        });
    }
    public void SetRagdoll(){
        colliders  = GetComponentsInChildren<Collider>();
        foreach(Collider coll in colliders){
            if(coll == null)continue;
            //coll.enabled = false;
            coll.material = material;
            var rigidbody = coll.gameObject.GetComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            rigidbody.mass = 0;
            rigidbody.drag = 0;
            rigidbody.angularDrag = 0;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }
    public void EnabledRagDolls(bool active){
        foreach(Collider coll in colliders){
            coll.enabled = active;
        }
    }

}
