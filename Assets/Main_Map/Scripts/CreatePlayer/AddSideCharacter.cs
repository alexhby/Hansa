using UnityEngine;
using System.Collections;

//Class Function: Adds side characters to the main character party

public class AddSideCharacter : MonoBehaviour {

    //Unique Companions Here!
    private BaseCharacter side = new BaseCharacter();
    

    // Use this for initialization
    void Start()
    {
        //side.PlayerName = "HI";
        //side.PlayerClass = BaseCharacterClass.CharacterClasses.Thief;
        //CreateNewCharacter.InitNewCharacter(side);
        //AddNewSideCharacter(side);
    }


    //If adding a side character, the function checks for the "next available character slot" and adds the character in
    //After the character is added, the data is saved
    public static void AddNewSideCharacter(BaseCharacter side)
    {
        if (GameInformation.Char1 == null)
        {
            GameInformation.Char1 = side;
            SaveInformation.SaveAllCharacterInformation();
        }
        else if (GameInformation.Char2 == null)
        {
            GameInformation.Char2 = side;
            SaveInformation.SaveAllCharacterInformation();
        }
        else if (GameInformation.Char3 == null)
        {
            GameInformation.Char3 = side;
            SaveInformation.SaveAllCharacterInformation();
        }
        else if (GameInformation.Char4 == null)
        {
            GameInformation.Char4 = side;
            SaveInformation.SaveAllCharacterInformation();
        }
        else if (GameInformation.Char5 == null)
        {
            GameInformation.Char5 = side;
            SaveInformation.SaveAllCharacterInformation();
        }
        else
        {
            Debug.Log("You're party is full!");
        }

    }
   
	
	
}
