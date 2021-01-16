using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UIElements;

public class CenterOfMass : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 defaultMass;
    public Vector3 tensor;
    public Vector3 inertiaTensorRotation;
    public GameObject objectCenter; // 
    public GameObject objectOfMass;

    public Transform massPoint;
    public Vector3 newMass;
    public float massIncrease = 0.1f;
    public float currentMass = 0;
    public float limit = 0.5f;
    Rigidbody rigidbody;
    public GameController gameController;
    bool isLeft;
    bool isRight;
    float rotation;
    float angle = 0;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameController = GetComponent<GameController>();

        rigidbody.ResetInertiaTensor();
        rigidbody.ResetCenterOfMass();
        defaultMass = objectOfMass.transform.localPosition;
       // Vector3 setInitialTensor =rigidbody.inertiaTensor+tensor;//this string is necessary for Unity 5.3f with new PhysX feature when Tensor decoupled from center of mass


        Debug.Log("center of mass "+rigidbody.centerOfMass);
        Debug.Log("inertiaTensor "+rigidbody.inertiaTensor);
        newMass = defaultMass;
        rigidbody.centerOfMass = newMass;
       // SetInertiaTensor();
        //rigidbody.inertiaTensorRotation = Quaternion.Euler(100,0,0);
    
        AbikeChopSystem.OnGrouned.Subscribe(grouned =>{
            // Debug.Log("Subscribe grouned "+grouned);
            // if(grouned)
            //     rigidbody.centerOfMass = objectOfMass.transform.localPosition;
            // else
            //     rigidbody.centerOfMass = objectCenter.transform.localPosition;
            
            // defaultMass = rigidbody.centerOfMass;
            // newMass = new Vector3(rigidbody.centerOfMass.x,rigidbody.centerOfMass.y,defaultMass.z+currentMass);
        });
        CrashDetecter.OnCrash.Subscribe(_=>{
            //OnCrash();
        }).AddTo(this);
        CrashDetecter.OnPlayerCrash.Subscribe(tuple =>{
            if(this.gameObject.GetInstanceID() != tuple.Item1)return;
            OnCrash();
        }).AddTo(this);

        
    }
    void SetInertiaTensor(){
        Vector3 setInitialTensor =rigidbody.inertiaTensor+tensor;
        rigidbody.inertiaTensor = setInitialTensor;
    }
    void OnCrash(){
        if(objectCenter == null)return;
        rigidbody.centerOfMass = objectCenter.transform.localPosition;
        defaultMass = rigidbody.centerOfMass;
        newMass = defaultMass;
    }
    public void Reset(){
        rigidbody.centerOfMass = objectOfMass.transform.localPosition;
        defaultMass = rigidbody.centerOfMass;
        newMass = defaultMass;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       // SetInertiaTensor();
        rigidbody.centerOfMass = newMass;
         //rigidbody.inertiaTensor = tensor;
        //rigidbody.maxAngularVelocity = 1;
         //rigidbody.AddForceAtPosition(new Vector3(1,1,1f),transform.position,ForceMode.Impulse);
         //rigidbody.MoveRotation(Quaternion.Euler(-90,90,0));
         //sphere.transform.localPosition = newMass;
        isLeft = gameController.isLeft;
        isRight = gameController.isRight;
        if(isLeft){
            currentMass = Mathf.Clamp(currentMass-massIncrease,-limit,limit);
            newMass = new Vector3(rigidbody.centerOfMass.x,rigidbody.centerOfMass.y,defaultMass.z+currentMass);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x -1,90,0);
            //transform.rotation = Quaternion.Euler(angle,90,0);
        }
        if(isRight){
            currentMass = Mathf.Clamp(currentMass+massIncrease,-limit,limit);
            newMass = new Vector3(rigidbody.centerOfMass.x,rigidbody.centerOfMass.y,defaultMass.z+currentMass);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x +1,90,0);
            //transform.rotation = Quaternion.Euler(angle,90,0);
        }
        
        if(!isLeft && !isRight){
            newMass = defaultMass;
            currentMass = 0;
        }
        massPoint.localPosition = rigidbody.centerOfMass;
    }
}
