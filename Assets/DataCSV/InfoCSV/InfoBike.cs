using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ColorSolid
{
    public string name;
    public string id = "1000";
    public int r;
    public int g;
    public int b;
}


[System.Serializable]
public class Status
{
    public string Id = "1000";
    public string nameItem = "";
    public float speed;
    public float weightFrontWheel;
    public float weightRearWheel;
    public float forceGear;
    public float forceJump;
    public float forceNitro;
    public float timeNitro;
    public float groundedWeightFactor;
    public float inAirRotationSpeed;
    public Price price;
    public Condition condition;

    public Status(string id, string nameItem, float speed, float weightFrontWheel, float weightRearWheel, float forceGear, float forceJump, float forceNitro, float timeNitro, float groundedWeightFactor, float inAirRotationSpeed, int gold, int diamond, int honor, string conditionId, string conditionNeedValue)
    {
        this.Id = id;
        this.nameItem = nameItem;
        this.speed = speed;
        this.weightFrontWheel = weightFrontWheel;
        this.weightRearWheel = weightRearWheel;
        this.forceGear = forceGear;
        this.forceJump = forceJump;
        this.forceNitro = forceNitro;
        this.timeNitro = timeNitro;
        this.groundedWeightFactor = groundedWeightFactor;
        this.inAirRotationSpeed = inAirRotationSpeed;
        price = new Price(gold, diamond, honor);
        condition = new Condition(conditionId, conditionNeedValue);
    }
}

public class InfoBike : MonoBehaviour {

    public static InfoBike Instance { get; protected set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }

    public TextAsset StickerCSV;
    public TextAsset BodyCSV;
    public TextAsset FrameCSV;

    public TextAsset EngineLevelCSV;
    public TextAsset WheelLevelCSV;
    public TextAsset FrameLevelCSV;

    public int startReadCsvAtRow = 3;
    [Header("Bike Info")]

    internal Dictionary<string, TextureData> dicSticker = new Dictionary<string, TextureData>();
    internal Dictionary<string, PrefabData> dicFrame = new Dictionary<string, PrefabData>();
    internal Dictionary<string, PrefabData> dicBody = new Dictionary<string, PrefabData>();

    public Material solidMaterial;
    public Material strickerMaterial;
    private Dictionary<string, ColorSolid> dicColorSolid = new Dictionary<string, ColorSolid>();

    public List<PrefabData> listFrame = new List<PrefabData>();
    public List<PrefabData> listBody = new List<PrefabData>();
    public List<TextureData> listSticker = new List<TextureData>();
    public List<ColorSolid> listColorSolid = new List<ColorSolid>();

    // Status

    internal Dictionary<string, Status> dicEngineLevel = new Dictionary<string, Status>();
    internal Dictionary<string, Status> dicWheelLevel = new Dictionary<string, Status>();
    internal Dictionary<string, Status> dicFrameLevel = new Dictionary<string, Status>();

    public List<Status> listEngineLevel = new List<Status>();
    public List<Status> listWheelLevel = new List<Status>();
    public List<Status> listFrameLevel = new List<Status>();
    

    public void Init()
    {
        LoadTextureInfo();
        LoadPrefabInfo();
        SetColorSolid();
        LoadStatusInfo();
    }

    private void SetColorSolid()
    {
        dicColorSolid = listColorSolid.ToDictionary(v => v.id, v => v);
    }

    private void LoadTextureInfo()
    {
        Dictionary<string, Sprite> dicSprite = Resources.LoadAll<Sprite>("Icons").ToDictionary(v => v.name, v => v);
        Dictionary<string, Texture> dicTexture = Resources.LoadAll<Texture>("Textures").ToDictionary(v => v.name, v => v);

        string[][] grid = CsvParser.Parse(StickerCSV.text);
        for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
        {
            TextureData StickerInfo = new TextureData(
                grid[i][1],
                grid[i][0],
                dicSprite[grid[i][2]],
                dicTexture[grid[i][3]],
                dicTexture[grid[i][4]],
                grid[i][5],
                int.Parse(grid[i][6]),
                int.Parse(grid[i][7]),
                int.Parse(grid[i][8]),
                grid[i][9],
                grid[i][10]
                );
            dicSticker.Add(grid[i][0], StickerInfo);
            listSticker.Add(StickerInfo);
        }
    }

    private void LoadPrefabInfo()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");
        Dictionary<string, GameObject> dicPrefab = prefabs.ToDictionary(v => v.name, v => v);

        Sprite[] iconSprites = Resources.LoadAll<Sprite>("Icons");
        Dictionary<string, Sprite> dicIconSprite = iconSprites.ToDictionary(v => v.name, v => v);
        
        string[][] grid;
        if (FrameCSV)
        {
            grid = CsvParser.Parse(FrameCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                PrefabData prefabInfo = new PrefabData(
                    grid[i][1],
                    grid[i][0],
                    dicIconSprite[grid[i][2]],
                    dicPrefab[grid[i][3]],
                    dicPrefab[grid[i][4]],
                    int.Parse(grid[i][5]),
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    grid[i][8],
                    grid[i][9]
                    );
                dicFrame.Add(grid[i][0], prefabInfo);
                listFrame.Add(prefabInfo);
            }
        }
        if (BodyCSV)
        {
            grid = CsvParser.Parse(BodyCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                PrefabData prefabInfo = new PrefabData(
                    grid[i][1],
                    grid[i][0],
                    dicIconSprite[grid[i][2]],
                    dicPrefab[grid[i][3]],
                    dicPrefab[grid[i][4]],
                    int.Parse(grid[i][5]),
                    int.Parse(grid[i][6]),
                    int.Parse(grid[i][7]),
                    grid[i][8],
                    grid[i][9]
                    );
                dicBody.Add(grid[i][0], prefabInfo);
                listBody.Add(prefabInfo);
            }
        }
    }

    private void LoadStatusInfo()
    {
        string[][] grid;
        if (EngineLevelCSV)
        {
            grid = CsvParser.Parse(EngineLevelCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                Status statusInfo = new Status(
                    grid[i][0],
                    grid[i][1],
                    float.Parse(grid[i][2]),
                    float.Parse(grid[i][3]),
                    float.Parse(grid[i][4]),
                    float.Parse(grid[i][5]),
                    float.Parse(grid[i][6]),
                    float.Parse(grid[i][7]),
                    float.Parse(grid[i][8]),
                    float.Parse(grid[i][9]),
                    float.Parse(grid[i][10]),
                    int.Parse(grid[i][11]),
                    int.Parse(grid[i][12]),
                    int.Parse(grid[i][13]),
                    grid[i][14],
                    grid[i][15]
                    );
                dicEngineLevel.Add(grid[i][0], statusInfo);
                listEngineLevel.Add(statusInfo);
            }
        }
        if (WheelLevelCSV)
        {
            grid = CsvParser.Parse(WheelLevelCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                Status statusInfo = new Status(
                    grid[i][0],
                    grid[i][1],
                    float.Parse(grid[i][2]),
                    float.Parse(grid[i][3]),
                    float.Parse(grid[i][4]),
                    float.Parse(grid[i][5]),
                    float.Parse(grid[i][6]),
                    float.Parse(grid[i][7]),
                    float.Parse(grid[i][8]),
                    float.Parse(grid[i][9]),
                    float.Parse(grid[i][10]),
                    int.Parse(grid[i][11]),
                    int.Parse(grid[i][12]),
                    int.Parse(grid[i][13]),
                    grid[i][14],
                    grid[i][15]
                    );
                dicWheelLevel.Add(grid[i][0], statusInfo);
                listWheelLevel.Add(statusInfo);
            }
        }
        if (FrameLevelCSV)
        {
            grid = CsvParser.Parse(FrameLevelCSV.text);
            for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
            {
                Status statusInfo = new Status(
                    grid[i][0],
                    grid[i][1],
                    float.Parse(grid[i][2]),
                    float.Parse(grid[i][3]),
                    float.Parse(grid[i][4]),
                    float.Parse(grid[i][5]),
                    float.Parse(grid[i][6]),
                    float.Parse(grid[i][7]),
                    float.Parse(grid[i][8]),
                    float.Parse(grid[i][9]),
                    float.Parse(grid[i][10]),
                    int.Parse(grid[i][11]),
                    int.Parse(grid[i][12]),
                    int.Parse(grid[i][13]),
                    grid[i][14],
                    grid[i][15]
                    );
                dicFrameLevel.Add(grid[i][0], statusInfo);
                listFrameLevel.Add(statusInfo);
            }
        }
    }

    public GameObject GetBodyObjHigh(string Id)
    {
        return dicBody[Id].prefabHigh;
    }
    public GameObject GetBodyObjLow(string Id)
    {
        return dicBody[Id].prefabLow;
    }
    public Texture GetBodyTextureHigh(string Id)
    {
        return dicSticker[Id].textureHigh;
    }
    public Texture GetBodyTextureLow(string Id)
    {
        return dicSticker[Id].textureLow;
    }
}
