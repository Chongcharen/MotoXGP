using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UniRx;
using System;

[RequireComponent(typeof(PhotonView))]
public class PhotonCustomTransformView : Photon.Pun.MonoBehaviourPun, IPunObservable
{
    private float m_Distance;
        private float m_Angle;

        private PhotonView m_PhotonView;

        private Vector3 m_Direction;
        private Vector3 m_NetworkPosition;
        private Vector3 m_StoredPosition;

        private Quaternion m_NetworkRotation;

        public bool m_SynchronizePosition = true;
        public bool m_SynchronizeRotation = true;
        public bool m_SynchronizeScale = false;

        bool m_firstTake = false;

        //
        public static Subject<Tuple<string,float>> OnPlayerMovement = new Subject<Tuple<string, float>>();
        public float SmoothingDelay = 5;
        public float fixPositionZ = 0;
        ExitGames.Client.Photon.Hashtable roomData;
        ExitGames.Client.Photon.Hashtable playerData;

    public void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();

        m_StoredPosition = transform.position;
        m_NetworkPosition = Vector3.zero;
        m_NetworkRotation = Quaternion.identity;

        //
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
    }
    void Start(){
             GameplayManager.OnGameEnd.Subscribe(_=>{
                this.enabled = false;
            }).AddTo(this);
            GetComponent<AbikeChopSystem>().SetController(photonView.IsMine);
    }
    void OnEnable()
        {
            m_firstTake = true;
        }
    public void Update()
        {
            if (!this.m_PhotonView.IsMine)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_NetworkPosition, m_Distance *(1.0f / PhotonNetwork.SerializationRate));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, this.m_NetworkRotation, this.m_Angle * (1.0f / PhotonNetwork.SerializationRate));
            }
             OnPlayerMovement.OnNext(Tuple.Create(photonView.Owner.UserId,transform.position.x));
        }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                if (this.m_SynchronizePosition)
                {
                    this.m_Direction = transform.position - this.m_StoredPosition;
                    this.m_StoredPosition = transform.position;
                    stream.SendNext(new Vector3(transform.position.x,transform.position.y,fixPositionZ));
                    stream.SendNext(this.m_Direction);
                }

                if (this.m_SynchronizeRotation)
                {
                    stream.SendNext(transform.rotation);
                }

                if (this.m_SynchronizeScale)
                {
                    stream.SendNext(transform.localScale);
                }
            }
            else
            {


                if (this.m_SynchronizePosition)
                {
                    this.m_NetworkPosition = (Vector3)stream.ReceiveNext();
                    this.m_Direction = (Vector3)stream.ReceiveNext();

                    if (m_firstTake)
                    {
                        transform.position = this.m_NetworkPosition;
                        this.m_Distance = 0f;
                    }
                    else
                    {
                        
                        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                        this.m_NetworkPosition += this.m_Direction * lag;
                        this.m_Distance = Vector3.Distance(transform.position, this.m_NetworkPosition);
                        if(this.m_Distance > 30){
                            transform.position = m_NetworkPosition;
                        }
                    }
                    // float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                    // this.m_NetworkPosition += this.m_Direction * lag;
                    // this.m_Distance = Vector3.Distance(transform.position, this.m_NetworkPosition);
                }

                if (this.m_SynchronizeRotation)
                {
                    this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();

                    if (m_firstTake)
                    {
                        this.m_Angle = 0f;
                        transform.rotation = this.m_NetworkRotation;
                    }
                    else
                    {
                        this.m_Angle = Quaternion.Angle(transform.rotation, this.m_NetworkRotation);
                    }
                }

                if (this.m_SynchronizeScale)
                {
                    transform.localScale = (Vector3)stream.ReceiveNext();
                }

                if (m_firstTake)
                {
                    m_firstTake = false;
                }
            }
        }
}
