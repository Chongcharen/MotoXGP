using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UniRx;
[BoltGlobalBehaviour(SceneName.LOBBY,SceneName.PLAYER_CUSTOM)]
public class LobbyClientCallback : GlobalEventListener
{
    public static Subject<Unit> OnJoinSession = new Subject<Unit>();
    public static Subject<Unit> OnBoltStart = new Subject<Unit>();
     public override void BoltStartBegin(){
        //register protocolToken 
        print(Depug.Log("BoltStartBegin RegisterTokenClass",Color.blue));
        BoltNetwork.RegisterTokenClass<ProtocolRoomProperty>();
        BoltNetwork.RegisterTokenClass<Bolt.Photon.PhotonRoomProperties>();
        BoltNetwork.RegisterTokenClass<ProtocolPlayerCustomize>();
        BoltNetwork.RegisterTokenClass<PlayerProfileToken>();
        BoltNetwork.RegisterTokenClass<PlayerEquipmentToken>();
        OnBoltStart.OnNext(default);
    }
    public override void SceneLoadLocalDone(string scene, IProtocolToken token){
        // if(BoltNetwork.IsServer){
        //     var entity = BoltNetwork.Instantiate(BoltPrefabs.BikePlayer,new Vector3(22,1.5f,0),Quaternion.Euler(0,90,0));
        //         entity.TakeControl();
        //     VirtualPlayerCamera.Instantiate();
        //     VirtualPlayerCamera.instance.LookupTarget(entity.transform);
        //     VirtualPlayerCamera.instance.FollowTarget(entity.transform);
        //     entity.GetComponent<AbikeChopSystem>().SetController(true);
        // }
    }
    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token){
        if(BoltNetwork.IsClient){

        }
    }
    public override void Connected(BoltConnection connection){
        print(Depug.Log("GameNetworkCallback Connected "+connection.ConnectionId,Color.green));
        // BikePlayerRegistry.CreateClientPlayer(connection);
        // if(BoltNetwork.IsClient){
        //     var entity = BoltNetwork.Instantiate(BoltPrefabs.RoomPlayerInfo);
        //     entity.AssignControl(connection);
        // }
    }
    public override void ControlOfEntityGained(BoltEntity entity){
        print(Depug.Log("ControlOfEntityGained "+entity,Color.green));
    }
    public override void EntityAttached(BoltEntity entity){
        print(Depug.Log("EntityAttached "+entity,Color.green));
            //2
            EntityAttachedEventHandler(entity);

            var photonPlayer = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
            Debug.Log("Entity controled "+entity.IsControlled);
            Debug.Log("Entity IsOwner "+entity.IsOwner);
            Debug.Log("Entity IsControllerOrOwner "+entity.IsControllerOrOwner);
            print(Depug.Log("Entity controled"+entity.IsControlled,Color.green));
            if (photonPlayer)
            {
                if (entity.IsControlled)
                {
                    photonPlayer.SetupPlayer();
                }
                else
                {
                    photonPlayer.SetupOtherPlayer();
                }
            }
    }
    private void EntityAttachedEventHandler(BoltEntity entity)
        {
            Depug.Log("EntityAttachedEventHandler "+entity,Color.green);
            OnJoinSession.OnNext(default);
            var lobbyPlayer = entity.gameObject.GetComponent<PlayerInRoom_Prefab>();
            PageManager.Instance.UI_Room.AddPlayer(lobbyPlayer);
        }
    
    public override void SessionCreatedOrUpdated(UdpKit.UdpSession session){
        print(Depug.Log("SessionCreatedOrUpdated "+session,Color.green));
    }
    public override void SessionListUpdated(UdpKit.Map<Guid, UdpKit.UdpSession> sessionList){
         print(Depug.Log("SessionListUpdated "+sessionList.Count,Color.green));
         foreach (var item in sessionList)
         {
            Debug.LogFormat("KEY {0} VALUE {1}",item.Key,item.Value);
            
         }
    }
    
    public override void Disconnected(BoltConnection connection){
        print(Depug.Log("On disconnected "+connection,Color.green));
    }
    
    
}
