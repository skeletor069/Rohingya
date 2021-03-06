﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class SlumWorld : MonoBehaviour {
	private GameController gameController;
	private static SlumWorld instance;
	private Facility currentFacility = null;
	private readonly WaitForSeconds interactionActiveDelay = new WaitForSeconds(.1f);
	private bool heroMovementActive = true;
	private int animTime = Animator.StringToHash("time");
	private bool campfireStarted = false;
	private bool gameOver = false;
	private SoundManager soundManager;
	
	public Facility[] facilities;
	public Facility home;
	public Facility[] trashAreas;
	public Facility[] foodFacilities;
	public Facility[] dealerFacilities;
	public GameObject facilityHolder;
	public Hero3d player;
	public GameObject actionBtn;
	public FollowPlayer cameraController;
	public FacilityDescriptionPanel facilityDescriptionPanel;
	public SimulationPanel simulationPanel;
	public PickupItemController pickupItemController;
	public Animator dayNightAnim;

	public Campfire[] campfires;
	
	public TutorialController tutorialController;
	public GameObject gameOverPanel;
	
	
	
	void Awake () {
		instance = this;
		facilities = facilityHolder.GetComponentsInChildren<Facility>();
		gameController = GameController.GetInstance();
		soundManager = SoundManager.GetInstance();
	}

	void Start() {
		if (gameController.IsTutorialRunning) {
			StartCoroutine(tutorialController.TutorialRoutine());
		}
		else {
			// load data
			
			SerializableVector heroPositionAtSave = gameController.World.GetHeroPositionAtSave();
			player.transform.position = new Vector3(heroPositionAtSave.x, heroPositionAtSave.y, heroPositionAtSave.z);
			ResetSceneWithCurrentWorldData();
			gameController.WorldRunning = true;
			facilityDescriptionPanel.NormalMode();
			tutorialController.trashFacility.FacilityActive = true;
			tutorialController.trashFacility.gameObject.SetActive(true);
			home.gameObject.SetActive(true);
			ActivateAllFacilities();
			player.SetMovementActive(true);
			player.ActivateAgent();
			cameraController.Initiate();
			simulationPanel.DissolveOverlay();
			soundManager.SwitchToNormalMode();
			StartCoroutine(tutorialController.speechPartner.FindALocationAndGoThere());
//			yield return new WaitForSeconds(2f);
//			player.transform.position.Set(3,0,0);
//			Debug.LogError("done");

		}
		
		soundManager.PlaySound(SoundTypes.DAY_BG);
		soundManager.PlaySound(SoundTypes.NIGHT_BG);
		soundManager.PlaySound(SoundTypes.MORNING_BG);
		soundManager.StopSound(SoundTypes.MENU_BG);
		
	}

	void ResetSceneWithCurrentWorldData() {
		dayNightAnim.SetFloat(animTime, gameController.World.GetHour());
		soundManager.UpdateAmbientSound((int)gameController.World.GetMinutesGone());
		if(gameController.WorldRunning)
			FacilityActivateCheck();

		if (gameController.World.GetHour() > 21 && !campfireStarted && gameController.World.GetHour() < 23) {
			campfireStarted = true;
			StartCampfire();
		}

		if (campfireStarted && (gameController.World.GetHour() > 23 || gameController.World.GetHour() < 21)) {
			campfireStarted = false;
			StopCampfire();
		}
	}

	public static SlumWorld GetInstance() {
		return instance;
	}

	public void ShowInteractionIcon(Facility facility) {
		currentFacility = facility;
		actionBtn.SetActive(true);
	}

	public void HideInteractionIcon() {
		actionBtn.SetActive(false);
		currentFacility = null;
	}

	public void ActivateAllFacilities() {
		for (int i = 0; i < facilities.Length; i++) {
			facilities[i].FacilityActive = true;
			facilities[i].gameObject.SetActive(true);
		}
	}
	
	public void DectivateAllFacilities() {
		for (int i = 0; i < facilities.Length; i++) {
			facilities[i].FacilityActive = false;
			facilities[i].gameObject.SetActive(false);
		}
	}

	private void Update() {
		if (Input.anyKey) {
			if (Input.GetKeyDown(KeyCode.Return) && currentFacility != null && player.IsMovementActive()) {
				cameraController.SetTarget(currentFacility.transform);
				player.SetMovementActive(false);
				actionBtn.SetActive(false);
				facilityDescriptionPanel.ShowDescription(currentFacility);
			}

			if (Input.GetKeyDown(KeyCode.I) && player.IsMovementActive()) {
				if(InventoryUI.GetInstance().IsInventoryOpen())
					InventoryUI.GetInstance().CloseInventoryPanel();
				else
					InventoryUI.GetInstance().ShowInventoryPanel();
			}

			if (Input.GetKeyDown(KeyCode.Escape) && player.IsMovementActive()) {
				ShowPauseMenu();
			}
		}
		dayNightAnim.SetFloat(animTime, gameController.World.GetHour());
		soundManager.UpdateAmbientSound((int)gameController.World.GetMinutesGone());
		HeroStateSoundCheck();
		if(gameController.WorldRunning)
			FacilityActivateCheck();

		if (gameController.World.GetHour() > 21 && !campfireStarted && gameController.World.GetHour() < 23) {
			campfireStarted = true;
			StartCampfire();
		}

		if (campfireStarted && (gameController.World.GetHour() > 23 || gameController.World.GetHour() < 21)) {
			campfireStarted = false;
			StopCampfire();
		}

		if (!gameOver && gameController.World.Hero.IsDead()) {
			// hero die animation
			// gameovercontroller routine
			// 		show message
			//		show stat
			gameOver = true;
			StopAllCoroutines();
			StartCoroutine(GameOver());
		}
	}

	void HeroStateSoundCheck() {
		if (gameController.World.Hero.Health < 15) {
			if(!soundManager.IsPlaying(SoundTypes.HEART_BEAT))
				soundManager.PlaySound(SoundTypes.HEART_BEAT);
		}
		else {
			if(soundManager.IsPlaying(SoundTypes.HEART_BEAT))
				soundManager.StopSound(SoundTypes.HEART_BEAT);
		}
		
		if (gameController.World.Hero.Energy < 15) {
			if(!soundManager.IsPlaying(SoundTypes.BREATH))
				soundManager.PlaySound(SoundTypes.BREATH);
		}
		else {
			if(soundManager.IsPlaying(SoundTypes.BREATH))
				soundManager.StopSound(SoundTypes.BREATH);
		}
		
		if (gameController.World.Hero.Food < 15) {
			if(!soundManager.IsPlaying(SoundTypes.HUNGRY))
				soundManager.PlaySound(SoundTypes.HUNGRY);
		}
		else {
			if(soundManager.IsPlaying(SoundTypes.HUNGRY))
				soundManager.StopSound(SoundTypes.HUNGRY);
		}
	}

	IEnumerator GameOver() {
		player.AnimDie();
		player.enabled = false;
		gameController.WorldRunning = false;
		simulationPanel.StartOverlay();
		Debug.LogError("Game Over");
		soundManager.SwitchToEndingMode();
		soundManager.PlaySound(SoundTypes.ENDING_MUSIC);
		yield return new WaitForSeconds(2);
		soundManager.PlaySound(SoundTypes.ENDING_MUSIC);
		yield return new WaitForSeconds(1);
		gameOverPanel.SetActive(true);
		// animator show
	}

	void StartCampfire() {
		Campfire.CAMPFIRE_STARTED = true;
		for(int i = 0 ; i < campfires.Length; i++)
			campfires[i].StartFire();
	}

	void StopCampfire() {
		Campfire.CAMPFIRE_STARTED = false;
		for(int i = 0 ; i < campfires.Length; i++)
			campfires[i].StopFire();
	}

	public Transform GetAFreeSeat() {
		Transform freeSeat = null;

		for (int i = 0; i < campfires.Length; i++) {
			freeSeat = campfires[i].GetAFreeSeat();
			if(freeSeat != null)
				break;
		}
		return freeSeat;
	}

	public void MakeSeatFree(Transform seat) {
		for (int i = 0; i < campfires.Length; i++) {
			campfires[i].MakeFree(seat);
		}
	}

	void FacilityActivateCheck() {

		float minutesGone = gameController.World.GetMinutesGone();
		for (int i = 0; i < foodFacilities.Length; i++)
			foodFacilities[i].CheckFacilityActive((int)minutesGone);
		for (int i = 0; i < dealerFacilities.Length; i++)
			dealerFacilities[i].CheckFacilityActive((int)minutesGone);
	}

	void ShowPauseMenu() {
		soundManager.SwitchToMenuMode(0);
		soundManager.PlaySound(SoundTypes.MENU_BG);
		gameController.WorldRunning = false;
		player.SetMovementActive(false);
		Time.timeScale = 0;
		PauseMenu.GetInstance().ShowPauseMenu();
	}

	public void PauseMenuClosed() {
		gameController.WorldRunning = true;
		player.SetMovementActive(true);
		Time.timeScale = 1;
		soundManager.StopSound(SoundTypes.MENU_BG);
		soundManager.SwitchToNormalMode();
	}

	public IEnumerator DescriptionPanelClosed(bool tutorialMode, bool showInventory) {
		if(showInventory)
			InventoryUI.GetInstance().ShowInventoryPanel();
		yield return interactionActiveDelay;

		if (!tutorialMode) {
			player.SetMovementActive(true);
			actionBtn.SetActive(true);
		}
		else {
			HideInteractionIcon();
		}
		cameraController.SetTarget(player.transform);
		heroMovementActive = true;

		if (showInventory) {
			yield return new WaitForSeconds(3);
			InventoryUI.GetInstance().CloseInventoryPanel();
		}
	}

	public void SleepActionPerformed(List<AttributeToken> tokens, float minutes) {
		StartCoroutine(SleepActionPerformedRoutine(tokens, minutes));
	}

	IEnumerator SleepActionPerformedRoutine(List<AttributeToken> tokens, float minutes) {
		facilityDescriptionPanel.ClosePanel(true);
		gameController.WorldRunning = false;
		simulationPanel.StartOverlay();
		soundManager.SwitchToSleepMode();
		yield return new WaitForSeconds(2);
		soundManager.PlaySound(SoundTypes.SLEEP);
		yield return new WaitForSeconds(4);
		soundManager.SwitchToNormalMode();
		gameController.World.Update(minutes);
		gameController.World.UpdateHeroAttribute(tokens);
		simulationPanel.DissolveOverlay();
		gameController.WorldRunning = true;
		facilityDescriptionPanel.ClosePanel();
	}
	
	public void JobDone(List<AttributeToken> tokens, float minutes, SoundTypes soundType) {
		StartCoroutine(JobDoneRoutine(tokens, minutes, soundType));
	}

	IEnumerator JobDoneRoutine(List<AttributeToken> tokens, float minutes, SoundTypes soundType) {
//		AttributeToken foodToken = new AttributeToken(HeroAttributes.FOOD, 0);
//		HeroConfig initialHeroConfig = gameController.World.Hero.GetHeroConfig();
//		for (int i = 0; i < tokens.Count; i++) {
//			if (tokens[i].attribute == HeroAttributes.FOOD) {
//				foodToken = tokens[i];
//				tokens.Remove(foodToken);
//				break;	
//			}
//		}
//		Debug.LogError("Initial food " + gameController.World.Hero.Food);
//		if (foodToken.amount > 0) {
//			float targetFood = foodToken.amount / minutes;
//			Debug.Log("Target food " + targetFood + ", total food " + foodToken.amount);
//			HeroConfig heroConfig = gameController.World.Hero.GetHeroConfig();
//			heroConfig.foodPerMinute = -targetFood;
//			gameController.World.Hero.SetHeroConfig(heroConfig);
//			
//		}
		if(!gameController.IsTutorialRunning)
			player.gameObject.SetActive(false);
		facilityDescriptionPanel.InteractionActive = false;
		soundManager.SwitchToActionMode();

		Time.timeScale = (minutes > 30)?16:8;
		float accum = 0;
		while (accum < minutes) {
			accum += Time.deltaTime;
			facilityDescriptionPanel.SetCurrentActionProgress(accum/minutes);
			yield return new WaitForEndOfFrame();
		}
		Debug.LogError("Final food " + gameController.World.Hero.Food);
		soundManager.SwitchToNormalMode();
		player.gameObject.SetActive(true);
		facilityDescriptionPanel.SetCurrentActionProgress(0);
		facilityDescriptionPanel.InteractionActive = true;
		//gameController.World.Hero.SetHeroConfig(initialHeroConfig);
		Time.timeScale = 1;
		soundManager.StopSound(soundType);
		soundManager.PlaySound(SoundTypes.SELL);
		gameController.World.UpdateHeroAttribute(tokens);
		facilityDescriptionPanel.ClosePanel();
	}

	public void ActionPerformed(List<AttributeToken> tokens, float minutes) {
		StartCoroutine(ActionPerformedRoutine(tokens, minutes));
	}

	IEnumerator ActionPerformedRoutine(List<AttributeToken> tokens, float minutes) {
		AttributeToken foodToken = new AttributeToken(HeroAttributes.FOOD, 0);
		HeroConfig initialHeroConfig = gameController.World.Hero.GetHeroConfig();
		for (int i = 0; i < tokens.Count; i++) {
			if (tokens[i].attribute == HeroAttributes.FOOD) {
				foodToken = tokens[i];
				tokens.Remove(foodToken);
				break;	
			}
		}
//		Debug.LogError("Initial food " + gameController.World.Hero.Food);
		if (foodToken.amount > 0) {
			float targetFood = foodToken.amount / minutes;
//			Debug.Log("Target food " + targetFood + ", total food " + foodToken.amount);
			HeroConfig heroConfig = gameController.World.Hero.GetHeroConfig();
			heroConfig.foodPerMinute = -targetFood;
			gameController.World.Hero.SetHeroConfig(heroConfig);
			
		}
		if(!gameController.IsTutorialRunning)
			player.gameObject.SetActive(false);
		facilityDescriptionPanel.InteractionActive = false;
		soundManager.SwitchToActionMode();
//		soundManager.PlaySound();
		Time.timeScale = (minutes > 30)?16:8;
		float accum = 0;
		while (accum < minutes) {
			accum += Time.deltaTime;
			facilityDescriptionPanel.SetCurrentActionProgress(accum/minutes);
			yield return new WaitForEndOfFrame();
		}
//		Debug.LogError("Final food " + gameController.World.Hero.Food);
		soundManager.SwitchToNormalMode();
		player.gameObject.SetActive(true);
		facilityDescriptionPanel.SetCurrentActionProgress(0);
		facilityDescriptionPanel.InteractionActive = true;
		gameController.World.Hero.SetHeroConfig(initialHeroConfig);
		Time.timeScale = 1;
		gameController.World.UpdateHeroAttribute(tokens);
		facilityDescriptionPanel.ClosePanel();
	}

	public void ItemsFound(List<Item> trashItems, float minutes) {
		StartCoroutine(ItemsFoundRoutine(trashItems, minutes));

	}

	IEnumerator ItemsFoundRoutine(List<Item> trashItems, float minutes) {
		facilityDescriptionPanel.InteractionActive = false;
		soundManager.PlaySound(SoundTypes.SEARCH);
		player.AnimSearch();
		Time.timeScale = 8;
		float accum = 0;
		while (accum < minutes) {
			accum += Time.deltaTime;
			facilityDescriptionPanel.SetCurrentActionProgress(accum/minutes);
			yield return new WaitForEndOfFrame();
		}
		facilityDescriptionPanel.SetCurrentActionProgress(0);
		facilityDescriptionPanel.InteractionActive = true;
		Time.timeScale = 1;
		for (int i = 0; i < trashItems.Count; i++) {
			GameController.GetInstance().World.Inventory.AddItem(trashItems[i]);	
		}
		soundManager.StopSound(SoundTypes.SEARCH);
		player.AnimMove();
		facilityDescriptionPanel.ClosePanel();
	}

	public void ItemPicked(PickupItem pickItem, Vector3 position) {
		soundManager.PlaySound(SoundTypes.PICK_UP);
		Item item = pickupItemController.ProcessPickupGetItem(pickItem, position);
		gameController.World.Inventory.AddItem(item);
	}
}
