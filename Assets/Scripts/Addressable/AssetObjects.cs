using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetObjects
{
    public static List<GameObject> gameObjects{get;} = new List<GameObject>();
    public static void ClearGameObjects(){
        foreach (var gameObject in gameObjects)
        {
            if(gameObject != null)
                UnityEngine.Object.Destroy(gameObject);
        }
        gameObjects.Clear();
    }

}
