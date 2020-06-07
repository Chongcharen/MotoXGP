using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_MotorEngine : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TextMeshProUGUI wanted_rpm_txt,rpm_txt,gear_txt,accelerator_txt;
    [SerializeField]POC_Controller_Motor motorController;
    [SerializeField]MotorEngine motorEngine;
    // Update is called once per frame
    void Update()
    {
        wanted_rpm_txt.text = ""+(int)motorEngine.wantedRPM;
        accelerator_txt.text = ""+motorController.accelerator;
        gear_txt.text = ""+motorEngine.currentGear;
    }
}
