using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public GameObject motorcyclePrefab;
	public string tagToCheck = "Player";
	public Color activatedColor = Color.green;
	public Renderer objectToChangeColor;
	
	private AudioSource audioSource;

	public static Transform lastPoint;
	private static GameObject moto;

	private static int scoreAtLastPoint = 0;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		moto = motorcyclePrefab;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(lastPoint != transform && col.tag == tagToCheck)
		{
			lastPoint = transform;
			objectToChangeColor.material.color = activatedColor;
			audioSource.Play ();
			scoreAtLastPoint = Motorcycle_Controller.score;
		}
	}

	public static void Reset()
	{
		Instantiate (moto, lastPoint.position, Quaternion.identity);
		Motorcycle_Controller.score = scoreAtLastPoint;
	}
}
