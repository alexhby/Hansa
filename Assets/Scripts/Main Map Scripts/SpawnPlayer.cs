using UnityEngine;
using System.Collections;

// Spawn and control the movement of player prefab in main map.
// Attach to Player object in main map scene
public class SpawnPlayer : MonoBehaviour {

	GameObject player;
	Vector3 initalPos = new Vector3(66,80,218);
	//Vector3 lastPos; // record the position during last frame

	// Use this for initialization
	void Start () {
		BaseCharacterClass.CharacterClasses aClass = GameInformation.PlayerCharacter.PlayerClass;

		// Instantiate the PlayerCharacter prefab based on class
		if ((int)aClass % 4 == 0) {
			// Squire
			player = (GameObject)Instantiate (Resources.Load ("MainMap/Squire"), initalPos, Quaternion.identity);

		} else if ((int)aClass % 4 == 1) {
			// Apprentice
			player = (GameObject)Instantiate (Resources.Load ("MainMap/Apprentice"), initalPos, Quaternion.identity);

		} else if ((int)aClass % 4 == 2) {
			// Thief
			player = (GameObject)Instantiate (Resources.Load ("MainMap/Thief"), initalPos, Quaternion.identity);

		} else {
			// Archer
			player = (GameObject)Instantiate (Resources.Load ("MainMap/Archer"), initalPos, Quaternion.identity);

		}

		player.transform.SetParent (gameObject.transform);

		//lastPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// if the player is moving, play the Run animation
		if (GameInformation.PlayerMapState == GameInformation.PlayerMapStates.Travelling) {
			player.GetComponent<Animator> ().SetFloat ("Run", 2.0f);
			// Update lastPosition
			//lastPos = gameObject.transform.position;
		} else {
			// if not moving, stop the Run animation
			player.GetComponent<Animator> ().SetFloat ("Run", 0.0f);
		}

	}

}
