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
		// show enter button UI
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
		if(gameController.WorldRunning)
			FacilityActivateCheck();
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
	
	public void ActionPerformed(List<AttributeToken> tokens, float minutes) {
		StartCoroutine(ActionPerformedRoutine(tokens, minutes));
	}

	IEnumerator ActionPerformedRoutine(List<AttributeToken> tokens, float minutes) {
		float targetMinute = gameController.World.GetMinutesGone() + minutes;
		Time.timeScale = (minutes > 30)?20:8;
		float lastFrame = gameController.World.GetMinutesGone();
		while (gameController.World.GetMinutesGone() < targetMinute) {
			if (gameController.World.GetMinutesGone() < lastFrame) {
				targetMinute -= 1440;

				while (gameController.World.GetMinutesGone() < targetMinute) {
					yield return new WaitForEndOfFrame();
				}
				break;
			}
			lastFrame = gameController.World.GetMinutesGone();
			yield return new WaitForEndOfFrame();
		}

		Time.timeScale = 1;
		//gameController.WorldRunning = false;
		//yield return StartCoroutine(simulationPanel.ShowSimulationDisplay(GetSimulationDisplayText()));
		gameController.World.ActionPerformed(tokens, minutes);
		//gameController.WorldRunning = true;
		facilityDescriptionPanel.ClosePanel();
	}

	public void ItemsFound(List<Item> trashItems, float minutes) {
		StartCoroutine(ItemsFoundRoutine(trashItems, minutes));

	}

	IEnumerator ItemsFoundRoutine(List<Item> trashItems, float minutes) {
		float targetMinute = gameController.World.GetMinutesGone() + minutes;
		Time.timeScale = 8;
		while (gameController.World.GetMinutesGone() < targetMinute) {
			yield return new WaitForEndOfFrame();
		}

		Time.timeScale = 1;
//		gameController.WorldRunning = false;
//		yield return StartCoroutine(simulationPanel.ShowSimulationDisplay(GetSimulationDisplayText()));
		for (int i = 0; i < trashItems.Count; i++) {
			GameController.GetInstance().World.Inventory.AddItem(trashItems[i]);	
		}
		gameController.World.ActionPerformed(new List<AttributeToken>(), minutes);
//		gameController.WorldRunning = true;
		facilityDescriptionPanel.ClosePanel();
	}

	string GetSimulationDisplayText() {
		return "";
	}

	public void ItemPicked(PickupItem pickItem) {
		Item item = pickupItemController.ProcessPickupGetItem(pickItem);
		gameController.World.Inventory.AddItem(item);
	}
}
