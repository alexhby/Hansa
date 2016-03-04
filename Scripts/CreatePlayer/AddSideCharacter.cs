using UnityEngine;
using System.Collections;

public class AddSideCharacter : MonoBehaviour {

    //Unique Companions Here!
    private BasePlayer side = new BasePlayer();
    

    // Use this for initialization
    void Start()
    {
        side.PlayerClass = new BaseThiefClass();
        CreateNewCharacter.CreateNewPlayer(side);
        AddNewSideCharacter(side);
    }

    public static void AddNewSideCharacter(BasePlayer side)
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
