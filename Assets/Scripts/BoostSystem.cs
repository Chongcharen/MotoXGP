using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Kino;
public class BoostSystem : MonoBehaviour
{
    [SerializeField]TrailRenderer trailRenderer;
    [SerializeField]Kino.Motion motionBlur;
    [SerializeField]ParticleSystem particle;
    ParticleSystem.EmissionModule emission;
    //[SerializeField]Motion

    void Start(){
        emission = particle.emission;
    }
    public void SetUpMotion(Kino.Motion _motion){
       motionBlur = _motion;
    }
    public void StartBoostEffect(float time){
        if(motionBlur == null)return;
        motionBlur.sampleCount = 0;
        motionBlur.shutterAngle = 10;
        motionBlur.frameBlending = 0.1f;
        trailRenderer.emitting = true;
        emission.enabled = true;
        Invoke("StopBoostEffect",time);
    }
    public void StopBoostEffect(){
        motionBlur.sampleCount = 0;
        motionBlur.shutterAngle = 0;
        motionBlur.frameBlending = 0f;
        trailRenderer.emitting = false;
        emission.enabled = false;
    }
}
