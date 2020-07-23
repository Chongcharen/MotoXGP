using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;
public class UI_Mapviewer : MonoBehaviour
{
    public static Subject<string> OnGenerateMap = new Subject<string>(); 
    [SerializeField]TMP_InputField input_mapName;
    [SerializeField]Button b_generateMap,b_quit;

    void Start(){
        b_generateMap.OnClickAsObservable().Subscribe(_=>{
            if(string.IsNullOrEmpty(input_mapName.text))return;
            OnGenerateMap.OnNext(input_mapName.text);
        });
        b_quit.OnClickAsObservable().Subscribe(_=>{
            SceneManager.LoadScene(SceneName.LOBBY);
        });
    }
}
