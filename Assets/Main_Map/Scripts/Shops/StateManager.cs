using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    //The shop state manager
	public enum ShopStates
    {
        OUTSIDE,
        SHOP,
        QUEST
    }
    public static ShopStates ShopState { get; set; }

}
