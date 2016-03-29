using UnityEngine;
using System.Collections;

public class PrjAutoDestroy : MonoBehaviour {

    private Projector prj;
    public float projDuration = 1;
    private float currentTime;

	// Use this for initialization
	void Start () {

        currentTime = Time.time;
	    prj = GetComponent<Projector>();

    }
	
	// Update is called once per frame
	void Update () {
      
        if( Time.time - currentTime > projDuration)
        {
            Destroy(gameObject);
        }
	}
}
