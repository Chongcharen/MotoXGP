using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
public class PlayerInRoom_Prefab : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playername_text;
    public void SetData(string playername){
        playername_text.text = playername;
    }
}
