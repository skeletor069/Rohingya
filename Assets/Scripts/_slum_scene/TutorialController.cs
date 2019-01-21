using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

	public Hero3d hero;
	public SpeechPartner speechPartner;
	public 
	
	void Start () {
		hero.SetMovementActive(false);
		StartCoroutine(TutorialRoutine());
	}

	IEnumerator TutorialRoutine() {
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(InitialTexts());
		yield return StartCoroutine(ConversationWithPedestrian());
		// show objective
		// activate facility highlight : trash area
		// activate hero movement
	}

	IEnumerator InitialTexts() {
		yield return StartCoroutine(hero.SpeakRoutine("Mom!!...Dad!!!......", false));
		CallPedestrianNearby();
		yield return StartCoroutine(hero.SpeakRoutine("mmmm mmmm (crying)...", false));
		

	}

	void CallPedestrianNearby() {
		Vector3 partnerPosition = hero.transform.position + new Vector3(2, 0, 0);
		speechPartner.GoToTarget(partnerPosition + new Vector3(2,0,6), partnerPosition);
	}

	IEnumerator ConversationWithPedestrian() {
		yield return 0;
		yield return StartCoroutine(speechPartner.SpeakRoutine("Why are you crying kid?", false));
		yield return StartCoroutine(hero.SpeakRoutine("I have lost my parents while the journey....mmmmmm....mmmmm...", false));
		yield return StartCoroutine(speechPartner.SpeakRoutine("BE STRONG FOOL!!!", true));
		yield return StartCoroutine(speechPartner.SpeakRoutine("There are hundreds of kids in this area just like you", true));
		yield return StartCoroutine(speechPartner.SpeakRoutine("You have to work hard and survive", false));
		yield return StartCoroutine(hero.SpeakRoutine("I don't have any money...", false));
		yield return StartCoroutine(speechPartner.SpeakRoutine("Go find the trash yard. Search for something you can sell to the local dealers.", false));
		StartCoroutine(speechPartner.StartRandomWalk());
	}


}
