using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
public class UI_PlayerCustom : MonoBehaviour
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_back;
    [SerializeField]CinemachineVirtualCamera virtualCamera;

    void Awake(){
        SceneFlow.Instance.StartScene();
    }
    void Start(){
        // AssetsLoader.Load(new List<string>{AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.HELMET,
        //                                     AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.GLOVE,
        //                                     AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.SUIT,
        //                                     AddressableKeys.ATLAS_EQUIPMENT+EquipmentKeys.BOOT

        //                 });
        b_back.OnClickAsObservable().Subscribe(_=>{
            SceneDownloadAsset.LoadScene(SceneName.LOBBY);
        }).AddTo(this);
    }
}
