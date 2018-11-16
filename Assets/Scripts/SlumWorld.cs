using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlumWorld : MonoBehaviour {
	private static SlumWorld instance;
	private Facility currentFacility = null;
	private readonly WaitForSeconds interactionActiveDelay = new WaitForSeconds(.1f);
	private bool heroMovementActive = true;

	public Hero3d player;
	public GameObject actionBtn;
	public FollowPlayer cameraController;
	public FacilityDescriptionPanel facilityDescriptionPanel;
	public SimulationPanel simulationPanel;
	public PickupItemController pickupItemController;
	
	void Awake () {
		instance = this;
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
		}
	}

	public IEnumerator DescriptionPanelClosed() {
		yield return interactionActiveDelay;
		player.SetMovementActive(true);
		actionBtn.SetActive(true);
		cameraController.SetTarget(player.transform);
		heroMovementActive = true;
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
	}

	string GetSimulationDisplayText() {
		return "";
	}

	public void ItemPicked(PickupItem pickItem) {
		Item item = pickupItemController.ProcessPickupGetItem(pickItem);
		GameController.GetInstance().World.Inventory.AddItem(item);
		
	}
}
