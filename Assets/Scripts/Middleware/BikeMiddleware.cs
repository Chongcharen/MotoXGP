using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeMiddleware : MonoBehaviour
{
    public PlayerEquipment playerEquipment;
    public void SetupPlayerEquipment(PlayerEquipmentToken equipmentToken){
        foreach (var item in equipmentToken.playerEquipmentMapper)
        {
            print(Depug.Log($"item key {item.Key} value {item.Value}",Color.green));
        }
        playerEquipment.SetupEquipment(equipmentToken.playerEquipmentMapper);
    }
}
