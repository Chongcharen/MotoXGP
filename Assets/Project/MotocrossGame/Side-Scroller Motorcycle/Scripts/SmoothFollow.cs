/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/


using UnityEngine;
using System.Collections;
using UniRx;
using DG.Tweening;
using DG.Tweening.Core;
// Place the script in the Camera-Control group in the component menu
[AddComponentMenu("Camera-Control/Smooth Follow C#")]
public class SmoothFollow : MonoBehaviour {

	// The target we are following
	public Transform target;
	public bool playerCrash = false;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

    public float sideX = 0;

	public float standardSideX = 1.74f;

	public float boostChangeSideX = -2;

	bool isBoost = false;
	float elapsed = 0;

	float boostChangeCameraTime = 1f;


	[Header("CameraRotate")]
	public float cameraRotateTime = 1f;
	Quaternion baseCameraRotation;
	
	private void Start() {
		baseCameraRotation = transform.rotation;
		AbikeChopSystem.OnBoostTime.Subscribe(timeChange =>{
			elapsed = timeChange;
			isBoost = true;
			//sideX += boostChangeSideX;
			//DOTween.To(() => sideX,sideX, sideX+boostChangeSideX , 0.5f);
			//transform.DOMoveX(sideX + boostChangeSideX,0.3f).SetEase(Ease.OutElastic);
			// DOGetter<float> doGetter = new DOGetter<float>(() => sideX);
            // DOSetter <float> dOSetter = new DOSetter <float> ((x) => { x = sideX;});
        	// DOTween.To(doGetter, dOSetter,sideX+boostChangeSideX, 0.5f);
			StartCoroutine("closetime",timeChange);
		}).AddTo(this);
		AbikeChopSystem.OnPlayerCrash.Subscribe(isCrash =>{
			playerCrash = isCrash;
		}).AddTo(this);	
		MapManager.OnCameraRotate.Subscribe(rotation =>{
			transform.DORotateQuaternion(rotation,cameraRotateTime).SetAutoKill();
		});
		ZoneDetecter.OnCameraZoneExit.Subscribe(_=>{
			transform.DORotateQuaternion(baseCameraRotation,cameraRotateTime).SetAutoKill();
		}).AddTo(this);
	}
	IEnumerator closetime(float time){
		yield return new WaitForSeconds(time);
		isBoost = false;
	}
	void LateUpdate () {
		// Early out if we don't have a target
		if (!target)return;
		if(playerCrash)return;
		
		// Calculate the current rotation angles
		var wantedRotationAngle = target.eulerAngles.y;
		var wantedHeight = target.position.y + height;
			
		var currentRotationAngle = transform.eulerAngles.y;
		var currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
	
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
	
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        //transform.position = target.position;

		if(isBoost){
			sideX = Mathf.Lerp(sideX,-0.5f,0.1f);
		}else
		{
			sideX = Mathf.Lerp(sideX,standardSideX,0.2f);
		}
        transform.position = new Vector3(target.position.x + sideX, currentHeight, target.position.z);
        transform.position -= currentRotation * Vector3.forward * distance;




        // Set the height of the camera
        //transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        //transform.LookAt (target);
	}
}
