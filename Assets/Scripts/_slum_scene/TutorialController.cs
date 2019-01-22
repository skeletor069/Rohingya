using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour {

	public Hero3d hero;
	public SpeechPartner speechPartner;
	public TextMeshProUGUI tutorialText;
	private Animator textAnimator;
	private int animShow = Animator.StringToHash("show");
	private int animHide = Animator.StringToHash("hide");
	public GameObject trashIcon;
	WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

	private void Awake() {
		textAnimator = tutorialText.GetComponent<Animator>();
	}

	void Start () {
		hero.SetMovementActive(false);
		StartCoroutine(TutorialRoutine());
	}

	IEnumerator TutorialRoutine() {
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(InitialTexts());
		yield return StartCoroutine(ConversationWithPedestrian());
		tutorialText.text = "Move around the area a little bit. Locate the TRASH YARD";
		textAnimator.SetTrigger(animShow);
		hero.SetMovementActive(true);
		trashIcon.SetActive(true);

		while (true) {
			if(Vector3.Distance(hero.transform.position, trashIcon.transform.position) < 5)
				break;
			Debug.Log(Vector3.Distance(hero.transform.position, trashIcon.transform.position));
			yield return endOfFrame;
		}
		
		textAnimator.SetTrigger(animHide);
		//trashIcon.SetActive(false);
		yield return new WaitForSeconds(1);
		tutorialText.text = "Search the trash area for a few minutes";
		textAnimator.SetTrigger(animShow);
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
