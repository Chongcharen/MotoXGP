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
    public Color nosColor;
    public Color lower_gear_color;
    //[SerializeField]Motion

    void Start(){
        emission = particle.emission;
    }
    public void SetUpMotion(Kino.Motion _motion){
       motionBlur = _motion;
    }
    public void StartBoostEffect(float time,bool isNos = true){
        // if(motionBlur == null)return;
        // motionBlur.sampleCount = 0;
        // motionBlur.shutterAngle = 10;
        // motionBlur.frameBlending = 0.1f;
        if(isNos){
        trailRenderer.emitting = true;
        
        }
        emission.enabled = true;
        var main = particle.main;
        main.startColor = isNos ? nosColor : lower_gear_color;
        //particle.main
        //particle.main = isNos ? nosColor : lower_gear_color;
        //particle.main.startColor = isNos ? nosColor : lower_gear_color;
        Invoke("StopBoostEffect",time);
    }
    public void StopBoostEffect(){
        // motionBlur.sampleCount = 0;
        // motionBlur.shutterAngle = 0;
        // motionBlur.frameBlending = 0f;
        trailRenderer.emitting = false;
        emission.enabled = false;
    }
}
