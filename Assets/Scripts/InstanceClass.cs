using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceClass<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = null;
    public static T Instance{
        get{
            if(instance == null){
                instance = new GameObject(typeof(T).Name,typeof(T)).GetComponent<T>();
            }
            return instance;
        }
    }
}
