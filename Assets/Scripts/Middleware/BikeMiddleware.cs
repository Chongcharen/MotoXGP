using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeMiddleware : MonoBehaviour
{
    public PlayerEquipment playerEquipment;
    public BikeEquipment bikeEquipment;
    public RagdollCollider ragdollCollider;
    public void SetupPlayerEquipment(PlayerEquipmentToken equipmentToken){
        playerEquipment.SetupEquipment(equipmentToken.playerEquipmentMapper);
    }
    public void SetupBikeEquipment(BikeEquipmentToken bikeEquipmentToken){
        bikeEquipment.SetupEquipment(bikeEquipmentToken.bikeEquipmentMapper);
    }
}
