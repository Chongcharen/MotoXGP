using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
	float maxSpeed = 10f;
    Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput,1);
        Vector3 acceleration = new Vector3(playerInput.x,0,playerInput.y) * maxSpeed;
        velocity += acceleration*Time.deltaTime;
        Vector3 displacement = velocity * Time.deltaTime;

        transform.localPosition += displacement;
    }
}
