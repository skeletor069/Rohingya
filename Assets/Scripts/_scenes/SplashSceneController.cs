using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSceneController : MonoBehaviour {

	void Start () {
		StartCoroutine(ChangeSceneAfterDelay());
	}

	IEnumerator ChangeSceneAfterDelay() {
		yield return new WaitForSeconds(3);
		GameController.GetInstance().GoToMainMenu();
	}
}
