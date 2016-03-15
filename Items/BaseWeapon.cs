using UnityEngine;
using System.Collections;

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


    public WeaponTypes WeaponType { get; set; }
    public int Damage { get; set; }
    public int SkillID { get; set; }


}
