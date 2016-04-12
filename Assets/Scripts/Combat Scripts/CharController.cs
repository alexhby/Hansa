using UnityEngine;
using System.Collections.Generic;
using TileDraw.Map;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{

    public Animator animator;
    //Text
    public Font myFont;
    private float currentTime = 0.0f; //Time text starts to display
    private float textTime = 5.0f; //Time text is displayed for
    //Movement
    private Vector3 startPos;
    private float startTime = 0.0f; //Time start to move
    public float duration = 1f; //Moving duration
    private bool showPaths;
    private bool waitForUserInput;
    //Booleans
    private bool isMoving = false;
    private bool isTurning = false;
    private bool isAttacking = false;
    public bool isDead { get; set; }
    public bool myTurn;
    //Booleans : GUI
    private bool showLabel1 = false;
    private bool showLabel2 = false;
    private bool showLabel3 = false;
    private bool showLabel4 = false;
    //Class Information
    public BaseCharacter myClass;
    public int myAgility;
    public int myStrength;
    public int myIntellect;
    public float myHealth;
    public float myMana;
    public int myDefense;
    //Other classes/Objects needed
    private PathFinding pf;
    private Cell c;
    //action/ability menu
    private GameObject actionBar;
    private GameObject abilityInfo;
    private Button a1;
    private Button a2;
    private Button a3;
    private Button a4;
    private Button next;
    //Attack Information
    private float myPhysicalDamage;
    private float myMagicDamage;

	// for the sleep spell
	public bool isSleep = false;
    //Abilities ----------------------------------------------------------------------------------------------------------------------------------------
    //SpearAttack sp;
    //DaggerAttack da;
    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        //Instantiate
        pf = transform.parent.GetComponentInParent<PathFinding>();
        c = transform.parent.parent.Find("(0,0)").GetComponent<Cell>();
        animator = GetComponent<Animator>();

        //action menu
        actionBar= transform.parent.parent.Find("Canvas/Panel").gameObject;
        abilityInfo = transform.parent.parent.Find("Canvas/Ability Description").gameObject;
        a1 = actionBar.transform.GetChild(0).GetComponent<Button>();
        a2 = actionBar.transform.GetChild(1).GetComponent<Button>();
        a3 = actionBar.transform.GetChild(2).GetComponent<Button>();
        a4 = actionBar.transform.GetChild(3).GetComponent<Button>();

        //Set player stats
        myAgility = 100;
        myStrength = 0;
        myIntellect = 0;
        myHealth = 100;

        //Set tile entity string
        Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
        Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        myTile.SetEntityString(tag);

        //Damage calculations
        myPhysicalDamage = 10 + (myStrength / 100 * 30);
        myMagicDamage = 10 + (myIntellect / 100 * 30);

        isDead = false;
        myTurn = false;

        //sp = new SpearAttack(c);
        //da = new DaggerAttack(c);

		// Alex: for testing
		//myClass.skills.Add (new Fireball (c));
		//myClass.skills.Add (new Lightning (c));
		//myClass.skills.Add (new ArcaneBlast (c));
		//myClass.skills.Add (new Sleep(c));

		//myClass.skills.Add (new Kick (c));
		//myClass.skills.Add (new Fog (c));
		//myClass.skills.Add (new IceArrow (c));
		//myClass.skills.Add (new BladeWind (c));

		myClass.skills.Add (new SpearAttack (c));
		myClass.skills.Add (new HealingLight (c));

		if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Archer)
        {
           
        }
        else if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice)
        {
			
        }
        else if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Thief)
        {
			
        }
        else if (myClass.PlayerClass == BaseCharacterClass.CharacterClasses.Squire)
        {
			
        }

        //Debug.Log(gameObject.name + " : current Weapon : " + weapon.getCurrentWeapon());
    }

    /*--------------------------------------------------------------------------------------------------------------------
     * Attack
     *@param tiles : The list of tiles to apply damage to.  
     *@param damage : The damage to be applied.      
     *---------------------------------------------------------------------------------------------------------------------
     */
    public void attack( List<string> tiles, bool isPhysical)
    {
        isAttacking = false;

        foreach ( string str in tiles)
        {
            Debug.Log("[ " + str + " ]" );
            Tile t = c.ConvertStringToTile(str);

            //Check if there is a player in the tile
            if ((t.GetEntityString().Contains("enemy") && tag.Contains("friendly")) || (t.GetEntityString().Contains("friendly") && tag.Contains("enemy")))
            {
                //Get transform of gameobject in tile
                float rayLength = 5f;
                Vector2 pos = c.GetPointInCellFromTileIndex(t.TileIndex);
                Vector2 worldPos = c.convertIndexToWorldPos((int)pos.x, (int)pos.y);
                Vector3 start = new Vector3(worldPos.x,t.GetHeight(),worldPos.y) + (rayLength * Vector3.up);
                RaycastHit hit;
                Debug.DrawRay(start, (-Vector3.up) * rayLength, Color.cyan, 20);
                if (Physics.Raycast(start, -Vector3.up, out hit, rayLength))
                {
					if (isPhysical)
						hit.transform.SendMessage ("applyDamage", myPhysicalDamage, SendMessageOptions.DontRequireReceiver);
					else {
						
						Debug.Log (hit.transform.position + "doing damage");
						hit.transform.SendMessage("applyDamage", myMagicDamage, SendMessageOptions.DontRequireReceiver);
					}
                        
                    
                }
                else
                {
                    throw new UnityException ("ERROR : getting transform of object in tile --> Produced by Toni");
                }
            }
        }
        exitAttackPhase();

    }
    /*--------------------------------------------------------------------------------------------------------------------
     * Apply Damage
     *@param theDamage: Damage to apply.
     *--------------------------------------------------------------------------------------------------------------------
     */
    public void applyDamage(int theDamage)
    {
        //Defense mitigates damage
        myHealth -= theDamage;
    }
    //Moves Player --------------------------------------------------------------------------------------------------------------------
    private void move()
    {
        int error = -9; //Starting error

        //Show paths
        if (showPaths)
        {
            Instantiate(Resources.Load("SquareProjector"), new Vector3(transform.position.x, -7.9f, transform.position.z), Quaternion.Euler(270, 0, 0));
            pf.showAvailableMoves(transform.position, myAgility);
            showPaths = false;
            waitForUserInput = true;
            
        }
        //Do untill mouse is clicked
        if(waitForUserInput) {
              
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                error = pf.FindPath(transform.position, hit.point);
            }
        }
        // Moves the Player if the Left Mouse Button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Error
            if (error == -9)
                return;
            if (error == -1)
            {
                showLabel4 = true;
                currentTime = Time.time;
                return;
            }

            animator.SetFloat("Run", 2.0f);
            waitForUserInput = false;
            //Change current tile str to ""
            Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
            Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
            myTile.SetEntityString("");
            //Destroy red prjector
            Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));
            //Destroy all green projectors
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GreenProj");
            for (var i = 0; i < objectsToDestroy.Length; i++)
                Destroy(objectsToDestroy[i]);

        }
        //Apply movement as long as movement path has elements
        if (!waitForUserInput && pf.mPath.Count > 0 )
        {
            Vector2 wp = c.GetPointInCellFromTileIndex(pf.mPath[0].TileIndex);
            Vector2 dest = c.convertIndexToWorldPos((int)wp.x, (int)wp.y);
            float height = pf.mPath[0].GetHeight();
            //Adjust height if using specific prefab
            if (tag == "enemyCharacter1")
                height += 0.05f;
            Vector3 d = new Vector3(dest.x, height, dest.y); //Vector3 -> The destination

            //4 possible locations of next tile in path
            Vector3 rot = transform.rotation.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            //Turn down
            if (startPos.z == d.z && startPos.x < d.x)
                rot.y = 90;
            //Turn up
            else if (startPos.z == d.z && startPos.x > d.x)
                rot.y = 270;
            //Turn right
            else if (startPos.x == d.x && startPos.z < d.z)
                rot.y = 0;
            //Turn left
            else if (startPos.x == d.x && startPos.z > d.z)
                rot.y = 180;
            transform.rotation = Quaternion.Euler(rot);

            //First movement : Make sure correct animation is playing before we start
            if (startTime == 0.0f)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && transform.rotation == Quaternion.Euler(rot))
                {
                    startTime = Time.time;
                    startPos = transform.position;
                }
            }
            if (startTime > 0.0f)
            {
                transform.position = Vector3.Lerp(startPos, d, (Time.time - startTime) / duration);

                //If we are at next tile remove it from movement list, and continue
                if (transform.position == d)
                {
                    startTime = Time.time;
                    startPos = transform.position;
                    pf.mPath.RemoveAt(0);
                }
                //Debug.Log("startpos = [" + startPos.x + "," + startPos.y + "," + startPos.z + "] currPos = " + transform.position.x + "," + transform.position.y + "," + transform.position.z + "] currentTime = " + (Time.time - startTime)); //DEBUG
            }
        }
        //Exiting movement phase
        if ((!waitForUserInput && pf.mPath.Count == 0))
        {
            animator.SetFloat("Run", 0.0f);
            exitMovePhase();
        }
    }
    //Exit phases functions--------------------------------------------------------------------------------------------------------------------
    public void exitMovePhase()
    {
        //Cant exit untill finished moving
        if (animator.GetFloat("Run") == 2.0f)
            return;
        currentTime = Time.time;
        animator.SetFloat("Run", 0.0f);
        startTime = 0.0f;
        showLabel1 = false;
        showLabel2 = true;
        isMoving = false;
        isTurning = true;
        showPaths = true;

        //Set entity strings
        foreach (Tile x in pf.availMoveTiles)
        {
            if ( x.EntityString == "canMoveHere")
                x.EntityString = "";
        }
		SC_SpellDuration.spellDuration = 3f;SC_SpellDuration.spellDuration = 3f;
        Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
        Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        myTile.SetEntityString(tag);

        //Destroy red prjector
        Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));
        //Destroy all green projectors
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GreenProj");
        for (var i = 0; i < objectsToDestroy.Length; i++)
            Destroy(objectsToDestroy[i]);
    }
    public void exitAttackPhase()
    {
        isAttacking = false;
        showLabel3 = false;
        currentTime = 0.0f;
        myTurn = false;
        actionBar.SetActive(false);
        abilityInfo.SetActive(false);
        a1.onClick.RemoveAllListeners();
        a2.onClick.RemoveAllListeners();
        a3.onClick.RemoveAllListeners();
        a4.onClick.RemoveAllListeners();
        next.onClick.RemoveAllListeners();
        Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));
    }
    //Check Button Clicks--------------------------------------------------------------------------------------------------------------------
    public void clickButton()
    {
        if (isMoving)
        {
            //Debug.Log("Exiting move phase");
            exitMovePhase();
            return;
        }
        else if (isTurning)
        {
          
            isTurning = false;
            isAttacking = true;
            currentTime = Time.time;
            showLabel2 = false;
            showLabel3 = true;
            showPaths = true;
            //Debug.Log("Exiting turn phase" + showPaths);
            return;
		}
        else if (isAttacking)
        {
            //Debug.Log("Exiting attack phase");
            exitAttackPhase();
            return;
        }

    }
    private void skillClicked(int i)
    {
        myClass.skills[i].updateLoc(this.gameObject);
        myClass.skills[i].attack();
    }

    //Rotate Player--------------------------------------------------------------------------------------------------------------------
    private void rotate()
    {
        if (showPaths && isTurning)
        {
            Instantiate(Resources.Load("SquareProjector"), new Vector3(transform.position.x, -7.9f, transform.position.z), Quaternion.Euler(270, 0, 0));
            showPaths = false;
        }
        
        //Rotating
        if (isTurning)
        {
            Vector3 rot = transform.rotation.eulerAngles;

            rot.x = 0;
            rot.z = 0;

            if (Input.GetKeyDown(KeyCode.RightArrow))
                rot.y += 90;

            transform.rotation = Quaternion.Euler(rot);
        }
    }

    /*
     * Attack
     * @param cwp = the current weapon equipped 
     */
    private void attack()
    {
        if (showPaths)
        {
            actionBar.SetActive(true);
            a1.onClick.AddListener(() => skillClicked(0));
            a2.onClick.AddListener(() => skillClicked(1));
            a3.onClick.AddListener(() => skillClicked(2));
            a4.onClick.AddListener(() => skillClicked(3));

            showPaths = false;
        }
    }

    //Display GUI--------------------------------------------------------------------------------------------------------------------
    public void OnGUI()
    {
        GUI.skin.font = myFont;
        if (showLabel1)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "1: It's your turn: To move (use mouse). Click 'Next' to continue...");
        else if (showLabel2)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "2: It's your turn: To rotate (use right arrow key to rotate clockwise). Click 'Next' to continue...");
        else if (showLabel3)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "3: It's your turn: To attack. Click 'Next' to finish.");
        else if (showLabel4) { }
            //GUI.Label(new Rect(0, Screen.height -20 , Screen.width, Screen.height), "HAHA! Thats not a valid move, funny guy! ( valid moves are shown in green )");
    }


    //Occurs every frame--------------------------------------------------------------------------------------------------------------------
    void Update()
    {

        if (isDead)
        {
            animator.SetBool("Dead", true);
        }

		if (myTurn)
        {

            //This happens only once
            if (currentTime == 0.0f)
            {
                next = transform.parent.parent.Find("Canvas/Next").GetComponent<Button>();
                next.onClick.AddListener(() => clickButton());
                isMoving = true;
                showPaths = true;
                currentTime = Time.time;
                showLabel1 = true;
            }

            //Handle Label expiration
            if (Time.time - currentTime > textTime)
            {
                if (showLabel1)
                {
                    showLabel1 = false;
                }
                if (showLabel2)
                {
                    showLabel2 = false;
                }
                if (showLabel3)
                {
                    showLabel3 = false;
                }
                if (showLabel4)
                {
                    showLabel4 = false;
                }

            }

            //MOVE
            if( isMoving )
                move();

            //ROTATE
            rotate();

            //ATTACK
            if (isAttacking)
                attack();
        }
    }
}