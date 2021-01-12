using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BikeEquipment : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public bool startLoadEquipment = true;
    public bool useTextureHD = true;
    private void Start() {
        if(startLoadEquipment)
            LoadEquipmentFromSave();
        EquipmentIconPrefab.OnEquipmentChanged.Subscribe(async tracked =>{
            if(tracked.id < 5 )return;
            var textureQuality = useTextureHD ? "HD/" :"SD/";
            var model = await AddressableManager.Instance.LoadObject<Mesh>(GameDataManager.Instance.gameConfigData.dataPath.equipment_models+tracked.model_name);
            var texture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment_textures+textureQuality+tracked.texture_name);
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
            var textureQuality = useTextureHD ? "HD/" :"SD/";
            skinnedMeshRenderers[index].sharedMesh = await AddressableManager.Instance.LoadObject<Mesh>(GameDataManager.Instance.gameConfigData.dataPath.equipment_models+mapper.Value.model_name);
            var texture = await AddressableManager.Instance.LoadObject<Texture>(GameDataManager.Instance.gameConfigData.dataPath.equipment_textures+textureQuality+mapper.Value.texture_name);
            Debug.Log("Bike Texture "+texture);
            Debug.Log("Maintexture "+skinnedMeshRenderers[index].sharedMaterial.mainTexture);
            skinnedMeshRenderers[index].material.mainTexture = texture;
            index++;
        }
    }
}
