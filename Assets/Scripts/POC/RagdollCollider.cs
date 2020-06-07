using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCollider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Collider[] colliders;
    [SerializeField]PhysicMaterial material;
    void Start()
    {
        
        colliders  = GetComponentsInChildren<Collider>();
        foreach(Collider coll in colliders){
            coll.enabled = false;
            //coll.material = material;
        }
    }

}
