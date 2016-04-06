using UnityEngine;
using System.Collections;
using TileDraw.Map;

public class Abilities : MonoBehaviour {

	protected CharController c;
	protected Tile myTile;
	Vector2 myPointInCell;
	protected Vector3 rot;
	protected Cell grid;
	protected const int offset = 25;
	public bool isPhysical; // true: physical, false: magical

	public Abilities() {
		throw new UnityException("No Constructor parameters --> produced by Alex");
	}

	public Abilities(Cell pCell, CharController pChar){

		c = pChar;
		grid = pCell;
	
		myPointInCell = grid.convertWorldPosToIndex(transform.position.x, transform.position.z);
		myTile = grid.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
		rot = c.transform.rotation.eulerAngles;

	}

}

public class slashTwoSwords : Abilities {

	public void attack(){
		
		isPhysical = true;

		//This goes in ability class

		//Default attack : Range of 1 tile infront of character
		if (rot.y < 0 + offset && rot.y > 0 - offset && myTile.Neighbours[0] != "None")
		{
			// neighbour on the right
			directionalAttact(0);
		}
		else if (rot.y < 270 + offset && rot.y > 270 - offset && myTile.Neighbours[1] != "None")
		{
			// neighbour in the front
			directionalAttact(1);
		}
		else if (rot.y < 180 + offset && rot.y > 180 - offset && myTile.Neighbours[2] != "None")
		{
			directionalAttact(2);
		}
		else if (rot.y < 90 + offset && rot.y > 90 - offset && myTile.Neighbours[3] != "None")
		{
			directionalAttact(3);
		}

		c.animator.Play("Attack SW");
	}

	// helper function
	private void directionalAttact(int neighbourIndex){
		
		c.attackTiles.Add(myTile.Neighbours[neighbourIndex]);

	}
}

public class SpearAttack: Abilities {

	public void attack(){

		//This goes in ability class

		//Default attack : Range of 1 tile infront of character
		if (rot.y < 0 + offset && rot.y > 0 - offset && myTile.Neighbours[0] != "None")
		{
			// neighbour on the right
			directionalAttact(0);
		}
		else if (rot.y < 270 + offset && rot.y > 270 - offset && myTile.Neighbours[1] != "None")
		{
			// neighbour in the front
			directionalAttact(1);
		}
		else if (rot.y < 180 + offset && rot.y > 180 - offset && myTile.Neighbours[2] != "None")
		{
			directionalAttact(2);
		}
		else if (rot.y < 90 + offset && rot.y > 90 - offset && myTile.Neighbours[3] != "None")
		{
			directionalAttact(3);
		
	}


		/* Jeremy do this to switch weapons
		Weapons weapon = c.transform.Find(c.melee).GetComponent<Weapons>();
	    weapon.setCurrentWeapon((int)c.weapons.spear);
		c.transform.Find(c.shield).GetComponent<ShieldManager>().hasShield = true;
		*/

		c.animator.Play("Attack SP");
	}

	// helper function
	private void directionalAttact(int neighbourIndex){

		c.attackTiles.Add (myTile.Neighbours[neighbourIndex]);
		c.attackTiles.Add (grid.ConvertStringToTile (myTile.Neighbours [neighbourIndex]).Neighbours [neighbourIndex]);
		c.attackTiles.Add (grid.ConvertStringToTile (grid.ConvertStringToTile(myTile.Neighbours[neighbourIndex]).Neighbours[neighbourIndex]).Neighbours[neighbourIndex] );

	}
}