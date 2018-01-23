using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour {

	public Vector3 HoldingOffset;
	public Vector3 HoldingRotation;

	public Vector3 PlacementOffset;
	public Vector3 PlacementRotation;

	public float CheckRadius;
	public Vector3 CheckOffset;

	public ItemUseEvent OnUse;

	public bool Use(GameObject g, RaycastHit h) {
		OnUse.Invoke (g, h);
		return (OnUse.GetPersistentEventCount () != 0);
	}

}

[System.Serializable]
public class ItemUseEvent : UnityEvent <GameObject, RaycastHit> {}
