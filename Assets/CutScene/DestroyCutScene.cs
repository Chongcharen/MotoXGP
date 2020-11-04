using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cinemachine;
public class DestroyCutScene : MonoBehaviour
{
    // Start is called before the first frame update
    public static Subject<CinemachineBlendDefinition.Style> OnCinemachineBlendChangeStyle = new Subject<CinemachineBlendDefinition.Style>();
    public static Subject<Unit> OnCutSceneComplete = new Subject<Unit>();
    public CinemachineBrain brain;
    void Awake(){
        brain = Camera.main.GetComponent<CinemachineBrain>();
         Debug.Log("Brain "+brain);
    }
    void OnEnable()
    {
       // brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Linear;
        OnCinemachineBlendChangeStyle.OnNext(CinemachineBlendDefinition.Style.Linear);
        Debug.Log("default blend ------------------------------------------>"+brain.m_DefaultBlend.m_Style);
        OnCutSceneComplete.OnNext(default);
        
    }

    public void DestroyThem(){
        Destroy(this.gameObject);   
    }
}
