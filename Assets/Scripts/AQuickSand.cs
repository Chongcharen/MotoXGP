using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AQuickSand : MonoBehaviour
{
    [SerializeField]Transform platformTranform;
    Rigidbody rigidbody;
    Vector3 positionTemp;
    public float platformSpeed = 0.001f;
    public Vector2 velocityLimit = new Vector2(-1,1);
    public float speed;
    void Awake(){
        positionTemp = platformTranform.position;
    }
    void OnTriggerEnter(Collider other){
        Debug.Log("OnTriggerEnter "+other.gameObject.name);
        if(other.gameObject.name == "PlayerCollider"){
            rigidbody = other.gameObject.transform.parent.GetComponent<Rigidbody>();
            Debug.Log("Get Rigibody "+rigidbody);
        }
    }
    void OnTriggerExit(Collider other){
         if(other.gameObject.name == "PlayerCollider"){
             rigidbody = null;
             platformTranform.position = positionTemp;
         }
    }

    void FixedUpdate(){
        if(rigidbody == null)return;
        Debug.Log("Rigidbody "+rigidbody);
        Debug.Log("velocity "+rigidbody.velocity);
        speed = rigidbody.velocity.x;
        if(rigidbody.velocity.x <velocityLimit.y && rigidbody.velocity.x >-velocityLimit.x){
            if(platformTranform.position.y > positionTemp.y)return;
            platformTranform.position += new Vector3(0,platformSpeed,0);
        }else{
            if(platformTranform.position.y < -2)return;
            platformTranform.position += new Vector3(0,-platformSpeed,0);
        }
        //if(platformTranform.position.y > positionTemp.y || platformTranform.position.y < -2)return;
    }

}
