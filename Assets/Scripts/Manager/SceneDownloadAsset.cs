using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneDownloadAsset 
{
    public static void LoadScene(string sceneName,List<string> assetLabels = null,bool useBoltLoadScene = false){
        AddressableManager.Instance.ClearAllAssets();
        Popup_Loading.Launch();
        if(assetLabels != null){
            AssetsLoader.Load(assetLabels,()=>{
                    if(useBoltLoadScene)
                        BoltNetwork.LoadScene(sceneName);
                    else
                        SceneManager.LoadSceneAsync(sceneName);
                });
        }else
        {
             if(useBoltLoadScene)
                BoltNetwork.LoadScene(sceneName);
            else
                SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
