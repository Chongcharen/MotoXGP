using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class PlayerTrigger : MonoBehaviour
{
    public float deplaySpawnTime = 1;
    public static Subject<Vector3> OnRespawnPosition = new Subject<Vector3>();
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "FallGround"){
            var positionRespawn = other.gameObject.GetComponent<FindRespawnTarget>().respawnTarget;
            StartCoroutine(Respawn(new Vector3(positionRespawn.x,positionRespawn.y,transform.position.z)));
        }else if(other.gameObject.tag == "Road")
        {
            Debug.Log("Crash !");
        }
    }
    IEnumerator Respawn(Vector3 respawnPos){
        yield return new WaitForSeconds(deplaySpawnTime);
        OnRespawnPosition.OnNext(respawnPos);
    }


    
}
