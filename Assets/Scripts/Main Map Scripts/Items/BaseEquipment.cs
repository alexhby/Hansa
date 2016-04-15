using UnityEngine;
using System.Collections;
[System.Serializable]

//Represents Any piece of equipment 
public class BaseEquipment : BaseStatItem {

    public enum EquipmentTypes
    {
        Helmet,
        Armor,
        Gauntlets,
        Grieves
    }

    public int Resistance { get; set; } //Damage reducer
    public EquipmentTypes EquipmentType { get; set; }
    
}
