using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    public static MapInfo mi;

    public int indexMap;

    public Dictionary<string, GameObject> dicMapObj = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        if (mi != null)
        {
            Destroy(gameObject);
            return;
        }
        mi = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dicMapObj = Resources.LoadAll<GameObject>("Prefabs/Maps").ToDictionary(v => v.name, v => v);
    }

    public void SelectMap(int index)
    {
        indexMap = index;
    }

}
