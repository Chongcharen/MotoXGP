using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LogAnimatorSetting : MonoBehaviour
{
    public Transform[] logTransforms;
    private void Start() {
        foreach (var log in logTransforms)
        {
            //log.DOLocalMoveY()
            log.DORotate(new Vector3(0,0,0),1f).SetLoops(-1, LoopType.Incremental);
        }
    }
}
