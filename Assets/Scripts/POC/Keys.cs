﻿using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{

}
public class StatusKeys{
    public const float NORNAL = 0.5f;
    public const float SLOW = 1; 
    public const float FAST = 0;
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
    public const string PILLAR = "Pillar";
    public const string Zone = "Zone";
    public const string ZonePoint = "ZonePoint";
    public const string DEADZONE = "DeadZone";
    public const string SAFEZONE = "SafeZone";
    public const string CAMERAZONE = "CameraZone";
    public const string CAMERAPOINT = "CameraPoint";
    public const string ENDPOINT = "EndPoint";
    public const string MONOLITH = "Monolith";
    public const string QUICKSAND = "QuickSand";
    
}
public static class RoomOptionKey{
    public const string PASSWORD = "password";
    public const string HOST_ID = "hostID";
    public const string MAP_THEME = "map_theme";
    public const string MAP_STAGE = "map_stage";

    public const string MAP_LEVEL = "map_level";

    public const string PLAYERS_RANK = "players_rank";
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
public static class GameConfigKeys{
    public const string EQUIPMENT = "equipment";
}

public static class SceneName{
    public const string START = "Initial";
    public const string GAMEPLAY = "NetworkGame";
    public const string LOBBY = "TestNetwork";
    public const string TEST_GENMAP = "MapGenerater";
    public const string MAP_VIEWER = "MapViewer";
    public const string PLAYER_CUSTOM = "PlayerCustom";
}
public static class MapKeys{
    public const string Map_Name = "Map_Name";
    public const string Map_Start = "Map_Start";
    public const string Object_Terrain = "Object_Terrain";
    public const string Object_Name = "Object_Name";
    public const string Position_X = "Position_X*0.01";
    public const string Position_Y = "Position_Y*0.01";  
    public const string Position_Z = "Position_Z*-1";      
}
public static class FilePath{
    public const string GAME_LEVEL_DATA = "GameLevel/GameLevelData.txt";
    public const string GAME_EQUIPMENT_DATA = "Equipment/EquipmentData.txt";
}
public static class SpreadSheetKeys{
    public const string GAME_CONFIG = "14azNtExST2X1bgVgnHPl1Mfs05FW-8C2JA_egyunDig";
    public const string EQUIPMENT = "1sxH20vXha8_DsUIoTR9m4-NchUrVatE_92oyVIzotAA";


    public const string GID_HELMET = "0";
    public const string GID_SUIT = "1278015005";
    public const string GID_GLOVE = "1697254392";
    public const string GID_BOOT = "432887015";
    public const string GID_BIKE_BODY_1 = "561684748";
    public const string GID_BIKE_BODY_2 = "1897718935";
    public const string GID_BIKE_BODY_3 = "348133245";

    public const string GID_BIKE_BODY_4 = "1887582513";

    public const string GID_BIKE_BODY_5 = "497383251";

    public const string GID_BIKE_BODY_6 = "1441191374";

    public const string GID_BIKE_BODY_7 = "1622833420";



    public const string HEADER_NAME = "Name";
    public const string HEADER_DATA = "Data";
    public const string HEADER_VALE = "Vale";
}
public static class EquipmentKeys{
    public const string HELMET = "helmet";
    public const string SUIT = "suit";
    public const string GLOVE = "glove";
    public const string BOOT = "boot";
    public const string HEAD = "head";
    public const string BIKE_BODY_1 = "bike_body_1";
    public const string BIKE_BODY_2 = "bike_body_2";
    public const string BIKE_BODY_3 = "bike_body_3";
    public const string BIKE_BODY_4 = "bike_body_4";
    public const string BIKE_BODY_5 = "bike_body_5";
    public const string BIKE_BODY_6 = "bike_body_6";
    public const string BIKE_BODY_7 = "bike_body_7";
}
public static class AddressableKeys{
    public const string LABEL_ATLAS = "Atlas";
    public const string LABEL_TEXTURE_EQUIPMENT_HD = "Equipment_Texture_HD";
    public const string LABEL_TEXTURE_EQUIPMENT_SD = "Equipment_Texture_SD";
    public const string LABEL_MODEL_EQUIPMENT = "Equipment_Model";

    //map
    public const string LABEL_MAP_FOREST = "Map_Forest";
    public const string LABEL_MAP_DESERT = "Map_Desert";
    public const string LABEL_MAP_BEACH = "Map_Beach";

}
public static class PopupKeys{
    public const string POPUP_BOLT_CONNECT = "popup_bolt_connect";
    public const string POPUP_LOGIN = "Popup_Login";
    public const string POPUP_JOINGAMEROOM = "Popup_JoinGameRoom";
    public const string POPUP_LOADING = "Popup_Loading";
    public const string PARAMETER_MESSAGE = "parameter_message";
    public const string PARAMETER_POPUP_HEADER = "parameter_popup_header";
}
public class UIName{
    public const string FRIENDS = "friends";
    public const string LOGIN = "login";
    public const string LOBBY = "lobby";
    public const string ROOM = "room";
    public const string SHOP = "shop";
    public const string PROFILE = "profile";

}