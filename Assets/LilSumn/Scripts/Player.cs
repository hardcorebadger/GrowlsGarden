using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject Hand;

	private GameController _gc;
	private GameObject _itemInHand;

	// Use this for initialization
	void Start () {
		_gc = GameController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit = GetHit();
			if (hit.transform != null)
				Interact (hit.transform.gameObject, hit);
			else if (_itemInHand != null)
				ThrowItem();
		}
	}

	private void Interact(GameObject g, RaycastHit hit) {

		if (_itemInHand != null) {
			// put down the item
			PutDown (hit.point);
		} else {
			// process chests
			Chest chest = _gc.GetTopParent (g).GetComponent<Chest>();
			if (chest != null) {
				chest.ToggleOpen ();
				return;
			}

			// process items
			if (g.tag == "item") {
				Pickup (_gc.GetTopParent (g));
				return;
			}

			// process terraform
			if (g.layer == 8) {
				_gc.PaintRoad (hit.point);
			}
		}

	}

	private RaycastHit GetHit() {
		RaycastHit hit;
		Physics.Raycast (Camera.main.ScreenPointToRay(_gc.Pointer.transform.position), out hit, _gc.ArmLength, LayerMask.GetMask (new string[]{ "Default", "Terrain" }));
		return hit;
	}

	private void Pickup(GameObject item) {
		item.transform.parent = Hand.transform;
		item.transform.localPosition = Vector3.zero;
		item.transform.localRotation = Quaternion.identity;
		Rigidbody r = item.GetComponent<Rigidbody> ();
		if (r != null)
			r.isKinematic = true;
		_itemInHand = item;
	}

	private void PutDown(Vector3 pos) {
		_itemInHand.transform.parent = null;
		_itemInHand.transform.position = pos + ((Hand.transform.position-pos).normalized*0.5f);
		_itemInHand.transform.rotation = Quaternion.identity;
		Rigidbody r = _itemInHand.GetComponent<Rigidbody> ();
		if (r != null)
			r.isKinematic = false;
		_itemInHand = null;
	}

	private void ThrowItem() {
		_itemInHand.transform.parent = null;
		_itemInHand.transform.rotation = Quaternion.identity;
		Rigidbody r = _itemInHand.GetComponent<Rigidbody> ();
		if (r != null)
			r.isKinematic = false;
		r.velocity = Camera.main.transform.forward * _gc.ThrowSpeed;
		_itemInHand = null;
	}
}
