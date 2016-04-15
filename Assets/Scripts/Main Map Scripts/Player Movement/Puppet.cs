using UnityEngine;
using System.Collections;

//Script attached to puppeteer -- the actual player object is updated to the raycast.down position of the puppeteer
//This creates the illusion that the player is travelling on the ground of the terrain
public class Puppet : MonoBehaviour {
    public Transform character;
    
    private GameObject curr;
	// Use this for initialization
	void Start () {
        //places the character in the area before scene switch (so returning from a shop wont make you spawn back at the original position)
        curr = GameObject.Find(WorldInformation.CurrentArea);
        gameObject.transform.position = curr.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        PlaceCharacter();
	}

    void PlaceCharacter()
    {
        
        //Rotates the character in the direction of travel -- only sideways rotations!
        character.transform.localEulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        //places the character at a new position
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 30))
        {
            character.position = hit.point;
        }
    }
}
