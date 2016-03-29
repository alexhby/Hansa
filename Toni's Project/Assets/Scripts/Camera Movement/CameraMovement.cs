using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float moveSpeed = 0.5f;
    public float rotateSpeed = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("a"))
        {
            transform.Translate((Vector3.left)*moveSpeed*Time.deltaTime );
        }
        if (Input.GetKey("d"))
        {
            transform.Translate((Vector3.right) * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("w"))
        {
            Vector3 f = transform.TransformDirection(Vector3.forward);
            f.y = 0;
            transform.Translate( f* moveSpeed * Time.deltaTime, Space.World);
        }  
        if (Input.GetKey("s"))
        {
            Vector3 f = transform.TransformDirection(Vector3.back);
            f.y = 0;
            transform.Translate(f * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("e"))
        {   
            transform.Rotate((new Vector3(0, 1)) *rotateSpeed* Time.deltaTime,Space.World);
        }
        if (Input.GetKey("q"))
        {
            transform.Rotate(-(new Vector3(0,1))  *rotateSpeed* Time.deltaTime,Space.World);
        }
        if (Input.GetKey("r"))
        {
            transform.Translate((new Vector3(0, 1, 0)) * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("f"))
        {
            transform.Translate(-(new Vector3(0, 1, 0)) * moveSpeed * Time.deltaTime, Space.World);
        }


    }
}
