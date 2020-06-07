using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{

    [SerializeField] GameObject[] Body;

    [SerializeField] MeshRenderer wheelRear;
    [SerializeField] MeshRenderer wheelFront;
    [SerializeField] MeshRenderer engine;
    [SerializeField] MeshRenderer choke1;
    [SerializeField] MeshRenderer choke2;
    [SerializeField] MeshRenderer swingarm;

    [SerializeField] MeshRenderer helmet;
    [SerializeField] SkinnedMeshRenderer suit;
    [SerializeField] SkinnedMeshRenderer glove;
    [SerializeField] SkinnedMeshRenderer boot;

    [Header("Point Spawn")]
    [SerializeField] internal Transform bodyPoint;
    [SerializeField] internal Transform helmetPoint;

    private GameObject currentBody;
    private GameObject currentHelmet;
    private void Start()
    {

    }

    public void SetAllTexture(GameObject bodyObj, Texture bodyTexture, GameObject helmetObj, Texture helmetTexture, Texture suitTexture, Texture gloveTexture, Texture bootTexture)
    {
        SetBikeTexture(bodyTexture, bodyObj);
        SetHelmetTexture(helmetTexture,helmetObj);
        SetSuitTexture(suitTexture);
        SetGloveTexture(gloveTexture);
        SetBootTexture(bootTexture);
    }
    
    public void SetBikeTexture(Texture texture, GameObject bodyObj)
    {
        //Body[0].SetActive(false);
        //Body[1].SetActive(false);
        //Body[bodyPrefabIndex].SetActive(true);
        Destroy(currentBody);
        currentBody = Instantiate(bodyObj, bodyPoint.position , bodyPoint.rotation, bodyPoint);
        //var texture = ib.dicSticker[textureName].textureHigh;
        wheelRear.materials[1].SetTexture("_Albedo", texture);
        wheelFront.materials[1].SetTexture("_Albedo", texture);
        engine.materials[1].SetTexture("_Albedo", texture);
        choke1.materials[0].SetTexture("_Albedo", texture);
        choke2.materials[0].SetTexture("_Albedo", texture);
        swingarm.materials[1].SetTexture("_Albedo", texture);
        currentBody.GetComponent<MeshRenderer>().material.SetTexture("_Albedo", texture);
    }
    public void SetHelmetTexture(Texture texture, GameObject helmetObj)
    {
        Destroy(currentHelmet);
        currentHelmet = Instantiate(helmetObj, helmetPoint.position, helmetPoint.rotation, helmetPoint);
        currentHelmet.GetComponent<MeshRenderer>().material.SetTexture("_Albedo", texture);
    }

    public void SetSuitTexture(Texture texture)
    {
        suit.materials[0].SetTexture("_Albedo", texture);
    }


    public void SetGloveTexture(Texture texture)
    {
        glove.materials[0].SetTexture("_Albedo", texture);
    }

    public void SetBootTexture(Texture texture)
    {
        boot.materials[0].SetTexture("_Albedo", texture);
    }
}
