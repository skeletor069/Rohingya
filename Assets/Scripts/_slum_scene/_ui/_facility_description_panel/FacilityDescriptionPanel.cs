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
	private JobBtn jobBtn;
	private bool tutorialMode = true;
	private TutorialController tutorialController;
	private bool jobLocked = false;
	public static Color foodColor, energyColor, moneyColor;
	public TextMeshProUGUI statusText;
	public TextMeshProUGUI activeHoursText;
	public Color greenColor;
	public Color redColor;

	private FacilityDescriptionBtn currentBtn;

	void Awake() {
		foodColor = new Color(11f/255, 173f/255, 63f/255);
		energyColor = new Color(137f/255, 184f/255, 185f/255);
		moneyColor = new Color(241f/255, 219f/255, 0f);
		for(int i = 0 ; i < 5; i++)
			actionBtns.Add(transform.GetChild(0).GetChild(i).GetComponent<FacilityDescriptionBtn>());
		jobBtn = transform.GetChild(0).GetChild(1).GetComponent<JobBtn>();
	}

	private void Start() {
		//jobBtn = actionBtns[1];
		panel.SetActive(false);
	}

	public void ShowDescription(Facility facility) {
		if (actionBtns.Count == 4) {
			
			actionBtns.Insert(1, jobBtn);
			jobBtn.SetVisible(true);
		}
			
		if (!facility.JobActive) {
			actionBtns.RemoveAt(1);
			jobBtn.SetVisible(false);
			activeHoursText.enabled = false;
			statusText.enabled = false;
		}
		else {
			activeHoursText.enabled = true;
			statusText.enabled = true;

			if (facility.FacilityActive) {
				statusText.text = "Open";
				int hour = (facility.closingMinute / 60);
				int minute = facility.closingMinute % 60;
				activeHoursText.text = "Closes at " + ((hour < 10)?"0":"") + hour + ":" + ((minute<10)?"0":"")+minute + " uhr";
				statusText.color = greenColor;
				activeHoursText.color = redColor;
				// status color green
				// notification color red
			}
			else {
				statusText.text = "Closed";
				int hour = (facility.openningMinute / 60);
				int minute = facility.openningMinute % 60;
				activeHoursText.text = "Opening at " + ((hour < 10)?"0":"") + hour + ":" + ((minute<10)?"0":"")+minute + " uhr";
				statusText.color = redColor;
				activeHoursText.color = greenColor;
				// status color red
				// notification color green
			}

			jobBtn.SetRelationData(facility.GetRelationStatus());
			if (facility.IsRelationMax()) {
				// job active
				jobLocked = false;
			}
			else {
				// try later
				jobLocked = true;
			}
		}

		Debug.Log("Relation " + facility.GetRelationStatus());
			
		interactionActive = true;
		this.facility = facility;
		if(currentBtn != null)
			currentBtn.SetProgress(0);
		facilityNameTxt.text = facility.GetFacilityName();
		facilityDescriptionTxt.text = facility.GetFacilityDescription();
//		PopulateBtnNames(facility.GetOptionNames(), facility.JobActive);
		PopulateBtnWithData(facility.GetActionBtnsData(), facility.JobActive);
		ResetSelection();
		panel.SetActive(true);	
		
		if(facility.ShowInventory)
			InventoryUI.GetInstance().ShowInventoryPanel();
		
		
	}

	public void SetCurrentActionProgress(float fillAmount) {
//		if(currentBtn != null)
//			currentBtn.SetProgress(fillAmount);
		actionBtns[selectedIndex].SetProgress(fillAmount);
	}

	void PopulateBtnNames(string[] optionNames, bool jobActive) {
		for(int i = 0 ; i < optionNames.Length; i++)
			actionBtns[i+2 - ((jobActive)?0:1)].SetBtnName(optionNames[i]);
	}
	
	void PopulateBtnWithData(FacilityBtnData[] options, bool jobActive) {
		for(int i = 0 ; i < options.Length; i++)
			actionBtns[i+2 - ((jobActive)?0:1)].SetBtnData(options[i]);
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
						if (facility.JobActive) {
							if (!jobLocked) {
								currentBtn = jobBtn;
								facility.DoJob();
							}
								
						}
						else {
							currentBtn = actionBtns[0];
							facility.ExecuteAction(0);
						}
							
						
						break;
					case 2:
						if (facility.JobActive) {
							currentBtn = actionBtns[0];
							facility.ExecuteAction(0);
						}
						else {
							currentBtn = actionBtns[1];
							facility.ExecuteAction(1);
						}

						
						break;
					case 3:
						if (facility.JobActive) {
							currentBtn = actionBtns[1];
							facility.ExecuteAction(1);
						}
						else {
							currentBtn = actionBtns[2];
							facility.ExecuteAction(2);
						}
						break;
					case 4:
						if (facility.JobActive) {
							currentBtn = actionBtns[2];
							facility.ExecuteAction(2);
						}
							
						
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

	public void ClosePanel(bool sleeping = false) {
//		if (facility.ShowInventory) {
//			InventoryUI.GetInstance().CloseInventoryPanel();
//		}
		
		if(!sleeping)
			StartCoroutine(SlumWorld.GetInstance().DescriptionPanelClosed(tutorialMode, (facility != null)?facility.ShowInventory:false));
		interactionActive = false;
		facility = null;
		panel.SetActive(false);
		
		if(tutorialMode && !sleeping)
			tutorialController.FacilityPanelClosed();
	}
	
	

	private void ButtonSelectionDown() {
		if (!facility.FacilityActive) {
			ResetSelection();
			return;
		}
			
		actionBtns[selectedIndex].SetBtnStateAtive(false);
		selectedIndex = Mathf.Max((selectedIndex - 1), 0);
		actionBtns[selectedIndex].SetBtnStateAtive(true);
	}

	private void ButtonSelectionUp() {
		if (!facility.FacilityActive) {
			ResetSelection();
			return;
		}
		actionBtns[selectedIndex].SetBtnStateAtive(false);
		selectedIndex = Mathf.Min((selectedIndex + 1), (actionBtns.Count - 1));
		actionBtns[selectedIndex].SetBtnStateAtive(true);
	}

	public bool InteractionActive {
		get { return interactionActive; }
		set { interactionActive = value; }
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
