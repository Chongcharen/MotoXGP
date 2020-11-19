using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UI_GameRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame updat
     public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged){
        foreach (var item in propertiesThatChanged)
        {
            Debug.Log(string.Format("Change Key : {0} , Value {1}",item.Key,item.Value));
            if(item.Key.ToString() == RoomPropertyKeys.PLAYER_DATA){
                ExitGames.Client.Photon.Hashtable playerDataHash = item.Value as ExitGames.Client.Photon.Hashtable;
                Debug.Log("playerdata hash count "+playerDataHash.Count);
                foreach (var playerData in playerDataHash)
                {
                    Debug.Log(string.Format("Playerdata key : {0} , value : {1}",playerData.Key,playerData.Value));
                }
            }
            if(item.Key.ToString() == RoomPropertyKeys.PLAYER_INDEX){
                ExitGames.Client.Photon.Hashtable playerIndex_hash = item.Value as ExitGames.Client.Photon.Hashtable;
                foreach (var index in playerIndex_hash)
                {
                     Debug.Log(string.Format("playerIndex_hash Key : {0} , Value {1}",item.Key,item.Value));
                }
            }
        }
        var playerindexHashtable = PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.PLAYER_INDEX] as Hashtable;
        if(playerindexHashtable != null){
             Debug.Log("playerindexHashtable total "+playerindexHashtable);
        }
        if(propertiesThatChanged.ContainsKey(RoomPropertyKeys.GAME_START)){
            if((bool)propertiesThatChanged[RoomPropertyKeys.GAME_START])
                PhotonNetwork.LoadLevel(SceneName.GAMEPLAY);
        }
    }
}
