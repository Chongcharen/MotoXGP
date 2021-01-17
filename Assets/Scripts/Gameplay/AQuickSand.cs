using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class AQuickSand : MonoBehaviour
{
    [SerializeField]Transform platformTranform;
    Rigidbody rigidbody;
    Vector3 positionTemp;
    public float rigidbodyDrag = 2;
    public float platformSpeed = 0.001f;
    public float sandDownSpeed = 1;
    public float limit = 0.5f;
    float sndDownLimit = 0;
    public float speed;
    void Awake(){
        positionTemp = platformTranform.position;
        AbikeChopSystem.OnPlayerCrash.Subscribe(_=>{
            //platformTranform.position = positionTemp;
            if(rigidbody != null){
                rigidbody.drag = 0.5f;
                rigidbody = null;
            }
        }).AddTo(this);
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCollider"){
            rigidbody = other.gameObject.transform.parent.GetComponent<Rigidbody>();
            Debug.Log("rigidbody "+rigidbody);
            other.gameObject.transform.parent.GetComponent<BikeBoltSystem>().SetBikeStatus(BikeStatus.Sand);
           // rigidbody.drag = rigidbodyDrag;
        }
    }
    void OnTriggerExit(Collider other){
         if(other.gameObject.name == "PlayerCollider"){
             if(rigidbody != null){
                rigidbody.drag = 0.01f;
                rigidbody = null;
             }
             other.gameObject.transform.parent.GetComponent<BikeBoltSystem>().SetBikeStatus(BikeStatus.Normal);
             platformTranform.position = positionTemp;
         }
    }

    void FixedUpdate(){
        if(rigidbody == null){
            if(platformTranform.position.y >= positionTemp.y)return;
            platformTranform.position += new Vector3(0,platformSpeed,0);
            return;
        }
        speed = rigidbody.velocity.x;
        if(speed <-sandDownSpeed || speed >sandDownSpeed){
            if(platformTranform.position.y >= positionTemp.y)return;
            platformTranform.position += new Vector3(0,platformSpeed,0);
        }else{
            if(platformTranform.position.y < positionTemp.y - limit)return;
            platformTranform.position += new Vector3(0,-platformSpeed,0);
        }
        //if(platformTranform.position.y > positionTemp.y || platformTranform.position.y < -2)return;
    }

}
