using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class UI_Gameplay : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI time_txt;

    private void Update() {
        var timeSpan = System.TimeSpan.FromSeconds(Time.time);
        time_txt.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
}
