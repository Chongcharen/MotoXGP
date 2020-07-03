using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class GameController : MonoBehaviour
{
    public float accelerator;
    public bool brake;
    public bool isLeft = false;
    public bool isRight = false;
    public bool isJump = false;

    public bool isBoost = false;
    public bool micActive = false;

    public static Subject<bool> OnMicActive = new Subject<bool>();

    InputControl inputControl;
    void Awake(){
        inputControl = new InputControl();
        inputControl.Player.Movement.performed += OnMovement;
        inputControl.Player.Movement.canceled += OncancelMovement;
        inputControl.Player.Brake.performed += (cbc)=>{brake = true;};
        inputControl.Player.Brake.canceled += cbc =>{ brake = false;};
        inputControl.Player.Jump.performed += cbc =>{isJump = true;};
        inputControl.Player.Jump.canceled += cbc =>{isJump = false;};
        inputControl.Player.Boost.started += cbc =>{isBoost = true;};
        inputControl.Player.Boost.canceled += cbc =>{isBoost = false;};
        inputControl.Player.VoiceActive.started += cbc =>{
            micActive = !micActive;
            OnMicActive.OnNext(micActive);
        };
        //inputControl.Player
    }

    private void OncancelMovement(InputAction.CallbackContext obj)
    {
        accelerator = 0;
        isLeft = false;
        isRight = false;
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        accelerator = value.ReadValue<Vector2>().y;
        isLeft = value.ReadValue<Vector2>().x <0 ? true : false;
        isRight = value.ReadValue<Vector2>().x > 0 ? true : false;
    }

    void OnEnable(){
        inputControl.Enable();
    }
    void OnDisable(){
        inputControl.Disable();
    }
}
