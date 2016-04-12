using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleGUI : MonoBehaviour {

    public int deathCount = 0;
    public int enemyDeathCount = 0;
    public int numEnemies = 6;
    public Vector3 currentEnemyPos;
    private Transform friendly;
    private Transform enemy;
    private Transform currentEnemy;
    private Image hp;
    private CharController c;

	void Start () {

        //Instantiate and initialize
        friendly = transform.parent.Find("Friendly");
        enemy = transform.parent.Find("Enemies");
        currentEnemyPos = transform.parent.Find("Enemies").GetChild(0).transform.position; //by default target is first enemy

        Transform p = transform.Find("Player");
        if (friendly.childCount == 2)
        {
            p.FindChild("SidePlayer1").gameObject.SetActive(true);
        }
        else if (friendly.childCount == 3)
        {
            p.FindChild("SidePlayer2").gameObject.SetActive(true);
        }
        else if (friendly.childCount == 4)
        {
            p.FindChild("SidePlayer3").gameObject.SetActive(true);
        }
        else if (friendly.childCount == 5)
        {
            p.FindChild("SidePlayer4").gameObject.SetActive(true);
        }
        else if (friendly.childCount == 6)
        {
            p.FindChild("SidePlayer5").gameObject.SetActive(true);
        }


        //Player
        Text playerName = transform.FindChild("Player").FindChild("SidePlayer0").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        //playerName.text = GameInformation.PlayerCharacter.PlayerName;

        Text spName1 = transform.FindChild("Player").FindChild("SidePlayer1").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
		//spName1.text = GameInformation.Char1.PlayerName;
        
        Text spName2 = transform.FindChild("Player").FindChild("SidePlayer2").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
		//spName2.text = GameInformation.Char2.PlayerName;

        Text spName3 = transform.FindChild("Player").FindChild("SidePlayer3").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
		//spName3.text = GameInformation.Char3.PlayerName;

        Text spName4 = transform.FindChild("Player").FindChild("SidePlayer4").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
		//spName4.text = GameInformation.Char4.PlayerName;

        Text spName5 = transform.FindChild("Player").FindChild("SidePlayer5").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        //spName5.text = GameInformation.Char5.PlayerName;

        //Target

        Text enemyName = transform.FindChild("Target").FindChild("PlayerIcon").FindChild("PlayerName").GetComponent<Text>();
        enemyName.text = "Enemy";

    }

	void Update () {

        for (int i = 0; i < friendly.childCount; i++)
        {
            c = friendly.GetChild(i).GetComponent<CharController>();
            hp = transform.Find("Player/SidePlayer" + i + "/HealthBar").GetComponent<Image>();
            float fillAmount = (c.myHealth) / 100.0f;
            hp.fillAmount = fillAmount;
            hp.GetComponentInChildren<Text>().text = Mathf.Round(fillAmount * 100) + "%";

            if (c.myHealth <= 0)
            {
                c.isDead = true;
                deathCount++;
                //Set lose state
                if ( deathCount == 6)
                    transform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().setCurrentState(5);
            }

        }

        for (int i = 0; i < enemy.childCount; i++)
        {
            if (enemy.GetChild(i).transform.position == currentEnemyPos)
            {
                c = enemy.GetChild(i).GetComponent<CharController>();
                hp = transform.Find("Target/HealthBar").GetComponent<Image>();
                float fillAmount = (c.myHealth) / 100.0f;
                hp.fillAmount = fillAmount;
                hp.GetComponentInChildren<Text>().text = Mathf.Round(fillAmount * 100) + "%";

                if (c.myHealth <= 0)
                {
                    c.isDead = true;
                    enemyDeathCount++;
                    //Set win state
                    if (enemyDeathCount == numEnemies)
                        transform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().setCurrentState(4);
                }
            }
        }
	}
}
