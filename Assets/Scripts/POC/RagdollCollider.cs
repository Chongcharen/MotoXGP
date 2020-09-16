using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
public class RagdollCollider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Collider[] colliders;
    List<Rigidbody> Rigidbodies = new List<Rigidbody>();
    [SerializeField]PhysicMaterial material;
    public GameObject rootObject;
    void Start()
    {
        SetRagdoll();
        //EnableKinematic(false);
        CrashDetecter.OnCrash.Subscribe(_=>{
            //  foreach(Collider coll in colliders){
            //     if(coll == null)continue;
            //     var rigidbody = coll.GetComponent<Rigidbody>();
            //     rigidbody.mass = 50;
            //     rigidbody.drag = 1f;
            //     rigidbody.angularDrag = 0.05f;
            //     rigidbody.velocity = Vector3.zero;
            //     rigidbody.angularVelocity = Vector3.zero;
            //     rigidbody.sleepThreshold = 0;
            //     rigidbody.constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationY;
            //  }
        });

        CrashDetecter.OnPlayerCrash.Subscribe(tuple =>{
            if(tuple.Item1 != rootObject.GetInstanceID())return;
            foreach(Collider coll in colliders){
                if(coll == null)continue;
                var rigidbody = coll.GetComponent<Rigidbody>();
                rigidbody.mass = 50;
                rigidbody.drag = 1f;
                rigidbody.angularDrag = 0.05f;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.sleepThreshold = 0;
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationY;
             }
        });
        AbikeChopSystem.OnReset.Subscribe(_=>{
            foreach(Collider coll in colliders){
                if(coll == null)continue;
                EnabledRagDolls(false);

                var rigidbody = coll.GetComponent<Rigidbody>();
                rigidbody.mass = 0;
                rigidbody.drag = 0;
                rigidbody.angularDrag = 0;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.sleepThreshold = 0.005f;
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                rigidbody.WakeUp();
                EnabledRagDolls(true);
             }
        });
        BikeBoltSystem.OnReset.Subscribe(_=>{
            foreach(Collider coll in colliders){
                if(coll == null)continue;
                EnabledRagDolls(false);

                var rigidbody = coll.GetComponent<Rigidbody>();
                rigidbody.mass = 0;
                rigidbody.drag = 0;
                rigidbody.angularDrag = 0;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.sleepThreshold = 0.005f;
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                rigidbody.WakeUp();
                EnabledRagDolls(true);
             }
        });
        BikeBoltSystem.OnControllGained.Subscribe(_=>{
            EnableKinematic(_);
        });
    }
    public void SetRagdoll(){
        colliders  = GetComponentsInChildren<Collider>();
        foreach(Collider coll in colliders){
            if(coll == null)continue;
            //coll.enabled = false;
            coll.material = material;
            var rigidbody = coll.gameObject.GetComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            rigidbody.mass = 0;
            rigidbody.drag = 0;
            rigidbody.angularDrag = 0;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            Rigidbodies.Add(rigidbody);
        }
    }
    public void EnabledRagDolls(bool active){
        foreach(Collider coll in colliders){
            coll.enabled = active;
        }
    }
    public void EnableKinematic(bool active){
        Debug.Log("EnableKinematic "+active);
        //Rigidbodies.cas
       // Rigidbodies.ForEach(r =>r.isKinematic = active);
    }

}
