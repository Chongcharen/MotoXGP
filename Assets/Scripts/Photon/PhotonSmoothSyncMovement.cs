using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
public class PhotonSmoothSyncMovement : Photon.Pun.MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public float SmoothingDelay = 5;
    public float fixPostionZ = 0;
    ExitGames.Client.Photon.Hashtable roomData;
    ExitGames.Client.Photon.Hashtable playerData;
    ExitGames.Client.Photon.Hashtable newData;
    
        public void Awake()
        {
            bool observed = false;
            foreach (Component observedComponent in this.photonView.ObservedComponents)
            {
                if (observedComponent == this)
                {
                    observed = true;
                    break;
                }
            }
            if (!observed)
            {
                Debug.LogWarning(this + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used.");
            }
            if(!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(RoomPropertyKeys.PLAYER_DATA)){
                Debug.LogError(" Can not found playerdata");
            }
            if(!photonView.IsMine){
                GetComponent<AbikeChopSystem>().RemoveCrashDetecter();
            }
            roomData = PhotonNetwork.CurrentRoom.CustomProperties as ExitGames.Client.Photon.Hashtable;
            playerData = PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.PLAYER_DATA] as ExitGames.Client.Photon.Hashtable;
            newData = new ExitGames.Client.Photon.Hashtable();
            
           
        }
        void Start(){
             GameplayManager.OnGameEnd.Subscribe(_=>{
                this.enabled = false;
            }).AddTo(this);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //We own this player: send the others our data
                stream.SendNext(new Vector3(transform.position.x,transform.position.y,fixPostionZ));
                stream.SendNext(transform.rotation);
                
            }
            else
            {
                //Network player, receive data
                correctPlayerPos = (Vector3)stream.ReceiveNext();
                correctPlayerRot = (Quaternion)stream.ReceiveNext();
            }
        }

        private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
        private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

        public void Update()
        {
            newData.Clear();
            if (!photonView.IsMine)
            {
                //Update remote player (smooth this, this looks good, at the cost of some accuracy)
                
                transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
                transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
            }
            else{
                newData.Add(photonView.Owner.UserId,transform.position.x);
                roomData[RoomPropertyKeys.PLAYER_DATA] = newData;
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomData);
            }
        }
        
}
