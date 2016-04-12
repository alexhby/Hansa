using UnityEngine;
using System.Collections;

/**
 * Destroy game object after duration.
 * 
 * @author j@gamemechanix.io
 * @project SpellCraft
 * @copyright GameMechanix.io 2016
 **/
public class SC_SpellDuration : MonoBehaviour {

	[Header("Config")]
	public float spellDuration;

	IEnumerator wait() {
		yield return new WaitForSeconds(1);
	}

	void Start () {

		StartCoroutine(wait());
		Destroy (gameObject, spellDuration);
	}
}
