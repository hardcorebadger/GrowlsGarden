using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	private Animator _animator;
	private bool _isOpen;

	// Use this for initialization
	void Start () {
		_animator = GetComponentInChildren<Animator> ();
		_isOpen = false;
	}
	
//	// Update is called once per frame
//	void OnTriggerEnter (Collider c) {
//		if (c.tag == "player")
//			SetOpen (true);
//	}
//
//	void OnTriggerExit (Collider c) {
//		if (c.tag == "player")
//			SetOpen (false);
//	}

	public void ToggleOpen() {
		SetOpen (!_isOpen);
	}

	public void SetOpen(bool b) {
		_isOpen = b;
		if (_isOpen)
			_animator.SetTrigger ("open");
		else
			_animator.SetTrigger ("close");
	}
}
