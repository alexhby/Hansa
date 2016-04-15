using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

//THE INVENTORY SET UP -- three Lists - weapons, equipments, potions
public class Inventory {
    private string str = "";
	public List<BasePotion> Potions { get; set; }
    public List<BaseWeapon> Weapons { get; set; }
    public List<BaseEquipment> Equipment { get; set; }

    //for testing purposes
    public void printInventory()
    {
        str = "";
        Weapons.ForEach(printWeaponName);
        Equipment.ForEach(printEquipmentName);
        Potions.ForEach(printPotionName);
        Debug.Log(str);
    }

    private void printWeaponName(BaseWeapon T)
    {
        str = str + T.ItemName + "\n";
    }
    private void printEquipmentName(BaseEquipment T)
    {
        str = str + T.ItemName + "\n";
    }
    private void printPotionName(BasePotion T)
    {
        str = str + T.ItemName + "\n";
    }
}
