using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour {

    [SerializeField] internal MeshRenderer choke;
    [SerializeField] internal MeshRenderer swingarm;
    [SerializeField] internal MeshRenderer engine;
    [SerializeField] internal MeshRenderer hand;
    /*
    public void SetSolid(MeshRenderer mesh, Color color)
    {

        var infoBike = InfoBike.Instance;

        var mat = mesh.materials[mesh.materials.Length - 1];
        mat.shader = Shader.Find("Standard");
        //mat = infoBike.solidMaterial;
        mat.color = color;
    }
    */
    public void SetStricker(MeshRenderer mesh,Texture texture)
    {
        
        //var infoBike = InfoBike.Instance;
        var mat = mesh.materials[mesh.materials.Length - 1];

        mat.shader = Shader.Find("Unlit/Texture");
        mat.mainTexture = texture;
    }
}
