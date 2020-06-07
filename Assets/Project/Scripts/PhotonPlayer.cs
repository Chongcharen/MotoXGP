using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System.IO;

public class PhotonPlayer : MonoBehaviour {

    internal PhotonView pv;
    public GameObject avatar;
    internal int pointIndex;

    public void Init()
    {
        pv = GetComponent<PhotonView>();

        var gs = GameSetup.gs;
        if (pv.IsMine)
        {
            var point = GameSetup.gs.spawnPoints[(int.Parse(pv.ViewID.ToString().Substring(0, 1)))];
            //var point = GameSetup.Instance.spawnPoints[pointIndex];
            //avatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Motorcycle"), point.position, point.rotation, 0);

            avatar = PhotonNetwork.Instantiate("Prefabs/Gameplay/Motorcycle", point.position, Quaternion.identity, 0);
            var mcc = avatar.GetComponent<Motorcycle_Controller>();
            if (mcc)
            {
                mcc.isLocalPlayer = true;
            }
        }
    }
}
