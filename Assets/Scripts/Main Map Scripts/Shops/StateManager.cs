using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

	public enum ShopStates
    {
        OUTSIDE,
        SHOP,
        QUEST
    }
    public static ShopStates ShopState { get; set; }

}
