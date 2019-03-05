using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlumWorld : MonoBehaviour {
	private GameController gameController;
	private static SlumWorld instance;
	private Facility currentFacility = null;
	private readonly WaitForSeconds interactionActiveDelay = new WaitForSeconds(.1f);
	private bool heroMovementActive = true;
	private int animTime = Animator.StringToHash("time");
	private bool campfireStarted = false;
	
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
	public SoundManager soundManager;
	
	
	
	void Awake () {
		instance = this;
		facilities = facilityHolder.GetComponentsInChildren<Facility>();
		gameController = GameController.GetInstance();
	}

	private void Start() {
		HUD.GetInstance().HideHud();
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
		soundManager.UpdateSound((int)gameController.World.GetMinutesGone());
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

	void FacilityActivateCheck() {
		float hour = gameController.World.GetHour();
		if (hour > 8 && hour < 21) {
			for (int i = 0; i < foodFacilities.Length; i++)
				foodFacilities[i].FacilityActive = true;
		}
		else {
			for (int i = 0; i < foodFacilities.Length; i++)
				foodFacilities[i].FacilityActive = false;
		}

		if (hour > 10 && hour < 19) {
			for (int i = 0; i < dealerFacilities.Length; i++)
				dealerFacilities[i].FacilityActive = true;
		}
		else {
			for (int i = 0; i < dealerFacilities.Length; i++)
				dealerFacilities[i].FacilityActive = false;
		}
	}

	void ShowPauseMenu() {
		gameController.WorldRunning = false;
		player.SetMovementActive(false);
		Time.timeScale = 0;
		PauseMenu.GetInstance().ShowPauseMenu();
	}

	public void PauseMenuClosed() {
		gameController.WorldRunning = true;
		player.SetMovementActive(true);
		Time.timeScale = 1;
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
		yield return new WaitForSeconds(3);
		gameController.World.Update(minutes);
		gameController.World.ActionPerformed(tokens, minutes);
		simulationPanel.DissolveOverlay();
		yield return new WaitForSeconds(1);
		gameController.WorldRunning = true;
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

		if (foodToken.amount > 0) {
			float targetFood = foodToken.amount / minutes;
			HeroConfig heroConfig = gameController.World.Hero.GetHeroConfig();
			heroConfig.foodPerMinute = -targetFood;
			gameController.World.Hero.SetHeroConfig(heroConfig);
		}

		facilityDescriptionPanel.InteractionActive = false;
		Time.timeScale = (minutes > 30)?16:8;
		float accum = 0;
		while (accum < minutes) {
			accum += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		facilityDescriptionPanel.InteractionActive = true;
		gameController.World.Hero.SetHeroConfig(initialHeroConfig);
		Time.timeScale = 1;
		gameController.World.ActionPerformed(tokens, minutes);
		facilityDescriptionPanel.ClosePanel();
	}

	public void ItemsFound(List<Item> trashItems, float minutes) {
		StartCoroutine(ItemsFoundRoutine(trashItems, minutes));

	}

	IEnumerator ItemsFoundRoutine(List<Item> trashItems, float minutes) {
		facilityDescriptionPanel.InteractionActive = false;
		Time.timeScale = 8;
		float accum = 0;
		while (accum < minutes) {
			accum += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		facilityDescriptionPanel.InteractionActive = true;
		Time.timeScale = 1;
		for (int i = 0; i < trashItems.Count; i++) {
			GameController.GetInstance().World.Inventory.AddItem(trashItems[i]);	
		}
		gameController.World.ActionPerformed(new List<AttributeToken>(), minutes);
		facilityDescriptionPanel.ClosePanel();
	}

	public void ItemPicked(PickupItem pickItem) {
		Item item = pickupItemController.ProcessPickupGetItem(pickItem);
		gameController.World.Inventory.AddItem(item);
	}
}
