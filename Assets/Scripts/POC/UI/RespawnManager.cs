using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class RespawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string tagName = "Fall";
    public string respawnPoint = "RespawnPoint";
    public string fallGround = "FallGround";
    GameObject[] areas;
    // List<Collider> colliders = new List<Collider>();
    // List<Vector3> respawnPositions = new List<Vector3>();
    void Start()
    {
        areas = GameObject.FindGameObjectsWithTag(tagName);
        foreach(GameObject area in areas){
            var fall = area.transform.Find(fallGround);
            var respawnTarget = fall.gameObject.AddComponent<FindRespawnTarget>();
            var resPoint = area.transform.Find(respawnPoint);
            if(resPoint != null)
                respawnTarget.respawnTarget = resPoint.transform.position;
            

            // colliders.Add(fall.GetComponent<Collider>());
            
            // var resPoint = area.transform.Find(respawnPoint);
            // respawnPositions.Add(resPoint.transform.position);
        }


    }
}

public class FindRespawnTarget : MonoBehaviour{
    public Vector3 respawnTarget;
}
