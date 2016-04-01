using UnityEngine;
using System.Collections.Generic;
using TileDraw.Map;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{

    private Transform myTransform;
    public Animator animator;
    public GameObject spell;
    public GameObject[] spells;     //spells the player can cast
    public enum character { Archer, Mage, Thief, Warrior }
    public enum weapons { sword, spear, crossbow, tome, none };
    //Text
    public Font myFont;
    private float currentTime = 0.0f; //Time text starts to display
    private float textTime = 5.0f; //Time text is displayed for
    //Movement
    private Vector3 startPos;
    public float startTime = 0.0f; //Time start to move
    public float duration = 10f; //Moving duration
    private bool showPaths;
    //Booleans
    private bool isMoving = false;
    private bool isTurning = false;
    private bool isAttacking = false;
    public bool isDead { get; set; }
    public bool myTurn { get; set; }
    private bool showLabel1 = false;
    private bool showLabel2 = false;
    private bool showLabel3 = false;
    private bool showLabel4 = false; 
    //Class Information
    public int myClass;
    private int myAgility;
    private int myStrength;
    private int myIntellect;
    //Other classes needed
    private PathFinding pf;
    private Cell c;
    //Path to shield and melee objects
    private string melee;
    private string shield;
    private string melee1 = "Root2/Spine/Chest/r_clavicle/r_shoulder/r_elbow/r_wrist/r_hand/Melee";
    //private string melee3 = "mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/Melee";
    private string melee2 = "Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Melee";
    private string shield1 = "Root2/Spine/Chest/l_clavicle/l_shoulder/l_elbow/l_wrist/Shield Mount";
    private string shield2 = "Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Shield Mount";
    //private string shield3 = "mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm/mixamorig:LeftHand/Shield Mount";
    //Attack
    public float maxDistance = 5;    //the max movement range for this class (base = 5)
    public float distance;           //distance from gameObject ( in front )
    private float physicalDamage;
    private float magicDamage;
    public float myHealth;


    void Start()
    {

        spells = Resources.LoadAll<GameObject>("Spells");   //load spells from resources into spell array
        pf = transform.parent.GetComponentInParent<PathFinding>();
        c = transform.parent.parent.Find("(0,0)").GetComponent<Cell>();
        animator = GetComponent<Animator>();
        myTransform = transform;

        //SET PLAYER STATISTICS
        myAgility = 0;
        myStrength = 0;
        myIntellect = 0;
        myHealth = 100;

        //DAMAGE CALCULATIONS
        physicalDamage = 10 + (myStrength / 100 * 30);
        physicalDamage = 10 + (myIntellect / 100 * 30);

        isDead = false;
        myTurn = false;
        animator.SetBool("grounded", true);

        if (gameObject.tag == "Enemy")
        {
            melee = melee2;
            shield = shield2;
        }
        else if (gameObject.tag == "Friendly")
        {
            melee = melee1;
            shield = shield1;
        }

        Weapons weapon = transform.Find(melee).GetComponent<Weapons>();

        if (myClass == (int)character.Archer)
        {
            weapon.setCurrentWeapon((int)weapons.crossbow);
        }
        else if (myClass == (int)character.Mage)
        {
            weapon.setCurrentWeapon((int)weapons.tome);
        }
        else if (myClass == (int)character.Thief)
        {
            weapon.setCurrentWeapon((int)weapons.sword);
            transform.Find(shield).GetComponent<ShieldManager>().hasSword = true;
        }
        else if (myClass == (int)character.Warrior)
        {
            weapon.setCurrentWeapon((int)weapons.sword);
            transform.Find(shield).GetComponent<ShieldManager>().hasShield = true;
            
        }

        Debug.Log("Current Weapon " + weapon.getCurrentWeapon());
    }
    //needs work
    void attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(myTransform.position, myTransform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawRay(myTransform.position, myTransform.TransformDirection(Vector3.forward) * 2000, Color.magenta, 10);
            distance = hit.distance;
            if (distance < maxDistance)
            {
                hit.transform.SendMessage("applyDamage", physicalDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    //needs work
    void applyDamage(int theDamage)
    {
        myHealth -= physicalDamage;

        Transform sys = myTransform.parent;
        Image hp = sys.Find("Canvas/Target/HealthBar").GetComponent<Image>();
        float fillAmount = (float)(myHealth)/100.0f;
        hp.fillAmount = fillAmount;

        if (myHealth <= 0)
        {
            myTransform.GetComponent<CharController>().isDead = true;
            if( gameObject.tag == "Enemy")
                myTransform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().setCurrentState(5);
            else if (gameObject.tag == "Friendly")
                myTransform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().setCurrentState(4);
        }
    }
    //MOVES PLAYER
    private void move()
    {
        int error = 0;

        //Show paths
        if ( showPaths)
        {
            pf.showAvailableMoves(transform.position, myAgility);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                error = pf.FindPath(myTransform.position, hit.point);
            }
        }
        // Moves the Player if the Left Mouse Button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Error
            if (error == -1)
            {
                showLabel4 = true;
                currentTime = Time.time;
                return;
            }    
            animator.SetFloat("Run", 2.0f);
            showPaths = false;
            //Destroy all green projectors
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GreenProj");
            for (var i = 0; i < objectsToDestroy.Length; i++)
                Destroy(objectsToDestroy[i]);

        }
        //Apply movement as long as movement path has elements
        if (!showPaths && pf.mPath.Count > 0 )
        {
            Vector2 wp = c.GetPointInCellFromTileIndex(pf.mPath[0].TileIndex);
            Vector2 dest = c.convertIndexToWorldPos((int)wp.x, (int)wp.y);
            float height = pf.mPath[0].GetHeight();
            if (tag == "Enemy")
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
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run CB") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run SP") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run SW"))
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
        if (Input.GetKeyDown("p") || (!showPaths && pf.mPath.Count == 0))
        {
            currentTime = Time.time;
            animator.SetFloat("Run", 0.0f);
            startTime = 0.0f;
            showLabel1 = false;
            showLabel2 = true;
            isMoving = false;
            isTurning = true;

            foreach (Tile t in pf.availMoveTiles)
            {
                t.EntityString = "";
            }
            //Destroy all green projectors
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("GreenProj");
            for (var i = 0; i < objectsToDestroy.Length; i++)
                Destroy(objectsToDestroy[i]);


        }
    }

    //ROTATE PLAYER 
    private void rotate()
    {
        //End Rotation phase
        if (Input.GetKeyDown("n") && isMoving == false)
        {
            isTurning = false;
            isMoving = false;
            isAttacking = true;
            currentTime = Time.time;
            showLabel2 = false;
            showLabel3 = true;
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
    private void attack(int cwp)
    {

        //Attack animations
        if (Input.GetKeyDown(KeyCode.X))
        {
            attack();
            if (cwp == 0)
            {
                animator.Play("Attack SW");
            }
            else if (cwp == 1)
            {
                animator.Play("Attack SP");
            }
            else if (cwp == 2)
            {
                //Shoot arrow at player
                animator.Play("Attack CB");
                //Instantiate prefab and launch forwards for certain duration
                //GameObject arrow;
            }
            else if (cwp == 3)
            {
                animator.Play("Cast");
                spell = Instantiate(spells[0], transform.position, transform.rotation) as GameObject;
            }

            isAttacking = false;
            showLabel3 = false;
            currentTime = 0.0f;
            myTurn = false;

        }
    }
    //Display Text Messages
    void OnGUI()
    {
        GUI.skin.font = myFont;
        if (showLabel1)
            GUI.Label(new Rect(Screen.width / 3 * 1.15f, Screen.height / 4, Screen.width, Screen.height), "1: It's your turn: To move (use mouse)\nPress 'p' to skip.");
        if (showLabel2)
            GUI.Label(new Rect(Screen.width / 3 * 1.15f, Screen.height / 4 + 25, Screen.width, Screen.height), "2: It's your turn: To rotate (use right arrow key to rotate CW)\nPress 'n' to finish.");
        if (showLabel3)
            GUI.Label(new Rect(Screen.width / 3 * 1.25f, Screen.height / 4 + 50, Screen.width, Screen.height), "3: It's your turn: To attack");
        if (showLabel4)
            GUI.Label(new Rect(0, Screen.height -20 , Screen.width, Screen.height), "Thats not a valid movement ( valid moves are shown in green )");
    }


    //Occurs every frame
    void Update()
    {

        //load current weapon from weapon class

        int cwp = transform.Find(melee).GetComponent<Weapons>().getCurrentWeapon();

        animator.SetBool("IdleSW", false);
        animator.SetBool("IdleSP", false);
        animator.SetBool("IdleCB", false);
        animator.SetBool("Idle", false);

        //Correct Idle animation
        if (cwp == 0)
        {
            animator.SetBool("IdleSW", true);
        }
        else if (cwp == 1)
        {
            animator.SetBool("IdleSP", true);
        }
        else if (cwp == 2)
        {
            animator.SetBool("IdleCB", true);
        }
        else if (cwp == 4)
        {
            animator.SetBool("Idle", true);
        }
        if (isDead)
        {
            animator.SetBool("Dead", true);
        }

        if (myTurn)
        {

            //check for jump
            if (Input.GetKeyDown("space"))
            {
                animator.SetBool("Jump", true);
            }

            //This happens only once
            if (currentTime == 0.0f)
            {
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
                attack(cwp);
     
        }
    }
}