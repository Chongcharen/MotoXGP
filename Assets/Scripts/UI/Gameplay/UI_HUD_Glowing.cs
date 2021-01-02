using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UniRx;
using System;
using DG.Tweening;
public class UI_HUD_Glowing : MonoBehaviour
{
    [SerializeField]Image glow_acc,glow_brake,glow_jump,glow_right,glow_left,glow_back;
    InputControl inputControl;
    void Awake(){
        // inputControl = new InputControl();
        // inputControl.Player.Movement.performed += OnMovement;
        // inputControl.Player.Movement.canceled += OncancelMovement;
        // inputControl.Player.Accel.started += OnAccel;
        // inputControl.Player.Accel.canceled += OnCancelAccel;
        // // inputControl.Player.Back.started += OnBackStarted;
        // // inputControl.Player.Back.started += OnBackCanceled;
        // inputControl.Player.Brake.started += OnBrakeStarted;
        // inputControl.Player.Brake.canceled += OnBrakeCanceled;
        // inputControl.Player.Jump.started += OnJumpStatred;
        // inputControl.Player.Jump.canceled += OnJumpCanceled;
        // inputControl.Player.Boost.started += OnBoos
        // inputControl.Player.Boost.canceled += cbc =>{isBoost = false;};
        // OnScreenUI.OnBackward.Subscribe(_=>{
        //     glow_left.DOFade(1,0.5f);
        // }).AddTo(this);
        //  OnScreenUI.OnForward.Subscribe(_=>{
        //     glow_right.DOFade(1,0.5f);
        // }).AddTo(this);
        //  OnScreenUI.OnCancelForward.Subscribe(_=>{
        //     glow_right.DOFade(0,0.5f);
        // }).AddTo(this);
        //  OnScreenUI.OnCancelBackward.Subscribe(_=>{
        //     glow_left.DOFade(0,0.5f);
        // }).AddTo(this);


         ScreenButton.OnGetKeyCode.Subscribe(key =>{
            switch(key){
                case KeyCode.D :
                    glow_right.DOFade(1,0.5f);
                break;
                case KeyCode.A :
                    glow_left.DOFade(1,0.5f);
                break;
                case KeyCode.W:
                    glow_acc.DOFade(1,0.5f);
                break;
                case KeyCode.S:
                    glow_back.DOFade(1,0.5f);
                break;
                case KeyCode.C:
                    glow_brake.DOFade(1,0.5f);
                break;
                case KeyCode.Space:
                    glow_jump.DOFade(1,0.5f);
                break;
                case KeyCode.B:
                   // isBoost = true;
                break;
            }
        }).AddTo(this);
        ScreenButton.OnCancelKeyCode.Subscribe(key =>{
             switch(key){
                case KeyCode.D :
                    glow_right.DOFade(0,0.5f);
                break;
                case KeyCode.A :
                    glow_left.DOFade(0,0.5f);
                break;
                case KeyCode.W:
                   glow_acc.DOFade(0,0.5f);
                break;
                case KeyCode.S:
                    glow_back.DOFade(0,0.5f);
                break;
                case KeyCode.C:
                    glow_brake.DOFade(0,0.5f);
                break;
                case KeyCode.Space:
                    glow_jump.DOFade(0,0.5f);
                break;
                case KeyCode.B:
                   // isBoost = false;
                break;
            }
        }).AddTo(this);
    }

    private void OnBackCanceled(InputAction.CallbackContext obj)
    {
         glow_back.DOFade(0,0.5f);
    }

    private void OnBackStarted(InputAction.CallbackContext obj)
    {
         glow_back.DOFade(1,0.5f);
    }

    private void OnCancelAccel(InputAction.CallbackContext obj)
    {
       glow_acc.DOFade(0,0.5f);
    }

    private void OnAccel(InputAction.CallbackContext obj)
    {
         glow_acc.DOFade(1,0.5f);
    }



    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
       glow_jump.DOFade(0,0.5f);
    }

    private void OnJumpStatred(InputAction.CallbackContext obj)
    {
       glow_jump.DOFade(1,0.5f);
    }

    private void OnBrakeCanceled(InputAction.CallbackContext obj)
    {
        glow_brake.DOFade(0,0.5f);
    }

    private void OnBrakeStarted(InputAction.CallbackContext obj)
    {
        glow_brake.DOFade(1,0.5f);
    }

    private void OncancelMovement(InputAction.CallbackContext obj)
    {
        glow_left.DOFade(0,0.5f);
        glow_right.DOFade(0,0.5f);
        
        glow_back.DOFade(0,0.5f);
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        var movement = value.ReadValue<Vector2>();
        Debug.Log("movement "+(int)movement.y);
        if((int)movement.x < 0){
            glow_left.DOFade(1,0.5f);
            glow_right.DOFade(0,0.5f);
        }
        else if((int)movement.x > 0){
            glow_right.DOFade(1,0.5f);
            glow_left.DOFade(0,0.5f);
        }else if((int)movement.x == 0){
            glow_right.DOFade(0,0.5f);
            glow_left.DOFade(0,0.5f);
        }
        if((int)movement.y > 0){
            glow_acc.DOFade(1,0.5f);
            glow_back.DOFade(0,0.5f);
        }else if((int)movement.y < 0){
            glow_acc.DOFade(0,0.5f);
            glow_back.DOFade(1,0.5f);
        }else if((int)movement.y == 0){
            glow_acc.DOFade(0,0.5f);
            glow_back.DOFade(0,0.5f);
        }
    } 
     void OnEnable(){
        inputControl.Enable();
    }
    void OnDisable(){
        inputControl.Disable();
    }
}
