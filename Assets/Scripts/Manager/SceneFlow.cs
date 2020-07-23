using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoSingleton<SceneFlow>
{
    bool isInit = false;

    public void StartScene(){
        Debug.Log("StartScene");
        Debug.Log("dirty "+SceneManager.GetSceneByPath(SceneName.START).isDirty);
        Debug.Log("isLoaded "+SceneManager.GetSceneByPath(SceneName.START).isLoaded);
        Debug.Log("GetSceneByName isLoaded "+SceneManager.GetSceneByName(SceneName.START).isLoaded);
        if(isInit)return;
        if(!SceneManager.GetSceneByName(SceneName.START).isLoaded){
            SceneManager.LoadScene(SceneName.START);
            isInit = true;
        }else{
            isInit = true;
        }
    }
}
