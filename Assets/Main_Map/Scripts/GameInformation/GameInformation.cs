using UnityEngine;
using System.Collections;

public class GameInformation : MonoBehaviour {

    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        
    }
    
   

	
    public static BaseCharacter PlayerCharacter { get; set; }

    void start()
    {
        
    }
    public static int Gold { get; set; }
    
    public static Inventory PlayerInventory { get; set; }


    public static QuestLog PlayerQuestLog { get; set; }
    

    
    public static BaseCharacter Char1 { get; set; }
    public static BaseCharacter Char2 { get; set; }
    public static BaseCharacter Char3 { get; set; }
    public static BaseCharacter Char4 { get; set; }
    public static BaseCharacter Char5 { get; set; }
}
