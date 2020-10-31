using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Reflection;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets.ResourceLocators;
using System.Reflection.Emit;
using UnityEngine.ResourceManagement.ResourceProviders;
using System;
using UniRx;
using UnityEngine.XR;

public class Addressableloader : MonoBehaviour
{
  public Slider slider;
  AsyncOperationHandle<IList<IResourceLocation>> handle;
    public string catalogPath;
    public string myLabel;
    public List<IResourceLocation> locations;

    private void Start()
    {
        initAddressables();
    }

    public void initAddressables()
    {
        //uiManager.loading.SetActive(true);
        AsyncOperationHandle<IResourceLocator> handle = Addressables.InitializeAsync();
        handle.Completed += initDone;
    }
 
 
    private void initDone(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("initDone Load catalog");
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            LoadCatalog();
        }
    }
 
 
    void LoadCatalog()
    {
       
        //AsyncOperationHandle<IResourceLocator> handle;
        AsyncOperationHandle<List<string>> handle;
        /* handle = new AsyncOperationHandle<IResourceLocator>();      
         handle = Addressables.LoadContentCatalogAsync(catalogPath);      
         handle.Completed += LoadCatalogsCompleted;   */
        handle = new AsyncOperationHandle<List<string>>();
        handle = Addressables.CheckForCatalogUpdates(true);
        handle.Completed += CheckCatalogComplete;
     
    }

    private void CheckCatalogComplete(AsyncOperationHandle<List<string>> obj)
    {
        Debug.Log("Catalogcomplete " + obj.Result.Count);
        foreach (var item in obj.Result)
        {
            Debug.Log($"item length {item.Length}");
            foreach (var data in item)
            {
                Debug.Log("data " + data);
            }
            Debug.Log($"item {item}");
        }
        LoadResourceLocation();
    }

    void LoadCatalogsCompleted(AsyncOperationHandle<IResourceLocator> obj)
    {
       
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("LoadCatalog done");
            //LoadResourceLocation();
        }
        else
        {
            Debug.LogError("LoadCatalogsCompleted is failed");
        }
        LoadResourceLocation();
    }
    void LoadResourceLocation()
    {
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(myLabel);
        Debug.Log(handle.PercentComplete);
        handle.Completed += LocationsLoaded;
    }
    private void Update()
    {
       // Debug.Log(asyncHandle.PercentComplete);

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
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(myLabel);
        handle.Completed += DependencyLoaded;
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

        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(locations, OnAssetsCategoryLoaded);
        handle.Completed += OnAssetsLoaded;
    }
    private void OnAssetsCategoryLoaded(GameObject obj)
    {
        SpawnItem(obj.name);
    }
    private void OnAssetsLoaded(AsyncOperationHandle<IList<GameObject>> obj)
    {
        Debug.Log("OnAssetsLoaded "+obj.Result);
    }
 
    void SpawnItem(string addressableKey)
    {
        Debug.Log("Spawn item " + addressableKey);
      /*  if (!instantiated)
        {
            AsyncOperationHandle<GameObject> asyncLoad = Addressables.InstantiateAsync(myLabel, Vector3.zero, Quaternion.identity);
            StartCoroutine(progressAsync(asyncLoad));
            asyncLoad.Completed += AssetSpawned;
            instantiated = true;
        }*/
     
    }
 
    private System.Collections.IEnumerator progressAsync(AsyncOperationHandle<GameObject> asyncOperation)
    {
        float percentLoaded = asyncOperation.PercentComplete;
        while (!asyncOperation.IsDone)
        {
            Debug.Log("Progress = " + percentLoaded + "%");
            yield return 0;
        }
    }
    void AssetSpawned(AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log("Asset Spawned");
    }
}
