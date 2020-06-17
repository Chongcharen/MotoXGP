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
    
}
public class RoomPropertyKeys{
    public const string PLAYER_INDEX = "player_index";
    public const string PLAYER_DATA = "player_data";
    public const string ROOM_DATA = "room_data";
    public const string GAME_START = "game_start";
}
public static class SceneName{
    public const string GAMEPLAY = "NetworkGame";
}
