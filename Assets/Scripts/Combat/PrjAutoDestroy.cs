using UnityEngine;
using System.Collections;

public class PrjAutoDestroy : MonoBehaviour {

    public float projDuration = 1;
    private float currentTime;

	// Use this for initialization
	void Start () {

        currentTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
      
        if( Time.time - currentTime > projDuration)
        {
            Destroy(gameObject);
        }
	}
}
