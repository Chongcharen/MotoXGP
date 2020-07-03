using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICore : MonoBehaviour {

    public static UICore Instance { get; protected set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public Motorcycle_Controller mcc;

    public void Throttle(bool isDown)
    {
        mcc.MyThooo = isDown;
    }

    public void Break(bool isDown)
    {
        mcc.MyBreak = isDown;
    }
    public void RollLeft(bool isDown)
    {
        mcc.RollLeft = isDown;
        if (mcc.hips)
            mcc.hips.targetPosition = Vector3.Lerp(mcc.hips.targetPosition, mcc.leanBackwardTargetPosition, /*mcc.leanSpeed*/20 * Time.deltaTime);
    }
    public void RollRight(bool isDown)
    {
        mcc.RollRight = isDown;
        if (mcc.hips)
            mcc.hips.targetPosition = Vector3.Lerp(mcc.hips.targetPosition, mcc.leanForwardTargetPosition, /*mcc.leanSpeed*/20 * Time.deltaTime);
    }
}
