using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class LayerKeys{
    public const string ROAD = "Road";
    public const string MOTORCYCLE = "Motorcycle";
    public const string RAGDOLL = "Ragdoll";
    public const string BODY = "Body";
}
public class TagKeys{
    public const string GROUND = "Ground";
    public const string ROAD = "Road";
    public const string Zone = "Zone";
    public const string DEADZONE = "DeadZone";
    public const string SAFEZONE = "SafeZone";
    public const string ENDPOINT = "EndPoint";
    
}
public class RoomPropertyKeys{
    public const string PLAYER_INDEX = "player_index";
    public const string PLAYER_DATA = "player_data";
    public const string ROOM_DATA = "room_data";
    public const string PLAYER_RESULT = "player_result";
    public const string GAME_START = "game_start";
}

//playerdata in photonplayer
public static class PlayerPropertiesKey{
    public const string PLAYER_COLOR = "player_color";
    public const string PLAYFAB_PROFILE = "playfab_profile";
}

public static class PlayerIndexProfileKeys{
    public const string COLOR = "player_color";
    public const string NICKNAME = "nickname";
    public const string INDEX = "index";
    public const string USERID = "userid";
} 

public static class SceneName{
    public const string START = "Initial";
    public const string GAMEPLAY = "NetworkGame";
    public const string LOBBY = "TestNetwork";
}
