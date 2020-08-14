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
        inputControl = new InputControl();
        inputControl.Player.Movement.performed += OnMovement;
        inputControl.Player.Movement.canceled += OncancelMovement;
        inputControl.Player.Brake.started += OnBrakeStarted;
        inputControl.Player.Brake.canceled += OnBrakeCanceled;
        inputControl.Player.Jump.started += OnJumpStatred;
        inputControl.Player.Jump.canceled += OnJumpCanceled;
        // inputControl.Player.Boost.started += OnBoos
        // inputControl.Player.Boost.canceled += cbc =>{isBoost = false;};
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
        glow_acc.DOFade(0,0.5f);
        glow_back.DOFade(0,0.5f);
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        var movement = value.ReadValue<Vector2>();
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
        }
        
    } 
     void OnEnable(){
        inputControl.Enable();
    }
    void OnDisable(){
        inputControl.Disable();
    }
}
