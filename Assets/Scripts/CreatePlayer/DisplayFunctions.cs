using UnityEngine;
using System.Collections;

public class DisplayFunctions  {

    private int classSelection;
    private string[] classSelectionName = new string[] { "Apprentice", "Squire","Rogue","Archer", "Black Mage","White Mage", "Sniper","Knight","Ninja","Paladin","Darksword","Berserker", "Assassin", "Arch-Mage"};


    public void DisplayClassSelections()
    {
        //list of toggle buttons with each button being a different class
        //Using a selection grid!
        //Displays stats of specific classes
        classSelection = GUI.SelectionGrid(new Rect(50,50,400,300), classSelection, classSelectionName, 4);


    }
   

    public void DisplayStatAllocation()
    {
        //list of stats with plus buttons --> logic to make sure player cant add more stats given

    }

    public void DisplayCharacterSelections()
    {
        //List of toggle buttons with each button being a different character
        //selection grid?


    }

    public void DisplayEquipItem()
    {

    }
    
}
