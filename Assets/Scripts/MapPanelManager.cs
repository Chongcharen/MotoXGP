using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanelManager : MonoBehaviour {

    public GameObject[] panels;

    private void Start()
    {
        ClearPanels();
        panels[0].SetActive(true);
    }

    public void ClearPanels()
    {
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
    }
    public void SelectMap(string mapName)
    {
        PlayerPrefs.SetString("MapId", mapName);
    }
}
