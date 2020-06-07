using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 defaultMass;
    public GameObject sphere;
    public Vector3 newMass;
    public float massIncrease = 0.1f;
    public float currentMass = 0;
    Rigidbody rigidbody;
    GameController gameController;
    bool isLeft;
    bool isRight;
    float rotation;
    float angle = 0;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameController = GetComponent<GameController>();
        defaultMass = sphere.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
         rigidbody.centerOfMass = newMass;
         //rigidbody.AddForceAtPosition(new Vector3(1,1,1f),transform.position,ForceMode.Impulse);
         //rigidbody.MoveRotation(Quaternion.Euler(-90,90,0));
         sphere.transform.localPosition = newMass;
         isLeft = gameController.isLeft;
         isRight = gameController.isRight;

         if(isLeft){
            currentMass = Mathf.Clamp(currentMass-massIncrease,-1,1);
            newMass = new Vector3(rigidbody.centerOfMass.x,rigidbody.centerOfMass.y,defaultMass.z+currentMass);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x -1,90,0);
            //transform.rotation = Quaternion.Euler(angle,90,0);
         }
        if(isRight){
            currentMass = Mathf.Clamp(currentMass+massIncrease,-1,1);
            newMass = new Vector3(rigidbody.centerOfMass.x,rigidbody.centerOfMass.y,defaultMass.z+currentMass);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x +1,90,0);
            //transform.rotation = Quaternion.Euler(angle,90,0);
        }
        
        if(!isLeft && !isRight){
            newMass = defaultMass;
            currentMass = 0;
        }
    }
}
