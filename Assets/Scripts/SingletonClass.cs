using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = null;
    public static T Instance{
        get{
            if(instance == null){
                instance = new GameObject(typeof(T).Name,typeof(T)).GetComponent<T>();
                DontDestroyOnLoad(instance.gameObject);
                //var test = TestSingleton
                var testsingle = TestingSingle.Instance;
                testsingle.DebugClassInstance(instance.name);
            }
            return instance;
        }
    }
    public void Dispose(){
        Debug.Log("Destroy SingleTon "+this.gameObject.name);
        Destroy(this.gameObject);
    }
}
public class TestingSingle : Singleton<TestingSingle> {
    
    public TestingSingle(){
    }
    internal void DebugClassInstance(string classname){
        Debug.Log("DebugClassInstance create singletonclass "+classname);
    }
    public void Init(){
        
    }
}
public abstract class Singleton<T> where T : class,new(){
    private static T instance = new T();

        public static T Instance
        {
            get
            {
                if(instance == null)
                    instance = new T();
                return instance;
            }
        }
}