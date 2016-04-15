﻿using UnityEngine;
using System.Collections;

public class MainMapCameraMovement : MonoBehaviour {

    public float moveSpeed = 100f;
    public float rotateSpeed = 100f;
	
	//the camera movement only operates on the XZ plane and doesn't ever increase or decrease in the y axis
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Left"))
        {
            transform.Translate((Vector3.left)*moveSpeed*Time.deltaTime );
        }
        if (Input.GetButton("Right"))
        {
            transform.Translate((Vector3.right) * moveSpeed * Time.deltaTime);
        }
        if (Input.GetButton("Forward"))
        {
            
            Vector3 f = transform.TransformDirection(Vector3.forward);
            f.y = 0;
            transform.Translate( f* moveSpeed * Time.deltaTime, Space.World);
        }  
        if (Input.GetButton("Back"))
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
            //Vector3 f = transform.TransformDirection(Vector3.back);
            //f.y = 0;
            transform.Rotate(-(new Vector3(0,1))  *rotateSpeed* Time.deltaTime,Space.World);
        }


    }
}
