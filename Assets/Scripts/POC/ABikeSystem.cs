using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABikeSystem : MonoBehaviour
{
    public bool isControll = false;
    public BikeWheels bikeWheels;
     private float flipRotate = 0.0f;
    bool[] isGround = new bool[2]{true,true};
    float Wheelie;
    WheelComponent[] wheels;
    [Header("Connect Wheels")]
    [SerializeField]ConnectWheel connectWheel;
    // [Header("Wheel Setting")]
    // [SerializeField]WheelSetting WheelSetting;

    [Header("Bike Setting")]
    [SerializeField]BikeSetting bikeSetting;
    /// <summary>
    /// POC
    /// </summary>
    /// <returns></returns>
    [SerializeField]GameController motorControl;
    
    float accel;
    public float forceJump = 150;
    [HideInInspector]
    public float motorRPM = 0.0f;

    public float wantedRPM = 0.0f;
    public float bikePower = 120;
    public float shiftPower = 150;
    public float brakePower = 8000;
    [HideInInspector]
    public float curTorque = 100f;
    [HideInInspector]
    public float speed = 0.0f;
    [HideInInspector]
    public float powerShift = 100;

    [HideInInspector]
    public bool shift;
    [HideInInspector]
    private bool shifmotor;
    private bool shifting;
    float[] efficiencyTable = { 0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 1.0f, 1.0f, 0.95f, 0.80f, 0.70f, 0.60f, 0.5f, 0.45f, 0.40f, 0.36f, 0.33f, 0.30f, 0.20f, 0.10f, 0.05f };
    float efficiencyTableStep = 250.0f;
    
    //Gear
    [HideInInspector]
    public int currentGear = 0;
    private float shiftDelay = 0.0f;
    private float shiftTime = 0.0f;
    [HideInInspector]
    public bool NeutralGear = true;
    [SerializeField]Rigidbody myRigidbody;

    [HideInInspector]
    public bool brake;
    public bool jump;
    private float slip = 0.0f;
    public bool isLeft = false;
    public bool isRight = false;
    [HideInInspector]
    public bool Backward = false;
    [HideInInspector]
    public bool grounded = true;

    private float MotorRotation;
    [SerializeField]Collider body;
    private float bodyMassZ = 0;

    
    void Awake(){
        wheels = new WheelComponent[2];
        wheels[0] = SetWheelComponent(connectWheel.wheelFront,connectWheel.AxleFront,false,0,connectWheel.AxleFront.localPosition.y);
        wheels[1] = SetWheelComponent(connectWheel.wheelBack,connectWheel.AxleBack,true,0,connectWheel.AxleBack.localPosition.y);
    }
    public void ShiftUp()
    {
           float now = Time.timeSinceLevelLoad;

          if (now < shiftDelay) return;

        if (currentGear < bikeSetting.gears.Length - 1)
        {

            // if (!bikeSounds.switchGear.isPlaying)


            // if (!bikeSetting.automaticGear)
            // {
            //     if (currentGear == 0)
            //     {
            //         if (NeutralGear) { currentGear++; NeutralGear = false; }
            //         else
            //         { NeutralGear = true; }
            //     }
            //     else
            //     {
            //         currentGear++;
            //     }
            // }
            // else
            // {
            //     currentGear++;
            // }
             currentGear++;

               shiftDelay = now + 1.0f;
               shiftTime = 1.0f;
        }
    }




    public void ShiftDown()
    {
        float now = Time.timeSinceLevelLoad;

        if (now < shiftDelay) return;

        if (currentGear > 0 )
        {

            //w if (!bikeSounds.switchGear.isPlaying)
            //bikeSounds.switchGear.GetComponent<AudioSource>().Play();

            if (!bikeSetting.automaticGear)
            {

                if (currentGear == 1)
                {
                    if (!NeutralGear) { currentGear--; NeutralGear = true; }
                }
                else if (currentGear == 0) { NeutralGear = false; } else { currentGear--; }
            }
            else
            {
                currentGear--;
            }


                shiftDelay = now + 0.1f;
                shiftTime = 2.0f;
        }

    }
    
    
    void FixedUpdate(){
        if(!isControll)return;
        speed = myRigidbody.velocity.magnitude * 2.7f;
        accel = motorControl.accelerator;
        brake = motorControl.brake;
        jump = motorControl.isJump;
        isLeft = motorControl.isLeft;
        isRight = motorControl.isRight;
        if(jump && (isGround[0] == true &&isGround[1] == true)){
            myRigidbody.AddForce(transform.up* myRigidbody.mass*forceJump);
        }
        if (bikeSetting.automaticGear && (currentGear == 1) && (accel < 0.0f))
        {
            if (speed < 1.0f)
                ShiftDown(); // reverse


        }
        else if (bikeSetting.automaticGear && (currentGear == 0) && (accel > 0.0f))
        {
            if (speed < 5.0f)
                ShiftUp(); // go from reverse to first gear

        }
        else if (bikeSetting.automaticGear && (motorRPM > bikeSetting.shiftUpRPM) && (accel > 0.0f) && speed > 10.0f && !brake)
        {
            // if (speed > 20)
            ShiftUp(); // shift up

        }
        else if (bikeSetting.automaticGear && (motorRPM < bikeSetting.shiftDownRPM) && (currentGear > 1))
        {
            ShiftDown(); // shift down
        }
        //Backward
        if (speed < 1.0f) Backward = true;
        if (currentGear == 0 && Backward == true)
        {
            if (speed < bikeSetting.gears[0] * -10)
                accel = -accel; // in automatic mode we need to hold arrow down for reverse
        }
        else
        {
            Backward = false;
        }
        if(isLeft){
            //transform.
             MotorRotation = Mathf.LerpAngle(MotorRotation,  bikeSetting.maxTurn * (Mathf.Clamp(speed / 100, 0.0f, 1.0f)), Time.deltaTime * 5.0f);
             flipRotate = (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) ? 180.0f : 0.0f;
            if (shifting)
            {
                Wheelie += bikeSetting.speedWheelie * Time.deltaTime / (speed / 50);
            }
            else
            {
                Wheelie = Mathf.MoveTowards(Wheelie, 0, (bikeSetting.speedWheelie * 2) * Time.deltaTime * 1.3f);
            }

            Debug.Log("Wheelie "+Wheelie);

            var deltaRotation1 = Quaternion.Euler(-Wheelie, 0, flipRotate - transform.localEulerAngles.z + (MotorRotation));
            var deltaRotation2 = Quaternion.Euler(0, 0, flipRotate - transform.localEulerAngles.z);

           // Debug.Log(myRigidbody.rotation);
           // myRigidbody.MoveRotation(Quaternion.Euler(0,90,0));
           
           //transform.RotateAroundLocal(Vector3.forward,0.02f);
           //myRigidbody.MoveRotation(Quaternion.Euler(transform.rotation.x,transform.rotation.y,transform.rotation.z));
           //transform.RotateAroundLocal(Vector3.forward,0.02f);
            //myRigidbody.centerOfMass = new Vector3(0,0,-5);
            bodyMassZ -=0.5f;
        }
        if(isRight){
             //transform.RotateAroundLocal(Vector3.forward,-0.02f);
            // wheels[0].collider.attachedRigidbody.
        }
        //myRigidbody.centerOfMass = new Vector3(0,0,bodyMassZ);
        body.gameObject.transform.localPosition = new Vector3(bodyMassZ,body.gameObject.transform.localPosition.y,0);

        
       
        wantedRPM = (5500.0f * accel) * 0.1f + wantedRPM * 0.9f;
        float rpm = 0.0f;
        int motorizedWheels = 0;
        bool floorContact = false;
        int currentWheel = 0;
        var indexWhell = 0;
        

        foreach(WheelComponent w in wheels){
            WheelHit hit;
            WheelCollider col = w.collider;
            
             if (w.drive)
            {
                if (!NeutralGear && brake && currentGear < 2)
                {
                    rpm += accel * bikeSetting.idleRPM;
                    /*
                    if (rpm > 1)
                    {
                        bikeSetting.shiftCentre.z = Mathf.PingPong(Time.time * (accel * 10), 0.5f) - 0.25f;
                    }
                    else
                    {
                        bikeSetting.shiftCentre.z = 0.0f;
                    }
                     * */
                }
                else
                {
                    if (!NeutralGear)
                    {
                        rpm += col.rpm;
                    }
                    else
                    {
                        rpm += ((bikeSetting.idleRPM * 2.0f) * accel);
                    }
                }

                motorizedWheels++;
            }
            if (brake || accel < 0.0f)
            {
                if ((accel < 0.0f) || (brake && w == wheels[1]))
                {

                    if (brake && (accel > 0.0f))
                    {
                        slip = Mathf.Lerp(slip, bikeSetting.slipBrake, accel * 0.01f);
                    }
                    else if (speed > 1.0f)
                    {
                        slip = Mathf.Lerp(slip, 1.0f, 0.002f);
                    }
                    else
                    {
                        slip = Mathf.Lerp(slip, 1.0f, 0.02f);
                    }


                    wantedRPM = 0.0f;
                    col.brakeTorque = bikeSetting.brakePower;

                }
            }
            else
            {


                col.brakeTorque = accel == 0 ? col.brakeTorque = 3000 : col.brakeTorque = 0;

                slip = Mathf.Lerp(slip, 1.0f, 0.02f);


            }
            WheelFrictionCurve fc = col.forwardFriction;


            if (w == wheels[1])
            {
                fc.stiffness = bikeSetting.stiffness / slip;
                col.forwardFriction = fc;
                fc = col.sidewaysFriction;
                fc.stiffness = bikeSetting.stiffness / slip;
                col.sidewaysFriction = fc; 
            }
            
            if (shift && (currentGear > 1 && speed > 50.0f) && shifmotor)
            {
                shifting = true;
                if (powerShift == 0) { shifmotor = false; }

                powerShift = Mathf.MoveTowards(powerShift, 0.0f, Time.deltaTime * 10.0f);

                // bikeSounds.nitro.volume = Mathf.Lerp(bikeSounds.nitro.volume, 1.0f, Time.deltaTime * 10.0f);

                // if (!bikeSounds.nitro.isPlaying)
                // {
                //     bikeSounds.nitro.GetComponent<AudioSource>().Play();

                // }


                curTorque = powerShift > 0 ? bikeSetting.shiftPower : bikeSetting.bikePower;
                //bikeParticles.shiftParticle1.emissionRate = Mathf.Lerp(bikeParticles.shiftParticle1.emissionRate, powerShift > 0 ? 50 : 0, Time.deltaTime * 10.0f);
                //bikeParticles.shiftParticle2.emissionRate = Mathf.Lerp(bikeParticles.shiftParticle2.emissionRate, powerShift > 0 ? 50 : 0, Time.deltaTime * 10.0f);
            }
            else
            {
                shifting = false;
                if (powerShift > 20)
                {
                    shifmotor = true;
                }

                // bikeSounds.nitro.volume = Mathf.MoveTowards(bikeSounds.nitro.volume, 0.0f, Time.deltaTime * 2.0f);

                // if (bikeSounds.nitro.volume == 0)
                //     bikeSounds.nitro.Stop();

                powerShift = Mathf.MoveTowards(powerShift, 100.0f, Time.deltaTime * 5.0f);
                curTorque = bikeSetting.bikePower;
                //bikeParticles.shiftParticle1.emissionRate = Mathf.Lerp(bikeParticles.shiftParticle1.emissionRate, 0, Time.deltaTime * 10.0f);
                //bikeParticles.shiftParticle2.emissionRate = Mathf.Lerp(bikeParticles.shiftParticle2.emissionRate, 0, Time.deltaTime * 10.0f);
            }
            w.rotation = Mathf.Repeat(w.rotation + Time.deltaTime * col.rpm * 360.0f / 60.0f, 360.0f);
            w.wheel.localRotation = Quaternion.Euler(w.rotation,0.0f, 0.0f);
            Vector3 lp = w.axle.localPosition;
            if(w.collider.GetGroundHit(out hit)){
                isGround[indexWhell] = true;
                lp.y -= Vector3.Dot(w.wheel.position - hit.point, transform.TransformDirection(0, 1, 0)) - (w.collider.radius);
                lp.y = Mathf.Clamp(lp.y,w.startPos.y - bikeWheels.setting.SuspensionDistance, w.startPos.y +  bikeWheels.setting.SuspensionDistance);
                    //lp.y = Mathf.Clamp(lp.y, w.startPos.y - bikeWheels.setting.Distance, w.startPos.y + bikeWheels.setting.Distance);
                    //Debug.Log("total "+lp.y);
            }else{
                isGround[indexWhell] = false;
            }
            w.axle.localPosition = lp;
            lp.y -= Vector3.Dot(w.wheel.position - hit.point, transform.TransformDirection(0, 1, 0)) - (col.radius);
            lp.y = Mathf.Clamp(lp.y, w.startPos.y - bikeWheels.setting.SuspensionDistance, w.startPos.y + bikeWheels.setting.SuspensionDistance);
            indexWhell++;
        }

        //End foreach wheel
        if (motorizedWheels > 1)
        {
            rpm = rpm / motorizedWheels;
        }
          motorRPM = 0.95f * motorRPM + 0.05f * Mathf.Abs(rpm * bikeSetting.gears[currentGear]);
        if (motorRPM > 5500.0f) motorRPM = 5200.0f;


        int index = (int)(motorRPM / efficiencyTableStep);
        if (index >= efficiencyTable.Length) index = efficiencyTable.Length - 1;
        if (index < 0) index = 0;
        float newTorque = curTorque * bikeSetting.gears[currentGear] * efficiencyTable[index];
        //float newTorque = 2000;
         // go set torque to the wheels
       foreach (WheelComponent w in wheels)
        {
            WheelCollider col = w.collider;
            // of course, only the wheels connected to the engine can get engine torque
            if (w.drive)
            {   
                //Debug.Log("col rpm = "+col.rpm +"wanted rpm = "+wantedRPM);
                if (Mathf.Abs(col.rpm) > Mathf.Abs(wantedRPM))
                {

                    col.motorTorque = 0;
                }
                else
                {
                    // 
                    float curTorqueCol = col.motorTorque;

                    if (accel != 0 )
                    {
                        if ((speed < bikeSetting.LimitForwardSpeed && currentGear > 0) ||
                            (speed < bikeSetting.LimitBackwardSpeed && currentGear == 0))
                        {

                            col.motorTorque = curTorqueCol * 0.9f + newTorque * 1.0f;
                        }
                        else
                        {
                            col.motorTorque = 0;
                            col.brakeTorque = 2000;
                        }


                    }
                    else
                    {
                        col.motorTorque = 0;

                    }
                }

            }
           
        }
        guiTorque = newTorque;
        guiGear = currentGear;
        gearSet = bikeSetting.gears[currentGear];
        efficiency = efficiencyTable[index];
        guimotorRPM = motorRPM;
        guiIndex = index;
        guiRpm = rpm;
        guiCurrentTorque = curTorque;
    }
    float guiTorque;
    float guiCurrentTorque;
    int guiGear;

    float gearSet;
    float efficiency;

    float guimotorRPM;
    float guiIndex;
    float guiRpm;
    
    void OnGUI(){
        GUIStyle style = new GUIStyle();
        style.fontSize = 25;
         GUI.Label(new Rect(10, 10, 300, 150), "Torque "+guiTorque,style);
          GUI.Label(new Rect(10, 25, 300, 150), "guiCurrentTorque "+guiCurrentTorque,style);
          GUI.Label(new Rect(10, 50, 300, 150), "guimotorRPM "+guimotorRPM,style);
          GUI.Label(new Rect(10, 100, 300, 150), "guiIndex "+guiIndex,style);
         GUI.Label(new Rect(10, 150, 300, 150), "Gear "+guiGear,style);
         GUI.Label(new Rect(10, 200, 300, 150), "gearSet "+gearSet,style);
         GUI.Label(new Rect(10, 300, 300, 150), "efficiency "+efficiency,style);
          GUI.Label(new Rect(10, 350, 300, 150), "guiRpm "+guiRpm,style);
    }

    #region Setting
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
        result.collider.radius = bikeWheels.setting.Radius;
        result.collider.center = bikeWheels.setting.WheelCenter;
        result.pos_y = pos_y;
        result.maxSteer = maxSteer;
        result.startPos = axle.transform.localPosition;
        if(!drive){
            // var spring = new JointSpring();
            // spring.spring = 35000;
            // spring.damper = 3500;
            // spring.targetPosition = 0.7f;
            // result.collider.suspensionSpring = spring;
        }

        // Physics.IgnoreCollision(result.collider, body);
        // Physics.IgnoreCollision(result.collider, body);
        return result;

    }
    #endregion

}
[System.Serializable]
    public class BikeWheels
    {
        public ConnectWheel wheels;
        public WheelSetting setting;
    }
[System.Serializable]
    public class BilkWheelSetting
    {
        public ConnectWheel wheels;
        public WheelSetting[] wheelSettings;
    }
[System.Serializable]
    public class ConnectWheel
    {

        public Transform wheelFront; // connect to Front Right Wheel transform
        public Transform wheelBack; // connect to Front Left Wheel transform

        public Transform AxleFront; // connect to Back Right Wheel transform
        public Transform AxleBack; // connect to Back Left Wheel transform

    }
[System.Serializable]
    public class WheelSetting
    {

        public float Radius = 0.3f; // the radius of the wheels
        public float Weight = 1000.0f; // the weight of a wheel
        public float SuspensionDistance = 0.2f;
        public float ForceAppointDistance =0;
        public float DampingRate = 0.5f;

        public Vector3 WheelCenter;

        public FrictionSetting ForwardFriction;
        public FrictionSetting SidewaysFriction;
        public SuspensionSpringSetting SuspensionSpring;

    }
[System.Serializable]
    public class WheelComponent
    {

        public Transform wheel;
        public Transform axle;
        public WheelCollider collider;
        public SphereCollider sphereCollider;
        public Vector3 startPos;
        public float rotation = 0.0f;
        public float maxSteer;
        public bool drive;
        public float pos_y = 0.0f;
    }
[System.Serializable]
    public class HitGround
    {
        public string tag = "street";
        public bool grounded = false;
        public AudioClip brakeSound;
        public AudioClip groundSound;
        public Color brakeColor;

    }
    
[System.Serializable]
    public class BikeSetting
    {


        public bool showNormalGizmos = false;

        public HitGround[] hitGround;



        public Transform bikerMan;
        public List<Transform> cameraSwitchView;


        public Transform MainBody;
        public Transform bikeSteer;


        public float maxWheelie = 40.0f;
        public float speedWheelie = 30.0f;

        public float slipBrake = 3.0f;


        public float springs = 35000.0f;
        public float dampers = 4000.0f;

        public float bikePower = 120;
        public float shiftPower = 150;
        public float brakePower = 8000;

        public Vector3 shiftCentre = new Vector3(0.0f, -0.6f, 0.0f); // offset of centre of mass

        public float maxSteerAngle = 30.0f; // max angle of steering wheels
        public float maxTurn = 1.5f;

        public float shiftDownRPM = 1500.0f; // rpm script will shift gear down
        public float shiftUpRPM = 4000.0f; // rpm script will shift gear up
        public float idleRPM = 700.0f; // idle rpm

        public float stiffness = 1.0f; // for wheels, determines slip



        public bool automaticGear = true;

        public float[] gears = { -10f, 9f, 6f, 4.5f, 3f, 2.5f }; // gear ratios (index 0 is reverse)

        public float LimitBackwardSpeed = 60.0f;
        public float LimitForwardSpeed = 220.0f;
    }
[System.Serializable]
public class FrictionSetting{
    public float extremumSlip;
    public float extremumValue;
    public float asymptoteSlip;
    public float asymptoteValue;
    public float stiffness;
}
[System.Serializable]
public class SuspensionSpringSetting{
    public float spring;
    public float damper;
    public float targetposition;
}
