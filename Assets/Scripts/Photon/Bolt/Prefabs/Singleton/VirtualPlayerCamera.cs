using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;
using DG.Tweening;
public class VirtualPlayerCamera : BoltSingletonPrefab<VirtualPlayerCamera>
{

    CinemachineVirtualCamera virtualCamera;
    CinemachineTransposer CinemachineTransposer;
    CinemachineComposer CinemachineComposer;
    void Awake(){
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        CinemachineComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
    }
    void Start(){
        AbikeChopSystem.OnBoostTime.Subscribe(timeChange =>{
			// elapsed = timeChange;
			// isBoost = true;
			//sideX += boostChangeSideX;
			//DOTween.To(() => sideX,sideX, sideX+boostChangeSideX , 0.5f);
			//transform.DOMoveX(sideX + boostChangeSideX,0.3f).SetEase(Ease.OutElastic);
			// DOGetter<float> doGetter = new DOGetter<float>(() => sideX);
            // DOSetter <float> dOSetter = new DOSetter <float> ((x) => { x = sideX;});
        	// DOTween.To(doGetter, dOSetter,sideX+boostChangeSideX, 0.5f);
			StartCoroutine("closetime",timeChange);
		}).AddTo(this);
    }
    IEnumerator closetime(float time){
        //CinemachineTransposer.DOComplete()
        DG.Tweening.DOTween.To(()=>CinemachineTransposer.m_XDamping,value => CinemachineTransposer.m_XDamping = value,1.5f,0.2f);
        DG.Tweening.DOTween.To(()=>CinemachineTransposer.m_FollowOffset.z,value => CinemachineTransposer.m_FollowOffset.z = value,0,0.2f);
       // DG.Tweening.DOTween.To(()=>CinemachineComposer.m_ScreenX,value => CinemachineComposer.m_ScreenX = value,0.45f,0.2f);
		yield return new WaitForSeconds(time);
        DG.Tweening.DOTween.To(()=>CinemachineTransposer.m_XDamping,value => CinemachineTransposer.m_XDamping = value,0,1f);
        DG.Tweening.DOTween.To(()=>CinemachineTransposer.m_FollowOffset.z,value => CinemachineTransposer.m_FollowOffset.z = value,3.15f,0.2f);
		//DG.Tweening.DOTween.To(()=>CinemachineComposer.m_ScreenX,value => CinemachineComposer.m_ScreenX = value,0.35f,0.2f);
        //isBoost = false;
	}
    public void LookupTarget(Transform transform){
        virtualCamera.LookAt = transform;
    }
    public void FollowTarget(Transform transform){
        virtualCamera.Follow = transform;
    }

}
