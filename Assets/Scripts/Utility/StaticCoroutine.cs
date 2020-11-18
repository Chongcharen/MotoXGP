using System.Net.Cache;
using System.IO;
using UnityEngine;
using System.Collections;
using System;
public class StaticCoroutine : MonoBehaviour{

	static StaticCoroutine instance;
    public static StaticCoroutine Instance
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject("StaticCoroutine");
                instance = go.AddComponent<StaticCoroutine>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

	IEnumerator Perform(IEnumerator coroutine, Action onComplete = null)
	{
		onComplete = onComplete ?? delegate {};
		yield return StartCoroutine(coroutine);
		onComplete();
	}

	static public void DoCoroutine(IEnumerator coroutine, Action onComplete = null){
		Instance.StartCoroutine(Instance.Perform(coroutine, onComplete)); //this will launch the coroutine on our instance
	}
	public static IEnumerator LoadImage(string path,Action<Texture2D> onComplete){
        Debug.Log("load path " + path);
		WWW www = new WWW (path);
		yield return www;
		onComplete (www.texture);

        
        //ImageManager.instance.AddImageLink(path, www.texture);
	}
 
}