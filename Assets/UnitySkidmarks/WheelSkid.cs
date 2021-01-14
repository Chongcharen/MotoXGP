using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example skid script. Put this on a WheelCollider.
// Copyright 2017 Nition, BSD licence (see LICENCE file). http://nition.co
[RequireComponent(typeof(WheelCollider))]
public class WheelSkid : MonoBehaviour {

	// INSPECTOR SETTINGS
	public Rigidbody rb;
	public Skidmarks skidmarksController;

	// END INSPECTOR SETTINGS

	WheelCollider wheelCollider;
	WheelHit wheelHitInfo;

	public GameObject animationSkid;
	public ParticleSystem ps;

	public float SKID_FX_SPEED = 0.00001f; // Min side slip speed in m/s to start showing a skid
	public float MAX_SKID_INTENSITY = 20.0f; // m/s where skid opacity is at full intensity
	public float WHEEL_SLIP_MULTIPLIER = 10.0f; // For wheelspin. Adjust how much skids show
	public int lastSkid = -1; // Array index for the skidmarks controller. Index of last skidmark piece this wheel used
	float lastFixedUpdateTime;

	// #### UNITY INTERNAL METHODS ####

	protected void Awake() {
		wheelCollider = GetComponent<WheelCollider>();
		lastFixedUpdateTime = Time.time;
	}

	protected void FixedUpdate() {
		lastFixedUpdateTime = Time.time;
	}

	protected void LateUpdate() {
		if (wheelCollider.GetGroundHit(out wheelHitInfo))
		{
			// Check sideways speed

			// Gives velocity with +z being the car's forward axis
			Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
			float skidTotal = Mathf.Abs(localVelocity.x);

			// Check wheel spin as well

			float wheelAngularVelocity = wheelCollider.radius * ((2 * Mathf.PI * wheelCollider.rpm) / 60);
			float carForwardVel = Vector3.Dot(rb.velocity, transform.forward);
			float wheelSpin = Mathf.Abs(carForwardVel - wheelAngularVelocity) * WHEEL_SLIP_MULTIPLIER;

			// NOTE: This extra line should not be needed and you can take it out if you have decent wheel physics
			// The built-in Unity demo car is actually skidding its wheels the ENTIRE time you're accelerating,
			// so this fades out the wheelspin-based skid as speed increases to make it look almost OK
			wheelSpin = Mathf.Max(0, wheelSpin * (0 - Mathf.Abs(carForwardVel)));

			skidTotal += wheelSpin;
			// Skid if we should
			if (skidTotal >= SKID_FX_SPEED) {
				float intensity = Mathf.Clamp01(skidTotal / MAX_SKID_INTENSITY);
				//Debug.Log("intensity "+intensity);
				// Account for further movement since the last FixedUpdate
				Vector3 skidPoint = wheelHitInfo.point + (rb.velocity * (Time.time - lastFixedUpdateTime));
				//Debug.Log("skidpoint "+skidPoint);
				animationSkid.transform.position = skidPoint;
				//animationSkid.gameObject.SetActive(true);
				//lastSkid = skidmarksController.AddSkidMark(skidPoint, wheelHitInfo.normal, intensity, lastSkid);
			}
			else {
				lastSkid = -1;
			}
			//animationSkid.gameObject.SetActive(skidTotal >= SKID_FX_SPEED);
			// if(!ps.isPlaying)
			// 	ps.Play();
			// var em = ps.emission;
			// em.enabled = (skidTotal >= SKID_FX_SPEED);

			if(skidTotal >= SKID_FX_SPEED)
				ps.Play();
			else
				ps.Stop();
		}
		else {
			lastSkid = -1;
			//animationSkid.gameObject.SetActive(false);
			ps.Stop();
		}
		
	}

	// #### PUBLIC METHODS ####

	// #### PROTECTED/PRIVATE METHODS ####


}
