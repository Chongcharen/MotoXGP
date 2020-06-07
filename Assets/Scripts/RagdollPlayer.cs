using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollPlayer : MonoBehaviour
{
    public HingeJoint leftHand;
    public HingeJoint rightHand;
    public HingeJoint leftFoot;
    public HingeJoint rightFoot;
    public ConfigurableJoint hips;



    [SerializeField] internal Renderer head;
    [SerializeField] internal Renderer helmet;
    [SerializeField] internal SkinnedMeshRenderer suit;
    [SerializeField] internal SkinnedMeshRenderer gloves;
    [SerializeField] internal SkinnedMeshRenderer boots;

    [SerializeField] internal Transform helmetPoint;

    public void InitRagdoll()
    {
        Motorcycle_Controller mcc = GetComponentInParent<Motorcycle_Controller>();

        mcc.hips = hips;
        mcc.leftHand = leftHand;
        mcc.rightHand = rightHand;
        mcc.leftFoot = leftFoot;
        mcc.rightFoot = rightFoot;

        leftHand.connectedBody = mcc.body;
        rightHand.connectedBody = mcc.body;
        leftFoot.connectedBody = mcc.body;
        rightFoot.connectedBody = mcc.body;
        hips.connectedBody = mcc.body;


    }

    public void OnCrash()
    {
        leftHand.connectedBody = null;
        rightHand.connectedBody = null;
        leftFoot.connectedBody = null;
        rightFoot.connectedBody = null;
        hips.connectedBody = null;
    }
   
}