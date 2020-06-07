using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemTexture
{
    public string nameItem;
    public Texture texture;
}

public class StoreItem : MonoBehaviour
{
    public static StoreItem si;

    private void Awake()
    {
        if(si != null)
        {
            Destroy(gameObject);
            return;
        }
        si = this;
        DontDestroyOnLoad(gameObject);
    }

    public itemTexture[] bikeBody;

    public itemTexture[] suitTex;
    public itemTexture[] helmetTex;
    public itemTexture[] gloveTex;
    public itemTexture[] bootTex;
    [Header("Low")]
    public itemTexture[] bikeBodyL;
    public itemTexture[] suitTexL;
    public itemTexture[] helmetTexL;
    public itemTexture[] gloveTexL;
    public itemTexture[] bootTexL;





    public Dictionary<string, itemTexture> dicSticker = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicSuit = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicHelmet = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicGlove = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicBoot = new Dictionary<string, itemTexture>();



    public Dictionary<string, itemTexture> dicStickerL = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicSuitL = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicHelmetL = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicGloveL = new Dictionary<string, itemTexture>();

    public Dictionary<string, itemTexture> dicBootL = new Dictionary<string, itemTexture>();

    private void Start()
    {

        InitAllItem();
    }

    void InitAllItem()
    {
        foreach(var item in bikeBody)
        {
            dicSticker.Add(item.nameItem, item);
        }

        foreach (var item in suitTex)
        {
            dicSuit.Add(item.nameItem, item);
        }

        foreach (var item in helmetTex)
        {
            dicHelmet.Add(item.nameItem, item);
        }

        foreach (var item in gloveTex)
        {
            dicGlove.Add(item.nameItem, item);
        }

        foreach (var item in bootTex)
        {
            dicBoot.Add(item.nameItem, item);
        }




        foreach (var item in bikeBodyL)
        {
            dicStickerL.Add(item.nameItem, item);
        }

        foreach (var item in suitTexL)
        {
            dicSuitL.Add(item.nameItem, item);
        }

        foreach (var item in helmetTexL)
        {
            dicHelmetL.Add(item.nameItem, item);
        }

        foreach (var item in gloveTexL)
        {
            dicGloveL.Add(item.nameItem, item);
        }

        foreach (var item in bootTexL)
        {
            dicBootL.Add(item.nameItem, item);
        }

    }
}
