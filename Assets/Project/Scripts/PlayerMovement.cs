using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerMovement : MonoBehaviour {

    private PhotonView pv;
    private CharacterController pcc;
    public float moveSpeed;
    public float rotSpeed;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        //pcc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            RotPlayer();
        }
        MovementPlayer();
    }

    private void MovementPlayer()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    pcc.Move(transform.forward * Time.deltaTime * moveSpeed);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    pcc.Move(-transform.forward * Time.deltaTime * moveSpeed);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    pcc.Move(-transform.right * Time.deltaTime * moveSpeed);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    pcc.Move(transform.right * Time.deltaTime * moveSpeed);
        //}
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(transform.right * Time.deltaTime * moveSpeed);
        }
    }

    private void RotPlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
