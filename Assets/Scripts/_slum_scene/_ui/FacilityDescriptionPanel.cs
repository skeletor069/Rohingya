using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FacilityDescriptionPanel : MonoBehaviour {

	public GameObject panel;
	public TextMeshProUGUI facilityNameTxt;
	public TextMeshProUGUI facilityDescriptionTxt;
	private bool interactionActive = false;
	private Facility facility = null;
	public List<FacilityDescriptionBtn> actionBtns = new List<FacilityDescriptionBtn>();
	private int selectedIndex = 0;
	private FacilityDescriptionBtn jobBtn;
	private bool tutorialMode = true;
	private TutorialController tutorialController;
	

	private void Start() {
		jobBtn = actionBtns[1];
		panel.SetActive(false);
	}

	public void ShowDescription(Facility facility) {
		if (actionBtns.Count == 4) {
			actionBtns.Insert(1, jobBtn);
			jobBtn.gameObject.SetActive(true);
		}
			
		if (!facility.JobActive) {
			actionBtns.RemoveAt(1);
			jobBtn.gameObject.SetActive(false);
		}
			
		interactionActive = true;
		this.facility = facility;
		facilityNameTxt.text = facility.GetFacilityName();
		facilityDescriptionTxt.text = facility.GetFacilityDescription();
		PopulateBtnNames(facility.GetOptionNames(), facility.JobActive);
		ResetSelection();
		panel.SetActive(true);	
		
		if(facility.ShowInventory)
			InventoryUI.GetInstance().ShowInventoryPanel();
	}

	void PopulateBtnNames(string[] optionNames, bool jobActive) {
		for(int i = 0 ; i < optionNames.Length; i++)
			actionBtns[i+2 - ((jobActive)?0:1)].SetBtnName(optionNames[i]);
	}

	void Update() {
		if (Input.anyKey && interactionActive) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				switch (selectedIndex) {
					case 0:
						if(!tutorialMode)
							ClosePanel();
						break;
					case 1:
						if(facility.JobActive)
							facility.DoJob();
						else 
							facility.ExecuteAction(0);
						
						break;
					case 2:
						if(facility.JobActive)
							facility.ExecuteAction(0);
						else 
							facility.ExecuteAction(1);
						break;
					case 3:
						if(facility.JobActive)
							facility.ExecuteAction(1);
						else 
							facility.ExecuteAction(2);
						break;
					case 4:
						if(facility.JobActive)
							facility.ExecuteAction(2);
						
						break;
				}
				
			}

			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				ButtonSelectionDown();
			}

			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				ButtonSelectionUp();
			}
		}
	}

	public void ClosePanel() {
		if (facility.ShowInventory) {
			InventoryUI.GetInstance().CloseInventoryPanel();
		}

		StartCoroutine(SlumWorld.GetInstance().DescriptionPanelClosed());
		interactionActive = false;
		facility = null;
		panel.SetActive(false);
		
		if(tutorialMode)
			tutorialController.FacilityPanelClosed();
	}

	private void ButtonSelectionDown() {
		actionBtns[selectedIndex].SetBtnStateAtive(false);
		selectedIndex = Mathf.Max((selectedIndex - 1), 0);
		actionBtns[selectedIndex].SetBtnStateAtive(true);
	}

	private void ButtonSelectionUp() {
		actionBtns[selectedIndex].SetBtnStateAtive(false);
		selectedIndex = Mathf.Min((selectedIndex + 1), (actionBtns.Count - 1));
		actionBtns[selectedIndex].SetBtnStateAtive(true);
	}

	public bool IsShowing() {
		return interactionActive;
	}

	public void TutorialMode(TutorialController tutorialController) {
		tutorialMode = true;
		this.tutorialController = tutorialController;
	}

	public void NormalMode() {
		tutorialMode = false;
	}

	void ResetSelection() {
		selectedIndex = 0;
		actionBtns[selectedIndex].SetBtnStateAtive(true);
		for(int i = 1; i < actionBtns.Count; i++)
			actionBtns[i].SetBtnStateAtive(false);
	}
}
