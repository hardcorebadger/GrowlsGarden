using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Death", 2f);
	}
	
	// Update is called once per frame
	void Death () {
		Destroy (gameObject);
	}
}
