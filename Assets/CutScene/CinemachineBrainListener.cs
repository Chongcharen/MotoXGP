using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;
public class CinemachineBrainListener : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineBrain brain;
    void Start()
    {
        DestroyCutScene.OnCinemachineBlendChangeStyle.Subscribe(_=>{
            brain.m_DefaultBlend.m_Style = _;
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
