using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour {

	/*
	 * static MainSceneManager Instance;
	*/

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
	private bool inCombat = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	// need baseChar and sideChar, areaType, their stats, starting positions,
	public void loadCombatScene(int areaType)
	{
		if (areaType == 1) {
			Application.LoadLevel ("Level1");
		}


	}
}
