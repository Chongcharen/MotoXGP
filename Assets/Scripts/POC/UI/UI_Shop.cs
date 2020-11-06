using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
public class UI_Shop : MonoBehaviour
{
    [SerializeField]Button b_back;
    // Start is called before the first frame update
    void Start()
    {
        b_back.OnClickAsObservable().Subscribe(_=> {
            PageManager.Instance.OpenLobby();
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
