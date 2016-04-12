using UnityEngine;
using System.Collections;

/**
 * Moves towards target location with a set speed.
 * 
 * @author j@gamemechanix.io
 * @project SpellCraft
 * @copyright GameMechanix.io 2016
 **/
public class SC_Projectile : MonoBehaviour {

	[Header("Config")]
	public bool isMoving = false;
	public float moveSpeed = 5.0f;
	[HideInInspector]
	public Transform target;

	public MeshRenderer rend;
	float aTime;

	void Start() {
		rend = GetComponent<MeshRenderer> ();
		rend.enabled = false;
		aTime = Time.time;
	}


	void Update () {
		
		if ((Time.time - aTime > 1f) && isMoving) {
			rend.enabled = true;
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		}
	}
	
	public void FireProjectile (Transform target) {
		this.target = target;
		isMoving = true;
	}
}
