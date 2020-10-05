using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Custom_PlayerEquipment : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    private void Start() {
        EquipmentIconPrefab.OnEquipmentChanged.Subscribe(tracked =>{
            Debug.Log("track id "+tracked.id);
            Debug.Log("model name "+tracked.model_name);
            var model = Resources.Load(tracked.model_name) as GameObject;
            var texture = Resources.Load(tracked.texture_name) as Texture;
            Debug.Log("Get model "+model);
            skinnedMeshRenderers[tracked.id].sharedMesh = model.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
            skinnedMeshRenderers[tracked.id].material.mainTexture = texture;
        }).AddTo(this);
    }

}
