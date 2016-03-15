using UnityEngine;
using System.Collections;

public class Area  {

    public enum AreaTypes
    {
        City,
        Plains,
        Desert
    }


    public AreaTypes AreaType { get; set;}
    public string AreaName { get; set; }
    public string AreaID { get; set; }
    
    public Kingdom BeingTakenOverBy { get; set; }
    public int TakeOverCount { get; set; }
    public int DefendCount { get; set; }
	public Kingdom OwnedBy { get; set; }
    public int IconNumber { get; set; }
    
}
