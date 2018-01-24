using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedAnimationHandler : MonoBehaviour {

	public void OnPlant() {
		transform.parent.parent.GetComponent<Seed> ().OnPlanted ();
	}
}
