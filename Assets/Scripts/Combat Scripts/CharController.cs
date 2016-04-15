using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TileDraw.Map;
using UnityEngine.EventSystems;
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
    public bool isSleep = false;
    public bool takeTwoTurns = false;
    public bool isAi = false;
    public bool isDead { get; set; }
    public bool myTurn;
    public bool isFriendly;
    //Booleans : GUI
    private bool showLabel1 = false;
    private bool showLabel2 = false;
    private bool showLabel3 = false;
    private bool showLabel4 = false;
    public bool showLabel5 = false;
    //Class Information
    public BaseCharacter myClass;
    public int myAgility;
    public int myStrength;
    public int myIntellect;
	public int mitigateDamage;
    public float myHealth;
    public float myMana;
    public int myDefense;
    //Other classes/Objects needed
    private PathFinding pf;
    private Cell c;
    private Transform friendly;
    //action/ability menu
    private GameObject actionBar;
    private GameObject abilityInfo;
    private Button a1;
    private Button a2;
    private Button a3;
    private Button a4;
    private Button next;
    //Attack Information
    public float myPhysicalDamage;
    public float myMagicDamage;
    public string weapon;
    private float takeTwoTurnsStartCD = 0.0f;
    private float takeTwoTurnsCooldown = 60;
    //Ai
    private int aiPhase = 0;
    private float aiWait = 0.0f;
    private int abilityToUse;
    private int rotdir = -1;
    private int maxRange;
    private int evalP = 0;

    void Start()
    {
        //Instantiate
        pf = transform.parent.GetComponentInParent<PathFinding>();
        c = transform.parent.parent.Find("(0,0)").GetComponent<Cell>();
        animator = GetComponent<Animator>();
        friendly = transform.parent.parent.Find("Friendly");

        //action menu
        actionBar = transform.parent.parent.Find("Canvas/Panel").gameObject;
        abilityInfo = transform.parent.parent.Find("Canvas/Ability Description").gameObject;
        a1 = actionBar.transform.GetChild(0).GetComponent<Button>();
        a2 = actionBar.transform.GetChild(1).GetComponent<Button>();
        a3 = actionBar.transform.GetChild(2).GetComponent<Button>();
        a4 = actionBar.transform.GetChild(3).GetComponent<Button>();

        //Set player stats -------------------------------------------------------------------------------------------
        int order = int.Parse (gameObject.tag.Substring(0,1));
        if (gameObject.transform.parent.name == "Friendly") {
            myClass = SpawnCharacters.friendlyList [order];
        } else {
            myClass = SpawnCharacters.enemyList [order];
        }

        if (transform.parent.gameObject.name == "Friendly")
            isFriendly = true;
        else
            isFriendly = false;

        
        myAgility = myClass.Agility;
        myStrength = myClass.Strength;
        myIntellect = myClass.Agility;
        myHealth = myClass.Health;
        myMana = myClass.Mana;
        myDefense = myClass.Defense;

        //-------------------------------------------------------------------------------------------
        Debug.Log("QUEST NAME IS : " + WorldInformation.CurrentQuest.QuestName);

        //Set tile entity string
        Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
        Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        myTile.SetEntityString(tag);

        //Damage calculations
        myPhysicalDamage = 10 + (myStrength / 100 * 30);
        myMagicDamage = 10 + (myIntellect / 100 * 30);
		mitigateDamage = 2 + (myDefense / 100 * 30);

        isDead = false;
        myTurn = false;

        // Add Abilities
        if ((int)myClass.PlayerClass % 4 == 0)
        {
            // Squire
            myClass.skills.Add (new SpearAttack (c));
            myClass.skills.Add (new HealingLight (c));
			myClass.skills.Add (new SpearAttack (c));
			myClass.skills.Add (new HealingLight (c));
            weapon = "Spear";
        }
        else if ((int)myClass.PlayerClass % 4 == 1)
        {
            // Apprentice
            myClass.skills.Add (new Fireball (c));
            myClass.skills.Add (new Lightning (c));
            myClass.skills.Add (new ArcaneBlast (c));
            myClass.skills.Add (new Sleep(c));
            weapon = "Spell Book";
        }
        else if ((int)myClass.PlayerClass % 4 == 2)
        {
            // Thief
            myClass.skills.Add (new Stab (c));
            myClass.skills.Add (new DoubleStab (c));
            myClass.skills.Add (new LegSweep (c));
            myClass.skills.Add (new TwoTurn (c));
            weapon = "Daggers";
        }
        else if ((int)myClass.PlayerClass % 4 == 3)
        {
            //Archer
            myClass.skills.Add (new Kick (c));
            myClass.skills.Add (new Fog (c));
            myClass.skills.Add (new IceArrow (c));
            myClass.skills.Add (new BladeWind (c));
            weapon = "Bow";
        }

        if (gameObject.transform.parent.name == "Enemies")
            isAi = true;
        else
            isAi = false;

        //Debug.Log(gameObject.name + " : current Weapon : " + weapon.getCurrentWeapon());
    }

    /*--------------------------------------------------------------------------------------------------------------------
     * Attack
     *@param tiles : The list of tiles to apply damage to.  
     *@param damage : The damage to be applied.      
     *---------------------------------------------------------------------------------------------------------------------
     */
    
    public void attack(List<string> tiles, bool isPhysical, string spellEffect)
    {
        isAttacking = false;
        actionBar.SetActive(false);
        abilityInfo.SetActive(false);

        foreach (string str in tiles)
        {
            //Debug.Log("[ " + str + " ]" );
            Tile t = c.ConvertStringToTile(str);

            //Check if there is a player in the tile
            if ((t.GetEntityString().Contains("enemy") && tag.Contains("friendly")) || (t.GetEntityString().Contains("friendly") && tag.Contains("enemy")))
            {
                //Get transform of gameobject in tile
                float rayLength = 5f;
                Vector2 pos = c.GetPointInCellFromTileIndex(t.TileIndex);
                Vector2 worldPos = c.convertIndexToWorldPos((int)pos.x, (int)pos.y);
                Vector3 start = new Vector3(worldPos.x, t.GetHeight(), worldPos.y) + (rayLength * Vector3.up);
                RaycastHit hit;
                Debug.DrawRay(start, (-Vector3.up) * rayLength, Color.cyan, 20);
                if (Physics.Raycast(start, -Vector3.up, out hit, rayLength))
                {
                    if (spellEffect == "sleep")
                    {
                        hit.transform.GetComponent<CharController>().isSleep = true;
                    }
                    else if (isPhysical)
                        hit.transform.SendMessage("applyDamage", myPhysicalDamage, SendMessageOptions.DontRequireReceiver);
                    else
                        hit.transform.SendMessage("applyDamage", myMagicDamage, SendMessageOptions.DontRequireReceiver);

                }
                else
                {
                    throw new UnityException("ERROR : getting transform of object in tile --> Produced by Toni");
                }
            }
        }
    }

    /*--------------------------------------------------------------------------------------------------------------------
     * Apply Damage
     *@param theDamage: Damage to apply.
     *--------------------------------------------------------------------------------------------------------------------
     */
    public void applyDamage(int theDamage)
    {
        //Defense mitigates damage
		if (theDamage > mitigateDamage )
			myHealth -= (theDamage - mitigateDamage);
    }
    
    //Find computer target --------------------------------------------------------------------------------------------------------------------
    private int computerFindMove(int p)
    {
        Vector3 target = new Vector3(0, 0, 0);
        Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
        Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
        Tile tTile = myTile;
        int distance = 999999;
        float minHealth = 999999;

        //Get the player with the least health
        for (int i = p; i < friendly.childCount; i++)
        {
            CharController ch = friendly.GetChild(i).GetComponent<CharController>();
            if (ch.myHealth < minHealth)
            {
                minHealth = ch.myHealth;
                Vector3 pos = friendly.GetChild(i).transform.position;
                Vector2 tPointInCell = c.convertWorldPosToIndex(pos.x, pos.z);
                tTile = c.GetTileFromPointInCell((int)tPointInCell.x, (int)tPointInCell.y);
                Debug.Log("Attacking player with least health at tile: " + tPointInCell);
            }
        }

        if (tTile == myTile)
        {
            Debug.Log("Error target not assigned");
            return -1;
        }

        maxRange = 0;
        int minRange = 99;

        //Decide which attack to use
        for (int j = 0; j < 4; j++)
        {
            //use attack with max range
            if (myClass.skills[j].maxRange > maxRange )
            {
                maxRange = myClass.skills[j].maxRange;
                minRange = myClass.skills[j].minRange;
                abilityToUse = j;
                //Debug.Log("Ai is using ability number : " + j);
                continue;
            }
            System.Random rand = new System.Random();
            if (myClass.skills[j].maxRange == maxRange && rand.Next(0, 100) < 25)
            {
                maxRange = myClass.skills[j].maxRange;
                minRange = myClass.skills[j].minRange;
                abilityToUse = j;
                //Debug.Log("Ai is using ability number : " + j);
            }
        }

        //Check if we can move to max range: Favor given to neighbor with smaller index
        for (int j = 0; j < 4; j++)
        {
            Tile t = myTile;
            try
            {
                t = c.getTile(tTile, j, maxRange);
            }
            catch(Exception e)
            {
                Debug.Log("Error from AI movement, no neighbour, continue! : " + e.ToString());
                if ( j != 3)
                    continue;
            }

            if (t.EntityString == "canMoveHere")
            {
         
                Vector2 gridPos = c.GetPointInCellFromTileIndex(t.TileIndex);
                Vector2 worldPos = c.convertIndexToWorldPos((int)gridPos.x, (int)gridPos.y);
                //Debug.Log("Found tile to move to at :" + worldPos + " at range " + maxRange);
                target = new Vector3(worldPos.x, t.GetHeight(), worldPos.y);
                pf.FindPath(transform.position, target, isAi, ""); //find the path
                return j;
            }
            //Debug.Log("j is: " + j);
            if (j == 3 )
            {
                if (maxRange > minRange)
                {
                    maxRange -= 1;
                    //Debug.Log("Decreasing max range trying to find tile again");
                    j = -1;
                }
                else if (evalP != friendly.childCount - 1)
                {
                    //Debug.Log("Checking other targets");
                    evalP += 1;
                    return computerFindMove(evalP);
                }
                    
            }
        }
        //Debug.Log("Moving as close as possible to target");
        //Move as close as possible
        int k = -1;
        foreach (string neighbour in tTile.Neighbours)
        {
            k++;
            //Debug.Log("k: " + k);
            if (neighbour == "None")
                continue;
            Tile current = c.ConvertStringToTile(neighbour);

            if (current.EntityString.Contains("Player") || current.EntityIndex != -1)
                continue;

            if ((pf.GetDistance(myTile, current) < distance))
            {
                //Debug.Log("closest tile updated");
                distance = pf.GetDistance(myTile, current);
                Vector2 gridPos = c.GetPointInCellFromTileIndex(current.TileIndex);
                Vector2 worldPos = c.convertIndexToWorldPos((int)gridPos.x, (int)gridPos.y);
                target = new Vector3(worldPos.x, current.GetHeight(), worldPos.y);
                //Debug.Log("Moving to : " + target);
                break;
            }
        }

        pf.FindPath(transform.position, target, isAi, "aiMoveFar");
        return k;
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
            return;
        }

        //Do untill mouse is clicked
        if (waitForUserInput && !isAi)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                error = pf.FindPath(transform.position, hit.point, isAi, "");
            }
        }
        //Display ai movement
        if (isAi && aiPhase == 0)
        {
            if (aiWait == 0.0f)
            {
                aiWait = Time.time;
                
            }
            rotdir = computerFindMove(evalP);
            error = rotdir;
            if (Time.time - aiWait < 2)
                return;
            else
                aiPhase = 1;

        }
        // Moves the Player if the Left Mouse Button was clicked
		if ( ( Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject ())|| aiPhase == 1)
        {
            //Errors:
            //No path
            if (error == -9)
                return;
            //Bad path
            if (error == -1)
            {
                showLabel4 = true;
                currentTime = Time.time;
                return;
            }
            if (isAi)
                aiPhase += 1;

            animator.SetFloat("Run", 2.0f);
            waitForUserInput = false;
            //Change current tile entity str to ""
            Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
            Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
            myTile.SetEntityString("");
            //Destroy red projector
            Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));
            //Destroy all green projectors
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GreenProj");
            for (var i = 0; i < objectsToDestroy.Length; i++)
                Destroy(objectsToDestroy[i]);

        }
        //Apply movement as long as movement path has elements
        if (!waitForUserInput && pf.mPath.Count > 0)
        {
            Vector2 wp = c.GetPointInCellFromTileIndex(pf.mPath[0].TileIndex);
            Vector2 dest = c.convertIndexToWorldPos((int)wp.x, (int)wp.y);
            float height = pf.mPath[0].GetHeight();
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
            if (x.EntityString == "canMoveHere")
                x.EntityString = "";
        }

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
        if (!takeTwoTurns)
        {
            Debug.Log("Ending turn");
            myTurn = false;
        }
        if (takeTwoTurns)
        {
            takeTwoTurns = false;
            takeTwoTurnsStartCD = Time.time;
        }

        startTime = 0.0f;
        isAttacking = false;
        showLabel3 = false;
        currentTime = 0.0f;
        aiPhase = 0;
        aiWait = 0.0f;
        abilityToUse = -1;
        evalP = 0;
        rotdir = -1;
        actionBar.SetActive(false);
        abilityInfo.SetActive(false);
        a1.onClick.RemoveAllListeners();
        a2.onClick.RemoveAllListeners();
        a3.onClick.RemoveAllListeners();
        a4.onClick.RemoveAllListeners();
        next.onClick.RemoveAllListeners();
        Destroy(GameObject.FindGameObjectWithTag("SquareProjector"));



        
    }
    public void exitTurningPhase()
    {
        isTurning = false;
        isAttacking = true;
        currentTime = Time.time;
        showLabel2 = false;
        showLabel3 = true;
        showPaths = true;
    }
    //Check Button Clicks--------------------------------------------------------------------------------------------------------------------
    public void clickButton()
    {
        if (isAi)
            return;
        if (isMoving)
        {
            //Debug.Log("Exiting move phase");
            exitMovePhase();
            return;
        }
        else if (isTurning)
        {
            exitTurningPhase();
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

            if (isAi)
            {
                Debug.Log("AI is rotating");
                if (rotdir == 0)
                {
                    rot.y = 180;
                }
                if (rotdir == 1)
                {
                    rot.y = 90;
                }
                if (rotdir == 2)
                {
                    rot.y = 0;
                }
                if (rotdir == 3)
                {
                    rot.y = 270;
                }

            }

            rot.x = 0;
            rot.z = 0;

            if (Input.GetKeyDown(KeyCode.RightArrow) && !isAi)
                rot.y += 90;

            transform.rotation = Quaternion.Euler(rot);

            if (isAi)
                exitTurningPhase();
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
            if ( !isAi )
                actionBar.SetActive(true);
            a1.onClick.AddListener(() => skillClicked(0));
            a2.onClick.AddListener(() => skillClicked(1));
            a3.onClick.AddListener(() => skillClicked(2));
            a4.onClick.AddListener(() => skillClicked(3));

            if (isAi)
            {
                Vector2 myPointInCell = c.convertWorldPosToIndex(transform.position.x, transform.position.z);
                Tile myTile = c.GetTileFromPointInCell((int)myPointInCell.x, (int)myPointInCell.y);
                Debug.Log("Range of ai attack is: " + maxRange);
                try
                {
                    int dir = 0;
                    //rotdir 0 -> 2, 1 -> 3, 2 -> 0, 3 -> 1
                    if (rotdir == 0) dir = 2;
                    else if (rotdir == 1) dir = 3;
                    else if (rotdir == 2) dir = 0;
                    else if (rotdir == 3) dir = 1;
                    Debug.Log("Dir is " + dir);
                    if (c.getTile(myTile, dir, maxRange).EntityString.Contains("friendly"))
                    {
                        skillClicked(abilityToUse);
                        Debug.Log("Ai is attacking!");
                    }
                        
                }
                catch (Exception e)
                {
                    Debug.Log("Error Ai attack, nothing to attack, continue! : " + e.ToString());
                }

                Debug.Log("AI DECIDES TO USE ABILITY NUMBER " + abilityToUse);
                exitAttackPhase();
         
            }

            showPaths = false;
        }
    }

    //Display GUI--------------------------------------------------------------------------------------------------------------------
    public void OnGUI()
    {
        GUI.skin.font = myFont;
        if (showLabel1 && !isAi)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "1: It's your turn: To move (use mouse). Click 'Next' to continue...");
        else if (showLabel2 && !isAi)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "2: It's your turn: To rotate (use right arrow key to rotate clockwise). Click 'Next' to continue...");
        else if (showLabel3 && !isAi)
            GUI.Label(new Rect(0, Screen.height - 20, Screen.width, Screen.height), "3: It's your turn: To attack. Click 'Next' to finish.");
        else if (showLabel5)
            GUI.Label(new Rect(0, Screen.height - 40, Screen.width, Screen.height), "TakeTwoTurns has a " + takeTwoTurnsCooldown + "second cooldown. Time spent : " + (Time.time - takeTwoTurnsStartCD));
    }


    //Occurs every frame--------------------------------------------------------------------------------------------------------------------
    void Update()
    {

        if (isDead)
        {
            animator.SetBool("Dead", true);
            myTurn = false;
        }

        if (isSleep && myTurn)
        {
            Debug.Log("You were a sleep and skipped your turn!");
            myTurn = false;
            isSleep = false;
        }

        if (myTurn)
        {
            if (takeTwoTurnsStartCD != 0.0f)
            {
                if (Time.time - takeTwoTurnsStartCD < takeTwoTurnsCooldown)
                {
                    showLabel5 = true;
                }
                else
                {
                    showLabel5 = false;
                    takeTwoTurnsStartCD = 0.0f;
                }
                    
            }

            //This happens only once
            if (currentTime == 0.0f)
            {
                next = transform.parent.parent.Find("Canvas/Next").GetComponent<Button>();

                for (int i = 0; i < actionBar.transform.childCount; i++)
                {
                    actionBar.transform.GetChild(i).GetComponent<PointerEventsControl>().charac = transform;
                }

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
            if (isMoving)
            {
                move();
                return;
            }
            //ROTATE
            if (isTurning)
            {
                rotate();
                return;
            }        
            //ATTACK
            if (isAttacking)
            {
                attack();
                return;
            }
            Debug.Log("Delayng end of turn");
            if (startTime == 0.0f)
            {
                startTime = Time.time;
            }
            if (Time.time - startTime > 5)
            {
                exitAttackPhase();
            }

        }

    }
}