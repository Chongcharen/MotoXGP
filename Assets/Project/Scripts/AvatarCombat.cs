using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarCombat : MonoBehaviour {

    private PhotonView pv;
    private AvatarSetup avs;
    public Transform rayOrigin;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        avs = GetComponent<AvatarSetup>();
    }

    private void Update()
    {
        if (!pv.IsMine)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            pv.RPC("RPC_Shooting",RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, transform.TransformDirection(Vector3.forward), out hit, 1000))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Hit");

            if (hit.transform.tag == "Avatar")
            {
                //hit.transform.GetComponent<AvatarSetup>().playerHealth -= avs.playerDamage;
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("not Hit");
        }
    }
}
