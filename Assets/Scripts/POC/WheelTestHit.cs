using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTestHit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform wheel;
    public Transform axle;

    public float radius;
    WheelHit hit;
    [System.Serializable]
     private class WheelComponent
    {

        public Transform wheel;
        public Transform axle;
        public WheelCollider collider;
        public Vector3 startPos;
        public float rotation = 0.0f;
        public float maxSteer;
        public bool drive;
        public float pos_y = 0.0f;
    }
    WheelComponent wheelComponent;
    void Start()
    {
        wheelComponent = SetWheelComponent(wheel,axle,true,0,axle.localPosition.y);
    }
    private WheelComponent SetWheelComponent(Transform wheel, Transform axle, bool drive, float maxSteer, float pos_y)
    {

        WheelComponent result = new WheelComponent();
        GameObject wheelCol = new GameObject(wheel.name + "WheelCollider");


        
        wheelCol.transform.parent = transform;
        wheelCol.transform.position = wheel.position;
        wheelCol.transform.eulerAngles = transform.eulerAngles;
        pos_y = wheelCol.transform.localPosition.y;


       
        wheelCol.AddComponent(typeof(WheelCollider));

       
        result.drive = drive;
        result.wheel = wheel;
        result.axle = axle;
        
        result.collider = wheelCol.GetComponent<WheelCollider>();
        result.collider.radius = radius;
        result.pos_y = pos_y;
        result.maxSteer = maxSteer;
        result.startPos = axle.transform.localPosition;

        return result;

    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hit "+hit);
        Vector3 lp = axle.localPosition;

        if(wheelComponent.collider.GetGroundHit(out hit)){
            lp.y -= Vector3.Dot(wheel.position - hit.point, transform.TransformDirection(0, 1, 0)) - (wheelComponent.collider.radius);
            lp.y = Mathf.Clamp(lp.y,wheelComponent.startPos.y - 0.25f, wheelComponent.startPos.y + 0.25f);
                //lp.y = Mathf.Clamp(lp.y, w.startPos.y - bikeWheels.setting.Distance, w.startPos.y + bikeWheels.setting.Distance);
                //Debug.Log("total "+lp.y);
        }
        axle.localPosition = lp;
    }
}
