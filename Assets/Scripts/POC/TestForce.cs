using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForce : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Rigidbody rigidbody;
    [SerializeField]Transform transformUp,transformDown;
    public float force = 20;
    public float offset =1;
    bool isLeft = false;
    bool isRight = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            if(!isLeft){
                rigidbody.AddForceAtPosition(Vector3.up*force,transformUp.position*offset);
                isLeft = true;
            }
        }else{
            isLeft = false;
        }

        if(Input.GetKeyDown(KeyCode.D)){
            if(!isRight){
                rigidbody.AddForceAtPosition(-Vector3.up*force, transformDown.position*offset);
                isRight = true;
            }
        }else{
            isRight = false;
        }
    }
}
