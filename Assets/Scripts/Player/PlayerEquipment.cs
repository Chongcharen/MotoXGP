using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class PlayerEquipment : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public bool startLoadEquipment = true;
    void Start(){
        Debug.Log("Playerequipment start =======================>");
        if(startLoadEquipment)
            LoadEquipmentFromSave();
        EquipmentIconPrefab.OnEquipmentChanged.Subscribe(async tracked =>{
             if(tracked.id > 4 )return;
            Debug.Log("Model name "+tracked.model_name);
            Debug.Log("texture name "+tracked.texture_name);
            Debug.Log("tracked id "+tracked.id);

            Debug.Log("data path "+GameDataManager.Instance.gameConfigData.dataPath.equipment);
            var model = await AddressableManager.Instance.LoadObject<Mesh>(GameDataManager.Instance.gameConfigData.dataPath.equipment+tracked.model_name);
            var texture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment+tracked.texture_name);
            Debug.Log("Get Texture "+texture);
            Debug.Log("Maintexture "+skinnedMeshRenderers[tracked.id].sharedMaterial.mainTexture);
            Debug.Log("Maintexture 2"+skinnedMeshRenderers[tracked.id].material.mainTexture);
            skinnedMeshRenderers[tracked.id].sharedMesh = model;
            skinnedMeshRenderers[tracked.id].material.mainTexture = texture;
            //skinnedMeshRenderers[tracked.id].material.mainTexture = texture;
             //skinnedMeshRenderers[tracked.id].GetComponent<Renderer>().material.mainTexture = texture;
            if(tracked.id <= 4)
                SaveMockupData.SaveEquipment(tracked.id,tracked.model_name,tracked.texture_name);
            //else
                //SaveMockupData.SaveBikeEquipment(tracked.id,tracked.model_name,tracked.texture_name);
        }).AddTo(this);
    }
    
    void LoadEquipmentFromSave(){
        Debug.Log("LoadEquipmentFormSave");
        var currentEquiped = SaveMockupData.GetEquipment;
        Debug.Log("current equiped "+currentEquiped.playerEquipmentMapper);
        SetupEquipment(currentEquiped.playerEquipmentMapper);
        
    }
    public async void SetupEquipment(Dictionary<string,PlayerEquipedData> data){
        Debug.Log("setupEquipment");
        var index = 0;
        foreach (var mapper in data)
        {
            print(Depug.Log("id "+index,Color.red));
            Debug.Log("model name "+GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.model_name);
            Debug.Log("texture name "+GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.texture_name);
            skinnedMeshRenderers[index].sharedMesh = await AddressableManager.Instance.LoadObject<Mesh>(GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.model_name);
            //skinnedMeshRenderers[index].material.mainTexture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.texture_name);
            var texture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.texture_name);
            //skinnedMeshRenderers[index].material.SetTexture("_MainTex",texture);
            Debug.Log("Maintexture "+skinnedMeshRenderers[index].sharedMaterial.mainTexture);
            skinnedMeshRenderers[index].material.mainTexture = texture;
            index++;
        }
    }


}
