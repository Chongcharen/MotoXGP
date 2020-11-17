using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;
using UniRx;
public class AddressableManager : MonoBehaviour
{
    public static Subject<Unit> OnUpdateCatalogComplete = new Subject<Unit>();
    public static Subject<Unit> OnDownLoadResourceLocationCompleted = new Subject<Unit>();
    public static Subject<Unit> OnDownloadDependenciesCompleted = new Subject<Unit>();
    static AddressableManager _instance;

    public string Label = "product";
    public List<object> Labels = new List<object>{AddressableKeys.LABEL_MODEL_EQUIPMENT,AddressableKeys.LABEL_TEXTURE_EQUIPMENT,AddressableKeys.LABEL_ATLAS};
    List<IResourceLocation> assetLocations {get;}= new List<IResourceLocation>();
    Dictionary<string,IList<IResourceLocation>> resourceLocationLabel = new Dictionary<string, IList<IResourceLocation>>();
    List<GameObject> assets = new List<GameObject>();
    AsyncOperationHandle<List<IResourceLocator>> updateCatalogHandle = new AsyncOperationHandle<List<IResourceLocator>>();
    List<IResourceLocator> resourceLocators = new List<IResourceLocator>();
    //Load object form addressable
    public List<object> addressableObjects = new List<object>();
    //Load Gameobject form addressable
    public List<GameObject> gameObjcts = new List<GameObject>();
    //map key , name form addressable objects
    public Dictionary<string,object> objectDic = new Dictionary<string, object>();
    public static AddressableManager Instance{
        get{
            if(_instance == null){
                var go = new GameObject("Addressable Manager",typeof(AddressableManager));
                _instance = go.GetComponent<AddressableManager>();
            }
            return _instance;
        }
    }
    private void Start()
    {
        OnUpdateCatalogComplete.Subscribe(async _=>{
            await DownLoadResourceLocation();
        }).AddTo(this);
        OnDownLoadResourceLocationCompleted.Subscribe(async _=>{
            DownloadDependencies();
        }).AddTo(this);
        OnDownloadDependenciesCompleted.Subscribe(_=>{
            GetBundleSize();
        }).AddTo(this);
    }
    public void Init(){
        Addressables.ResourceManager.ResourceProviders.Add(new AssetBundleProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new PlayFabStorageHashProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new PlayFabStorageAssetBundleProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new PlayFabStorageJsonAssetProvider());
        Addressables.InitializeAsync().Completed += OnInitcomplete;
    }
    public void SetLabel(string labelKey){
        Label = labelKey;
    }
    #region  UpdateCatalog
    public void CheckCatalogUpdate()
    {
        Debug.Log("CheckCatalogUpdate");
        var updateCatalogHandle = Addressables.CheckForCatalogUpdates();
        updateCatalogHandle.Completed += listStr =>{
            Debug.Log("catalog list "+listStr.Result.Count);
           if(listStr.Result.Count > 0){
               Addressables.UpdateCatalogs(listStr.Result);
           }else{
               OnUpdateCatalogComplete.OnNext(default);
           }
       };
    }
    async Task UpdateCatalog(AsyncOperationHandle<List<string>> catalogString){
        var handle = await Addressables.UpdateCatalogs(catalogString.Result).Task;
        resourceLocators = handle;
        OnUpdateCatalogComplete.OnNext(default);
    }
    #endregion
    public async Task DownLoadResourceLocation(){
        Debug.Log("DownLoadResourceLocation......");
        await AddressableLocationLoader.GetAll(Labels,assetLocations);
        OnDownLoadResourceLocationCompleted.OnNext(default);
    }
    public void DownloadDependencies(){
        Debug.Log("DownloadDependencies......");
        AsyncOperationHandle dependenciesHandle = Addressables.DownloadDependenciesAsync(assetLocations);
        Popup_DownloadData.Launch(dependenciesHandle);
        dependenciesHandle.Completed += op =>{
            Debug.Log("DownloadDependencies completed");
            Debug.Log(op.Result);
            OnDownloadDependenciesCompleted.OnNext(default);
        };
    }
    // public async Task CreateAssets(){
    //     await p.ByLoadedAddress(assetLocations,assets);
    //     foreach (var asset in assets)
    //     {
    //         Debug.Log("Location "+asset);
    //     }
    // }
    // async void CreateAssetsGameObject(){
    //     await CreateAssets();
    // }
    public void GetBundleSize(){
        Addressables.GetDownloadSizeAsync(Labels).Completed+= size =>{
            Debug.Log("size "+size.Result);
        };
    }
    

    private void OnAssetsCategoryLoaded(object obj)
    {
        if(obj.GetType() != typeof(GameObject))return;
        Instantiate(obj as GameObject);
    }

    
    public async Task DownloadObject<T>(string name) where T : UnityEngine.Object{
       var task = await Addressables.LoadAssetAsync<T>(name).Task;
    }
    public async Task<T> LoadObject<T>(string name) where T : UnityEngine.Object{
        print(Depug.Log("load object name "+name,Color.green));
        Debug.Log("ContainsKey "+objectDic.ContainsKey(name));
        if(objectDic.ContainsKey(name))
            return (T)objectDic[name];
        var handle = Addressables.LoadAssetAsync<T>(name);
        await handle.Task;
        Debug.Log("result "+handle.Result);
        if(handle.Result !=null){
            Debug.Log("toadd "+name +"= "+handle.Result);
            Debug.Log("before count "+objectDic.Count);
            if(!objectDic.ContainsKey(name))
                objectDic.Add(name,handle.Result);
            Debug.Log("after count "+objectDic.Count);
        }else{
            Debug.LogWarning("Not found "+name +" in addressable Asset");
        }
        Debug.Log("Object count "+addressableObjects.Count);
        return handle.Result;
    }
    public async Task<GameObject> CreateGameObject(string name){
        var handle =  await Addressables.InstantiateAsync(name).Task;
        gameObjcts.Add(handle);
        return handle;
    }
    public static async Task<T> LoadAssetLocation<T>(string name) where T:UnityEngine.Object{
        var handle = Addressables.LoadAssetAsync<T>(name);
        await handle.Task;
        return handle.Result;
    }
   
    //release asset form addressable
    public void ReleaseLabel(string key_label){
        Addressables.Release(key_label);
        resourceLocationLabel.Remove(key_label);
    }
    public void ReleaseAsset(GameObject go){
        Addressables.Release(go);
        assets.Remove(go);
    }
    public void ReleaseObjects(){
        foreach (var item in addressableObjects)
        {
            Addressables.Release(item);
        }
        addressableObjects.Clear();
    }
    //
    public void ClearDictionaryAsset(){
        foreach (var item in objectDic.Values)
        {
            Addressables.Release(item);
        }
        objectDic.Clear();
    }
    public void ClearAllAssets(){
        ReleaseObjects();
        ClearGameObjects();
        ClearDictionaryAsset();
        AssetObjects.ClearGameObjects();
        Debug.Log("clear count "+assets.Count);
    }
    public void ClearGameObjects(){
        foreach (var obj in gameObjcts)
        {
            Addressables.ReleaseInstance(obj);
        }
        gameObjcts.Clear();
    }
    public void ClearDependencies(){
        Debug.Log("ClearDependencies");
        Addressables.ClearResourceLocators();
        Addressables.ClearDependencyCacheAsync(Labels);
        // ยังไม่สมบูรณ์
    }
    #region Callback
    void OnInitcomplete(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("init complete "+obj);
        CheckCatalogUpdate();
    }

    #endregion
}
