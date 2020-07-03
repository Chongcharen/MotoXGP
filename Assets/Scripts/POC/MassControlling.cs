using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassControlling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject objectMass;
    [SerializeField]Transform postionUp,positionDown;
    [SerializeField]Vector3 direction = Vector3.up; 
    [SerializeField]float mass =1;
    [SerializeField]GameController controller;
    Vector3 standPoint;
    void Start()
    {
        standPoint = objectMass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isLeft){
            objectMass.transform.localPosition = -direction*mass;
        }
        if(controller.isRight){
            objectMass.transform.localPosition = direction*mass;
        }
        if(!controller.isLeft&&!controller.isRight){
            objectMass.transform.localPosition = standPoint;
        }
    }
}
