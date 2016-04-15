using UnityEngine;
using System.Collections;

public class CreatePlayerGUI : MonoBehaviour {
    public enum CharacterMenuPlayerStates
    {
        CHARACTERSELECT, //shows all you characters
        CLASSSELECT, // display all class types & descriptions
        STATALLOCATION,  //Allocate new stats from levels
        EQUIPITEMS //Equip items from you inventory
    }

    private DisplayFunctions displayFunctions = new DisplayFunctions();
    public static CharacterMenuPlayerStates currentState;

    // Use this for initialization
    void Start()
    {
        currentState = CharacterMenuPlayerStates.CLASSSELECT;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (CharacterMenuPlayerStates.CHARACTERSELECT):
                break;
            case (CharacterMenuPlayerStates.CLASSSELECT):
                break;
            case (CharacterMenuPlayerStates.STATALLOCATION):
                break;
            case (CharacterMenuPlayerStates.EQUIPITEMS):
                break;
        }

    }

    void OnGUI()
    {
        if (currentState == CharacterMenuPlayerStates.CHARACTERSELECT)
        {
            //Display Character Selection function
            displayFunctions.DisplayCharacterSelections();
        }
        if (currentState == CharacterMenuPlayerStates.CLASSSELECT)
        {
            //Display Class Selection function
            displayFunctions.DisplayClassSelections();
        }
        if (currentState == CharacterMenuPlayerStates.STATALLOCATION)
        {
            //Display Stat Allocation function
            displayFunctions.DisplayStatAllocation();
        }
        if (currentState == CharacterMenuPlayerStates.EQUIPITEMS)
        {
            //Display Equip Items function
            displayFunctions.DisplayEquipItem();
        }
    }
   
}
