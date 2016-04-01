using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseWeapon : BaseStatItem { //BaseWeapon <- BaseStatItem <- BaseItem

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
