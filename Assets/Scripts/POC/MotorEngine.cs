using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorEngine : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]POC_Controller_Motor motorControl;
    [HideInInspector]
    public float motorRPM = 0.0f;

    public float wantedRPM = 0.0f;
    public float bikePower = 120;
    public float shiftPower = 150;
    public float brakePower = 8000;


    //RPM = revolutions per minute
        public float shiftDownRPM = 1500.0f; // rpm script will shift gear down
        public float shiftUpRPM = 4000.0f; // rpm script will shift gear up
        public float idleRPM = 700.0f; // idle rpm

        public float stiffness = 1.0f; // for wheels, determines slip

    [HideInInspector]
    public int currentGear = 0;
    private float shiftDelay = 0.0f;
    private float shiftTime = 0.0f;
    public float[] gears = { -10f, 9f, 6f, 4.5f, 3f, 2.5f }; // gear ratios (index 0 is reverse)
    float accel;
     [HideInInspector]
    public float speed = 0.0f;
    [HideInInspector]
    public bool brake;
    private float slip = 0.0f;
    private bool shifmotor;

    [HideInInspector]
    public float curTorque = 100f;

    [HideInInspector]
    public float powerShift = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((currentGear == 1) && (accel < 0.0f))
        {
            if (speed < 1.0f)
                ShiftDown(); // reverse


        }
        else if ( (currentGear == 0) && (accel > 0.0f))
        {
            if (speed < 5.0f)
                ShiftUp(); // go from reverse to first gear

        }
        else if ( (motorRPM > shiftUpRPM) && (accel > 0.0f) && speed > 10.0f && !brake)
        {
            // if (speed > 20)
            ShiftUp(); // shift up

        }
        else if ( (motorRPM < shiftDownRPM) && (currentGear > 1))
        {
            ShiftDown(); // shift down
        }

        accel = motorControl.accelerator;
        wantedRPM = (5500.0f * accel) * 0.1f + wantedRPM * 0.9f;
    }
     public void ShiftUp()
    {
           float now = Time.timeSinceLevelLoad;

          if (now < shiftDelay) return;


        if (currentGear < gears.Length - 1)
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

        if (currentGear > 0)
        {

            //w if (!bikeSounds.switchGear.isPlaying)
            //bikeSounds.switchGear.GetComponent<AudioSource>().Play();

            // if (!bikeSetting.automaticGear)
            // {

            //     if (currentGear == 1)
            //     {
            //         if (!NeutralGear) { currentGear--; NeutralGear = true; }
            //     }
            //     else if (currentGear == 0) { NeutralGear = false; } else { currentGear--; }
            // }
            // else
            // {
            //     currentGear--;
            // }
            currentGear--;

                shiftDelay = now + 0.1f;
                shiftTime = 2.0f;
        }
    }

}
