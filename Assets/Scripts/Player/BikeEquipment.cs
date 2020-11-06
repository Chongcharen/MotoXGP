using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BikeEquipment : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public bool startLoadEquipment = true;
    private void Start() {
        if(startLoadEquipment)
            LoadEquipmentFromSave();
        EquipmentIconPrefab.OnEquipmentChanged.Subscribe(async tracked =>{
            if(tracked.id <= 4 )return;
            var model = await AddressableManager.Instance.LoadObject<Mesh>(GameDataManager.Instance.gameConfigData.dataPath.equipment+tracked.model_name);
            var texture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment+tracked.texture_name);
            skinnedMeshRenderers[0].sharedMesh = model;
            skinnedMeshRenderers[0].material.mainTexture = texture;
            SaveMockupData.SaveBikeEquipment(0,tracked.model_name,tracked.texture_name);
        }).AddTo(this);
    }
     void LoadEquipmentFromSave(){
        var currentEquiped = SaveMockupData.GetBikeEquipment;
        SetupEquipment(currentEquiped.bikeEquipmentMapper);
        
        
    }
    public async void SetupEquipment(Dictionary<string,BikeEquipedData> data){
        Debug.Log("setupEquipment");
        var index = 0;
        foreach (var mapper in data)
        {
            print(Depug.Log("id "+index,Color.red));
            skinnedMeshRenderers[index].sharedMesh = await AddressableManager.Instance.LoadObject<Mesh>(GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.model_name);
            //skinnedMeshRenderers[index].material.mainTexture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.texture_name);
            var texture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment+mapper.Value.texture_name);
            //skinnedMeshRenderers[index].material.SetTexture("_MainTex",texture);
            Debug.Log("Bike Texture "+texture);
            Debug.Log("Maintexture "+skinnedMeshRenderers[index].sharedMaterial.mainTexture);
            skinnedMeshRenderers[index].material.mainTexture = texture;
            index++;
        }
    }
}
