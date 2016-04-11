using UnityEngine;
using System.Collections;

public class ShieldManager : MonoBehaviour {

    public bool hasShield { get; set; }
    public bool hasSword { get; set; }

    void Start(){

        hasShield = false;

    }

	// Update is called once per frame
	void Update () {

        if (hasShield)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
        
        if (hasSword)
            transform.GetChild(1).gameObject.SetActive(true);
        else
            transform.GetChild(1).gameObject.SetActive(false);
	}
}
