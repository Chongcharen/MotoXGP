using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    Transform poolTransform;
    Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
    List<GameObject> goLists;
    GameObject go;

    public void Init(){
        poolTransform = this.gameObject.transform;
        print(Depug.Log("Objectpool init ",Color.green));
    }
    //Pools
    public void CreatePool(string path, int count)
    {
        if (pools.ContainsKey(path))
        {
            goLists = pools[path];
        }
        else
        {
            goLists = new List<GameObject>();
            pools.Add(path, goLists);
        }
        for (int i = 0; i < count; i++)
        {
            var poolObject = GetAssetPrefab(path);
            if (poolObject != null)
            {
                go = Object.Instantiate(poolObject, new Vector3(-1000, -1000, -1000), Quaternion.identity, poolTransform);
                go.SetActive(false);
                goLists.Add(go);
            }
        }
    }
     public GameObject GetAssetPrefab(string assetPath) {
        var prefab = Resources.Load(assetPath) as GameObject;
        if(prefab == null)
            Debug.LogError("Not found "+assetPath + "in resources folder");
        
        return prefab != null ? prefab : new GameObject();
        //Debug.Log("GetAssetPrefab : "+assetPath);
        //Debug.Assert(assetBind.ContainsKey(assetPath));
        // if (assetBind.ContainsKey(assetPath)){
        //     if (assetBind.TryGetValue(assetPath, out GameObject gameObject)) {
        //         return gameObject;
        //     }
        //     BugsnagUnity.Bugsnag.Notify(new KeyNotFoundException(assetPath + " is not found in assets"));
        //     return null;
        // }else{
        //     LoadPrefabAsset(assetPath,2);
        //     return new GameObject();
        // }
        //throw new KeyNotFoundException(assetPath + " is not found in assets");
    }
    public GameObject GetObjectFormPool(string path)
    {
        go = null;
        if (pools.ContainsKey(path))
        {
            goLists = pools[path];
            for (var i = 0; i < goLists.Count; i++)
            {
                if (!goLists[i].activeSelf)
                {
                    go = goLists[i];
                    go.SetActive(true);
                    return go;
                }
            }
            if (go == null)
            {
                CreatePool(path, 1);
                go = goLists[goLists.Count - 1];
                go.SetActive(true);
                return go;
            }
        }
        return go;
    }
    public void Recycle(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = new Vector3(-1000, -1000, -1000);
    }
    void DestroyGameObjectFormPool(string path){
        if(pools.ContainsKey(path)){
            var gameObjectList = pools[path];
            DestroyGameObjectFormList(gameObjectList);
            pools.Remove(path);
            gameObjectList = null;
        }
    }
    void DestroyGameObjectFormList(List<GameObject> objList){
        foreach (GameObject gameObject in objList.ToList())
        {
            if (gameObject != null)
            {
                Object.Destroy(gameObject);
            }
        }
        objList.Clear();
    }
    public void ClearPool(){
        foreach (string path in pools.Keys.ToList())
        {
            DestroyGameObjectFormPool(path);
        }
        pools.Clear();
    }
}
