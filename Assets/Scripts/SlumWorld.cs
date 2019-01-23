using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlumWorld : MonoBehaviour {
	private static SlumWorld instance;
	private Facility currentFacility = null;
	private readonly WaitForSeconds interactionActiveDelay = new WaitForSeconds(.1f);
	private bool heroMovementActive = true;
	private int animTime = Animator.StringToHash("time");

	public Hero3d player;
	public GameObject actionBtn;
	public FollowPlayer cameraController;
	public FacilityDescriptionPanel facilityDescriptionPanel;
	public SimulationPanel simulationPanel;
	public PickupItemController pickupItemController;
	public Animator dayNightAnim;
	
	void Awake () {
		instance = this;
	}

	private void Start() {
		HUD.GetInstance().HideHud();
	}

	public static SlumWorld GetInstance() {
		return instance;
	}

	public void ShowInteractionIcon(Facility facility) {
		this.currentFacility = facility;
		actionBtn.SetActive(true);
		// show enter button UI
	}

	public void HideInteractionIcon() {
		actionBtn.SetActive(false);
		currentFacility = null;
	}

	private void Update() {
		if (Input.anyKey) {
			if (Input.GetKeyDown(KeyCode.Return) && currentFacility != null && heroMovementActive) {
				// show options of current facility
				// change to option select mode
				Debug.Log("here");
				cameraController.SetTarget(currentFacility.transform);
				player.SetMovementActive(false);
				actionBtn.SetActive(false);
				facilityDescriptionPanel.ShowDescription(currentFacility);
				heroMovementActive = false;
			}

			if (Input.GetKeyDown(KeyCode.I) && heroMovementActive) {
				if(InventoryUI.GetInstance().IsInventoryOpen())
					InventoryUI.GetInstance().CloseInventoryPanel();
				else
					InventoryUI.GetInstance().ShowInventoryPanel();
			}
		}

		dayNightAnim.SetFloat(animTime, GameController.GetInstance().World.GetHour());
	}

	public IEnumerator DescriptionPanelClosed(bool tutorialMode, bool showInventory) {
		if(showInventory)
			InventoryUI.GetInstance().ShowInventoryPanel();
		yield return interactionActiveDelay;
		player.SetMovementActive(true);
		if(!tutorialMode)
			actionBtn.SetActive(true);
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
		GameController.GetInstance().WorldRunning = false;
		yield return StartCoroutine(simulationPanel.ShowSimulationDisplay(GetSimulationDisplayText()));
		GameController.GetInstance().World.ActionPerformed(tokens, minutes);
		GameController.GetInstance().WorldRunning = true;
		facilityDescriptionPanel.ClosePanel();
		//yield return StartCoroutine(DescriptionPanelClosed());
	}

	public void ItemsFound(List<Item> trashItems, float minutes) {
		StartCoroutine(ItemsFoundRoutine(trashItems, minutes));

	}

	IEnumerator ItemsFoundRoutine(List<Item> trashItems, float minutes) {
		GameController.GetInstance().WorldRunning = false;
		yield return StartCoroutine(simulationPanel.ShowSimulationDisplay(GetSimulationDisplayText()));
		for (int i = 0; i < trashItems.Count; i++) {
			GameController.GetInstance().World.Inventory.AddItem(trashItems[i]);	
		}
		GameController.GetInstance().World.ActionPerformed(new List<AttributeToken>(), minutes);
		GameController.GetInstance().WorldRunning = true;
		facilityDescriptionPanel.ClosePanel();
		//yield return StartCoroutine(DescriptionPanelClosed());
	}

	string GetSimulationDisplayText() {
		return "";
	}

	public void ItemPicked(PickupItem pickItem) {
		Item item = pickupItemController.ProcessPickupGetItem(pickItem);
		GameController.GetInstance().World.Inventory.AddItem(item);
		
	}
}
