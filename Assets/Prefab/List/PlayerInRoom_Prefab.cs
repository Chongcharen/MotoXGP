using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
public class PlayerInRoom_Prefab : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playername_text;
    [SerializeField]Image bg_image;
    public void SetData(string playername,Color color){
        playername_text.text = playername;
        bg_image.color = color;
    }
}
