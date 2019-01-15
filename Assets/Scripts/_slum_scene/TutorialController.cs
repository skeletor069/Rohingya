using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

	public Hero3d hero;
	
	void Start () {
		//hero.SetMovementActive(false);
		//StartCoroutine(TutorialRoutine());
	}

	IEnumerator TutorialRoutine() {
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(InitialTexts());
	}

	IEnumerator InitialTexts() {
		yield return StartCoroutine(hero.SpeakRoutine("Mom!!...Dad!!!......", false));
		yield return new WaitForSeconds(2);
		StartCoroutine(CallPedestrianNearby());
		yield return StartCoroutine(hero.SpeakRoutine("mmmm mmmm (crying)...", false));

	}

	IEnumerator CallPedestrianNearby() {
		yield return 0;
	}

	IEnumerator ConversationWithPedestrian() {
		yield return 0;
	}


}
