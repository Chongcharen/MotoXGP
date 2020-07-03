using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayer : MonoBehaviour
{

    [SerializeField] ModelManager modelManager;

    public string type;
    public int numBody;
    private string itemId;

    private InfoBike ib;
    private InfoCharacter ic;

    public GameObject[] panels;


    private void Start()
    {
        ib = InfoBike.Instance;
        ic = InfoCharacter.Instance;

        ClearPanel();
        panels[0].SetActive(true);
    }

    public void SetType(string i)
    {
        type = i;
        itemId = PlayerPrefs.GetString(type);
    }
    

    public void CustomSelectItem(string itemId)
    {
        this.itemId = itemId;
        if (type == "Body")
        {
            modelManager.SetBikeTexture(ib.GetBodyTextureHigh(itemId), ib.GetBodyObjHigh(ib.dicSticker[itemId].prefabId));
            Debug.Log(ib.dicSticker[itemId].prefabId);
        }

        else if (type == "Suit")
        {
            modelManager.SetSuitTexture(ic.GetSuitTextureHigh(itemId));
        }
        else if (type == "Helmet")
        {
            modelManager.SetHelmetTexture(ic.GetHelmetTextureHigh(itemId), ic.GetHelmetObjHigh(ic.dicTextureHelmetInfo[itemId].prefabId));
        }

        else if (type == "Gloves")
        {
            modelManager.SetGloveTexture(ic.GetGlovesTextureHigh(itemId));
        }
        else if (type == "Boots")
        {
            modelManager.SetBootTexture(ic.GetBootsTextureHigh(itemId));
        }
        else
            Debug.LogError("Don't have menu.");
    }

    public void ClearPanel()
    {
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
    }

    public void SelectItem()
    {
        LobbyManager.lm.SetCustomProperties(type, itemId);
    }


}
