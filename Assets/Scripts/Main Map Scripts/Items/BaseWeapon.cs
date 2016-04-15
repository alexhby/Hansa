using UnityEngine;
using System.Collections;
[System.Serializable]

//Represents a weapon
public class BaseWeapon : BaseStatItem { //BaseWeapon <- BaseStatItem <- BaseItem

    //Note: swords are not used!
    public enum WeaponTypes
    {
        Sword,
        Spear,
        Tomb,
        Bow,
        Dagger
    }

    private WeaponTypes weaponType;

    public int SkillID { get; set; } 
    public WeaponTypes WeaponType { get; set; }
    public int Damage { get; set; }



}
