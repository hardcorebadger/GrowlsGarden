using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

	public GameObject Sapling;
	public float SaplingOffset;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = transform.Find ("parent").GetComponentInChildren<Animator> ();
	}
	
	public void Plant() {
		GetComponent<Rigidbody> ().isKinematic = true;
		_animator.SetTrigger ("plant");
	}

	public void OnPlanted() {
		Destroy (gameObject);
		Instantiate (Sapling, transform.position+Vector3.up*SaplingOffset, Quaternion.Euler(new Vector3(Random.Range(-5f,5f), Random.Range(0f,360f), Random.Range(-5f,5f))));
		Instantiate (GameController.Instance.PlantParticles, transform.position, Quaternion.Euler(new Vector3(90,0,0)));
	}
		
}
