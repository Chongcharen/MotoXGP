using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class PlayerEquipment : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    void Start(){
        EquipmentIconPrefab.OnEquipmentChanged.Subscribe(tracked =>{
            Debug.Log("Model name "+tracked.model_name);
            Debug.Log("texture name "+tracked.texture_name);
            Debug.Log("tracked id "+tracked.id);
            var model = Resources.Load(tracked.model_name) as Mesh;
            var texture = Resources.Load(tracked.texture_name) as Texture;
            skinnedMeshRenderers[tracked.id].sharedMesh = model;
            skinnedMeshRenderers[tracked.id].material.mainTexture = texture;
        }).AddTo(this);
    }
    
}
