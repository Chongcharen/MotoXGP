using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour {

    public string respawnPointName = "FallSpawnPoint";
    public Transform respawnPoint;

    private void Start()
    {
        respawnPoint = transform.Find(respawnPointName);
    }
}
