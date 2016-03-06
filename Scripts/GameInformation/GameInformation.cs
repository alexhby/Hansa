using UnityEngine;
using System.Collections;

public class GameInformation : MonoBehaviour {

    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    public static BaseEquipment EquipmentOne { get; set;}

	public static string PlayerName { get; set;}
    public static int PlayerLevel { get; set;}
    public static BaseCharacterClass PlayerClass { get; set;}
    public static int Strength { get; set;}
    public static int Agility { get; set;}
    public static int Intellect { get; set;}
    public static int Defense { get; set;}
    public static int CurrentXP { get; set; }
    public static int RequiredXP { get; set; }
    public static int Health { get; set; }
    public static int Mana { get; set; }
    public static int Gold { get; set; }

    public static BasePlayer Main { get; set; }
    public static BasePlayer Char1 { get; set; }
    public static BasePlayer Char2 { get; set; }
    public static BasePlayer Char3 { get; set; }
    public static BasePlayer Char4 { get; set; }
    public static BasePlayer Char5 { get; set; }
}
