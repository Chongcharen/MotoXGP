using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class VirtualCameraForWaiting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        GameCallback.OnCutSceneReady.Subscribe(_=>{
            Destroy(this.gameObject);
        }).AddTo(this);
    }
}
