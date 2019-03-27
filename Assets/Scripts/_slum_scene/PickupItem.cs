using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			SlumWorld.GetInstance().ItemPicked(this, transform.position);
		}
	}
}
