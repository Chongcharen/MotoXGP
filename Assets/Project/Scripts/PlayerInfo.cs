using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInfo : MonoBehaviour {

    public static PlayerInfo pi;

    public int mySelectedCharacter;

    public GameObject[] allCharacters;


    [Header("Player High")]
    public Dictionary<string, Texture> dicStickerH = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicSuitH = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicHelmetH = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicGloveH = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicBootH = new Dictionary<string, Texture>();

    [Header("Player Low")]
    public Dictionary<string, Texture> dicStickerL = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicSuitL = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicHelmetL = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicGloveL = new Dictionary<string, Texture>();
    public Dictionary<string, Texture> dicBootL = new Dictionary<string, Texture>();

    public GameObject[] bodyFramesObj;
    public GameObject ragdollPrefab;

    private void OnEnable()
    {
        if (pi != null)
        {
            Destroy(gameObject);
            return;
        }
        pi = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter);
        }
        SetupTextureHigh();
        SetupTextureLow();
    }

    private void SetupTextureHigh()
    {
        dicStickerH = Resources.LoadAll<Texture>("Textures/Bike/Bike01/High/").ToDictionary(v => v.name, v => v);
        dicSuitH = Resources.LoadAll<Texture>("Textures/Character/High/Suit/").ToDictionary(v => v.name, v => v);
        dicHelmetH = Resources.LoadAll<Texture>("Textures/Character/High/Helmet/").ToDictionary(v => v.name, v => v);
        dicGloveH = Resources.LoadAll<Texture>("Textures/Character/High/Gloves/").ToDictionary(v => v.name, v => v);
        dicBootH = Resources.LoadAll<Texture>("Textures/Character/High/Boots/").ToDictionary(v => v.name, v => v);
    }

    private void SetupTextureLow()
    {
        dicStickerL = Resources.LoadAll<Texture>("Textures/Bike/Bike01/Low/").ToDictionary(v => v.name, v => v);
        dicSuitL = Resources.LoadAll<Texture>("Textures/Character/Low/Suit/").ToDictionary(v => v.name, v => v);
        dicHelmetL = Resources.LoadAll<Texture>("Textures/Character/Low/Helmet/").ToDictionary(v => v.name, v => v);
        dicGloveL = Resources.LoadAll<Texture>("Textures/Character/Low/Gloves/").ToDictionary(v => v.name, v => v);
        dicBootL = Resources.LoadAll<Texture>("Textures/Character/Low/Boots/").ToDictionary(v => v.name, v => v);
    }

    public void SelectPlayer(int indexChar)
    {
        mySelectedCharacter = indexChar;
    }
}
