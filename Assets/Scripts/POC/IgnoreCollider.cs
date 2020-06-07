
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
         Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ragdoll"),LayerMask.NameToLayer("Motorcycle"),true);
         Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ragdoll"),LayerMask.NameToLayer("Ragdoll"),true);
        // Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Ragdoll"), true);
         Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Motorcycle"), true);
         Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Body"), true);
         Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ragdoll"), LayerMask.NameToLayer("Body"), true);
         Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Body"), LayerMask.NameToLayer("Body"), true);
        // Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Road"), LayerMask.NameToLayer("Body"), true);
         //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Body"), LayerMask.NameToLayer("Road"), true);
        
    }

}
