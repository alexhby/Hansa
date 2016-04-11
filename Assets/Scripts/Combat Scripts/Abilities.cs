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

	public string Description;
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

	// call this to suspend the ability prefab
	protected IEnumerator wait(float time)
	{
		yield return WaitForSeconds (time);
	}

}
	



public class DaggerAttack : Abilities
{
    public DaggerAttack(Cell pCell) : base(pCell) {

		Description = "Basic dagger attack.";
		range = 1;
	}

    public override void attack()
    {

        isPhysical = true;

        //Default attack : Range of 1 tile infront of character
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
		Description = "This is a spear attack that can damage 3 targets in front of the character.";
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

public class Fireball : Abilities{

	public Fireball(Cell pCell) : base(pCell) { 
		Description = "Shoot a fireball that can damage 3 targets in front of the character.";
		range = 3;
	}

	public override void attack()
	{
		isPhysical = false;

		// TODO: add animator in CharController & call animator
		trans.GetComponent<Animator>().Play("Standing 2H Magic Attack 2");

		Instantiate (Resources.Load ("Spells/Fireball"), trans.position + new Vector3 (0.5F, 0.5F, 0.0F), trans.rotation);

		trans.GetComponent<CharController>().attack(attackTiles, !isPhysical);


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

		Description = "Create a lightning that demanges one enemy target in range 4.";
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

					trans.GetComponent<CharController>().attack(attackTiles, !isPhysical);
					return;

					attackTiles.Add (getString(neighbourIndex, i));


				}
			}
		}

		// No valid target, then do nothing
		trans.GetComponent<CharController>().attack(attackTiles, !isPhysical);

	}
		


}