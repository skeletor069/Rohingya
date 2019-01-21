using System.Collections;
using UnityEngine;
using TMPro;

public class SpeechPartner : Pedestrian {

	public Transform canvas;
	private Narrator narrator;
	WaitForSeconds waitOne = new WaitForSeconds(1);
	WaitForSeconds waitTwo = new WaitForSeconds(2);
	WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
	private bool waitForSkippingNarration = false;

	private void Awake() {
		base.Awake();
		narrator = new Narrator(canvas.GetChild(0).gameObject, canvas.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>());
	}

	private void Start() {
		
	}

	public void GoToTarget(Vector3 startPosition, Vector3 endPosition) {
		StopAllCoroutines();
		transform.position = startPosition;
		agent.SetDestination(endPosition);
	}
	
	public IEnumerator SpeakRoutine(string text, bool auto) {
		narrator.ShowText(text);
		yield return waitTwo;
		if(auto)
			narrator.Hide();
		else {
			// enable btn hint
			waitForSkippingNarration = true;
			while (waitForSkippingNarration)
				yield return endOfFrame;
			narrator.Hide();
		}

		yield return waitOne;
	}

	void Update() {
		if (waitForSkippingNarration && Input.GetKeyDown(KeyCode.Return))
			waitForSkippingNarration = false;
		
		canvas.forward =  canvas.position - Camera.main.transform.position;
	}
}
