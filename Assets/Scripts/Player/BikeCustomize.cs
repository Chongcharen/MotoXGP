using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class BikeCustomize : Photon.Pun.MonoBehaviourPun, IPunObservable{
    GameObject body;
    public GameObject[] bikeBodys;
    [Range(1,9)]
    public int bodyTextureCount = 9;
    public int randomBodyIndex;
    public int randomTextureBodyIndex;

    public bool firstTake;

    public string texturePath = "Bike/Texture";
    public void RandomBike(){
        randomBodyIndex = Mathf.FloorToInt(Random.Range(1,bikeBodys.Length));
        randomTextureBodyIndex = Mathf.FloorToInt(Random.Range(1,bodyTextureCount));
        ChangeBody();
    }
    void ChangeBody(){
        Debug.Log("--------------> changebody");
        bikeBodys.FirstOrDefault(b => b.activeSelf == true).SetActive(false);
        body = bikeBodys[randomBodyIndex].gameObject;
        body.gameObject.SetActive(true);
        var bodyIndex = randomBodyIndex < 10 ? "0"+(randomBodyIndex+1).ToString() : (randomBodyIndex+1).ToString();
        var textureIndex = randomTextureBodyIndex < 10 ? "0"+randomTextureBodyIndex.ToString() : randomTextureBodyIndex.ToString();
        var textureName = "B200CC_Body"+bodyIndex+"_"+textureIndex;
        var texture = Resources.Load<Texture>(Path.Combine(texturePath,textureName));
        Debug.Assert(texture != null,"Texture name "+textureName + "not found");
        body.GetComponent<Renderer>().material.mainTexture = texture;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting){
            stream.SendNext(randomBodyIndex);
            stream.SendNext(randomTextureBodyIndex);
        }else{  
            if(!firstTake)return;
            randomBodyIndex = (int)stream.ReceiveNext();
            randomTextureBodyIndex = (int)stream.ReceiveNext();
            firstTake = false;
            ChangeBody();
        }
    }
    void OnEnable()
        {
            firstTake = true;
        }
}
