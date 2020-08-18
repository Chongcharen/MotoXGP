using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolithAnimatorSetting : MonoBehaviour
{
    public float time = 0;
    Animator animator;
    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start(){
        animator.StopPlayback();
        animator.enabled = false;
        StartCoroutine("Delaytime");
    }
    IEnumerator Delaytime(){
        yield return new WaitForSeconds(time);
        Debug.Log("-------------------------> DelayTime "+time);
        animator.enabled = true;
        //animator.Play("Take 001");
    }
}
