using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class POC_Controller_Motor : MonoBehaviour
{
    public float accelerator;
    public bool brake;
    public bool isLeft = false;
    public bool isRight = false;
    InputControl control;
    void Awake(){
        control = new InputControl();
        control.Player.Movement.performed += Forward;
        control.Player.click.performed += OnTestClick;
        Debug.Log("control "+control);
    }

    private void OnTestClick(InputAction.CallbackContext obj)
    {
        Debug.Log("OnTestClick "+obj);
    }

    void Test(){
        Debug.Log("test ");
    }
    private void Forward(InputAction.CallbackContext ctx)
    {
        //throw new NotImplementedException();
        Debug.Log("ctx ???? "+ctx.ReadValue<Vector2>());
    }

    // void Update(){
    //     accelerator = Input.GetAxis("Vertical");
    //     brake = Input.GetKey(KeyCode.Space);
    //     isLeft = Input.GetKey(KeyCode.A);
    //     isRight = Input.GetKey(KeyCode.D);
    // }
    void OnEnable(){
        control.Enable();
    }
    void OnDisable(){
        control.Disable();
    }
    
}
