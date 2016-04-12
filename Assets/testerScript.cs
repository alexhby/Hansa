using UnityEngine;
using System.Collections;

public class testerScript : MonoBehaviour {

	//GameObject Friendly;
	//GameObject Enemies;
	//GameObject prefab;
	// Use this for initialization
	void Start () {
		Instantiate (Resources.Load ("Spells/Fireball"), new Vector3 (15.0F, 0.5F, -16.0F), Quaternion.identity);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
