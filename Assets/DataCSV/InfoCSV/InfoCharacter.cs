using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Price
{
    public int gp;
    public int diamond;
    public int honor;

    public Price(int gp, int diamond, int honor)
    {
        this.gp = gp;
        this.diamond = diamond;
        this.honor = honor;
    }
}

[System.Serializable]
public class Condition
{
    public string conditionId;
    public string conditionNeedValue;
    public Condition(string conditionId, string conditionNeedValue)
    {
        this.conditionId = conditionId;
        this.conditionNeedValue = conditionNeedValue;
    }
}

[System.Serializable]
public class PrefabData
{
    public string nameItem;
    public string Id;
    public Sprite icon;
    public GameObject prefabHigh;
    public GameObject prefabLow;
    public Price price;
    public Condition condition;

    public PrefabData(string name, string Id, Sprite icon, GameObject prefabHigh, GameObject prefabLow, int gold, int diamond, int honor, string conditionId, string conditionNeedValue)
    {
        this.nameItem = name;
        this.Id = Id;
        this.icon = icon;
        this.prefabHigh = prefabHigh;
        this.prefabLow = prefabLow;
        price = new Price(gold, diamond, honor);
        condition = new Condition(conditionId, conditionNeedValue);
    }
}
[System.Serializable]
public class TextureData
{
    public string nameItem;
    public string Id;
    public Sprite icon;
    public Texture textureHigh;
    public Texture textureLow;
    public string prefabId;
    public Price price;
    public Condition condition;

    public TextureData(string name, string id, Sprite icon, Texture textureHigh, Texture textureLow,string prefabId, int gold, int diamond, int honor, string conditionId, string conditionNeedValue)
    {
        this.nameItem = name;
        this.Id = id;
        this.icon = icon;
        this.textureHigh = textureHigh;
        this.textureLow = textureLow;
        this.prefabId = prefabId;
        price = new Price(gold, diamond, honor);
        condition = new Condition(conditionId, conditionNeedValue);
    }
}


public class InfoCharacter : MonoBehaviour
{
    public static InfoCharacter Instance { get; protected set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPrefabsInfo();
        LoadTextureInfo();
    }

    public TextAsset CharacterCSV;
    public TextAsset HelmetPrefabCSV;
    public TextAsset HelmetTextureCSV;

    public TextAsset SuitCSV;
    public TextAsset GloveCSV;
    public TextAsset BootsCSV;

    public int startReadCsvAtRow = 3;
    [Header("Character Info")]
    internal Dictionary<string, PrefabData> dicCharacterInfo = new Dictionary<string, PrefabData>();
    public List<PrefabData> listCharacterInfo = new List<PrefabData>();

    [Header("Helmet Info")]
    internal Dictionary<string, PrefabData> dicHelmetPrefabInfo = new Dictionary<string, PrefabData>();
    public List<PrefabData> listHelmetPrefabInfo = new List<PrefabData>();
    internal Dictionary<string, TextureData> dicTextureHelmetInfo = new Dictionary<string, TextureData>();
    public List<TextureData> listHelmetTextureInfo = new List<TextureData>();

    [Header("Suit Info")]
    internal Dictionary<string, TextureData> dicTextureSuitInfo = new Dictionary<string, TextureData>();
    public List<TextureData> listTextureSuitInfo = new List<TextureData>();

    [Header("Glove Info")]
    internal Dictionary<string, TextureData> dicTextureGloveInfo = new Dictionary<string, TextureData>();
    public List<TextureData> listTextureGloveInfo = new List<TextureData>();

    [Header("Boots Info")]
    internal Dictionary<string, TextureData> dicTextureBootsInfo = new Dictionary<string, TextureData>();
    public List<TextureData> listTextureBootsInfo = new List<TextureData>();
    

    public void LoadPrefabsInfo()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");
        Dictionary<string, GameObject> dicPrefab = prefabs.ToDictionary(v => v.name, v => v);
        
        Sprite[] sprites = Resources.LoadAll<Sprite>("Icons");
        Dictionary<string, Sprite> dicIcon = sprites.ToDictionary(v => v.name, v => v);

        string[][] grid;
        // Character ------------------------------------------
        if (CharacterCSV)
        {
            grid = CsvParser.Parse(CharacterCSV.text);

            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                PrefabData prefabData = new PrefabData(
                    grid[i][1],
                    grid[i][0],
                    dicIcon[grid[i][2]],
                    dicPrefab[grid[i][3]],
                    dicPrefab[grid[i][4]],
                    int.Parse(grid[i][5]),
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    grid[i][8],
                    grid[i][9]
                    );
                listCharacterInfo.Add(prefabData);
                dicCharacterInfo.Add(grid[i][0], prefabData);
            }
        }
        // Helmet Prefab ------------------------------------------
        if (HelmetPrefabCSV)
        {
            grid = CsvParser.Parse(HelmetPrefabCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                PrefabData prefabData = new PrefabData(
                    grid[i][1],
                    grid[i][0],
                    dicIcon[grid[i][2]],
                    dicPrefab[grid[i][3]],
                    dicPrefab[grid[i][4]],
                    int.Parse(grid[i][5]),
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    grid[i][8],
                    grid[i][9]
                    );
                dicHelmetPrefabInfo.Add(grid[i][0], prefabData);
                listHelmetPrefabInfo.Add(prefabData);
            }
        }
    }

    public void LoadTextureInfo()
    {
        Dictionary<string, Texture> dicTexture = Resources.LoadAll<Texture>("Textures").ToDictionary(v => v.name, v => v);
        Dictionary<string, Sprite> dicIcon = Resources.LoadAll<Sprite>("Icons").ToDictionary(v => v.name, v => v);

        string[][] grid;

        // HelmetTexture ------------------------------------------
        if (HelmetTextureCSV)
        {
            grid = CsvParser.Parse(HelmetTextureCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                TextureData textureData = new TextureData(
                    grid[i][1],
                    grid[i][0],
                    dicIcon[grid[i][2]],
                    dicTexture[grid[i][3]],
                    dicTexture[grid[i][4]],
                    grid[i][5],
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    int.Parse(grid[i][8]),
                    grid[i][9],
                    grid[i][10]
                    );
                dicTextureHelmetInfo.Add(grid[i][0], textureData);
                listHelmetTextureInfo.Add(textureData);
            }
        }
        // Suit ------------------------------------------
        if (SuitCSV)
        {
            grid = CsvParser.Parse(SuitCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                TextureData textureData = new TextureData(
                    grid[i][1],
                    grid[i][0],
                    dicIcon[grid[i][2]],
                    dicTexture[grid[i][3]],
                    dicTexture[grid[i][4]],
                    grid[i][5],
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    int.Parse(grid[i][8]),
                    grid[i][9],
                    grid[i][10]
                    );
                dicTextureSuitInfo.Add(grid[i][0], textureData);
                listTextureSuitInfo.Add(textureData);
            }
        }

        // Glove ------------------------------------------
        if (GloveCSV)
        {
            grid = CsvParser.Parse(GloveCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                TextureData textureData = new TextureData(
                    grid[i][1],
                    grid[i][0],
                    dicIcon[grid[i][2]],
                    dicTexture[grid[i][3]],
                    dicTexture[grid[i][4]],
                    grid[i][5],
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    int.Parse(grid[i][8]),
                    grid[i][9],
                    grid[i][10]
                    );
                dicTextureGloveInfo.Add(grid[i][0], textureData);
                listTextureGloveInfo.Add(textureData);
            }
        }

        // Boots ------------------------------------------
        if (BootsCSV)
        {
            grid = CsvParser.Parse(BootsCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                TextureData textureData = new TextureData(
                    grid[i][1],
                    grid[i][0],
                    dicIcon[grid[i][2]],
                    dicTexture[grid[i][3]],
                    dicTexture[grid[i][4]],
                    grid[i][5],
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    int.Parse(grid[i][8]),
                    grid[i][9],
                    grid[i][10]
                    );
                dicTextureBootsInfo.Add(grid[i][0], textureData);
                listTextureBootsInfo.Add(textureData);
            }
        }
    }

    public GameObject GetHelmetObjHigh(string Id)
    {
        return dicHelmetPrefabInfo[Id].prefabHigh;
    }
    public GameObject GetHelmetObjLow(string Id)
    {
        return dicHelmetPrefabInfo[Id].prefabLow;
    }
    public Texture GetHelmetTextureHigh(string Id)
    {
        return dicTextureHelmetInfo[Id].textureHigh;
    }
    public Texture GetHelmetTextureLow(string Id)
    {
        return dicTextureHelmetInfo[Id].textureLow;
    }

    public Texture GetSuitTextureHigh(string Id)
    {
        return dicTextureSuitInfo[Id].textureLow;
    }
    public Texture GetSuitTextureLow(string Id)
    {
        return dicTextureSuitInfo[Id].textureLow;
    }

    public Texture GetGlovesTextureHigh(string Id)
    {
        return dicTextureGloveInfo[Id].textureLow;
    }
    public Texture GetGlovesTextureLow(string Id)
    {
        return dicTextureGloveInfo[Id].textureLow;
    }

    public Texture GetBootsTextureHigh(string Id)
    {
        return dicTextureBootsInfo[Id].textureLow;
    }
    public Texture GetBootsTextureLow(string Id)
    {
        return dicTextureBootsInfo[Id].textureLow;
    }
}
