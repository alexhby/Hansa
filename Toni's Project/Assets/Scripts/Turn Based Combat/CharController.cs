using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{

    private Transform myTransform;
    private Vector3 destinationPosition;    //coordinates of location player wants to move to   
    private float destinationDistance;
    private float accuracyMovement = 0.8f;
    private float currentTime = 0.0f;
    private float textTime = 5.0f;
    private int maxRotations = 5;
    private bool isMoving = false;
    private bool isTurning = false;
    private bool isAttacking = false;

    public Animator animator;
    public GameObject spell;
    public GameObject[] spells;     //spells the player can cast
    public bool myTurn { get; set; }
    public bool isDead { get; set; }

    public enum character { Archer, Mage, Thief, Warrior }
    public enum weapons { sword, spear, crossbow, tome, none };
    public int myClass;

    private string melee;
    private string shield;
    private string melee1 = "Root2/Spine/Chest/r_clavicle/r_shoulder/r_elbow/r_wrist/r_hand/Melee";
    private string melee3 = "mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/Melee";
    private string melee2 = "Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Melee";
    private string shield1 = "Root2/Spine/Chest/l_clavicle/l_shoulder/l_elbow/l_wrist/Shield Mount";
    private string shield2 = "Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Shield Mount";
    private string shield3 = "mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/mixamorig:LeftForeArm/mixamorig:LeftHand/Shield Mount";

    public float maxDistance = 2;    //the max damage range for this class 
    public float distance;    //distance from gameObject ( in front )
    public int damage = 25;
    public int Health = 100;


    void Start()
    {

        spells = Resources.LoadAll<GameObject>("Spells");   //load spells from resources into spell array
        animator = GetComponent<Animator>();
        myTransform = transform;
        destinationPosition = myTransform.position;

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
        else
        {
            melee = melee3;
            shield = shield3;
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

    void attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(myTransform.position, myTransform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawRay(myTransform.position, myTransform.TransformDirection(Vector3.forward) * 2000, Color.magenta, 10);
            distance = hit.distance;
            if (distance < maxDistance)
            {
                hit.transform.SendMessage("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void applyDamage(int theDamage)
    {
        Health -= damage;

        Transform sys = myTransform.parent.parent;
        Image hp = sys.Find("Canvas/Target/HealthBar").GetComponent<Image>();
        float fillAmount = (float)(Health)/100.0f;
        hp.fillAmount = fillAmount;

        if (Health <= 0)
        {
            myTransform.GetComponent<CharController>().isDead = true;
            if( gameObject.tag == "Enemy")
                myTransform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().setCurrentState(5);
            else if (gameObject.tag == "Friendly")
                myTransform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().setCurrentState(4);
        }
    }

    //Rotate player (up,down,left,right)
    void rotate()
    {

        if (maxRotations == 0 || Input.GetKeyDown(KeyCode.Keypad2))
        {
            isTurning = false;
            isMoving = false;
            isAttacking = true;
            currentTime = Time.time;
            showLabel2 = false;
            showLabel3 = true;
        }
            

        if (isTurning)
        {
            Vector3 rot = transform.rotation.eulerAngles;

            rot.x = 0;
            rot.z = 0;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rot.y = 0;
                maxRotations--;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rot.y = 180;
                maxRotations--;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rot.y = 270;
                maxRotations--;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rot.y = 90;
                maxRotations--;
            }

            transform.rotation = Quaternion.Euler(rot);
        }        
    }

    //Moves player
    void move()
    {
        // Still need to restrict player movement based on statistics
        // Moves the Player if the Left Mouse Button was clicked
        if (Input.GetMouseButtonDown(0))
        {

            Plane playerPlane = new Plane(Vector3.up, myTransform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hit = 0.0f;

            Debug.Log("Dest:" + destinationDistance);

            //Rotate player towards mouse click
            if (playerPlane.Raycast(ray, out hit))
            {
                Vector3 targetPoint = ray.GetPoint(hit);
                destinationPosition = ray.GetPoint(hit);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                myTransform.rotation = targetRotation;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            currentTime = Time.time;
            showLabel1 = false;
            showLabel2 = true;
            isMoving = false;
            isTurning = true;
            
        }
    }

    /*
     * Attack animations 
     * @param cwp = the current weapon equipped 
     */
    void attack(int cwp)
    {

        //Attack animations
        if (Input.GetKeyDown(KeyCode.F))
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
                GameObject arrow;
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

    private bool showLabel1;
    private bool showLabel2;
    private bool showLabel3;

    void OnGUI()
    {
        if (showLabel1)
            GUI.Label(new Rect(Screen.width / 3 * 1.15f, Screen.height / 4, Screen.width, Screen.height), "It's your turn: To move (use mouse)");
        if (showLabel2)
            GUI.Label(new Rect(Screen.width / 3 * 1.15f, Screen.height / 4, Screen.width, Screen.height), "It's your turn: To rotate (use arrow keys)");
        if (showLabel3)
            GUI.Label(new Rect(Screen.width / 3 * 1.25f, Screen.height / 4, Screen.width, Screen.height), "It's your turn: To attack");
    }


    //Occurs every frame
    void Update()
    {

        //load current weapon from weapon class

        int cwp = transform.Find(melee).GetComponent<Weapons>().getCurrentWeapon();

        destinationDistance = Vector3.Distance(destinationPosition, myTransform.position); //update distance

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
            //Check for run, jump, crouch, death
            if (destinationDistance < accuracyMovement)
            {
                animator.SetFloat("Run", 0.0f);
                
                //Automatically adjust players rotation to (up,down,left,right)
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Crossbow")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Spear") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Swords"))
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.z = 0;
                    rot.x = 0;

                    if (rot.y < 90)
                        rot.y = 0;
                    else if (rot.y < 180)
                        rot.y = 90;
                    else if (rot.y < 270)
                        rot.y = 180;
                    else
                        rot.y = 270;

                    transform.rotation = Quaternion.Euler(rot);

                    rotate();
                }
            }
            else if (destinationDistance > accuracyMovement)
            {
                animator.SetFloat("Run", 2.0f);
            }
            if (Input.GetKeyDown("space") && animator.GetFloat("Run") > 0)
            {
                animator.SetBool("JumpRun", true);
            }
            if (Input.GetKeyDown("space"))
            {
                animator.SetBool("Jump", true);
            }
            if (animator.GetBool("Idle") && Input.GetKeyDown("c"))
            {
                animator.Play("Crouch");
            }


            //Three parts to character's turn:

            //1 : Move
            if (currentTime == 0.0f)
            {
                isMoving = true;
                currentTime = Time.time;
                showLabel1 = true;
            }
            //2 : Rotate
            //3 : Attack

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
                
            }

            //MOVE
            if( isMoving )
                move();

            //ATTACK (my turn is over)
            //isAttacking = true;
            if (isAttacking)
            {
                attack(cwp);
                
            }
                

        }
    }
}