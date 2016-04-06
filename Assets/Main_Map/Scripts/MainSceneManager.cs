using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoBehaviour {

	static MainSceneManager Instance;

	void Start()
	{
		if (Instance != null) {
			GameObject.DestroyObject (gameObject);
		}
		else
		{
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}

	void LoadCombatScene (string pScene) {

		Application.LoadLevel ("Level1");
	}

	void LoadPauseMenu (){

	}
}
