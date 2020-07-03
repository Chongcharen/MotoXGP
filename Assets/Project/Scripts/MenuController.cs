using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    public void OnClickCharacterPick(int whichCharacter)
    {
        if(PlayerInfo.pi != null)
        {
            PlayerInfo.pi.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
    }
}
