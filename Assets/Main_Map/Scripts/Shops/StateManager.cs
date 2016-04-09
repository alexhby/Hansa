using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
    //The states in the shop
	public enum ShopStates
    {
        OUTSIDE,
        SHOP,
        QUEST
    }
    public static ShopStates ShopState { get; set; }

}
