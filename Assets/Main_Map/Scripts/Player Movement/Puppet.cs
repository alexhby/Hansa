using UnityEngine;
using System.Collections;

public class Puppet : MonoBehaviour {
    public Transform character;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        PlaceCharacter();
	}

    void PlaceCharacter()
    {
        character.rotation = gameObject.transform.rotation;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 30))
        {
            character.position = hit.point;
        }
    }
}
