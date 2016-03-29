using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleGUI : MonoBehaviour {

    private Text playerName;
    private Text playerHealth;
    private Text enemyName;

	// Use this for initialization
	void Start () {
        playerName = transform.FindChild("Player").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        playerName.text = GameInformation.PlayerName;
        enemyName = transform.FindChild("Target").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        enemyName.text = "Enemy";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
