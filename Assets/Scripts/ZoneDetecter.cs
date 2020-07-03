using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetecter : MonoBehaviour
{
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
    }
}
