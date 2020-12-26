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
        
        // inputControl.Player.Accel.started += OnAccel;
        // inputControl.Player.Accel.canceled += OnCancelAccel;
        // inputControl.Player.Back.started += OnBackStarted;
        // inputControl.Player.Back.started += OnBackCanceled;
        // inputControl.Player.Brake.performed += (cbc)=>{brake = true;};
        // inputControl.Player.Brake.canceled += cbc =>{ brake = false;};
        // inputControl.Player.Jump.performed += cbc =>{isJump = true;};
        // inputControl.Player.Jump.canceled += cbc =>{isJump = false;};
        // inputControl.Player.Boost.performed += cbc =>{isBoost = true;};
        // inputControl.Player.Boost.canceled += cbc =>{isBoost = false;};
        // inputControl.Player.VoiceActive.started += cbc =>{
        //     micActive = !micActive;
        //     OnMicActive.OnNext(micActive);
        // };
        // OnScreenUI.OnBackward.Subscribe(_=>{
        //     isLeft = true;
        // }).AddTo(this);
        //  OnScreenUI.OnForward.Subscribe(_=>{
        //      isRight = true;
        // }).AddTo(this);
        //  OnScreenUI.OnCancelForward.Subscribe(_=>{
        //      isRight = false;
        // }).AddTo(this);
        //  OnScreenUI.OnCancelBackward.Subscribe(_=>{
        //      isLeft = false;
        // }).AddTo(this);
        //inputControl.Player

        ScreenButton.OnGetKeyCode.Subscribe(key =>{
            GetKeyDown(key);
        }).AddTo(this);
        ScreenButton.OnCancelKeyCode.Subscribe(key =>{
            GetKeyUp(key);
        }).AddTo(this);
    }
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.D)){
            GetKeyDown(KeyCode.D);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            GetKeyDown(KeyCode.A);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            GetKeyDown(KeyCode.W);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            GetKeyDown(KeyCode.S);
        }
        if(Input.GetKeyDown(KeyCode.C)){
            GetKeyDown(KeyCode.C);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            GetKeyDown(KeyCode.Space);
        }
        if(Input.GetKeyDown(KeyCode.B)){
            GetKeyDown(KeyCode.B);
        }
        

        if(Input.GetKeyUp(KeyCode.D)){
            GetKeyUp(KeyCode.D);
        }
        if(Input.GetKeyUp(KeyCode.A)){
            GetKeyUp(KeyCode.A);
        }
        if(Input.GetKeyUp(KeyCode.W)){
            GetKeyUp(KeyCode.W);
        }
        if(Input.GetKeyUp(KeyCode.S)){
            GetKeyUp(KeyCode.S);
        }
        if(Input.GetKeyUp(KeyCode.C)){
            GetKeyUp(KeyCode.C);
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            GetKeyUp(KeyCode.Space);
        }
        if(Input.GetKeyUp(KeyCode.B)){
            GetKeyUp(KeyCode.B);
        }

    }
    void GetKeyDown(KeyCode key){
        switch(key){
                case KeyCode.D :
                    isRight = true;
                break;
                case KeyCode.A :
                    isLeft = true;
                break;
                case KeyCode.W:
                    accelerator = 1;
                break;
                case KeyCode.S:
                    accelerator = -1;
                break;
                case KeyCode.C:
                    brake = true;
                break;
                case KeyCode.Space:
                    isJump = true;
                break;
                case KeyCode.B:
                    isBoost = true;
                break;
            }
    }
    void GetKeyUp(KeyCode key){
         switch(key){
                case KeyCode.D :
                    isRight = false;
                break;
                case KeyCode.A :
                    isLeft = false;
                break;
                case KeyCode.W:
                    accelerator = 0;
                break;
                case KeyCode.S:
                    accelerator = 0;
                break;
                case KeyCode.C:
                    brake = false;
                break;
                case KeyCode.Space:
                    isJump = false;
                break;
                case KeyCode.B:
                    isBoost = false;
                break;
            }
    }

    private void OnBackCanceled(InputAction.CallbackContext obj)
    {
        //throw new NotImplementedException();
        accelerator = 0;
    }

    private void OnBackStarted(InputAction.CallbackContext obj)
    {
        accelerator = -1;
    }

    private void OnCancelAccel(InputAction.CallbackContext obj)
    {
       accelerator = 0;
    }

    private void OnAccel(InputAction.CallbackContext obj)
    {
         accelerator = 1;
    }

    private void OncancelMovement(InputAction.CallbackContext obj)
    {
       accelerator = 0;
        isLeft = false;
        isRight = false;
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
       
        isLeft = value.ReadValue<Vector2>().x <0 ? true : false;
        isRight = value.ReadValue<Vector2>().x > 0 ? true : false;
        accelerator = value.ReadValue<Vector2>().y;
    }

    void OnEnable(){
        inputControl.Enable();
    }
    void OnDisable(){
        inputControl.Disable();
    }
}
