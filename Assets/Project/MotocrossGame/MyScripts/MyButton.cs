using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler {

    public Motorcycle_Controller mcc;

    public bool MyThooo;
    public bool MyBreak;
    public bool RollLeft;
    public bool RollRight;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(mcc == null)
            mcc = FindObjectOfType<Motorcycle_Controller>();
    }


    public virtual void OnPointerDown(PointerEventData ped) {
        Debug.Log("OnPointerDown");

        if(MyThooo)
            mcc.MyThooo = true;
        
        if(MyBreak)
            mcc.MyBreak = true;

        if(RollLeft)
            mcc.RollLeft = true;
        if(RollRight)
            mcc.RollRight = true;
    }

    public virtual void OnPointerUp(PointerEventData ped) {
        Debug.Log("OnPointerUp");

        if(MyThooo)
            mcc.MyThooo = false;

        if(MyBreak)
            mcc.MyBreak = false;

        if(RollLeft)
            mcc.RollLeft = false;

        if(RollRight)
            mcc.RollRight = false;
    }
}
