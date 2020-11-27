using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;
public class CameraDetecter : MonoBehaviour
{
    [SerializeField]CinemachineVirtualCamera virtualCamera;
    private void Awake() {
        //virtualCamera = GetComponent<CinemachineVirtualCamera>();   
        BikeBoltSystem.OnCameraLookup.Subscribe(target =>{
            virtualCamera.LookAt = target;
            virtualCamera.Follow = target;
        }).AddTo(this); 
    }
    private void Start() {
         
        Debug.Log("virtualcam "+virtualCamera);
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("ontriggerenter ");
            //virtualCamera.enabled = true;
    }
    private void OnTriggerExit(Collider other) {
        Debug.Log("onenterExit");
            //virtualCamera.enabled = false;
        //virtualCamera.des
    }
}
