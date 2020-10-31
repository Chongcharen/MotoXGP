using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.U2D;
using System;

public static class AssetsLoader 
{
    public static async void Load(List<string> keys,Action act){
        
        foreach (var key in keys)
        {
             await AddressableManager.Instance.LoadObject<UnityEngine.Object>(key);
        }
        Debug.Log("Asset loader load completed");
        act();
    }
}
