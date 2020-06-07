using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Transform lhs,rhs;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var total = Vector3.Dot(lhs.position,rhs.position);
        //Debug.Log(" = "+total);
        if (lhs)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = lhs.position - rhs.position;

            if (Vector3.Dot(forward, toOther) < 0)
            {
                print("The other transform is behind me!");
            }
        }
    }
}
