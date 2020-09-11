using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;


public class BikeCustomize : MonoBehaviour{
    GameObject body;
    public GameObject[] bikeBodys;
    [Range(1,9)]
    public int bodyTextureCount = 9;

    // index 
    public int bikeId;
    public int bikeTextureId;

    public bool firstTake;

    public string texturePath = "Bike/Texture";
    public void RandomBike(){
        bikeId = Mathf.FloorToInt(Random.Range(1,bikeBodys.Length));
        bikeTextureId = Mathf.FloorToInt(Random.Range(1,bodyTextureCount));
        ChangeBody();
    }
    void ChangeBody(){
        Debug.Log("--------------> changebody");
        print(Depug.Log("ChangeBody "+bikeId+" texture "+bikeTextureId,Color.yellow));
        bikeBodys.FirstOrDefault(b => b.activeSelf == true).SetActive(false);
        body = bikeBodys[bikeId].gameObject;
        body.gameObject.SetActive(true);
        var bodyIndex = bikeId < 10 ? "0"+(bikeId+1).ToString() : (bikeId+1).ToString();
        var textureIndex = bikeTextureId < 10 ? "0"+bikeTextureId.ToString() : bikeTextureId.ToString();
        var textureName = "B200CC_Body"+bodyIndex+"_"+textureIndex;
        var texture = Resources.Load<Texture>(Path.Combine(texturePath,textureName));
        Debug.Assert(texture != null,"Texture name "+textureName + "not found");
        body.GetComponent<Renderer>().material.mainTexture = texture;
    }

    public void SetUpBike(PlayerCustomize playerCustomize){
        print(Depug.Log("SetupBike "+playerCustomize,Color.yellow));
        bikeId = playerCustomize.BikeId;
        bikeTextureId = playerCustomize.BikeTextureId;
        ChangeBody();
    }

}
