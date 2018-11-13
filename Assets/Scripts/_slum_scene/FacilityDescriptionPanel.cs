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
	

	private void Start() {
		panel.SetActive(false);
	}

	public void ShowDescription(Facility facility) {
		interactionActive = true;
		this.facility = facility;
		facilityNameTxt.text = facility.GetFacilityName();
		facilityDescriptionTxt.text = facility.GetFacilityDescription();
		PopulateBtnNames(facility.GetOptionNames());
		ResetSelection();
		panel.SetActive(true);	
		
	}

	void PopulateBtnNames(string[] optionNames) {
		for(int i = 0 ; i < optionNames.Length; i++)
			actionBtns[i+2].SetBtnName(optionNames[i]);
	}

	void Update() {
		if (Input.anyKey && interactionActive) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				switch (selectedIndex) {
					case 0:
						ClosePanel();
						break;
					case 1:
						facility.DoJob();
						break;
					case 2:
						facility.ExecuteAction(0);
						break;
					case 3:
						facility.ExecuteAction(1);
						break;
					case 4:
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

	private void ClosePanel() {
		StartCoroutine(SlumWorld.GetInstance().DescriptionPanelClosed());
		interactionActive = false;
		facility = null;
		panel.SetActive(false);
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

	void ResetSelection() {
		selectedIndex = 0;
		actionBtns[selectedIndex].SetBtnStateAtive(true);
		for(int i = 1; i < actionBtns.Count; i++)
			actionBtns[i].SetBtnStateAtive(false);
	}
}
