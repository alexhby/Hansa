using UnityEngine;
using System.Collections;

public class Puppet : MonoBehaviour {
    public Transform character;
    private GameObject curr;
	// Use this for initialization
	void Start () {
        Debug.Log(WorldInformation.CurrentArea + "is your current area");
        curr = GameObject.Find(WorldInformation.CurrentArea);
        gameObject.transform.position = curr.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        PlaceCharacter();
	}

    void PlaceCharacter()
    {
        
        //character.rotation = gameObject.transform.rotation;
        character.transform.localEulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 30))
        {
            character.position = hit.point;
        }
    }
}
