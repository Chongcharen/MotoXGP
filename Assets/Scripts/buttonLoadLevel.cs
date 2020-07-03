using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class buttonLoadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
