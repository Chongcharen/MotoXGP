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
       // bones = bones.Where(b => targetRender.bones.Any(t =>t.name == b.name)).ToArray();
        Matrix4x4[] bindPoses = new Matrix4x4[targetRender.bones.Length];
        Debug.Log("before length "+targetRender.bones.Length);
        
        Debug.Log("after length "+bones.Length);
        List<Transform> tBones = new List<Transform>();
        //targetRender.BakeMesh()
        for (int i = 0; i < targetRender.bones.Length; i++)
        {
            Debug.Log("mybone name"+targetRender.bones[i].name+" ------  other bone name"+bones[i].name);
            Debug.Log("mybone "+targetRender.bones[i].localPosition+" ------  other bone "+bones[i].localPosition);
            //bones[i].localRotation = Quaternion.identity;
            //bones[i].localPosition = Vector3.zero;
            //bindPoses[i] =  bones[i].worldToLocalMatrix * transform.localToWorldMatrix;
            for (int j = 0; j < bones.Length; j++)
            {
                if(targetRender.bones[i].name == bones[j].name){
                    //targetRender.bones[i] = bones[j];
                    tBones.Add(bones[j]);
                   // bindPoses[i] =   targetRender.bones[i].worldToLocalMatrix * transform.localToWorldMatrix;
                }
            }
        }
        targetRender.bones = tBones.ToArray();
        //targetRender.BakeMesh(targetRender.sharedMesh);
        // targetRender.sharedMesh.
        //targetRender.sharedMesh.bindposes = bindPoses;
    }
}
