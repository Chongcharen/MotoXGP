using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class MapData
{
    public string mapName;
    public string Id;
    public string theme;
    public Sprite mapImgSprite;
    public GameObject prefab;
    public int level;
    public string conditionId;
    public string conditionNeedValue;

    public MapData(string mapName, string Id, string theme, Sprite mapImgTexture, GameObject prefab, int level, string conditionId, string conditionNeedValue)
    {
        this.mapName = mapName;
        this.Id = Id;
        this.theme = theme;
        this.mapImgSprite = mapImgTexture;
        this.prefab = prefab;
        this.level = level;
        this.conditionId = conditionId;
        this.conditionNeedValue = conditionNeedValue;
    }
}
public class InfoMap : MonoBehaviour
{
    //public LobbyMapPanel _LobbyMapPanel;

    public static InfoMap Instance { get; protected set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadMapInfo();
    }

    public TextAsset MapCSV;

    public int startReadCsvAtRow = 3;
    [Header("Map Info")]
    public Dictionary<string, MapData> dicMapInfo = new Dictionary<string, MapData>();
    public List<MapData> listMapInfo = new List<MapData>();
    

    public void Init()
    {
        //LoadMapInfo();
    }

    private void LoadMapInfo()
    {
        Dictionary<string, Sprite> dicSprite = Resources.LoadAll<Sprite>("Icons/Maps").ToDictionary(v => v.name, v => v);

        Dictionary<string, GameObject> dicPrefab = new Dictionary<string, GameObject>();

        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Maps");
        foreach (var prefab in prefabs)
        {
            dicPrefab.Add(prefab.name, prefab);
        }

        string[][] grid = CsvParser.Parse(MapCSV.text);
        for (int i = startReadCsvAtRow - 1; i < grid.Length; i++)
        {
            MapData MapInfo = new MapData(
                grid[i][1],
                grid[i][0],
                grid[i][2],
                dicSprite[grid[i][3]],
                dicPrefab[grid[i][4]],
                int.Parse(grid[i][5]),
                grid[i][6],
                grid[i][7]
                );
            dicMapInfo.Add(grid[i][0], MapInfo);
            listMapInfo.Add(MapInfo);
            //LobbyGameManager.Instance.SetDicMapInfo(MapInfo);
        }

        //_LobbyMapPanel.SetMap();
       // Debug.LogError(dicMapInfo.Count);
    }
}
