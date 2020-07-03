using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Sprite[] imgBg;
    public Text loadingText;
    public Text mapName;
    public Slider sliderBar;
    public Image bgLoading;
    public GameObject cam;

    public Image[] progressLoading;
    public float [] percenProgress;

    public float delayTimeLoading = 0f;
    
    public void LoadScene(AsyncOperation async)
    {
        if(this.gameObject.name == "OffLineLoad")
        {
            //StartCoroutine(LoadOfflineGame(async));
            SceneManager.LoadScene("Lobby");
        }

        else
        {
            StartCoroutine(LoadNewScene(async));
        }
    }

    private IEnumerator LoadNewScene(AsyncOperation async)
    {
        yield return new WaitForSeconds(delayTimeLoading);
        if (progressLoading.Length > 0)
            while (!async.isDone || progressLoading[0].fillAmount < 1)
            {
                //float progressValue = Mathf.Clamp01(async.progress / 1f);

                foreach (var progress in progressLoading)
                {
                    progress.fillAmount += Time.deltaTime;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }


        yield return new WaitForSeconds(delayTimeLoading);
        //MotoUiMain.Instance.Unload();
    }

    private IEnumerator LoadOfflineGame(AsyncOperation async)
    {
        yield return new WaitForSeconds(delayTimeLoading);

        yield return new WaitForSeconds(delayTimeLoading);
        //MotoUiLobby.Instance.CloseAllPanal();
        //MotoUiMain.Instance.Unload();
    }
    

}