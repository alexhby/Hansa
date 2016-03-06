using UnityEngine;
using System.Collections;

public class CharController: MonoBehaviour
{

    private Transform myTransform;             
    private Vector3 destinationPosition;        
    private float destinationDistance;
    public Animator animator;
    public GameObject spell;
    public GameObject[] spells;           

    void Start()
    {
        spells = Resources.LoadAll<GameObject>("Spells");
        animator = GetComponent<Animator>();
        myTransform = transform;
        destinationPosition = myTransform.position;
    }

    void FixedUpdate()
    {

        destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);



        if (destinationDistance < 1)
        {
            animator.SetFloat("Run", 0.0f);
        }
        else if (destinationDistance > 1)
        {
            animator.SetFloat("Run", 2.0f);
        }
        if (Input.GetKeyDown("space") && animator.GetFloat("Run") > 0)
        {
            animator.SetBool("Jump", true);
        }

        Debug.Log("Dest:" + destinationDistance);

        if (Input.GetKeyDown("c"))
        {
            //animator.SetBool("Crouch", true);
            animator.Play("Crouch");
        }
        if (Input.GetKeyDown("x"))
        {
            //animator.SetBool("Cast", true);
            animator.Play("Cast");
        }
        if (Input.GetKeyDown("q"))
        {
            //animator.SetBool("Cast", true);
            animator.Play("Cast");
            spell = Instantiate(spells[1], transform.position, transform.rotation) as GameObject;
        }
        if (Input.GetKeyDown("d"))
        {
            animator.SetBool("Dead", true);
        }
        // Moves the Player if the Left Mouse Button was clicked
        if (Input.GetMouseButtonDown(0))
        {

            Plane playerPlane = new Plane(Vector3.up, myTransform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hit = 0.0f;

            if (playerPlane.Raycast(ray, out hit))
            {
                Vector3 targetPoint = ray.GetPoint(hit);
                destinationPosition = ray.GetPoint(hit);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                myTransform.rotation = targetRotation;
            }
        }
        animator.SetBool("idle", true);
    }
}