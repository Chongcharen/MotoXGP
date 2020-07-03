using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTexture_Sprite : MonoBehaviour {

    private MeshRenderer rend;
    [SerializeField] private float offset = 0;
    public int spriteValue;
    [SerializeField] private int spriteOrder = 0;
    public float tileSpeed = 1.5f;
    public float delaySpeed = 0.25f;

    private bool isPlay = false;
    public bool isRandom = false;

    public enum OffsetAxis {X, Y}

    public OffsetAxis axis;

    // Use this for initialization
    void Start() {
        rend = this.GetComponent<MeshRenderer>();
        StartCoroutine(DelayAnimate(delaySpeed));
    }


    IEnumerator DelayAnimate(float dSpeed) {
        if (!isPlay) {
            isPlay = true;
            AnimateOffset();
            StartCoroutine(DelayAnimate(dSpeed));
        } else {
            yield return new WaitForSeconds(dSpeed);
            AnimateOffset();
            StartCoroutine(DelayAnimate(dSpeed));
        }
        
    }

    void AnimateOffset() {

        if (isRandom) offset = tileSpeed * (Random.RandomRange(1, spriteValue + 1));
        else {
 
            if (spriteOrder < spriteValue) spriteOrder++;
            else spriteOrder = 1;

            offset = tileSpeed * spriteOrder;
            

        }
        //offset *= speed;
        //offset++;

        if (axis == OffsetAxis.X) {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        else {
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }

    }
}
