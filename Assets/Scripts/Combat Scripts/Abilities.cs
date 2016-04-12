using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TileDraw.Map;

[System.Serializable]
public abstract class Abilities : MonoBehaviour
{
	protected float tilesize = 2;
    protected Tile myTile;
    protected Vector2 myPointInCell;
    protected Vector3 rot;
    protected Cell grid;
    protected Transform trans;
    protected List<String> attackTiles;

    protected const int offset = 25;
    public bool isPhysical; // true: physical, false: magical

	public string description;
	public int range = 1;

    public Abilities(Cell pCell) 
    {
        grid = pCell;
    }

	public Abilities(){}

    public void updateLoc(GameObject pObj)
    {

        trans = pObj.transform;
        attackTiles = new List<string>();

        myPointInCell = grid.convertWorldPosToIndex(trans.position.x, trans.position.z);
        myTile = grid.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        rot = trans.rotation.eulerAngles;

    }

    public abstract void attack();


	// Get the string of the i-th neigbhour
	protected string getString(int neighbourIndex, int i){

		if (i == 1) {
			return myTile.Neighbours [neighbourIndex]; 
		} else {
			return grid.ConvertStringToTile (getString (neighbourIndex, i - 1)).Neighbours [neighbourIndex];
		}
	}


	// Get the tile of the i-th neigbhour
	protected Tile getTile(int neighbourIndex, int i){
		return grid.ConvertStringToTile (getString (neighbourIndex, i));
	}

	// Get the world position of a Tile
	protected Vector3 getPosition(Tile pTile){
		Vector2 temp = grid.convertIndexToWorldPos( (int)(grid.GetPointInCellFromTileIndex (pTile.TileIndex).x), (int)(grid.GetPointInCellFromTileIndex (pTile.TileIndex).y) );
		return new Vector3 (temp.x, pTile.GetHeight (), temp.y);
	}

}
	



public class DaggerAttack : Abilities
{
    public DaggerAttack(Cell pCell) : base(pCell) {

		description = "Attack one target in the front.";
		range = 1;
	}

    public override void attack()
    {

        isPhysical = true;

        //Default attack : Range of 1 tile on front of character
        if (rot.y < 0 + offset && rot.y > 0 - offset)
        {
            // neighbour on the right
            directionalAttact(0);
        }
        else if (rot.y < 270 + offset && rot.y > 270 - offset)
        {
            // neighbour on the front
            directionalAttact(1);
        }
        else if (rot.y < 180 + offset && rot.y > 180 - offset)
        {
            directionalAttact(2);
        }
        else if (rot.y < 90 + offset && rot.y > 90 - offset)
        {
            directionalAttact(3);
        }

        trans.GetComponent<Animator>().Play("Attack SP");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical);
    }

    // helper function
    private void directionalAttact(int neighbourIndex)
    {
        if (myTile.Neighbours[neighbourIndex] != "None")
            attackTiles.Add(myTile.Neighbours[neighbourIndex]);

    }
}

public class SpearAttack : Abilities
{

    public SpearAttack (Cell pCell) : base(pCell) { 
		description = "Damage up to 3 targets in a row.";
		range = 3;
	}

    public override void attack()
    {

        isPhysical = true;

        if (rot.y < 0 + offset && rot.y > 0 - offset)
        {
            // neighbour on the right
            directionalAttact(0);
        }
        else if (rot.y < 270 + offset && rot.y > 270 - offset)
        {
            // neighbour in the front
            directionalAttact(1);
        }
        else if (rot.y < 180 + offset && rot.y > 180 - offset)
        {
            directionalAttact(2);
        }
        else if (rot.y < 90 + offset && rot.y > 90 - offset)
        {
            directionalAttact(3);

        }

        trans.GetComponent<Animator>().Play("Attack SP");
        trans.GetComponent<CharController>().attack(attackTiles, isPhysical);
    }

    // helper function: attack 3 targets on a row
    private void directionalAttact(int neighbourIndex)
    {
		if (getString(neighbourIndex,1) != "None")
			attackTiles.Add(getString(neighbourIndex,1));
		
		if (getString(neighbourIndex,2) != "None")
			attackTiles.Add(getString(neighbourIndex,2));
		
		if (getString(neighbourIndex,3) != "None")
			attackTiles.Add(getString(neighbourIndex,3));

    }
}

/*--------------------------------------------------------------------------------------------------------------------
* 
* Mage Abilities
* 
*---------------------------------------------------------------------------------------------------------------------
*/


public class Fireball : Abilities{

	public Fireball(Cell pCell) : base(pCell) { 
		description = "Damage up to 3 targets in a row.";
		range = 3;
	}
		
	public override void attack()
	{
		isPhysical = false;

		// Play animation
		trans.GetComponent<Animator>().Play("Standing 2H Magic Attack 2");

		// Load spell particle systems
		Instantiate (Resources.Load ("Spells/Fireball"), trans.position + new Vector3 (0.0F, 0.5F, 0.0F), trans.rotation);

		if (rot.y < 0 + offset && rot.y > 0 - offset)
		{
			// neighbour on the right
			directionalAttact(0);
		}
		else if (rot.y < 270 + offset && rot.y > 270 - offset)
		{
			// neighbour in the front
			directionalAttact(1);
		}
		else if (rot.y < 180 + offset && rot.y > 180 - offset)
		{
			directionalAttact(2);
		}
		else if (rot.y < 90 + offset && rot.y > 90 - offset)
		{
			directionalAttact(3);

		}

		// Apply damage
		trans.GetComponent<CharController>().attack(attackTiles, isPhysical);

	}

	// helper function: attack 3 targets on a row
	private void directionalAttact(int neighbourIndex)
	{
		if (getString(neighbourIndex,1) != "None")
			attackTiles.Add(getString(neighbourIndex,1));

		if (getString(neighbourIndex,2) != "None")
			attackTiles.Add(getString(neighbourIndex,2));

		if (getString(neighbourIndex,3) != "None")
			attackTiles.Add(getString(neighbourIndex,3));

	}



}

public class Lightning : Abilities{

	public Lightning(Cell pCell) : base(pCell) { 

		description = "Damange one enemy target.";
		range = 4;
	
	}

	public override void attack()
	{
		isPhysical = false;

		if (rot.y < 0 + offset && rot.y > 0 - offset)
		{
			// neighbour on the right
			directionalAttact(0);
		}
		else if (rot.y < 270 + offset && rot.y > 270 - offset)
		{
			// neighbour in the front
			directionalAttact(1);
		}
		else if (rot.y < 180 + offset && rot.y > 180 - offset)
		{
			directionalAttact(2);
		}
		else if (rot.y < 90 + offset && rot.y > 90 - offset)
		{
			directionalAttact(3);

		}

		// Apply damage
		trans.GetComponent<CharController>().attack(attackTiles, isPhysical);


	}

	// helper function: attack a target in a range of 4
	private void directionalAttact(int neighbourIndex)
	{
		// check if this char is friendly or an enemy
		bool isFriendly = myTile.EntityString.Contains ("friendly");

		for (int i = 1; i <= range; i++) {
			
			if (getString(neighbourIndex,i) != "None") {

				// check if the target is in the opposite team
				if ( (getTile(neighbourIndex,i).EntityString.Contains ("enemy") && isFriendly) || (getTile(neighbourIndex,i).EntityString.Contains ("friendly") && !isFriendly) ) {


					trans.GetComponent<Animator>().Play("Standing 1H Magic Attack");
					Instantiate (Resources.Load ("Spells/LightningSpark"), getPosition (getTile(neighbourIndex,i)) + new Vector3 (0f, 0.5f, 0f), Quaternion.identity);

					attackTiles.Add (getString(neighbourIndex, i));
					return;

				}
			}
		}

		// If no valid target, then do nothing
	}
}


public class ArcaneBlast : Abilities{

	public ArcaneBlast(Cell pCell) : base(pCell) { 

		description = "Damage all adjacent enemy targets.";
		range = 1;

	}

	// area attack
	public override void attack()
	{
		isPhysical = false;

		trans.GetComponent<Animator>().Play("Standing 2H Area magic");
		Instantiate (Resources.Load ("Spells/ArcaneBlast"), trans.position + new Vector3 (0.0F, 0.5F, 0.0F), trans.rotation);

		bool isFriendly = myTile.EntityString.Contains ("friendly");

		for (int x = 0; x <= 3; x++) {
			if (myTile.Neighbours [x] != "None") {

				// check if the target is in the opposite team
				if ( (getTile(x,1).EntityString.Contains ("enemy") && isFriendly) || (getTile(x,1).EntityString.Contains ("friendly") && !isFriendly) ) {

					attackTiles.Add (myTile.Neighbours [x]);

				}
			}

		}

		// Apply damage
		trans.GetComponent<CharController>().attack(attackTiles, isPhysical);


	}


}


public class Fog : Abilities{

	public Fog(Cell pCell) : base(pCell) { 

		description = "Damange one enemy target with poison fog.";
		range = 3;

	}

	// similar to lightning
	public override void attack()
	{
		isPhysical = false;

		if (rot.y < 0 + offset && rot.y > 0 - offset)
		{
			// neighbour on the right
			directionalAttact(0);
		}
		else if (rot.y < 270 + offset && rot.y > 270 - offset)
		{
			// neighbour in the front
			directionalAttact(1);
		}
		else if (rot.y < 180 + offset && rot.y > 180 - offset)
		{
			directionalAttact(2);
		}
		else if (rot.y < 90 + offset && rot.y > 90 - offset)
		{
			directionalAttact(3);

		}

		// Apply damage
		trans.GetComponent<CharController>().attack(attackTiles, isPhysical);


	}

	// helper function: attack a target in a range of 3
	private void directionalAttact(int neighbourIndex)
	{
		// check if this char is friendly or an enemy
		bool isFriendly = myTile.EntityString.Contains ("friendly");

		for (int i = 1; i <= range; i++) {

			if (getString(neighbourIndex,i) != "None") {

				// check if the target is in the opposite team
				if ( (getTile(neighbourIndex,i).EntityString.Contains ("enemy") && isFriendly) || (getTile(neighbourIndex,i).EntityString.Contains ("friendly") && !isFriendly) ) {


					trans.GetComponent<Animator>().Play("Standing 1H Magic Attack 02");
					Instantiate (Resources.Load ("Spells/Fog"), getPosition (getTile(neighbourIndex,i)) + new Vector3 (0f, 0.5f, 0f), Quaternion.identity);

					attackTiles.Add (getString(neighbourIndex, i));
					return;

				}
			}
		}

		// If no valid target, then do nothing
	}
}