using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleGUI : MonoBehaviour {

    private Text playerName;
    private Text playerHealth;
    private Text enemyName;

	void Start () {

        //Instantiate and initialize

        playerName = transform.FindChild("Player").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        playerName.text = "Toni"; //GameInformation.PlayerName;

        enemyName = transform.FindChild("Target").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        enemyName.text = "Enemy";

        Text playerHealthPoints = transform.FindChild("Player").FindChild("HealthBar").GetComponentInChildren<Text>();
        playerHealthPoints.text = "100%";

        Text enemyHealthPoints = transform.FindChild("Target").FindChild("HealthBar").GetComponentInChildren<Text>();
        enemyHealthPoints.text = "100%";
    }
	
	void Update () {
	
	}
}
