using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System;
using UnityEngine.U2D;

public class AddressableConsole : MonoBehaviour
{
    [SerializeField]Button b_setLabel,b_release,b_loadCatalog,b_loadResource,b_loadDependencies,b_downloadSize,b_load_object,b_clear_cache,b_remove_assets,b_load_sprite;
    [SerializeField]TMP_InputField input_label,input_gameobject_name;
    [SerializeField]Image image_content;
    public string atlas_name = "texture_atlas";
    Dictionary<string,AsyncOperationHandle> opDictionary = new Dictionary<string, AsyncOperationHandle>();
    GameObject instanceObj;
    List<IResourceLocation> locations;
    private void Start()
    {
        //catalog
        //location
        //getsize
        //dependency (bundle)
        //getobject
        b_loadCatalog.OnClickAsObservable().Subscribe(_=>{
            // AsyncOperationHandle<List<string>> handle;
            // handle = new AsyncOperationHandle<List<string>>();
            // handle = Addressables.CheckForCatalogUpdates();
            // handle.Completed += DownloadCatalogComplete;
            //AddressableManager.Instance.DownLoadCatalog();
            AddressableManager.Instance.CheckCatalogUpdate();
        }).AddTo(this);
        b_loadResource.OnClickAsObservable().Subscribe(async _=>{
            //if(string.IsNullOrEmpty(input_label.text))return;
            //Addressables.LoadResourceLocationsAsync(input_label).Completed += OnLoadResourceLocationComplted;
            await AddressableManager.Instance.DownLoadResourceLocation();
        }).AddTo(this);
         b_downloadSize.OnClickAsObservable().Subscribe(_=>{
            AddressableManager.Instance.GetBundleSize();
        }).AddTo(this);
        b_loadDependencies.OnClickAsObservable().Subscribe(_=>{
            //StartCoroutine(DownloadDependencies());
            AddressableManager.Instance.DownloadDependencies();
        }).AddTo(this);
       b_load_object.OnClickAsObservable().Subscribe(async _=>{
           if(string.IsNullOrEmpty(input_gameobject_name.text))return;
           //var loadHandle = await AddressableManager.Instance.LoadObject<GameObject>(input_gameobject_name.text);

            var loadHandle = await AddressableManager.Instance.LoadObject<GameObject>(input_gameobject_name.text);
            if(loadHandle == null)return;
            AssetObjects.gameObjects.Add(Instantiate(loadHandle));
           //(loadHandle);
           //await AddressableManager.Instance.CreateGameObject(input_gameobject_name.text);
           //image_content.sprite = loadHandle.GetSprite(input_gameobject_name.text);
            //AddressableManager.Instance.GetAsset(input_gameobject_name.text);
           //AddressableManager.Instance.DownloadObject();
            // Addressables.InstantiateAsync(input_gameobject_name.text).Completed += obj =>{
            //     instanceObj = obj.Result;
            // };
       }).AddTo(this);
       b_load_sprite.OnClickAsObservable().Subscribe(async _=>{
           Debug.Log("load atlas "+atlas_name);
           Debug.Log("id "+input_gameobject_name.text);
           var loadHandle = await AddressableManager.Instance.LoadObject<SpriteAtlas>(atlas_name);
           if(loadHandle == null)return;
           image_content.sprite = loadHandle.GetSprite(input_gameobject_name.text);
       }).AddTo(this);
        b_setLabel.OnClickAsObservable().Subscribe(_=>{
            if(string.IsNullOrEmpty(input_label.text))return;
            AddressableManager.Instance.SetLabel(input_label.text);
        }).AddTo(this);
        b_release.OnClickAsObservable().Subscribe(_=>{
            //Addressables.Release(opDic);
            if(string.IsNullOrEmpty(input_label.text))return;
            AddressableManager.Instance.ReleaseLabel(input_label.text);
            // Debug.Log(instanceObj);
            // Addressables.ReleaseInstance(instanceObj);
            // Addressables.Release(opDictionary[input_label.text]);
            // Addressables.ClearDependencyCacheAsync(input_label.text);
            // opDictionary.Remove(input_label.text);
        }).AddTo(this);
        b_clear_cache.OnClickAsObservable().Subscribe(_=>{
            AddressableManager.Instance.ClearDependencies();
        }).AddTo(this);
        b_remove_assets.OnClickAsObservable().Subscribe(_=>{
            AddressableManager.Instance.ClearAllAssets();
        }).AddTo(this);

    }
    private void DownloadCatalogComplete(AsyncOperationHandle<List<string>> obj)
    {
        Debug.Log("DownloadCatalogComplete "+obj.Result.Count);
    }
    private void OnLoadResourceLocationComplted(AsyncOperationHandle<IList<IResourceLocation>> handle)
    {
        Debug.Log("Onloadresourcelocation complete"+handle);
        //opDictionary.Add(input_label.text,handle);
        
        if(handle.Status == AsyncOperationStatus.Succeeded){
            Debug.Log("Success "+handle.Result.Count);
            locations = new List<IResourceLocation>(handle.Result);

            foreach (var item in locations)
            {
                Debug.Log("item Key "+item.PrimaryKey);
            }
            //Addressables.GetDownloadSizeAsync(input_label.text).Completed += OnDownloadSizeComplete;
            //StartCoroutine(DownloadDependencies());
           // Addressables.DownloadDependenciesAsync(input_label.text).Completed += OnDownloadDependenciesComplete;

        }else if(handle.Status == AsyncOperationStatus.Failed){
            Debug.Log("failed "+handle.Status);
        }
    }
    IEnumerator DownloadDependencies(){
        var handle = Addressables.DownloadDependenciesAsync(input_label.text);
        while(handle.Status != AsyncOperationStatus.Succeeded){
            Debug.Log($"download {handle.PercentComplete*10}");
            yield return null;
        }
        opDictionary.Add(input_label.text,handle);
        Debug.Log("Add label "+input_label.text);
        // Addressables.LoadAssetAsync<GameObject>("Forest").Completed += op =>{
        //     Instantiate(op.Result);
        // };
       
    }
    IEnumerator DownloadSizeCoroutine(){
        var handle = Addressables.GetDownloadSizeAsync (locations);
       yield return handle;
        // Succeeded returns required size properly even when offline
        if (handle.Status == AsyncOperationStatus.Succeeded) {
            var dataSize = handle.Result;
            Debug.Log("downlaod size "+dataSize);
            // if (dataSize>0) {
            //     // If offline, DownloadDependenciesAsync will be Failed
            //     var download = Addressables.DownloadDependenciesAsync ("CharaKnight");
            // }
        } else if (handle.Status == AsyncOperationStatus.Failed) {
        } else {
        }
    }
    private void OnDownloadSizeComplete(AsyncOperationHandle<long> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded){
            Debug.Log("Download size "+obj.Result);
        }
    }

    private void OnDownloadDependenciesComplete(AsyncOperationHandle obj)
    {
        Debug.Log("OnDownloadDependenciesComplete "+obj.Result);
        opDictionary.Add(input_label.text,obj);
    }
    
}
