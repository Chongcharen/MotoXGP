using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomListItem : MonoBehaviour {

    public Transform content;
    public GameObject itemPrefab;

    private CustomPlayer cp;

    private void Start()
    {
        cp = GetComponentInParent<CustomPlayer>();
    }

    public void InitItem(Dictionary<string, TextureData> dicTextureInfo)
    {
        foreach (var item in dicTextureInfo.Values)
        {
            var itemObj = Instantiate(itemPrefab, content);
            itemObj.GetComponent<Image>().sprite = item.icon;
            itemObj.GetComponent<Button>().onClick.AddListener(delegate { SelectItem(item.Id); });
            itemObj.SetActive(true);
        }
    }
    public void InitMap(Dictionary<string, MapData> dicMap , string theme)
    {
        foreach (var item in dicMap.Values)
        {
            if(item.theme == theme)
            {
                var itemObj = Instantiate(itemPrefab, content);
                itemObj.GetComponent<Image>().sprite = item.mapImgSprite;
                itemObj.GetComponent<Button>().onClick.AddListener(delegate { SelectMap(item.Id); });
                itemObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = item.mapName;
                itemObj.SetActive(true);
            }
        }
    }

    private void SelectItem(string Id)
    {
        cp.CustomSelectItem(Id);
    }
    private void SelectMap(string Id)
    {
        PlayerPrefs.SetString("MapId", Id);
    }
}
