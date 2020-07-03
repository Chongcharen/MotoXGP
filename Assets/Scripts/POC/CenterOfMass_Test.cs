using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass_Test : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigid;
    public Vector3 defaultMass;
    public Vector3 newMass;
    public GameObject objectOfMass;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Vector3 setInitialTensor = rigid.inertiaTensor;
        rigid.inertiaTensor = setInitialTensor;
        rigid.centerOfMass = objectOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
         rigid.centerOfMass = objectOfMass.transform.localPosition;
    }
}
