using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class BikePlayerRegistry
{

    public static List<BikePlayerObject> players = new List<BikePlayerObject>();
    public static Dictionary<uint,bool> playersReady = new Dictionary<uint, bool>();

    //public static Dictionary<>
    static BikePlayerObject CreatePlayer(BoltConnection connection,PlayerProfileToken token){
      BikePlayerObject player = new BikePlayerObject();
      player.profileToken = token;
      player.connection = (connection == null) ? BoltNetwork.Server : connection;
      player.index = players.Count;
      player.profileToken.playerBikeData.runningTrack = player.index;
      players.Add(player);
      

        //Server Only

        //Client
      if(player.connection != null){
        player.connection.UserData = player;
        playersReady.Add(connection.ConnectionId,false);
      }
      
      return player;
    }
   public static void RemovePlayer(BoltConnection connection){
        var playerFinder = players.Find(p => p.connection == connection);
        if(playersReady.ContainsKey(connection.ConnectionId))
            playersReady.Remove(connection.ConnectionId);
        if(playerFinder == null)return;
        players.Remove(playerFinder);
                
   }
    public static IEnumerable<BikePlayerObject> AllPlayers
    {
        get { return players; }
    }
    public static int GetIndexOf(BikePlayerObject player){
       return players.IndexOf(player);
    }

    // finds the server player by checking the
    // .IsServer property for every player object.
    public static BikePlayerObject ServerPlayer
    {
        get { return players.Find(player => player.IsServer); }
    }

    // utility function which creates a server player
    public static BikePlayerObject CreateServerPlayer(PlayerProfileToken token)
    {
        return CreatePlayer(null,token);
    }

    // utility that creates a client player object.
    public static BikePlayerObject CreateClientPlayer(BoltConnection connection,PlayerProfileToken token)
    {
        return CreatePlayer(connection,token);
    }

    // utility function which lets us pass in a
    // BoltConnection object (even a null) and have
    // it return the proper player object for it.
    public static BikePlayerObject GetBikePlayer(BoltConnection connection)
    {
        if (connection == null)
        {
            return ServerPlayer;
        }

        return (BikePlayerObject) connection.UserData;
    }
    public static BikePlayerObject GetBikePlayer(BoltEntity entity)
    {
        if (entity.Source == null)
        {
            return ServerPlayer;
        }

        return (BikePlayerObject) entity.Source.UserData;
    }
    public static void Dispose(){
       players.Clear();
    }

    #region playerReady
    public static void RegisterPlayerReady(BoltConnection connection){
        if(connection == null)return;
        playersReady[connection.ConnectionId] = true; 
    }
    public static bool AllPlayerReadys{
        get{
            return playersReady.All(p => p.Value == true);
        }
    }
    #endregion
}
