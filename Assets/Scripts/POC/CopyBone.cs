using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CopyBone : MonoBehaviour
{
    public GameObject sourceGameObject;

    private void Start() {
        //var sourceRenderer = sourceGameObject.GetComponent<SkinnedMeshRenderer>();
        var targetRender = GetComponent<SkinnedMeshRenderer>();

        //targetRender.bones = sourceRenderer.bones.Where(b => sourceRenderer.bones.Any(t => t.name == b.name)).ToArray();
        var bones = sourceGameObject.GetComponentsInChildren<Transform>();
        Debug.Log("length "+bones.Length);
        targetRender.bones = bones;
        //targetRender.BakeMesh()

    }
}
