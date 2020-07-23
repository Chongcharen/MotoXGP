using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour
{
    public Transform localSpawnPoint;
    public Transform[] spawnPoints;

    public Vector3 GetLocalSpawnPosition(){
        return localSpawnPoint.position;
    }
    public Transform[] GetSpawnPoints(){
        return spawnPoints;
    }
}
