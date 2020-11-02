using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ZoneDetecter : MonoBehaviour
{
    //public static Subject<Unit> OnCameraZoneExit = new Subject<Unit>();
    public static Subject<Transform> OnEnterQuicksand = new Subject<Transform>();
    public static Subject<Transform> OnExitQuicksand = new Subject<Transform>();
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == TagKeys.DEADZONE){
           MapManager.Instance.GetZone(other.transform.GetInstanceID());
           MapManager.Instance.PassDeadZone(true);
       }else if(other.tag == TagKeys.SAFEZONE){
           MapManager.Instance.PassDeadZone(false);
       }else if(other.tag == TagKeys.ENDPOINT){
           MapManager.Instance.GetEndPoint(other.transform.GetInstanceID());
       }
    //    else if(other.tag == TagKeys.CAMERAZONE){
    //        MapManager.Instance.GetCameraZone(other.transform.GetInstanceID());
    //    }
    }

    // private void OnTriggerExit(Collider other) {
    //     if(other.tag == TagKeys.CAMERAZONE){
    //         OnCameraZoneExit.OnNext(default);
    //     }
    // }
}
