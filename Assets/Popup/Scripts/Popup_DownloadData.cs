using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using TMPro;

public class Popup_DownloadData : MonoBehaviour
{
    static AsyncOperationHandle handle;
    [SerializeField]Slider slider;
    [SerializeField]TextMeshProUGUI detail_txt;
    public static Popup_DownloadData Launch(AsyncOperationHandle _handle){
        handle = _handle;
        var prefab = Resources.Load<Popup_DownloadData>("Popup_DownloadData");
        return Instantiate<Popup_DownloadData>(prefab);
    }
    private void Start()
    {
        StartCoroutine(CheckDownloadContent());
    }
    IEnumerator CheckDownloadContent(){
        if(handle.OperationException != null){
            Debug.Log("stacktrace "+handle.OperationException.StackTrace);
            Debug.Log("Data "+handle.OperationException.Data);
        }
        while (!handle.IsDone)
        {
            Debug.Log("progress "+handle.PercentComplete);
            slider.value = handle.PercentComplete;
            yield return null;
        }
        slider.value = handle.PercentComplete;
        Debug.Log("task "+handle.Task);
        Debug.Log(handle.DebugName);
        Debug.Log("result "+handle.Result);
        //yield return new WaitForSeconds(1);
        Destroy(this.gameObject);

    }
    public void ClosePopup(){
        Destroy(this.gameObject);
    }
}
