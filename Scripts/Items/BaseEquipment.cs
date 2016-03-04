using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseEquipment : BaseStatItem {

    public enum EquipmentTypes
    {
        HEAD,
        CHEST,
        ARMS,
        LEGS
    }


    public EquipmentTypes EquipmentType { get; set; }
    
}
