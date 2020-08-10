using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTexture_Main : MonoBehaviour {

    private MeshRenderer rend;
    private float offset = 0;
    public float speed = 1.5f;
    //private float randomSpriteSpeed;

    public enum OffsetAxis {
        X,Y
    }

    //public bool isRandom = false;
    public OffsetAxis axis;

	// Use this for initialization
	void Start () {
        rend = this.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimateOffset();
	}

    void AnimateOffset() {
        offset += Time.deltaTime * speed;

        if (axis == OffsetAxis.Y) rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        
        else rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
    
}
