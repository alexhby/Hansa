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

	float aTime;

	void Start() {
		aTime = Time.time;
	}


	void Update () {

		// wait one second to shoot
		if ((Time.time - aTime > 1f) && isMoving) {
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		}
	}
	
	public void FireProjectile (Transform target) {
		this.target = target;
		isMoving = true;
	}
}
