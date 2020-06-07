using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveButton : MonoBehaviour {

    public AudioClip audioClick;
    public AudioSource audioSource;
    public GameObject[] panels;

    private void Start()
    {
        Button[] btns = FindObjectsOfType<Button>();
        foreach (var item in btns)
        {
            item.onClick.AddListener(OnClickButton);
        }
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
        panels[0].SetActive(true);
    }


    public void OnClickButton()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
