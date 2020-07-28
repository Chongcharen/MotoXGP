using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
public class PhotonSmoothSyncMovement : Photon.Pun.MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public static Subject<Tuple<string,float>> OnPlayerMovement = new Subject<Tuple<string, float>>();
    public float SmoothingDelay = 5;
    public float fixPostionZ = 0;
    ExitGames.Client.Photon.Hashtable roomData;
    ExitGames.Client.Photon.Hashtable playerData;
    ExitGames.Client.Photon.Hashtable newData;

    private Vector3 m_Direction;
    private Vector3 m_StoredPosition;

    private float m_Distance;
    private float m_Angle;
    bool m_firstTake = false;

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
    public void Awake()
        {

            m_StoredPosition = transform.position;

            correctPlayerRot = Quaternion.identity;

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
            GetComponent<AbikeChopSystem>().SetController(photonView.IsMine);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            // if (stream.IsWriting)
            // {
            //     this.m_Direction = transform.position - this.m_StoredPosition;
            //     this.m_StoredPosition = transform.position;
            //     //We own this player: send the others our data
            //     stream.SendNext(new Vector3(transform.position.x,transform.position.y,fixPostionZ));
            //     stream.SendNext(this.m_Direction);
            //     stream.SendNext(transform.rotation);
                
            // }
            // else
            // {
            //     //Network player, receive data
            //     correctPlayerPos = (Vector3)stream.ReceiveNext();
            //     this.m_Direction = (Vector3)stream.ReceiveNext();
            //     correctPlayerRot = (Quaternion)stream.ReceiveNext();

            //      if (m_firstTake)
            //         {
            //             transform.position = this.correctPlayerPos;
            //             this.m_Distance = 0f;
            //         }
            //         else
            //         {
            //             float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            //             correctPlayerPos += this.m_Direction * lag;
            //             this.m_Distance = Vector3.Distance(transform.position, this.correctPlayerPos);
            //         }

            //     if (m_firstTake)
            //         {
            //             this.m_Angle = 0f;
            //             transform.rotation = correctPlayerRot;
            //         }
            //         else
            //         {
            //             this.m_Angle = Quaternion.Angle(transform.rotation, correctPlayerRot);
            //         }
            //     if (m_firstTake)
            //     {
            //         m_firstTake = false;
            //     }
            // }
        }

        

        void Update()
        {
            newData.Clear();
            // if (!photonView.IsMine)
            // {
            //     //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            //     // transform.position = Vector3.Lerp(transform.position, new Vector3((float)Math.Round(correctPlayerPos.x,1),(float)Math.Round(correctPlayerPos.y,1),(float)Math.Round(correctPlayerPos.z,1)), Time.deltaTime * this.SmoothingDelay);
            //     // transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * this.SmoothingDelay);

            //     transform.position = Vector3.MoveTowards(transform.position, new Vector3((float)Math.Round(correctPlayerPos.x,1),(float)Math.Round(correctPlayerPos.y,1),(float)Math.Round(correctPlayerPos.z,1)), this.m_Distance * (1.0f / PhotonNetwork.SerializationRate));
            //     transform.rotation = Quaternion.RotateTowards(transform.rotation,correctPlayerRot, this.m_Angle * (1.0f / PhotonNetwork.SerializationRate));


            //     //transform.position = correctPlayerPos;
            //     //transform.rotation = correctPlayerRot;
            //     //UI_PlayersDistance.Instance.SetDistance(photonView.Owner.UserId,correctPlayerPos.x);
            // }
            // else{
            //     // newData.Add(photonView.Owner.UserId,transform.position.x);
            //     // roomData[RoomPropertyKeys.PLAYER_DATA] = newData;
            //     // PhotonNetwork.CurrentRoom.SetCustomProperties(roomData);
            // }
            OnPlayerMovement.OnNext(Tuple.Create(photonView.Owner.UserId,transform.position.x));
        }

    void OnEnable(){
        m_firstTake = true;
    }
        
}
