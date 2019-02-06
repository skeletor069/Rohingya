using System.Collections;
using System.Collections.Generic;
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
	public GameObject dealerIcon;
	public GameObject homeIcon;
	WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
	public FacilityDescriptionPanel facilityPanel;
	private bool waitForFacilityPanelClose = false;

	public Facility trashFacility;
	public Facility paperDealerFacility;
	public Facility teaStallFacility;
	public Facility homeFacility;

	private void Awake() {
		textAnimator = tutorialText.GetComponent<Animator>();
	}

	void Start () {
		
		StartCoroutine(TutorialRoutine2());
	}

	IEnumerator TutorialRoutine() {
		hero.SetMovementActive(false);
		yield return new WaitForSeconds(3);
		facilityPanel.TutorialMode(this);
		yield return StartCoroutine(InitialTexts());
		yield return StartCoroutine(ConversationWithPedestrian());
		hero.SetMovementActive(true);
		HUD.GetInstance().ShowHud();
		yield return TrashLocateRoutine();
		yield return TrashSearchroutine();
//		yield return StartCoroutine(energyExplainRoutine());
		yield return LocatePaperDealerRoutine();
		yield return PaperSellRoutine();
		facilityPanel.NormalMode();
		
		SlumWorld.GetInstance().ActivateAllFacilities();


		// show objective
		// activate facility highlight : trash area
		// activate hero movement
	}

	IEnumerator TutorialRoutine2() {
		
		hero.SetMovementActive(false);
		yield return new WaitForSeconds(3);
//		CallPedestrianNearby();
//		facilityPanel.TutorialMode(this);
//		yield return StartCoroutine(InitialTexts());
//		yield return StartCoroutine(ConversationWithPedestrian2());
//		yield return StartCoroutine(EnergyBarShowRoutine());
//		yield return FoodBarShowRoutine();
		StartSimulation();
	}

	private void StartSimulation() {
		facilityPanel.NormalMode();
		trashFacility.FacilityActive = true;
		hero.SetMovementActive(true);
		SlumWorld.GetInstance().ActivateAllFacilities();
		GameController.GetInstance().World.SetMinutesGone(600);
		HUD.GetInstance().ShowHud();
		GameController.GetInstance().WorldRunning = true;
		GameController.GetInstance().StartSurvival();
	}

	IEnumerator FacilityPanelCloseWait() {
		waitForFacilityPanelClose = true;
		while (waitForFacilityPanelClose) {
			yield return endOfFrame;
		}
	}

	IEnumerator FoodBarShowRoutine() {
		//GameController.GetInstance().World.h
		GameController.GetInstance().World.SetMinutesGone(600);
		homeFacility.FacilityActive = false;
		hero.SetMovementActive(false);
		yield return StartCoroutine(speechPartner.SpeakRoutine("Did you have a good sleep, kiddo?", true));
		
		yield return StartCoroutine(hero.SpeakRoutine("Yes, thanks.", false));
		HUD.GetInstance().foodBar.Show();
		yield return StartCoroutine(hero.SpeakRoutine("But I am so hungry now.", false));
		yield return StartCoroutine(speechPartner.SpeakRoutine("You should be. Eat these food for the time being.", true));
		List<AttributeToken> tokens = new List<AttributeToken>();
		tokens.Add(new AttributeToken(HeroAttributes.FOOD, 100));
		SlumWorld.GetInstance().ActionPerformed(tokens, 10);
		yield return StartCoroutine(FacilityPanelCloseWait());
		GameController.GetInstance().WorldRunning = false;
		GameController.GetInstance().World.SetMinutesGone(600);
		yield return StartCoroutine(speechPartner.SpeakRoutine("Here you have to earn money and buy your own food", false));
		HUD.GetInstance().careerPanel.SetActive(true);
		yield return StartCoroutine(hero.SpeakRoutine("I don't have any money. How can I earn some?", true));
		yield return StartCoroutine(speechPartner.SpeakRoutine("Nobody will give you job right now.", true));
		yield return StartCoroutine(speechPartner.SpeakRoutine("But you can search the trash yard to get something to sell to the local dealers.", false));
		yield return StartCoroutine(hero.SpeakRoutine("Thanks for the information.", false));
		
	}

	IEnumerator EnergyBarShowRoutine() {
		HUD.GetInstance().energyBar.Show();
		yield return StartCoroutine(hero.SpeakRoutine("I feel very tired now", false));
		yield return StartCoroutine(speechPartner.SpeakRoutine("I can understand. Follow me. Lets find a place for you to sleep", false));
		hero.SetMovementActive(true);
		Vector3 targetPosition = homeFacility.transform.position + homeFacility.transform.forward * -2;
		speechPartner.GoToTarget(speechPartner.transform.position, targetPosition);
		tutorialText.text = "Follow the stranger to your hut.";
		textAnimator.SetTrigger(animShow);
		
		
		

		while (true) {
			if(Vector3.Distance(hero.transform.position, targetPosition) < 1.5f)
				break;
			//Debug.Log(Vector3.Distance(hero.transform.position, trashIcon.transform.position));
			yield return endOfFrame;
		}
		textAnimator.SetTrigger(animHide);
		//hero.SetMovementActive(false);
		//yield return StartCoroutine(hero.GoToTarget(targetPosition + new Vector3(-1, 0, -1.5f)));
		hero.SetMovementActive(false);
		hero.transform.LookAt(speechPartner.transform);
		yield return StartCoroutine(speechPartner.SpeakRoutine("This is your home now. Get a nice sleep. We will talk tomorrow.", false));
		//StartCoroutine(speechPartner.StartRandomWalk());
		hero.SetMovementActive(true);
		homeIcon.SetActive(true);
		homeFacility.FacilityActive = true;
		yield return StartCoroutine(FacilityPanelCloseWait());
		
	}

	IEnumerator InitialTexts() {
		yield return StartCoroutine(hero.SpeakRoutine("Mom!!...Dad!!!......", false));
		//CallPedestrianNearby();
		yield return StartCoroutine(hero.SpeakRoutine("mmmm mmmm (crying)...", false));
		

	}

	void CallPedestrianNearby() {
		Vector3 partnerPosition = hero.transform.position + new Vector3(0, 0, 2);
		speechPartner.GoToTarget(hero.transform.position + new Vector3(0,0,4), partnerPosition);
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
	
	IEnumerator ConversationWithPedestrian2() {
		yield return 0;
		yield return StartCoroutine(speechPartner.SpeakRoutine("Why are you crying kid?", false));
		yield return StartCoroutine(hero.SpeakRoutine("I have lost my parents while the journey....mmmmmm....mmmmm...", false));
		yield return StartCoroutine(speechPartner.SpeakRoutine("BE STRONG FOOL!!!", true));
		yield return StartCoroutine(speechPartner.SpeakRoutine("There are hundreds of kids in this area just like you", true));
		yield return StartCoroutine(speechPartner.SpeakRoutine("You better keep on surviving.", false));
	}

	IEnumerator TrashLocateRoutine() {
		tutorialText.text = "Move around the area a little bit. Locate the TRASH YARD";
		textAnimator.SetTrigger(animShow);
		
		trashIcon.SetActive(true);
		trashFacility.FacilityActive = true;

		while (true) {
			if(Vector3.Distance(hero.transform.position, trashIcon.transform.position) < 5)
				break;
			//Debug.Log(Vector3.Distance(hero.transform.position, trashIcon.transform.position));
			yield return endOfFrame;
		}
		textAnimator.SetTrigger(animHide);
		yield return new WaitForSeconds(1);
		
	}

	IEnumerator TrashSearchroutine() {
		tutorialText.text = "Search the trash area for a few minutes";
		textAnimator.SetTrigger(animShow);
		waitForFacilityPanelClose = true;
		while (waitForFacilityPanelClose) {
			yield return endOfFrame;
		}
		trashFacility.FacilityActive = false;
		textAnimator.SetTrigger(animHide);
		yield return new WaitForSeconds(1);
		trashIcon.SetActive(false);
	}

	IEnumerator LocatePaperDealerRoutine() {
		tutorialText.text = "Now find the local paper dealer and sell some papers";
		textAnimator.SetTrigger(animShow);
		paperDealerFacility.FacilityActive = true;
		dealerIcon.SetActive(true);
		while (true) {
			if(Vector3.Distance(hero.transform.position, dealerIcon.transform.position) < 5)
				break;
			//Debug.Log(Vector3.Distance(hero.transform.position, dealerIcon.transform.position));
			yield return endOfFrame;
		}
		textAnimator.SetTrigger(animHide);
		yield return new WaitForSeconds(1);
	}
	
	IEnumerator PaperSellRoutine() {
		tutorialText.text = "Sell few papers to get some money";
		textAnimator.SetTrigger(animShow);
		waitForFacilityPanelClose = true;
		while (waitForFacilityPanelClose) {
			yield return endOfFrame;
		}
		paperDealerFacility.FacilityActive = false;
		textAnimator.SetTrigger(animHide);
		yield return new WaitForSeconds(1);
		dealerIcon.SetActive(false);
	}

	public void FacilityPanelClosed() {
		waitForFacilityPanelClose = false;
	}


}
