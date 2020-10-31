using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets.ResourceLocators;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;

// โค้ดนี้ไมไ่ด้ใช้แล้ว เป็น POC ตอน project แยก
public class AddressableInitial : MonoBehaviour
{
    public string label = "product";
    
    List<IResourceLocation> locations;
    private void Start()
    {
        // LoginPlayFab.OnLoginComplete.Subscribe(_=>{
        //     Debug.Log("Loginplayfab complete");
        //     //CheckFileSize();
        //    // Addressables.ClearDependencyCacheAsync(label);
        //     //Addressables.ClearResourceLocators();
        //     Addressables.ResourceManager.ResourceProviders.Add(new AssetBundleProvider());
        //     Addressables.ResourceManager.ResourceProviders.Add(new PlayFabStorageHashProvider());
        //     Addressables.ResourceManager.ResourceProviders.Add(new PlayFabStorageAssetBundleProvider());
        //     Addressables.ResourceManager.ResourceProviders.Add(new PlayFabStorageJsonAssetProvider());
        //     InitAsync();
        // }).AddTo(this);
    }
    void InitAsync(){
        // var addressableInitialize = Addressables.InitializeAsync();
        // await addressableInitialize.Task;
        //Addressables.ClearDependencyCacheAsync(label);
       
        Addressables.InitializeAsync().Completed += OnInitcomplete;
        //addressableInitialize.Completed += OnInitcomplete;
    }

    private void OnInitcomplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IResourceLocator> handle)
    {
        Debug.Log("InitializeAsync Complete");
        //load catalog first
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
          //  LoadCatalog();
        }

        // AsyncOperationHandle<IList<IResourceLocation>> resourceLocationHandle = Addressables.LoadResourceLocationsAsync(label);
        // resourceLocationHandle.Completed += op =>{
        //     Debug.Log("resourcelocation count "+op.Result.Count);
        //     if (op.Status == AsyncOperationStatus.Succeeded)
        //     {
        //         locations = new List<IResourceLocation>(op.Result);
        //         StartCoroutine(TryDownloadDlc());
        //         //StartCoroutine(TryDownLoadGameObject(op));
        //     }
        //    // GetSize();
        // //     //Addressables.GetDownloadSizeAsync(op.Result).Completed += OncheckfileSizeCompleted;
        // };
        
       
       // CheckFileSize();
    }
    void LoadCatalog()
    {
       
        //AsyncOperationHandle<IResourceLocator> handle;
        AsyncOperationHandle<List<string>> handle;
        handle = new AsyncOperationHandle<List<string>>();
        handle = Addressables.CheckForCatalogUpdates(true);
        handle.Completed += CheckCatalogComplete;
     
    }
    private void CheckCatalogComplete(AsyncOperationHandle<List<string>> obj)
    {
         Debug.Log("Catalogcomplete " + obj.Result.Count);
        
        // foreach (var item in obj.Result)
        // {
        //     Debug.Log($"item length {item.Length}");
        //     foreach (var data in item)
        //     {
        //         Debug.Log("data " + data);
        //     }
        //     Debug.Log($"item {item}");
        // }
        // //CheckFileSize();
        // LoadResourceLocation();
    }
    void LoadResourceLocation()
    {
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(label);
        Debug.Log(handle.PercentComplete);
        handle.Completed += LocationsLoaded;
    }
     void LocationsLoaded(AsyncOperationHandle<IList<IResourceLocation>> obj)
    {
        Debug.Log("load Resource complete "+obj.Result.Count);
        foreach (var item in obj.Result)
        {
            Debug.Log("PrimaryKey " + item.PrimaryKey);
            Debug.Log("resourceType " + item.ResourceType);
        }
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            locations = new List<IResourceLocation>(obj.Result);
            LoadDependency();
        }
        else
        {
            Debug.LogError("locationsLoaded is failed");
        }
    }
    void LoadDependency()
    {
        CheckFileSize();
       // AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(label);
        //handle.Completed += DependencyLoaded;
    }
    void DependencyLoaded(AsyncOperationHandle obj)
    {
        Debug.Log("DependencyLoaded complete "+obj.Result);
        //AssetBundleRes = obj.Result;

        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            LoadAssets();
        }
        else
        {
            Debug.LogError("dependencyLoaded is Failed");
        }
    }
    private void LoadAssets()
    {
        Debug.Log("toload Asset "+locations.Count);
       
        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(locations,OnAssetsCategoryLoaded);
    }
    IEnumerator TryDownLoadGameObject(AsyncOperationHandle<IList<IResourceLocation>> obj){
        Debug.Log("TryDownLoadGameObject "+obj.Result[0]);
        AsyncOperationHandle<GameObject> matHandle = Addressables.LoadAssetAsync<GameObject>(obj.Result[0]);
        yield return matHandle;
        if (matHandle.Status == AsyncOperationStatus.Succeeded)
        {
           Debug.Log("mathandle "+matHandle.Result);
           Instantiate(matHandle.Result);
            //etc...
        }
    }
    public IEnumerator TryDownloadDlc()
    {
        var downloadSize = Addressables.GetDownloadSizeAsync(label);
        yield return downloadSize;
        if(downloadSize.IsDone){
            Debug.Log("download size = "+downloadSize.Result);
            //Popup.Launch("download size = "+downloadSize.Result);
        }
        var operationHandle = Addressables.DownloadDependenciesAsync(label);
        //yield return operationHandle;
            while (operationHandle.IsDone == false)
            {
                //_progressText.text = $"{operationHandle.PercentComplete * 100.0f} %";
                Debug.Log($"{operationHandle.PercentComplete* 100.0f} %");
                yield return null;
            }
        operationHandle.Completed += DependencyLoaded;
        Debug.Log(operationHandle.Result);
    }
    
    
    private void OnAssetsCategoryLoaded(GameObject obj)
    {
        //SpawnItem(obj.name);
        Debug.Log("OnAssetsCategoryLoaded "+obj.name);
        Instantiate(obj);
       // Addressables.Release(locations);
    }
    private void OnAssetsLoaded(AsyncOperationHandle<IList<GameObject>> obj)
    {
        Debug.Log("OnAssetsLoaded "+obj.Result);
        foreach (var item in obj.Result)
        {
            Instantiate(item);
        }
    }
    void CheckFileSize(){
        
        
        //await getDownloadSize.Task;
        //Debug.Log("Getsize "+GetSize().IsCompleted);
        //Debug.Log("Getsize "+GetSize());
        GetSize();
    }

    private void OncheckfileSizeCompleted(AsyncOperationHandle<long> obj)
    {
        Debug.Log("OncheckfileSizeCompleted "+obj.Result);
    }

    async void GetSize(){
        var downloadSize = Addressables.GetDownloadSizeAsync(label);
        await downloadSize.Task;
        Debug.Log("downloadSize " + downloadSize.Result);
        if(downloadSize.Status == AsyncOperationStatus.Succeeded){
            //Popup.Launch("download file size "+downloadSize.Result);
        }
        
    }
}
