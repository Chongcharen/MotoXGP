using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour
{
    /*
    public float qicksandforce = 8f;
    public Transform offsetCenter;
    */

    private float sandDensity = 50f;
    private float downForce = 0f;

    private float forceFactor = 50f;
    private Vector3 floatForce;

    public float drag;
    public float angularDrag;

    public float wheelMassFront;
    public float wheelMassBack;

    private float frontMass;
    private float rearMass;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            frontMass = other.GetComponent<Motorcycle_Controller>().frontWheel.mass;
            rearMass = other.GetComponent<Motorcycle_Controller>().rearWheel.mass;
            Debug.Log("Front  " + frontMass + "  Back  " + rearMass);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {

            var rb = other.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionZ;

            if (other.GetComponent<Motorcycle_Controller>().accelerate)
            {              
                floatForce = -Physics.gravity * rb.mass * (forceFactor - rb.velocity.y * sandDensity);
                floatForce += new Vector3(0.0f, -downForce * rb.mass, 0.0f);
                rb.AddForceAtPosition(floatForce, other.transform.position);

                other.GetComponent<Motorcycle_Controller>().frontWheel.mass = wheelMassFront;
                other.GetComponent<Motorcycle_Controller>().rearWheel.mass = wheelMassBack;

                Debug.Log("Front  " + other.GetComponent<Motorcycle_Controller>().frontWheel.mass
                    + "  Back  " + other.GetComponent<Motorcycle_Controller>().rearWheel.mass);

            }

            else
            {
                var downForce = -Physics.gravity * (forceFactor/2 - rb.velocity.y * sandDensity);
                rb.AddForce(downForce);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Motorcycle_Controller>().frontWheel.mass = frontMass;
            other.GetComponent<Motorcycle_Controller>().rearWheel.mass = rearMass;

            Debug.Log("Front  " + other.GetComponent<Motorcycle_Controller>().frontWheel.mass
                    + "  Back  " + other.GetComponent<Motorcycle_Controller>().rearWheel.mass);
        }
    }
}
