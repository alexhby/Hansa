using UnityEngine;
using System.Collections;
[System.Serializable]

//Represents an Area (icon) on the map with all the relevant information
public class Area  {

    public enum AreaTypes
    {
        City,
        Plains,
        Desert
    }

    //public GameObject Icon { get; set; }
    public AreaTypes AreaType { get; set;} //Determines playing field on battle
    public string AreaName { get; set; } 
    public string AreaID { get; set; } //Stored in database
    
    public Kingdom BeingTakenOverBy { get; set; } 
    public int TakeOverCount { get; set; }
    public int DefendCount { get; set; }
	public Kingdom OwnedBy { get; set; }
    public int IconNumber { get; set; } //The Icon number in the game world
    
}
