using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_PlayerCustom : MonoBehaviour
{
    [SerializeField]GameObject root;
    [SerializeField]Button b_back;

    void Awake(){
        SceneFlow.Instance.StartScene();
    }
    void Start(){
        b_back.OnClickAsObservable().Subscribe(_=>{
            SceneManager.LoadScene(SceneName.LOBBY);
        }).AddTo(this);
    }
}
