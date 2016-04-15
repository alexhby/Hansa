using UnityEngine;
using System.Collections;

//PLAYER MODEL -- stores all player relevant information (i.e. characters, etc.)
public class GameInformation : MonoBehaviour {

    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
   
    public enum PlayerMapStates
    {
        Travelling,
        Idle
    }
    
    //The players status in the world
    public static PlayerMapStates PlayerMapState { get; set; }

    void start()
    {
       
    }

    //Store player important data
    public static int Gold { get; set; }
    public static Inventory PlayerInventory { get; set; }
    public static QuestLog PlayerQuestLog { get; set; }

    //store all character info!
    public static BaseCharacter PlayerCharacter { get; set; }
    public static BaseCharacter Char1 { get; set; }
    public static BaseCharacter Char2 { get; set; }
    public static BaseCharacter Char3 { get; set; }
    public static BaseCharacter Char4 { get; set; }
    public static BaseCharacter Char5 { get; set; }
}
