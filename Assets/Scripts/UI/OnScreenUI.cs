using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx; 
using TMPro;
public class OnScreenUI : MonoBehaviour
{
    public static Subject<uint> OnForward = new Subject<uint>();
    public static Subject<uint> OnBackward = new Subject<uint>();
    public static Subject<uint> OnCancelForward = new Subject<uint>();
    public static Subject<uint> OnCancelBackward = new Subject<uint>();

    public TextMeshProUGUI txt_tap_count;
    
    void Update()
    {
        txt_tap_count.text = Input.touches.Length.ToString();
    }
    public void DirectionActive(int direction){
      //  OnForward.OnNext(direction);
    }
    public void Forward(){
        OnForward.OnNext(default);
    }
    public void Backward(){
        OnBackward.OnNext(default);
    }
    public void CancelForward(){
        OnCancelForward.OnNext(default);
    }
    public void CancelBackward(){
        OnCancelBackward.OnNext(default);
    }
}
