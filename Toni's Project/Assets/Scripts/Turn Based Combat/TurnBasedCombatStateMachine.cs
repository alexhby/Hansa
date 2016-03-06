using UnityEngine;
using System.Collections;

public class TurnBasedCombatStateMachine : MonoBehaviour {

    private bool hasAddedXP = false;

    public enum BattleStates
    {
        START,
        PLAYERCHOICE,
        PLAYERANIMATE,
        ENEMYCHOICE,
        LOSE,
        WIN
    }

    private BattleStates currentState;
	// Use this for initialization
	void Start () {
        currentState = BattleStates.START;
        hasAddedXP = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentState);
        switch (currentState)
        {
            //setup functions that run when each state is active.
            case (BattleStates.START):
                //setup battle function
                break;
            case (BattleStates.PLAYERCHOICE):

                break;
            case (BattleStates.ENEMYCHOICE):
                break;
            case (BattleStates.LOSE):
                break;
            case (BattleStates.WIN):
                if (!hasAddedXP)
                {
                    IncreaseExperience.AddExperience();
                    hasAddedXP = true;
                }
                break;

        }
	}

    void OnGUI()
    {
        if(GUILayout.Button("NEXT STATE"))
        {
            if (currentState == BattleStates.START)
            {
                currentState = BattleStates.PLAYERCHOICE;
            }
            else if (currentState == BattleStates.ENEMYCHOICE)
            {
                currentState = BattleStates.PLAYERCHOICE;
            }

        }
    }
}
