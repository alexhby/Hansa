using UnityEngine;
using System.Collections;

public class CombatSceneManager : MonoBehaviour {

	// Attach to Empty Object SceneSwitch in test scene (Main Map)

	public static GameObject[] prefabs = new GameObject[4];

	void start() {

	}

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}


	// Update is called once per frame
	void Update () {

		//TODO: trigger of loading combat scene

	}

	private void LoadCombatScene (string pCombatScene) {

		//TODO: other necessary change
		Application.LoadLevel (pCombatScene);

	}




}
