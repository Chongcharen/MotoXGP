using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "PlayerCollider"){
            Debug.Log("Water Collision");
            //rigidbody = other.gameObject.transform.parent.GetComponent<Rigidbody>();
            other.gameObject.transform.parent.GetComponent<BikeBoltSystem>().SetBikeStatus(BikeStatus.Water);
           // rigidbody.drag = rigidbodyDrag;
        }
    }
    void OnTriggerExit(Collider other){
         if(other.gameObject.name == "PlayerCollider"){
             other.gameObject.transform.parent.GetComponent<BikeBoltSystem>().SetBikeStatus(BikeStatus.Normal);
         }
    }
}
