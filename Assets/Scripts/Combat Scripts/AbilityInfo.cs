using UnityEngine;
using System.Collections;

public class AbilityInfo : MonoBehaviour {
    public bool isShown;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isShown)
            transform.gameObject.SetActive(true);
        else
            transform.gameObject.SetActive(false);
	}
}
