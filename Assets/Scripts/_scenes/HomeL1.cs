using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeL1 : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		GameController.GetInstance().GoToCityMap();
	}
}
