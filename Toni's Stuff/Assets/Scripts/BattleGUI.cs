using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleGUI : MonoBehaviour {

    private Text playerName;
    private Text playerHealth;


	// Use this for initialization
	void Start () {
        playerName = transform.FindChild("Player").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        //playerName.text = GameInformation.PlayerName();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
