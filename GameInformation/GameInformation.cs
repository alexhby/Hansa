using UnityEngine;
using System.Collections;

public class GameInformation : MonoBehaviour {

    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        
    }
    
   

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
    public static BaseEquipment Helmet { get; set; }
    public static BaseEquipment Armor { get; set; }
    public static BaseEquipment Gauntlets { get; set; }
    public static BaseEquipment Grieves { get; set; }
    public static BaseWeapon Weapon { get; set; }
    public static Inventory PlayerInventory { get; set; }

    public static QuestLog PlayerQuestLog { get; set; }
    

    
    public static BaseCharacter Char1 { get; set; }
    public static BaseCharacter Char2 { get; set; }
    public static BaseCharacter Char3 { get; set; }
    public static BaseCharacter Char4 { get; set; }
    public static BaseCharacter Char5 { get; set; }
}
